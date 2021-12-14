using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType("ComponentVisibility")]
	public class BCFComponentVisibility
	{
		[XmlAttribute]
		public bool DefaultVisibility { get; set; }

		[XmlArray("Exceptions")]
		public List<BCFComponent_2_1> Exceptions;

		public bool ShouldSerializeExceptions()
		{
			return Exceptions != null && Exceptions.Count > 0;
		}

		public BCFComponentVisibility()
		{
			DefaultVisibility = false;
			Exceptions = new List<BCFComponent_2_1>();
		}

		public BCFComponentVisibility(XElement node)
		{
			DefaultVisibility = (bool?)node.Attribute("DefaultVisibility") ?? false;
			Exceptions = new List<BCFComponent_2_1>(node.Element("Exceptions")?.Elements("Component").Select(n => new BCFComponent_2_1(n)) ?? Enumerable.Empty<BCFComponent_2_1>());
		}
	}
}