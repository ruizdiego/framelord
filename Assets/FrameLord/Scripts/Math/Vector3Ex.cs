// Unity Framework
using UnityEngine;

namespace FrameLord
{
	public static class Vector3Ex
	{
		public static Vector3 NoValidPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

		//     P
		//    /|
		//   / |
		//  /  v
		// A---X----->D
		//
		// p: Point
		// a: point from Vector
		// d: direction vector
		public static Vector3 PerpendicularVectorPoint(Vector3 a, Vector3 d, Vector3 p)
		{
			return (a + (Vector3.Dot((p - a), d) * d)) - p;
		}

		// Expect: (x, y, z)
		// The decimal point of the vector should be '.'
		public static Vector3 Parse(string strVector3)
		{
			string stripped = strVector3.Substring(1, strVector3.Length - 1);
			string[] vals = stripped.Split(new char[] {','});
			return new Vector3(System.Convert.ToSingle(vals[0]), System.Convert.ToSingle(vals[1]), System.Convert.ToSingle(vals[2]));
		}

		public static float Angle(float u1, float u2, float v1, float v2)
		{
			float dotprod = u1 * v1 + u2 * v2;
			float un = Mathf.Sqrt(u1 * u1 + u2 * u2);
			float vn = Mathf.Sqrt(v1 * v1 + v2 * v2);
			return Mathf.Acos(dotprod / (un * vn)) * Mathf.Rad2Deg;
		}

		public static float DistanceXZ(Vector3 v1, Vector3 v2)
		{
			return Mathf.Sqrt((v2.x - v1.x) * (v2.x - v1.x) + (v2.z - v1.z) * (v2.z - v1.z));
		}

		public static Vector3 RotateVector(Vector3 vec, float angle)
		{
			float nx = vec.x * Mathf.Cos(angle * Mathf.Deg2Rad) - vec.z * Mathf.Sin(angle * Mathf.Deg2Rad);
			float nz = vec.x * Mathf.Sin(angle * Mathf.Deg2Rad) + vec.z * Mathf.Cos(angle * Mathf.Deg2Rad);
			return new Vector3(nx, 0f, nz);
		}
	}
}
