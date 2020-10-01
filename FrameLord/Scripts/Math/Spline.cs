// Mono Framework
using System;
using System.Collections.Generic;

// Unity Framework
using UnityEngine;

namespace FrameLord
{
	public class Spline
	{
		// Points of the spline
		private Vector3[] point;

		// Lengths
		private float[] length;

		// Prev count of nodes
		private int prevCount = -1;

		// Steps for debug aproximation
		public float splineSteps = 3;

		// Total length of the spline
		public float lengthTotal = 0;

		/// <summary>
		/// Update of the spline information
		/// </summary>
		public void Update(Vector3[] pnts)
		{
			point = pnts;

			if (length == null || length.Length != pnts.Length)
				length = new float[pnts.Length];

			if (point.Length > 1)
			{
				if (point.Length != prevCount)
				{
					length[0] = 0;
					length[1] = 0;
					length[point.Length - 1] = 0;

					for (int i = 2; i < point.Length - 1; i++)
						CalculateLengthInSegment(i);

					prevCount = point.Length;
				}

				CalculateLength();
			}
		}

		/// <summary>
		/// Update of the spline information
		/// </summary>
		public void Update(List<Transform> transfs)
		{
			point = new Vector3[transfs.Count];
			for (int i = 0; i < point.Length; i++)
			{
				point[i] = transfs[i].localPosition;
			}

			Update(point);
		}

		/// <summary>
		/// Function to calculate the length
		/// </summary>
		public float CalculateLength()
		{
			float lengthStep = 10.0f;

			Vector3 prev = GetPoint(2, 0);
			Vector3 q, dif;
			lengthTotal = 0;

			for (int i = 2; i < point.Length - 1; i++)
			{
				for (int j = 1; j <= lengthStep; j++)
				{
					q = GetPoint(i, j / lengthStep);
					dif = q - prev;
					lengthTotal += dif.magnitude;
					prev = q;
				}
			}

			return lengthTotal;
		}

		/// <summary>
		/// Calculate the length in the specified segment
		/// </summary>
		float CalculateLengthInSegment(int index)
		{
			if (index < 2) return -1;
			if (index > point.Length - 1) return -2;

			float lengthStep = 10.0f;
			Vector3 prev = GetPoint(index, 0);
			Vector3 q, dif;
			length[index] = 0;
			for (int j = 1; j <= lengthStep; j++)
			{
				q = GetPoint(index, j / lengthStep);
				dif = q - prev;
				length[index] += dif.magnitude;
				prev = q;
			}

			return length[index];
		}


		/// <summary>
		/// Reusable local variables (optimization)
		/// </summary>
		float calc1, calc2, calc3, calc4, px, py, pz;

		/// <summary>
		/// Evaluate a point on the B spline
		/// </summary>
		public Vector3 GetPoint(int i, float t)
		{
			if (point == null || point.Length < 4 || i < 2) return Vector3.zero;

			calc1 = (((-t + 3) * t - 3) * t + 1) / 6;
			calc2 = (((3 * t - 6) * t) * t + 4) / 6;
			calc3 = (((-3 * t + 3) * t + 3) * t + 1) / 6;
			calc4 = (t * t * t) / 6;

			px = calc1 * point[i - 2].x;
			py = calc1 * point[i - 2].y;
			pz = calc1 * point[i - 2].z;

			px += calc2 * point[i - 1].x;
			py += calc2 * point[i - 1].y;
			pz += calc2 * point[i - 1].z;

			px += calc3 * point[i].x;
			py += calc3 * point[i].y;
			pz += calc3 * point[i].z;

			px += calc4 * point[i + 1].x;
			py += calc4 * point[i + 1].y;
			pz += calc4 * point[i + 1].z;

			return new Vector3(px, py, pz);
		}

		/// <summary>
		/// Returns a point inside the path at a distance from the origin.
		/// </summary>
		public Vector3 GetPointAtDistance(float distance)
		{
			int val;
			return GetPointAtDistance(distance, out val);
		}

		/// <summary>
		/// Returns a point inside the path at a distance from the origin.
		/// </summary>
		public Vector3 GetPointAtDistance(float distance, out int val)
		{
			int index = 2;
			int indexCheck = -1;
			bool done = false;

			float acumDist = 0.0f;

			try
			{
				acumDist = length[index];
			}
			catch
			{
				val = -1;
				return Vector3.zero;
			}

			val = -1;
			if (distance < 0)
			{
				done = true;
				val = 0;
				return GetPoint(2, 0);
			}

			while (!done)
			{
				if (index >= point.Length - 1)
				{
					done = true;
				}
				else
				{
					if (distance > acumDist)
					{
						index++;
						acumDist += length[index];
					}
					else
					{
						done = true;
						indexCheck = index;
					}
				}
			}

			if (indexCheck != -1)
			{
				float relDist = length[indexCheck] - (acumDist - distance);
				float proportional = relDist / length[indexCheck];
				return GetPoint(indexCheck, proportional);
			}
			else
			{
				val = 1;
				return GetPoint(point.Length - 2, 1);
			}
		}

		public Vector3 GetForward(float distance, float totalLen)
		{
			Vector3 pnt1 = GetPointAtDistance(distance);
			Vector3 pnt2 = GetPointAtDistance(distance * 1.1f < totalLen ? distance * 1.1f : totalLen);

			if (pnt1 != pnt2)
			{
				return (pnt2 - pnt1).normalized;
			}
			else
			{
				return Vector3.zero;
			}
		}
	}
}