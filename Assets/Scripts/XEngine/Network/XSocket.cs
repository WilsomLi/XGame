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

		private XStream m_recvStream;
		private XStream m_sendStream;

		private bool m_bConnected = false;
		private bool m_bReceiving = false;

		private const int MAX_BUFFER_SIZE = 65535;
		private const int CONNECT_TIME_OUT = 2000;
		private const int RETRY_CONNECT_CNT = 3;

		public XSocket ()
		{
			m_recvBuff = new byte[MAX_BUFFER_SIZE];
			m_recvStream = XSocketMgr.Instance.GetXStream();
		}

		public void Connect(string strIp, int port)
		{
			if (m_socket == null)
			{
				m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				m_socket.SendBufferSize = MAX_BUFFER_SIZE;
				m_socket.ReceiveBufferSize = MAX_BUFFER_SIZE;

				XSocketMgr.Instance.AddSocket(this);
			}

			IPAddress ip = IPAddress.Parse(strIp);
			IPEndPoint ipe = new IPEndPoint(ip, port);

			int cnt = RETRY_CONNECT_CNT;
			IAsyncResult result;
			bool bConnectSuccess;
			while (cnt-- > 0)
			{
				result = m_socket.BeginConnect(ipe, new AsyncCallback(OnConnected), m_socket);
				bConnectSuccess = result.AsyncWaitHandle.WaitOne(CONNECT_TIME_OUT, true);
				if (bConnectSuccess)
				{
					break;
				}
			}
			if (cnt < 0)
			{
				Console.WriteLine("Socket connect fail,ip:{0},port:{1}",strIp,port);
			}
		}

		private void OnConnected(IAsyncResult ar)
		{
			m_socket.EndConnect(ar);
			m_bConnected = true;
			m_bReceiving = false;
		}

		public void Close()
		{
			XSocketMgr.Instance.RemoveSocket(this);
			if (m_socket != null && m_socket.Connected)
			{
				m_socket.Close();
			}
			m_socket = null;
			m_bConnected = false;
			m_bReceiving = false;
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
				Console.WriteLine(exp);
				Close();
			}
		}

		public XStream BeginSend(int ptID)
		{
			if (m_sendStream == null)
			{
				m_sendStream = XSocketMgr.Instance.GetXStream();
			}
			m_sendStream.Reset();
			m_sendStream.WriteInt(ptID);
			return m_sendStream;
		}

		public void EndSend()
		{
			if (m_sendStream != null)
			{
				byte[] buffer = m_sendStream.GetBuffer();
				Send(buffer, buffer.Length);
			}
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
				if (!m_bReceiving)
				{
					m_bReceiving = true;
					m_socket.BeginReceive(m_recvBuff, 0, MAX_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnEndRecvStream), m_socket);
				}
			}
			catch (Exception exp)
			{
				Console.WriteLine(exp);
				Close();
			}
		}

		private void OnEndRecvStream(IAsyncResult ar)
		{
			try
			{
				int length = m_socket.EndReceive(ar);
				m_bReceiving = false;
				if (length == 0)
				{
					Close();
					return;
				}

				m_recvStream.Reset();
				m_recvStream.Write(m_recvBuff, 0, length);
				int protoID = m_recvStream.ReadInt();
				XProtocolMgr.Instance.OnProtocol(protoID, m_recvStream);
			}
			catch (Exception exp)
			{
				Console.WriteLine(exp);
				Close();
			}
		}
	}
}

