using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xbim.BCF.XMLNodes;

namespace Xbim.BCF
{
	[Serializable]
    [XmlType("VisualizationInfo")]
    public class VisualizationXMLFile
    {
        [XmlArray(Order = 1)]
        public List<BCFComponent> Components;
        [XmlElement(ElementName = "Components", Order = 2)]
        public BCFComponents Components_2_1;
        [XmlElement(Order = 3)]
        public BCFOrthogonalCamera OrthogonalCamera { get; set; }
        [XmlElement(Order = 4)]
        public BCFPerspectiveCamera PerspectiveCamera { get; set; }
        [XmlArray(Order = 5)]
        public List<BCFLine> Lines;
        [XmlArray(Order = 6)]
        public List<BCFClippingPlane> ClippingPlanes;
        [XmlElement(ElementName = "Bitmaps", Order = 7)]
        public List<BCFBitmap> Bitmaps;

        public VisualizationXMLFile()
        {
            Components = new List<BCFComponent>();
            Components_2_1 = new BCFComponents();
            Lines = new List<BCFLine>();
            ClippingPlanes = new List<BCFClippingPlane>();
            Bitmaps = new List<BCFBitmap>();
        }

        public VisualizationXMLFile(XDocument xdoc, bool isLower_2_1)
        {
            Components = new List<BCFComponent>();
            Components_2_1 = new BCFComponents();                
            Lines = new List<BCFLine>();
            ClippingPlanes = new List<BCFClippingPlane>();
            Bitmaps = new List<BCFBitmap>();

            var orth = xdoc.Root.Elements("OrthogonalCamera").FirstOrDefault();
            if (orth != null)
            {
                OrthogonalCamera = new BCFOrthogonalCamera(orth);
            }
            var pers = xdoc.Root.Elements("PerspectiveCamera").FirstOrDefault();
            if (pers != null)
            {
                PerspectiveCamera = new BCFPerspectiveCamera(pers);
            }
            if (xdoc.Root.Element("Components") != null && xdoc.Root.Element("Components").Elements("Component") != null)
			{
                if (isLower_2_1)
				{
                    foreach (var comp in xdoc.Root.Element("Components").Elements("Component"))
                    {
                        Components.Add(new BCFComponent(comp));
                    }
                }

				else
				{
                    var components = xdoc.Root.Element("Components");
                    Components_2_1 = new BCFComponents(components);
				}
            }
            var lines = xdoc.Root.Elements("Lines").FirstOrDefault();
            if (lines != null)
            {
                foreach (var line in xdoc.Root.Element("Lines").Elements("Line"))
                {
                    Lines.Add(new BCFLine(line));
                }
            }
            var clippingplanes = xdoc.Root.Elements("ClippingPlanes").FirstOrDefault();
            if (clippingplanes != null)
            {
                foreach (var plane in xdoc.Root.Element("ClippingPlanes").Elements("ClippingPlane"))
                {
                    ClippingPlanes.Add(new BCFClippingPlane(plane));
                }
            }
            var bitmaps = xdoc.Root.Elements("Bitmaps").FirstOrDefault();
            if (bitmaps != null)
            {
                foreach (var bmap in xdoc.Root.Elements("Bitmaps"))
                {
                    Bitmaps.Add(new BCFBitmap(bmap));
                }
            }
        }
    }
}
