using System;
using System.Collections.Generic;

namespace Xbim.BCF
{
	[Serializable]
    public class Topic
    {
        /// <summary>
        /// .bcf File Projection
        /// </summary>
        public MarkupXMLFile Markup { get; set; }
        /// <summary>
        /// .bcfv Files Projections
        /// </summary>
        public List<KeyValuePair<String, VisualizationXMLFile>> Visualizations;
        /// <summary>
        /// Collection of key/value pairs representing the (Key)name and (Value)Base64 String representations of a .png file associated with the topic
        /// </summary>
        public List<KeyValuePair<String, byte[]>> Snapshots;

        public Topic()
        {
            Visualizations = new List<KeyValuePair<String, VisualizationXMLFile>>();
            Snapshots = new List<KeyValuePair<String, byte[]>>();
        }
    }
}
