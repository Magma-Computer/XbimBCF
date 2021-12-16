using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType(TypeName = "Component")]
	public class BCFComponent_2_1
	{
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

		public BCFComponent_2_1() { }

		public BCFComponent_2_1(XElement node)
		{
			IfcGuid = (string)node.Attribute("IfcGuid") ?? "";
			OriginatingSystem = (string)node.Element("OriginatingSystem") ?? "";
			AuthoringToolId = (string)node.Element("AuthoringToolId") ?? "";
		}
	}
}