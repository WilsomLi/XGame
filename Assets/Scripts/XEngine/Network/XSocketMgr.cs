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

		}

		public void CloseAllSocket()
		{
			for (int i = 0; i < m_lisSocket.Count; i++)
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

