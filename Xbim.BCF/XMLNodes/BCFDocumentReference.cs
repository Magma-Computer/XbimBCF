using System;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	public class BCFDocumentReference
	{
		/// <summary>
		/// Guid attribute for identifying uniquely
		/// </summary>
		[XmlAttribute("Guid")]
		public string ID { get; set; }

		public bool ShouldSerializeID()
		{
			return !string.IsNullOrEmpty(ID);
		}

		/// <summary>
		/// Is the Document external or within the bcfzip. (Default = false).
		/// </summary>
		[XmlAttribute]
		public bool IsExternal { get; set; }

		public bool ShouldSerializeIsExternal()
		{
			return IsExternal = true;
		}

		/// <summary>
		/// URI to document. IsExternal=false "..\exampleDoc.docx" (within bcfzip) IsExternal=true "https://.../ exampleDoc.docx"
		/// </summary>
		public string ReferencedDocument { get; set; }

		public bool ShouldSerializeReferencedDocument()
		{
			return !string.IsNullOrEmpty(ReferencedDocument);
		}

		/// <summary>
		/// Description of the document
		/// </summary>
		public string Description { get; set; }

		public bool ShouldSerializeDescription()
		{
			return !string.IsNullOrEmpty(Description);
		}

		public BCFDocumentReference() { }

		public BCFDocumentReference(XElement node)
		{
			Description = (string)node.Element("Description") ?? "";
			ReferencedDocument = (string)node.Element("ReferencedDocument") ?? "";
			IsExternal = (bool?)node.Attribute("IsExternal") ?? false;
			ID = (string)node.Attribute("Guid") ?? "";
		}
	}
}