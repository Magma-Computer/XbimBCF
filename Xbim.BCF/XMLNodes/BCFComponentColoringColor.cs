using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xbim.BCF.XMLNodes
{
    [Serializable]
    [XmlType("Color")]
    public class BCFComponentColoringColor
    {
        private string _color;
        [XmlAttribute]
        public string Color
        {
            get { return _color; }
            set
            {
                if (string.IsNullOrEmpty(value) || !IsHex(value))
                {
                    throw new ArgumentException("Color - must be a valid hex sequence");
                }
                _color = value;
            }
        }

        [XmlElement("Component")]
        public List<BCFComponent_2_1> Components;
        public bool ShouldSerializeComponent()
        {
            return Components != null && Components.Count > 0;
        }

        private BCFComponentColoringColor()
        { }

        public BCFComponentColoringColor(string color)
        {
            Color = color;
            Components = new List<BCFComponent_2_1>();
        }

        public BCFComponentColoringColor(XElement node)
        {
            Color = (string)node.Attribute("Color");
            Components = new List<BCFComponent_2_1>(node.Elements("Component").Select(n => new BCFComponent_2_1(n)));
        }

        private bool IsHex(IEnumerable<char> chars)
        {
            bool isHex;
            foreach (var c in chars)
            {
                isHex = ((c >= '0' && c <= '9') ||
                    (c >= 'a' && c <= 'f') ||
                    (c >= 'A' && c <= 'F'));

                if (!isHex)
                    return false;
            }
            return true;
        }
    }
}
