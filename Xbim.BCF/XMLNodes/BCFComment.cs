using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	[XmlType("Comment")]
	public class BCFComment
	{
		/// <summary>
		/// A free text status. The options for this can be agreed, for example, in a project.
		/// </summary>
		[XmlElement(Order = 1)]
		public string VerbalStatus { get; set; }

		public bool ShouldSerializeVerbalStatus()
		{
			return !string.IsNullOrEmpty(VerbalStatus);
		}

		private Guid _guid;

		/// <summary>
		/// Unique Identifier for this topic
		/// </summary>
		[XmlAttribute]
		public Guid Guid
		{
			get
			{
				return _guid;
			}

			set
			{
				if (value == null || value == Guid.Empty)
				{
					throw new ArgumentException(GetType().Name + " - Guid attribute is mandatory and must contain a valid Guid value");
				}

				else
				{
					_guid = value;
				}
			}
		}

		private string _status;

		/// <summary>
		/// Status of the comment / topic (Predefined list in “extension.xsd”)
		/// </summary>
		[XmlElement(Order = 2)]
		public string Status
		{
			get
			{
				return _status;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentException(GetType().Name + " - Status shouldn't be null");
				}

				else
				{
					_status = value;
				}
			}
		}

		private DateTime _date;

		/// <summary>
		/// Date of the comment
		/// </summary>
		[XmlElement(Order = 3)]
		public DateTime Date
		{
			get
			{
				return _date;
			}

			set
			{
				if (value == null || value == DateTime.MinValue)
				{
					throw new ArgumentException(GetType().Name + " - Date is mandatory");
				}

				else
				{
					_date = value;
				}
			}
		}
		private string _author;

		/// <summary>
		/// Comment author
		/// </summary>
		[XmlElement(Order = 4)]
		public string Author
		{
			get
			{
				return _author;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(GetType().Name + " - Author is mandatory");
				}

				else
				{
					_author = value;
				}
			}
		}

		private string _comment;

		/// <summary>
		/// The comment text
		/// </summary>
		[XmlElement(Order = 5)]
		public string Comment
		{
			get
			{
				return _comment;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentException(GetType().Name + " - Comment text shouldn't be null");
				}

				else
				{
					_comment = value;
				}
			}
		}

		private AttrIDNode _topic;

		/// <summary>
		/// Back reference to the topic 
		/// </summary>
		[XmlElement(Order = 6)]
		public AttrIDNode Topic
		{
			get
			{
				return _topic;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentException(GetType().Name + " - Topic is mandatory and must contain a valid Guid value");
				}

				else
				{
					_topic = value;
				}
			}
		}

		/// <summary>
		/// Reference back to Viewpoint
		/// </summary>
		[XmlElement(Order = 7)]
		public AttrIDNode Viewpoint { get; set; }

		public bool ShouldSerializeViewpoint()
		{
			return Viewpoint != null;
		}

		/// <summary>
		/// Guid of the comment to which this comment is a reply
		/// </summary>
		[XmlElement(Order = 8)]
		public AttrIDNode ReplyToComment { get; set; }

		public bool ShouldSerializeReplyToComment()
		{
			return ReplyToComment != null;
		}

		/// <summary>
		/// The date when comment was modified
		/// </summary>
		[XmlElement(Order = 9)]
		public DateTime? ModifiedDate { get; set; }

		public bool ShouldSerializeModifiedDate()
		{
			return ModifiedDate != null;
		}

		/// <summary>
		/// The author who modified the comment
		/// </summary>
		[XmlElement(Order = 10)]
		public string ModifiedAuthor { get; set; }

		public bool ShouldSerializeModifiedAuthor()
		{
			return !string.IsNullOrEmpty(ModifiedAuthor);
		}

		private BCFComment() { }

		public BCFComment(Guid id, Guid topicId, string status, DateTime date, string author, string comment)
		{
			Status = status;
			Date = date;
			Author = author;
			Comment = comment;
			Guid = id;
			Topic = new AttrIDNode(topicId);
		}

		public BCFComment(XElement node, Guid topicGuid)
		{
			Guid = (System.Guid?)node.Attribute("Guid") ?? Guid.Empty;
			Status = (string)node.Element("Status") ?? "";
			Date = (DateTime?)node.Element("Date") ?? DateTime.MinValue;
			Author = (string)node.Element("Author") ?? "";
			Comment = (string)node.Element("Comment") ?? "";
			ModifiedDate = (DateTime?)node.Element("ModifiedDate") ?? null;
			ModifiedAuthor = (string)node.Element("ModifiedAuthor") ?? "";
			VerbalStatus = (string)node.Element("VerbalStatus") ?? "";
			Topic = new AttrIDNode(topicGuid);

			var reply = node.Elements("ReplyToComment").FirstOrDefault();
			if (reply != null)
				ReplyToComment = new AttrIDNode(node.Element("ReplyToComment"));

			var viewp = node.Elements("Viewpoint").FirstOrDefault();
			if (viewp != null)
				Viewpoint = new AttrIDNode(node.Element("Viewpoint"));
		}
	}
}