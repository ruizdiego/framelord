// Unity Framework
using UnityEngine;

namespace FrameLord
{
	public static class Vector2Ex
	{
		public static Vector2 Rotate(Vector2 vec, float angleDeg)
		{
			float angle = angleDeg * Mathf.Deg2Rad;
			float cos = Mathf.Cos(angle);
			float sin = Mathf.Sin(angle);

			float x2 = vec.x * cos - vec.y * sin;
			float y2 = vec.x * sin + vec.y * cos;

			return new Vector2(x2, y2);
		}

		public static Vector3 RotateXZ(Vector3 vec, float angleDeg)
		{
			float angle = angleDeg * Mathf.Deg2Rad;
			float cos = Mathf.Cos(angle);
			float sin = Mathf.Sin(angle);

			return new Vector3(vec.x * cos - vec.z * sin, vec.y, vec.x * sin + vec.z * cos);
		}
	}
}
