using System;
using XEngine;

public class PtAccountLogin: XProtocol
{
	private int m_playerKey;
	
	public PtAccountLogin()
	{
		m_ptID = 0x00020001;
	}

	public override void OnAnalyze(byte[] stream)
	{
	}

	public void Send()
	{

	}
}

