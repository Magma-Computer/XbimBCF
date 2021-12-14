using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF
{
	[Serializable]
	public class AttrIDNode
	{
		private Guid _id;

		/// <summary>
		/// Identifying Attribute
		/// </summary>
		[XmlAttribute("Guid")]
		public Guid ID
		{
			get
			{
				return _id;
			}

			set
			{
				if (value == null || value == Guid.Empty)
				{
					throw new ArgumentException(GetType().Name + " - identifier is mandatory and must contain a valid Guid value");
				}

				else
				{
					_id = value;
				}
			}
		}

		private AttrIDNode() { }

		public AttrIDNode(Guid id)
		{
			ID = id;
		}

		public AttrIDNode(XElement node)
		{
			ID = (System.Guid?)node.Attribute("Guid") ?? Guid.Empty;
		}
	}
}