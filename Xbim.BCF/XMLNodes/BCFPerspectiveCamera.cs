﻿using System;
using System.Xml.Linq;

namespace Xbim.BCF.XMLNodes
{
	[Serializable]
	public class BCFPerspectiveCamera
	{
		Vector _cameraViewPoint;

		/// <summary>
		/// Camera location
		/// </summary>
		public Vector CameraViewPoint
		{
			get
			{
				return _cameraViewPoint;
			}

			set
			{
				if (value.X == double.NaN || value.Y == double.NaN || value.Z == double.NaN)
				{
					throw new ArgumentException(GetType().Name + " - CameraViewPoint - must contain X, Y and Z nodes containing a valid 64-bit floating-point value");
				}

				else
				{
					_cameraViewPoint = value;
				}
			}
		}

		Vector _cameraDirection;

		/// <summary>
		/// Camera direction
		/// </summary>
		public Vector CameraDirection
		{
			get
			{
				return _cameraDirection;
			}

			set
			{
				if (value.X == double.NaN || value.Y == double.NaN || value.Z == double.NaN)
				{
					throw new ArgumentException(GetType().Name + " - CameraDirection - must contain X, Y and Z nodes containing a valid 64-bit floating-point value");
				}

				else
				{
					_cameraDirection = value;
				}
			}
		}

		Vector _cameraupVector;

		/// <summary>
		/// Camera up vector
		/// </summary>
		public Vector CameraUpVector
		{
			get
			{
				return _cameraupVector;
			}

			set
			{
				if (value.X == double.NaN || value.Y == double.NaN || value.Z == double.NaN)
				{
					throw new ArgumentException(GetType().Name + " - CameraUpVector - must contain X, Y and Z nodes containing a valid 64-bit floating-point value");
				}

				else
				{
					_cameraupVector = value;
				}
			}
		}

		private double _fieldOfView;

		/// <summary>
		/// Camera’s field of view angle in degrees 
		/// </summary>
		public double FieldOfView
		{
			get
			{
				return _fieldOfView;
			}

			set
			{
				if (value == double.NaN)
				{
					throw new ArgumentException(GetType().Name + " - FieldOfView - must be a valid 64-bit floating-point value");
				}

				else
				{
					_fieldOfView = value;
				}
			}
		}

		private BCFPerspectiveCamera() { }

		public BCFPerspectiveCamera(Vector cameraViewPoint, Vector cameraDirection, Vector cameraUpVector, double fieldOfView)
		{
			CameraViewPoint = cameraViewPoint;
			CameraDirection = cameraDirection;
			CameraUpVector = cameraUpVector;
			FieldOfView = fieldOfView;
		}

		public BCFPerspectiveCamera(XElement node)
		{
			CameraViewPoint =
				new Vector(
					(double?)node.Element("CameraViewPoint").Element("X") ?? double.NaN,
					(double?)node.Element("CameraViewPoint").Element("Y") ?? double.NaN,
					(double?)node.Element("CameraViewPoint").Element("Z") ?? double.NaN);

			CameraDirection =
				new Vector(
					(double?)node.Element("CameraDirection").Element("X") ?? double.NaN,
					(double?)node.Element("CameraDirection").Element("Y") ?? double.NaN,
					(double?)node.Element("CameraDirection").Element("Z") ?? double.NaN);

			CameraUpVector =
				new Vector(
					(double?)node.Element("CameraUpVector").Element("X") ?? double.NaN,
					(double?)node.Element("CameraUpVector").Element("Y") ?? double.NaN,
					(double?)node.Element("CameraUpVector").Element("Z") ?? double.NaN);

			FieldOfView = (double?)node.Element("FieldOfView") ?? double.NaN;
		}
	}
}