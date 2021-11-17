﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF
{
	[Serializable]
    public class BCF
    {
        /// <summary>
        /// .bcfp File Representation
        /// </summary>
        public ProjectXMLFile Project { get; set; }
        /// <summary>
        /// .version file Representation
        /// </summary>
        public VersionXMLFile Version { get; set; }
        /// <summary>
        /// A collection of Topics contained within the BCF
        /// </summary>
        public List<Topic> Topics;

        public BCF()
        {
            Topics = new List<Topic>();
        }

        /// <summary>
        /// Creates an object representation of a bcf zip file.
        /// </summary>
        /// <param name="BCFZipData">A Stream of bytes representing a bcf .zip file</param>
        /// <returns>A new BCF object</returns>
        public static BCF Deserialize(Stream BCFZipData)
        {
            BCF bcf = new BCF();
            Topic currentTopic = null;
            Guid currentGuid = Guid.Empty;
            ZipArchive archive = new ZipArchive(BCFZipData);
            bool isLower_2_1 = true;
            foreach (ZipArchiveEntry entry in archive.Entries.OrderBy(x => x.FullName))
            {
                if (entry.FullName.EndsWith(".version", StringComparison.OrdinalIgnoreCase))
                {
                    bcf.Version = new VersionXMLFile(XDocument.Load(entry.Open()));
                    if (bcf.Version != null && !string.IsNullOrEmpty(bcf.Version.VersionId))
                    {
                        double version = 1.0;
                        double parseResult;
                        double.TryParse(bcf.Version.VersionId, NumberStyles.Any, CultureInfo.InvariantCulture, out parseResult);

                        if (parseResult > version)
                            version = parseResult;

                        isLower_2_1 = version < 2.1;
                    }
                }
            }
            foreach (ZipArchiveEntry entry in archive.Entries.OrderBy(x => x.FullName))
            {
                if (entry.FullName.EndsWith(".bcfp", StringComparison.OrdinalIgnoreCase))
                {
                    bcf.Project = new ProjectXMLFile(XDocument.Load(entry.Open()));
                }                
                else if (entry.FullName.EndsWith(".bcf", StringComparison.OrdinalIgnoreCase))
                {
                    if (entry.ExtractGuidFolderName() != currentGuid)
                    {
                        if (currentTopic != null)
                        {
                            bcf.Topics.Add(currentTopic);
                        }
                        currentGuid = entry.ExtractGuidFolderName();
                        currentTopic = new Topic();
                    }
                    currentTopic.Markup = new MarkupXMLFile(XDocument.Load(entry.Open()), isLower_2_1);
                }
                else if (entry.FullName.EndsWith(".bcfv", StringComparison.OrdinalIgnoreCase))
                {
                    if (entry.ExtractGuidFolderName() != currentGuid)
                    {
                        if (currentTopic != null)
                        {
                            bcf.Topics.Add(currentTopic);
                        }
                        currentGuid = entry.ExtractGuidFolderName();
                        currentTopic = new Topic();
                    }
                    currentTopic.Visualizations.Add(new KeyValuePair<string, VisualizationXMLFile>(entry.Name.Substring(0, entry.Name.IndexOf(".")), new VisualizationXMLFile(XDocument.Load(entry.Open()), isLower_2_1)));
                }
                else if (entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    if (entry.ExtractGuidFolderName() != currentGuid)
                    {
                        if (currentTopic != null)
                        {
                            bcf.Topics.Add(currentTopic);
                        }
                        currentGuid = entry.ExtractGuidFolderName();
                        currentTopic = new Topic();
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.Open().CopyTo(ms);
                        currentTopic.Snapshots.Add(new KeyValuePair<string, byte[]>(entry.Name.Substring(0, entry.Name.IndexOf(".")), ms.ToArray()));
                    }
                }
            }
            if (currentTopic != null)
            {
                bcf.Topics.Add(currentTopic);
            }
            return bcf;
        }

        /// <summary>
        /// Serializes the object to a Stream of bytes representing the bcf .zip file 
        /// </summary>
        /// <returns>A Stream of bytes representing the bcf as a .zip file</returns>
        public Stream Serialize()
        {
            XmlSerializer bcfSerializer = new XmlSerializer(typeof(MarkupXMLFile));
            XmlSerializer bcfvSerializer = new XmlSerializer(typeof(VisualizationXMLFile));
            XmlSerializer bcfpSerializer = new XmlSerializer(typeof(ProjectXMLFile));
            XmlSerializer versionSerializer = new XmlSerializer(typeof(VersionXMLFile));

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                if (this.Project != null)
                {
                    var bcfp = archive.CreateEntry("project.bcfp");
                    using (var bcfpStream = bcfp.Open())
                    {
                        using (var bcfpWriter = new StreamWriter(bcfpStream))
                        {
                            bcfpSerializer.Serialize(bcfpWriter, this.Project);
                            bcfpWriter.Close();
                        }
                    }
                }

                if (this.Version != null)
                {
                    var version = archive.CreateEntry("bcf.version");
                    using (var versionStream = version.Open())
                    {
                        using (var versionWriter = new StreamWriter(versionStream))
                        {
                            versionSerializer.Serialize(versionWriter, this.Version);
                            versionWriter.Close();
                        }
                    }
                }

                foreach (Topic t in this.Topics)
                {
                    string bcfName = t.Markup.Topic.Guid.ToString() + "/markup.bcf";
                    var bcf = archive.CreateEntry(bcfName);
                    using (var bcfStream = bcf.Open())
                    {
                        using (var bcfWriter = new StreamWriter(bcfStream))
                        {
                            bcfSerializer.Serialize(bcfWriter, t.Markup);
                            bcfWriter.Close();
                        }
                    }

                    foreach (KeyValuePair<string, VisualizationXMLFile> visualization in t.Visualizations)
                    {
                        string bcfvName = String.Format("{0}/{1}.bcfv", t.Markup.Topic.Guid, visualization.Key);
                        var bcfv = archive.CreateEntry(bcfvName);
                        using (var bcfvStream = bcfv.Open())
                        {
                            using (var bcfvWriter = new StreamWriter(bcfvStream))
                            {
                                bcfvSerializer.Serialize(bcfvWriter, visualization.Value);
                                bcfvWriter.Close();
                            }
                        }
                    }

                    foreach (KeyValuePair<String, byte[]> img in t.Snapshots)
                    {
                        string snapshotName = String.Format("{0}/{1}.png", t.Markup.Topic.Guid, img.Key);
                        var png = archive.CreateEntry(snapshotName);
                        using (var pngStream = png.Open())
                        {
                            using (var pngWriter = new BinaryWriter(pngStream))
                            {
                                pngWriter.Write(img.Value);
                                pngWriter.Close();
                            }
                        }
                    }
                }
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}
