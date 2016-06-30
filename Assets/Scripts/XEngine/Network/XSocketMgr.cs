using System;
using System.Collections.Generic;

namespace XEngine
{
	public class XSocketMgr: XSingleton<XSocketMgr>
	{
		private List<XSocket> m_lisSocket;

		public XSocketMgr()
		{
			m_lisSocket = new List<XSocket>();
		}

		public void Destroy()
		{
			CloseAllSocket();
			m_lisSocket.Clear();
			m_lisSocket = null;
		}

		public void CloseAllSocket()
		{
			int cnt = m_lisSocket.Count;
			for (int i = 0; i < cnt; i++)
			{
				m_lisSocket[i].Close();
			}
		}

		public void AddSocket(XSocket socket)
		{
			m_lisSocket.Add(socket);
		}

		public void RemoveSocket(XSocket socket)
		{
			m_lisSocket.Remove(socket);
		}
	}
}

