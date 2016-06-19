using System;

namespace XEngine
{
	public class XTimer
	{
		private bool m_bStop = false;
		private float m_endTime = 0;
		private float m_second = 0;
		private Action<object> m_callback = null;
		private object m_callbackParam = null;

		public XTimer ()
		{
		}

		public void Reset(float nowTime, float sec, Action<object> callback, object param)
		{
			m_bStop = false;
			m_second = sec;
			m_endTime = nowTime + sec;
			m_callback = callback;
			m_callbackParam = param;
		}

		public void OnRun(float nowTime)
		{
			float escapeTime = nowTime - m_endTime;
			if (escapeTime < 0)
				return;
			m_callback (m_callbackParam);
			m_endTime = nowTime + m_second;
		}

		public void Stop()
		{
			m_bStop = true;
			m_callback = null;
			m_callbackParam = null;
		}

		public bool IsStop()
		{
			return m_bStop;
		}
	}
}

