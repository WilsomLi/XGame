using System;
using XEngine;

public class GameMain : XSingleton<GameMain>
{
	public static WindowMgr m_wndMgr = WindowMgr.Instance;
	public static XProtocolMgr m_protoMgr = XProtocolMgr.Instance;

	public GameMain ()
	{
	}

	public void StartUp()
	{
		LoadConfig ();
		RegisterWnd ();
		RegisterProto ();

		m_wndMgr.OpenWindow (WndID.Login);
	}

	private void LoadConfig()
	{
	}

	private void RegisterWnd()
	{
		m_wndMgr.RegisterWnd<WndLogin>(WndID.Login);
	}

	private void RegisterProto()
	{
		m_protoMgr.Register<GnpAccountLogin>(OnLogin);
	}

	private void OnLogin(XProtocol pt)
	{
	}

	public void OnRun(float runSecond)
	{
		LXEngine.Instance.OnRun (runSecond);
	}
}

