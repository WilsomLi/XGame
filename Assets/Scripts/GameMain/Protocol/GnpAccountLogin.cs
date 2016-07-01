using System;
using XEngine;

public class GnpAccountLogin: XProtocol
{
	private int m_playerKey;
	
	public GnpAccountLogin()
	{
		m_ptID = (int)Protocol.GNP_ACCOUNT_LOGIN;
	}

	public int GetPlayerKey()
	{
		return m_playerKey;
	}

	public override void OnAnalyze(XStream stream)
	{
        m_playerKey = stream.ReadInt();
	}

	public void Send(XSocket socket, string name, string pwd)
	{
		XStream stream = socket.BeginSend(m_ptID);
		stream.WriteString(name);
		stream.WriteString(pwd);
		socket.EndSend();
	}
}

