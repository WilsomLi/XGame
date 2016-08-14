using System;
using XEngine;

public class GameMain : XSingleton<GameMain>
{
	private WindowMgr m_wndMgr;

	public GameMain ()
	{
		m_wndMgr = WindowMgr.Instance;
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
//		XProtocolMgr.Instance.Register (new GnpAccountLogin(), OnLogin);
	}

	private void OnLogin()
	{
	}

	public void OnRun(float runSecond)
	{
		LXEngine.Instance.OnRun (runSecond);
	}
}

