using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XEngine
{
	public class XSocket
	{
		private Socket m_socket;
		private byte[] m_recvBuff;

		private bool m_bConnected = false;
		private bool m_bReceiving = false;

		private const int MAX_BUFFER_SIZE = 65535;
		private const int CONNECT_TIME_OUT = 3000;

		public XSocket ()
		{
			m_recvBuff = new byte[MAX_BUFFER_SIZE];
		}

		public void Connect(string strIp, int port)
		{
			if (m_socket == null)
			{
				m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				m_socket.ReceiveBufferSize = MAX_BUFFER_SIZE;

				XSocketMgr.Instance.AddSocket(this);
			}

			IPAddress ip = IPAddress.Parse(strIp);
			IPEndPoint ipe = new IPEndPoint(ip, port);
			IAsyncResult result = m_socket.BeginConnect(ipe, new AsyncCallback(OnConnected), m_socket);
			bool bConnectSuccess = result.AsyncWaitHandle.WaitOne(CONNECT_TIME_OUT, true);
			if (!bConnectSuccess)
			{
				Close();
			}
		}

		private void OnConnected(IAsyncResult ar)
		{
			m_socket.EndConnect(ar);
			m_bConnected = true;
			m_bReceiving = true;
		}

		public void Close()
		{
			if (m_socket != null && m_socket.Connected)
			{
				m_socket.Close();
			}
			m_bConnected = false;
		}

		public void Send(byte[] bytes, int length)
		{
			if (!m_bConnected)
				return;
			try
			{
				m_socket.Send(bytes, length, SocketFlags.None);
			}
			catch (Exception exp)
			{
				Console.Write(exp);
				Close();
			}
		}

		public void BeginSend(int ptID)
		{

		}

		public void EndSend()
		{

		}

		public void OnRun()
		{
			if (!m_bConnected)
				return;
			RecvStream();
		}

		private void RecvStream()
		{
			try
			{
				if (m_bReceiving)
				{
					m_bReceiving = false;
					int length = m_socket.Available;
					m_socket.BeginReceive(m_recvBuff, 0, length, SocketFlags.None, new AsyncCallback(EndRecvStream), m_socket);
				}
			}
			catch (Exception exp)
			{
				Console.Write(exp);
			}
		}

		private void EndRecvStream(IAsyncResult ar)
		{
			m_socket.EndReceive(ar);
			m_bReceiving = true;
		}
	}
}

