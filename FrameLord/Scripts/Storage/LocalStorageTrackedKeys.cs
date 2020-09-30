// Mono Framework
using System.Collections.Generic;

namespace FrameLord.Storage
{
	[System.Serializable]
	public class LocalStorageTrackedKeys
	{
		public List<string> trackedKeys;

		// Constructor
		public LocalStorageTrackedKeys()
		{
			trackedKeys = new List<string>();
		}
	}
}