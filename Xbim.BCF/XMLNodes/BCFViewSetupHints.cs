using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType("ViewSetupHints")]
	public class BCFViewSetupHints
	{
		[XmlAttribute]
		public bool SpacesVisible { get; set; }

		[XmlAttribute]
		public bool SpaceBoundariesVisible { get; set; }

		[XmlAttribute]
		public bool OpeningsVisible { get; set; }

		public BCFViewSetupHints()
		{ }

		public BCFViewSetupHints(XElement node)
		{
			SpacesVisible = (bool?)node.Attribute("SpacesVisible") ?? false;
			SpaceBoundariesVisible = (bool?)node.Attribute("SpaceBoundariesVisible") ?? false;
			OpeningsVisible = (bool?)node.Attribute("OpeningsVisible") ?? false;
		}
	}
}