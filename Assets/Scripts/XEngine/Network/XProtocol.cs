using System;


using System.Collections.Generic;

namespace XEngine
{
	public class XProtocol
	{
		protected int m_ID = 0;
		private List<Action> m_lisCallback;

		public XProtocol ()
		{
			m_lisCallback = new List<Action> ();
		}

		public int GetID()
		{
			return m_ID;
		}

		public void RegisterCallback(Action callback)
		{
			m_lisCallback.Add (callback);
		}

		public void UnregisterCallback(Action callback)
		{
			m_lisCallback.Remove (callback);
		}

		public void OnCallback()
		{
			for (int i = m_lisCallback.Count - 1; i >= 0; i--) {
				m_lisCallback [i] ();
			}
		}

		public virtual void OnAnalyze(byte[] stream)
		{

		}

		public void Send(XSocket socket)
		{

		}
	}
}

