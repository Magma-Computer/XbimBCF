using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	public class BCFBimSnippet
	{
		private string _snippetType;

		/// <summary>
		/// Type of the Snippet (Predefined list in "extension.xsd")
		/// </summary>
		[XmlAttribute]
		public string SnippetType
		{
			get
			{
				return _snippetType;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(GetType().Name + " - SnippetType is mandatory");
				}

				else
				{
					_snippetType = value;
				}
			}
		}

		private string _reference;

		/// <summary>
		/// URI to BimSnippet. IsExternal=false "..\snippetExample.ifc" (within bcfzip) IsExternal=true "https://.../snippetExample.ifc"
		/// </summary>
		[XmlElement(Order = 1)]
		public string Reference
		{
			get
			{
				return _reference;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(GetType().Name + " - Reference is mandatory");
				}

				else
				{
					_reference = value;
				}
			}
		}

		/// <summary>
		/// URI to BimSnippetSchema (always external)
		/// </summary>
		[XmlElement(Order = 2)]
		public string ReferenceSchema { get; set; }

		public bool ShouldSerializeReferenceSchema()
		{
			return !string.IsNullOrEmpty(ReferenceSchema);
		}

		/// <summary>
		/// Is the BimSnippet external or within the bcfzip. (Default = false).
		/// </summary>
		[XmlAttribute]
		public bool isExternal { get; set; }

		private BCFBimSnippet() { }

		public BCFBimSnippet(string snippetType, string reference)
		{
			SnippetType = snippetType;
			Reference = reference;
		}

		public BCFBimSnippet(XElement node)
		{
			//attributes
			SnippetType = (string)node.Attribute("SnippetType") ?? "";
			isExternal = (bool?)node.Attribute("isExternal") ?? false;

			//nodes
			Reference = (string)node.Element("Reference") ?? "";
			ReferenceSchema = (string)node.Element("ReferenceSchema") ?? "";
		}
	}
}