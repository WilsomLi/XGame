using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XEngine
{
	public class XSocket
	{
		private Socket m_socket;

		public XSocket ()
		{
		}

		public void Connect(string strIp, int port)
		{
			m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			XSocketMgr.Instance.AddSocket(this);
			IPAddress ip = IPAddress.Parse(strIp);
			IPEndPoint ipe = new IPEndPoint(ip, port);
			IAsyncResult result = m_socket.BeginConnect(ipe, new AsyncCallback(ConnectCallback), m_socket);
			bool bConnectSuccess = result.AsyncWaitHandle.WaitOne(5000, true);
			if (bConnectSuccess)
			{
				Thread readThread = new Thread(new ThreadStart(RecvStream));
				readThread.IsBackground = true;
				readThread.Start();
			}
		}

		private void ConnectCallback(IAsyncResult result)
		{

		}

		private void RecvStream()
		{

		}

		public void Send()
		{

		}

		public void Close()
		{
			if (m_socket != null)
			{
				m_socket.Close();
				m_socket = null;
			}
		}

		public void BeginSend(int ptID)
		{

		}

		public void EndSend()
		{

		}
	}
}

