using System;
using System.Collections.Generic;

namespace XEngine
{
	public class XSocketMgr: XSingleton<XSocketMgr>
	{
		private List<XSocket> m_lisSocket;
		private XObjectPool<XStream> m_streamPool;

		private const int STREAM_POOL_SIZE = 128;

		public XSocketMgr()
		{
			m_lisSocket = new List<XSocket>();
			m_streamPool = new XObjectPool<XStream>(STREAM_POOL_SIZE);
		}

		public void Destroy()
		{
			CloseAllSocket();
			m_lisSocket.Clear();
			m_lisSocket = null;

            m_streamPool.Destroy();
			m_streamPool = null;
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

		public XStream GetXStream()
		{
			return m_streamPool.Acquire();
		}

		public void ReturnXStream(XStream stream)
		{
			m_streamPool.Relase(stream);
		}
	}
}

