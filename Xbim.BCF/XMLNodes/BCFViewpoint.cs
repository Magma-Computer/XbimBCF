﻿using System;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	public class BCFViewpoint
	{
		private Guid _guid;

		/// <summary>
		/// Unique Identifier for this Viewpoint
		/// </summary>
		[XmlAttribute("Guid")]
		public Guid ID
		{
			get
			{
				return _guid;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentException(GetType().Name + " - Guid attribute is mandatory and must contain a valid Guid value");
				}

				else
				{
					_guid = value;
				}
			}
		}

		/// <summary>
		/// FileName of the viewpoint (.bcfv)
		/// </summary>
		[XmlElement(Order = 1)]
		public string Viewpoint { get; set; }

		public bool ShouldSerializeViewpoint()
		{
			return !string.IsNullOrEmpty(Viewpoint);
		}

		/// <summary>
		/// FileName of the snapshot (.png)
		/// </summary>
		[XmlElement(Order = 2)]
		public string Snapshot { get; set; }

		public bool ShouldSerializeSnapshot()
		{
			return !string.IsNullOrEmpty(Snapshot);
		}

		/// <summary>
		/// Index
		/// </summary>
		[XmlElement(Order = 3)]
		public int? Index { get; set; }

		public bool ShouldSerializeIndex()
		{
			return Index != null;
		}

		private BCFViewpoint() { }

		public BCFViewpoint(Guid identifier)
		{
			ID = identifier;
		}

		public BCFViewpoint(XElement node)
		{
			ID = (System.Guid?)node.Attribute("Guid") ?? Guid.Empty;
			Viewpoint = (string)node.Element("Viewpoint") ?? "";
			Snapshot = (string)node.Element("Snapshot") ?? "";
			Index = (int?)node.Element("Index");
		}
	}
}