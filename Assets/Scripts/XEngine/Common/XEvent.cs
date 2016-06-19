using System;

namespace XEngine
{
	public class XEvent
	{
		private int m_ID = 0;
		private Action m_callback;

		public XEvent (int eventID, Action callback)
		{
			m_ID = eventID;
			m_callback = callback;
		}

		public int GetID()
		{
			return m_ID;
		}

		public void OnEvent()
		{
			m_callback ();
		}
	}
}

