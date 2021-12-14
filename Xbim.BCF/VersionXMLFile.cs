using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF
{
	[Serializable]
	[XmlType("Version")]
	public class VersionXMLFile
	{
		public string DetailedVersion { get; set; }

		public bool ShouldSerializeDetailedVersion()
		{
			return !string.IsNullOrEmpty(DetailedVersion);
		}

		private string _versionId;

		[XmlAttribute]
		public string VersionId
		{
			get
			{
				return _versionId;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(GetType().Name + " - VersionId is mandatory");
				}

				else
				{
					_versionId = value;
				}
			}
		}

		private VersionXMLFile() { }

		public VersionXMLFile(string versionID)
		{
			VersionId = versionID;
		}

		public VersionXMLFile(XDocument xdoc)
		{
			VersionId = (string)xdoc.Root.Attribute("VersionId") ?? "";
			DetailedVersion = (string)xdoc.Root.Element("DetailedVersion") ?? "";
		}
	}
}