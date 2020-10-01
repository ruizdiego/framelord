
namespace FrameLord
{
	public static class AngleUtil
	{
		public static float Normalize(float angle)
		{
			return ((angle + 180f) % 360f) - 180f;
		}
	}
}
