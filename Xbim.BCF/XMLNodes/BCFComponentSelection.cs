using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType("ComponentSelection")]
	public class BCFComponentSelection
	{
		[XmlElement(ElementName = "Component")]
		public List<BCFComponent_2_1> Components;

		public bool ShouldSerializeComponent()
		{
			return Components != null && Components.Count > 0;
		}

		public BCFComponentSelection()
		{
			Components = new List<BCFComponent_2_1>();
		}

		public BCFComponentSelection(XElement node)
		{
			Components = new List<BCFComponent_2_1>(node.Elements("Component").Select(c => new BCFComponent_2_1(c)));
		}
	}
}