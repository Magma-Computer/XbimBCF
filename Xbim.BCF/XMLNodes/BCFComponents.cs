﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType("Components")]
	public class BCFComponents
	{
		[XmlElement(ElementName = "ViewSetupHints", Order = 1)]
		public BCFViewSetupHints ViewSetupHints { get; set; }

		public bool ShouldSerializeViewSetupHints()
		{
			return ViewSetupHints != null;
		}

		[XmlElement(ElementName = "Selection", Order = 2)]
		public BCFComponentSelection Selection { get; set; }

		public bool ShouldSerializeSelection()
		{
			return Selection != null;
		}

		[XmlElement(ElementName = "Visibility", Order = 3)]
		public BCFComponentVisibility Visibility { get; set; }

		[XmlArray(ElementName = "Coloring", Order = 4)]
		public List<BCFComponentColoringColor> Colorings;

		public bool ShouldSerializeExceptions()
		{
			return Colorings != null && Colorings.Count > 0;
		}

		public BCFComponents()
		{
			ViewSetupHints = new BCFViewSetupHints();
			Selection = new BCFComponentSelection();
			Visibility = new BCFComponentVisibility();
			Colorings = new List<BCFComponentColoringColor>();
		}

		public BCFComponents(XElement node)
		{
			var hints = node.Element("ViewSetupHints");
			ViewSetupHints = hints != null ? new BCFViewSetupHints(hints) : null;

			var selection = node.Element("Selection");
			Selection = selection != null ? new BCFComponentSelection(selection) : null;

			var visibility = node.Element("Visibility");
			Visibility = visibility != null ? new BCFComponentVisibility(visibility) : null;

			var coloring = node.Element("Coloring");
			if (coloring != null)
				Colorings = new List<BCFComponentColoringColor>(coloring.Elements("Color").Select(c => new BCFComponentColoringColor(c)));
		}
	}
}