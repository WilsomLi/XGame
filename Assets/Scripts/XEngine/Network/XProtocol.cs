using System;


using System.Collections.Generic;

namespace XEngine
{
	public class XProtocol
	{
		protected int m_ptID = 0;
		private List<Action<XProtocol>> m_lisCallback;

		public XProtocol ()
		{
			m_lisCallback = new List<Action<XProtocol>> ();
		}

		public int GetProtocolID()
		{
			return m_ptID;
		}

		public void RegisterCallback(Action<XProtocol> callback)
		{
			m_lisCallback.Add (callback);
		}

		public void UnregisterCallback(Action<XProtocol> callback)
		{
			m_lisCallback.Remove (callback);
		}

		public void OnCallback()
		{
			for (int i = m_lisCallback.Count - 1; i >= 0; i--) {
				m_lisCallback [i] (this);
			}
		}

		public virtual void OnAnalyze(XStream stream)
		{

		}

		public void Send(XSocket socket)
		{
			socket.BeginSend(m_ptID);
			socket.EndSend();
		}
	}
}

