

namespace FrameLord
{

	/// <summary>
	/// Game Event base class
	/// </summary>
	public class GameEvent
	{
		// Event name
		protected string eventName;

		public GameEvent()
		{
		}

		public GameEvent(string eventName)
		{
			this.eventName = eventName;
		}

		public string EventName
		{
			get { return eventName; }
		}
	}
}