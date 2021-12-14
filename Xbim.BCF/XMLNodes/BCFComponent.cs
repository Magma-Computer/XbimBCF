using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType("Component")]
	public class BCFComponent
	{
		private string _color;

		/// <summary>
		/// Color of the component. This can be used to provide special highlighting of components in the viewpoint. The color is given in ARGB format
		/// </summary>
		[XmlAttribute]
		public string Color
		{
			get
			{
				return _color;
			}

			set
			{
				if (!string.IsNullOrEmpty(value) && IsHex(value))
				{
					_color = value;
				}

				else
				{
					_color = "000000";
				}
			}
		}

		public bool ShouldSerializeColor()
		{
			return !string.IsNullOrEmpty(Color);
		}

		private string _ifcGuid;

		/// <summary>
		/// The id of the component selected in a BIM tool
		/// </summary>
		[XmlAttribute]
		public string IfcGuid
		{
			get { return _ifcGuid; }
			set
			{
				if (value.Length == 22)
				{
					_ifcGuid = value;
				}

				else
				{
					throw new ArgumentException(GetType().Name + " - IfcGuid - IfcGuid must be 22 chars exactly");
				}
			}
		}

		public bool ShouldSerializeIfcGuid()
		{
			return !string.IsNullOrEmpty(IfcGuid);
		}

		/// <summary>
		/// Name of the system in which the component is originated
		/// </summary>
		[XmlElement(Order = 1)]
		public string OriginatingSystem { get; set; }

		public bool ShouldSerializeOriginatingSystem()
		{
			return !string.IsNullOrEmpty(OriginatingSystem);
		}

		/// <summary>
		/// System specific identifier of the component in the originating BIM tool
		/// </summary>
		[XmlElement(Order = 2)]
		public string AuthoringToolId { get; set; }

		public bool ShouldSerializeAuthoringToolId()
		{
			return !string.IsNullOrEmpty(AuthoringToolId);
		}

		/// <summary>
		/// Attribute, which store Selected value in bool? type
		/// </summary>
		[XmlIgnore]
		public bool? _selected;

		/// <summary>
		/// Selected attribute, which can be null and not displayed in BCF 1.0
		/// </summary>
		[XmlAttribute]
		public string Selected
		{
			get
			{
				if (_selected.HasValue)
				{
					string selectedStringValue = _selected.Value.ToString().ToLower();
					return selectedStringValue;
				}

				else
					return null;
			}

			set
			{
				if (value != null)
					_selected = bool.Parse(value);

				else
					_selected = null;
			}
		}

		/// <summary>
		/// Attribute, which store Visible value in bool? type
		/// </summary>
		[XmlIgnore]
		public bool? _visible;

		/// <summary>
		/// Visibility attribute, which can be null and not displayed in BCF 1.0
		/// </summary>
		[XmlAttribute]
		public string Visible
		{
			get
			{
				if (_visible.HasValue)
				{
					string visibleStringValue = _visible.Value.ToString().ToLower();
					return visibleStringValue;
				}

				else
					return null;
			}
			set
			{
				if (value != null)
					_visible = bool.Parse(value);

				else
					_visible = null;
			}
		}

		public BCFComponent() { }

		public BCFComponent(XElement node)
		{
			IfcGuid = (string)node.Attribute("IfcGuid") ?? "";
			Visible = (string)node.Attribute("Visible") ?? null;
			Selected = (string)node.Attribute("Selected") ?? null;
			Color = (string)node.Attribute("Color") ?? "";
			OriginatingSystem = (string)node.Element("OriginatingSystem") ?? "";
			AuthoringToolId = (string)node.Element("AuthoringToolId") ?? "";
		}

		private bool IsHex(IEnumerable<char> chars)
		{
			bool isHex;

			foreach (var c in chars)
			{
				isHex = (
					(c >= '0' && c <= '9') ||
					(c >= 'a' && c <= 'f') ||
					(c >= 'A' && c <= 'F'));

				if (!isHex)
					return false;
			}

			return true;
		}
	}
}