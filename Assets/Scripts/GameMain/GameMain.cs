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


		XStream stream = new XStream ();
		stream.WriteInt (123);
		stream.Seek (0,System.IO.SeekOrigin.Begin);
//		stream.Seek(0,System.IO.SeekOrigin.Current);
		m_protoMgr.OnProtocol (Protocol.GNP_ACCOUNT_LOGIN,stream);
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

	private void OnLogin(XProtocol proto)
	{
		GnpAccountLogin pt = proto as GnpAccountLogin;
		Debugger.Log ("OnLogin playerKey:" + pt.GetPlayerKey ());
	}

	public void OnRun(float runSecond)
	{
		LXEngine.Instance.OnRun (runSecond);
	}
}

