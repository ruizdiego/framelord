using UnityEngine;
using System.Collections;

namespace FrameLord
{
	public class MathUtils
	{
		/// <summary>
		/// Samples the hemisphere. Takes focus as 1 - spread
		/// </summary>
		/// <param name="focus">focus = 1 - spread</param>
		public static Vector3 SampleHemisphere(float focus)
		{
			float u = Random.value;
			float v = Mathf.Clamp01(focus + Random.value * (1f - focus));

			float theta = 2f * Mathf.PI * u;
			float phi = Mathf.Acos(v);

			float cosPhi = Mathf.Cos(phi);
			float tmp = Mathf.Sqrt(1f - cosPhi * cosPhi);

			return new Vector3(tmp * Mathf.Cos(theta), cosPhi, tmp * Mathf.Sin(theta));
		}

		public static Quaternion SmoothDamp(Quaternion fromQuat, Quaternion toQuat, ref Vector3 velocity, float smoothTime, float deltaTime)
		{
			if (deltaTime <= 0.001f)
				return fromQuat;

			Vector3 fromQuatEuler = fromQuat.eulerAngles;
			Vector3 targetQuatEuler = toQuat.eulerAngles;

			Quaternion smoothedRotation = Quaternion.Euler(
				new Vector3(
					Mathf.SmoothDampAngle(fromQuatEuler.x, targetQuatEuler.x, ref velocity.x, smoothTime, float.PositiveInfinity, deltaTime),
					Mathf.SmoothDampAngle(fromQuatEuler.y, targetQuatEuler.y, ref velocity.y, smoothTime, float.PositiveInfinity, deltaTime),
					Mathf.SmoothDampAngle(fromQuatEuler.z, targetQuatEuler.z, ref velocity.z, smoothTime, float.PositiveInfinity, deltaTime))
			);

			return smoothedRotation;
		}

		public static Vector2 SmoothDamp(Vector2 fromVector, Vector2 toVector, ref Vector2 velocity, float smoothTime, float deltaTime)
		{
			if (deltaTime <= 0.001f)
				return fromVector;

			Vector3 velocity3 = velocity;

			Vector3 smoothedPosition = Vector3.SmoothDamp(fromVector, toVector, ref velocity3, smoothTime, float.PositiveInfinity, deltaTime);

			velocity = velocity3;

			return smoothedPosition;
		}

		// Ref: http://en.wikipedia.org/wiki/Smoothstep
		public static float Smootherstep(float x, float edge0, float edge1)
		{
			// Scale, and clamp x to 0..1 range
			x = Mathf.Clamp01((x - edge0) / (edge1 - edge0));

			// Evaluate polynomial
			return x * x * x * (x * (x * 6 - 15) + 10);
		}

		// Ref: http://en.wikipedia.org/wiki/Smoothstep
		public static float Smoothstep(float x, float edge0, float edge1)
		{
			// Scale, and clamp x to 0..1 range
			x = Mathf.Clamp01((x - edge0) / (edge1 - edge0));

			// Evaluate polynomial
			return x * x * (3 - 2 * x);
		}

		public static bool ContainsWithoutTranslation(Bounds container, Bounds target)
		{
			container.center = target.center;
			return container.Contains(target.min) && container.Contains(target.max);
		}

		public static bool Contains(Bounds container, Bounds target)
		{
			return container.Contains(target.min) && container.Contains(target.max);
		}

		private static Vector3[] aabbVertices =
		{
			new Vector3(1f, 1f, 1f), new Vector3(1f, -1f, -1f), new Vector3(1f, 1f, -1f), new Vector3(1f, -1f, 1f),
			new Vector3(-1f, 1f, 1f), new Vector3(-1f, -1f, -1f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, -1f, 1f),
		};

		public static Bounds TransformBounds(ref Matrix4x4 t, Bounds bounds)
		{
			Vector3 max = Vector3.one * -float.MaxValue;
			Vector3 min = Vector3.one * float.MaxValue;

			for (int i = 0; i < aabbVertices.Length; i++)
			{
				Vector3 vertex = bounds.center + Vector3.Scale(bounds.extents, aabbVertices[i]);
				Vector3 transformedVertex = t.MultiplyPoint3x4(vertex);

				max = Vector3.Max(max, transformedVertex);
				min = Vector3.Min(min, transformedVertex);
			}

			bounds.SetMinMax(min, max);

			return bounds;
		}

		// ------------------------------------------------------------------------
		// Easing functions
		public static float LinearTween(float time, float totalDelta, float startValue, float duration)
		{
			return totalDelta * time / duration + startValue;
		}

		public static float QuadEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;

			return totalDelta * time * time + startValue;
		}

		public static float QuadEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;

			return -totalDelta * time * (time - 2.0f) + startValue;
		}

		public static float QuadEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration / 2.0f;

			if (time < 1.0f)
				return totalDelta / 2.0f * time * time + startValue;

			time--;

			return -totalDelta / 2.0f * (time * (time - 2.0f) - 1.0f) + startValue;
		}

		public static float CubicEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;

			return totalDelta * time * time * time + startValue;
		}

		public static float CubicEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;

			time--;

			return totalDelta * (time * time * time + 1.0f) + startValue;
		}

		public static float CubicEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration / 2.0f;

			if (time < 1.0f)
				return totalDelta / 2.0f * (time * time * time) + startValue;

			time -= 2.0f;

			return totalDelta / 2.0f * (time * time * time + 2.0f) + startValue;
		}

		public static float QuartEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;
			return totalDelta * time * time * time * time + startValue;
		}

		public static float QuartEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;
			time--;

			return -totalDelta * (time * time * time * time - 1.0f) + startValue;
		}

		public static float QuartEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration / 2.0f;

			if (time < 1.0f)
				return totalDelta / 2.0f * time * time * time * time + startValue;

			time -= 2.0f;

			return -totalDelta / 2.0f * (time * time * time * time - 2.0f) + startValue;
		}

		public static float QuinticEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;
			return totalDelta * time * time * time * time * time + startValue;
		}

		public static float QuinticEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;
			time--;

			return totalDelta * (time * time * time * time * time - 1.0f) + startValue;
		}

		public static float QuinticEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration / 2.0f;

			if (time < 1.0f)
				return totalDelta / 2.0f * time * time * time * time * time + startValue;

			time -= 2.0f;

			return totalDelta / 2.0f * (time * time * time * time * time - 2.0f) + startValue;
		}

		public static float SinEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			return -totalDelta * Mathf.Cos(time / duration * (Mathf.PI / 2.0f)) + totalDelta + startValue;
		}

		public static float SinEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			return totalDelta * Mathf.Sin(time / duration * (Mathf.PI / 2.0f)) + startValue;
		}

		public static float SinEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			return -totalDelta / 2 * (Mathf.Cos(Mathf.PI * time / duration) - 1) + startValue;
		}

		public static float ExpEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			return totalDelta * Mathf.Pow(2.0f, 10.0f * (time / duration - 1)) + startValue;
		}

		public static float ExpEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			return totalDelta * (-Mathf.Pow(2.0f, -10.0f * time / duration) + 1.0f) + startValue;
		}

		public static float ExpEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration / 2.0f;

			if (time < 1.0f)
				return totalDelta / 2.0f * Mathf.Pow(2.0f, 10.0f * (time - 1.0f)) + startValue;

			time--;

			return totalDelta / 2.0f * (-Mathf.Pow(2.0f, -10.0f * time) + 2.0f) + startValue;
		}

		public static float CircularEaseIn(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;
			return -totalDelta * (Mathf.Sqrt(1.0f - time * time) - 1.0f) + startValue;
		}

		public static float CircularEaseOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration;

			time--;

			return totalDelta * Mathf.Sqrt(1.0f - time * time) + startValue;
		}

		public static float CircularEaseInOut(float time, float totalDelta, float startValue, float duration)
		{
			time /= duration / 2.0f;

			if (time < 1.0f)
				return -totalDelta / 2.0f * (Mathf.Sqrt(1.0f - time * time) - 1.0f) + startValue;

			time -= 2.0f;

			return totalDelta / 2.0f * (Mathf.Sqrt(1.0f - time * time) + 1.0f) + startValue;
		}

		public static float EaseOutBounce(float time, float totalDelta, float startValue, float duration)
		{
			if ((time /= duration) < (1 / 2.75f))
			{
				return totalDelta * (7.5625f * time * time) + startValue;
			}
			else if (time < (2 / 2.75f))
			{
				return totalDelta * (7.5625f * (time -= (1.5f / 2.75f)) * time + .75f) + startValue;
			}
			else if (time < (2.5f / 2.75f))
			{
				return totalDelta * (7.5625f * (time -= (2.25f / 2.75f)) * time + .9375f) + startValue;
			}
			else
			{
				return totalDelta * (7.5625f * (time -= (2.625f / 2.75f)) * time + .984375f) + startValue;
			}
		}

		public static float EaseOutBack(float time, float totalDelta, float startValue, float duration, float bounceFactor = 1.70158f)
		{
			return totalDelta * ((time = time / duration - 1) * time * ((bounceFactor + 1) * time + bounceFactor) + 1) + startValue;
		}

		public static float EaseOutElastic(float time, float totalDelta, float startValue, float duration)
		{
			if (time == 0) return startValue;
			if ((time /= duration) == 1) return startValue + totalDelta;
			float p = duration * .3f;
			float s = 0;
			float a = 0;
			if (a == 0f || a < Mathf.Abs(totalDelta))
			{
				a = totalDelta;
				s = p / 4;
			}
			else
			{
				s = p / (2 * Mathf.PI) * Mathf.Asin(totalDelta / a);
			}

			return (a * Mathf.Pow(2, -10 * time) * Mathf.Sin((time * duration - s) * (2 * Mathf.PI) / p) + totalDelta + startValue);
		}

	}
}