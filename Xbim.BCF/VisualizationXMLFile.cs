using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xbim.BCF.XMLNodes;

namespace Xbim.BCF
{
	internal class VisualizationXMLFileConstants
	{
		public const string VISUALIZATION_INFO = "VisualizationInfo";
	}

	[Serializable]
	public abstract class VisualizationXMLFile
	{
		protected internal BCFOrthogonalCamera _orthogonalCamera;

		public void SetOrthogonalCamera(BCFOrthogonalCamera orthogonalCamera)
		{
			_orthogonalCamera = orthogonalCamera;
		}

		public BCFOrthogonalCamera GetOrthogonalCamera()
		{
			return _orthogonalCamera;
		}

		protected internal BCFPerspectiveCamera _perspectiveCamera;

		public void SetPerspectiveCamera(BCFPerspectiveCamera orthogonalCamera)
		{
			_perspectiveCamera = orthogonalCamera;
		}

		public BCFPerspectiveCamera GetPerspectiveCamera()
		{
			return _perspectiveCamera;
		}

		protected internal List<BCFLine> _lines;

		public void SetLines(List<BCFLine> lines)
		{
			_lines = lines;
		}

		public List<BCFLine> GetLines()
		{
			return _lines;
		}

		protected internal List<BCFClippingPlane> _clippingPlanes;

		public void SetClippingPlanes(List<BCFClippingPlane> clippingPlanes)
		{
			_clippingPlanes = clippingPlanes;
		}

		public List<BCFClippingPlane> GetClippingPlanes()
		{
			return _clippingPlanes;
		}

		protected internal List<BCFBitmap> _bitmaps;

		public void SetBitmaps(List<BCFBitmap> bitmaps)
		{
			_bitmaps = bitmaps;
		}

		public List<BCFBitmap> GetBitmaps()
		{
			return _bitmaps;
		}

		protected internal void SetFileds(XDocument xdoc)
		{
			_lines = new List<BCFLine>();
			_clippingPlanes = new List<BCFClippingPlane>();
			_bitmaps = new List<BCFBitmap>();

			var orth = xdoc.Root.Elements("OrthogonalCamera").FirstOrDefault();
			if (orth != null)
			{
				_orthogonalCamera = new BCFOrthogonalCamera(orth);
			}

			var pers = xdoc.Root.Elements("PerspectiveCamera").FirstOrDefault();
			if (pers != null)
			{
				_perspectiveCamera = new BCFPerspectiveCamera(pers);
			}

			var lines = xdoc.Root.Elements("Lines").FirstOrDefault();
			if (lines != null)
			{
				foreach (var line in xdoc.Root.Element("Lines").Elements("Line"))
					_lines.Add(new BCFLine(line));
			}

			var clippingplanes = xdoc.Root.Elements("ClippingPlanes").FirstOrDefault();
			if (clippingplanes != null)
			{
				foreach (var plane in xdoc.Root.Element("ClippingPlanes").Elements("ClippingPlane"))
					_clippingPlanes.Add(new BCFClippingPlane(plane));
			}

			var bitmaps = xdoc.Root.Elements("Bitmaps").FirstOrDefault();
			if (bitmaps != null)
			{
				foreach (var bmap in xdoc.Root.Elements("Bitmaps"))
					_bitmaps.Add(new BCFBitmap(bmap));
			}
		}
	}

	[Serializable]
	[XmlType(VisualizationXMLFileConstants.VISUALIZATION_INFO)]
	public class VisualizationXMLFile_lower_2_1 : VisualizationXMLFile
	{
		[XmlArray(Order = 1)]
		public List<BCFComponent> Components;

		[XmlElement(Order = 2)]
		public BCFOrthogonalCamera OrthogonalCamera { get { return _orthogonalCamera; } set { _orthogonalCamera = value; } }

		[XmlElement(Order = 3)]
		public BCFPerspectiveCamera PerspectiveCamera { get { return _perspectiveCamera; } set { _perspectiveCamera = value; } }

		[XmlArray(Order = 4)]
		public List<BCFLine> Lines { get { return _lines; } set { _lines = value; } }

		[XmlArray(Order = 5)]
		public List<BCFClippingPlane> ClippingPlanes { get { return _clippingPlanes; } set { _clippingPlanes = value; } }

		[XmlElement(Order = 6)]
		public List<BCFBitmap> Bitmaps { get { return _bitmaps; } set { _bitmaps = value; } }

		public VisualizationXMLFile_lower_2_1()
		{
			Components = new List<BCFComponent>();
		}

		public VisualizationXMLFile_lower_2_1(XDocument xdoc)
		{
			Components = new List<BCFComponent>();

			if (xdoc.Root.Element("Components") != null && xdoc.Root.Element("Components").Elements("Component") != null)
			{
				foreach (var comp in xdoc.Root.Element("Components").Elements("Component"))
					Components.Add(new BCFComponent(comp));
			}

			SetFileds(xdoc);
		}
	}

	[Serializable]
	[XmlType(VisualizationXMLFileConstants.VISUALIZATION_INFO)]
	public class VisualizationXMLFile_2_1 : VisualizationXMLFile
	{
		[XmlElement(ElementName = "Components", Order = 1)]
		public BCFComponents Components_2_1;

		[XmlElement(Order = 2)]
		public BCFOrthogonalCamera OrthogonalCamera { get { return _orthogonalCamera; } set { _orthogonalCamera = value; } }

		[XmlElement(Order = 3)]
		public BCFPerspectiveCamera PerspectiveCamera { get { return _perspectiveCamera; } set { _perspectiveCamera = value; } }

		[XmlArray(Order = 4)]
		public List<BCFLine> Lines { get { return _lines; } set { _lines = value; } }

		[XmlArray(Order = 5)]
		public List<BCFClippingPlane> ClippingPlanes { get { return _clippingPlanes; } set { _clippingPlanes = value; } }

		[XmlElement(Order = 6)]
		public List<BCFBitmap> Bitmaps { get { return _bitmaps; } set { _bitmaps = value; } }

		public VisualizationXMLFile_2_1()
		{
			Components_2_1 = new BCFComponents();
		}

		public VisualizationXMLFile_2_1(XDocument xdoc)
		{
			Components_2_1 = new BCFComponents();

			if (xdoc.Root.Element("Components") != null && xdoc.Root.Element("Components").Elements("Component") != null)
			{
				var components = xdoc.Root.Element("Components");
				Components_2_1 = new BCFComponents(components);
			}

			SetFileds(xdoc);
		}
	}
}