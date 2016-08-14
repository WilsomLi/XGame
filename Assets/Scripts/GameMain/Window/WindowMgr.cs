using System;
using System.Collections.Generic;
using XEngine;
using UnityEngine;

public class WindowMgr : XSingleton<WindowMgr>
{
	private Dictionary<WndID, Window> m_dicWnd;

	private Dictionary<WndID, Type> m_dicRegWnd;

	private Transform m_uiRoot;

	public WindowMgr ()
	{
		m_dicWnd = new Dictionary<WndID, Window> ();
		m_dicRegWnd = new Dictionary<WndID, Type> ();

		m_uiRoot = GameObject.Find ("Canvas").transform;
		GameObject.DontDestroyOnLoad (m_uiRoot);

		GameObject goEventSystem = GameObject.Find("EventSystem");
		GameObject.DontDestroyOnLoad(goEventSystem);
	}

	public void RegisterWnd<T>(WndID wndID)
	{
		m_dicRegWnd.Add (wndID, typeof(T));
	}

	public bool OpenWindow(WndID wndID, object param=null)
	{
		Type type;
		if (!m_dicRegWnd.TryGetValue (wndID, out type)) {
			return false;
		}
		Window wnd = System.Activator.CreateInstance (type) as Window;

		string path = "Prefabs/UI/" + type.ToString ();
		GameObject prefabe = Resources.Load (path) as GameObject;
		if (prefabe == null)
			return false;
		GameObject wndCtrl = GameObject.Instantiate (prefabe);
		wndCtrl.transform.SetParent(m_uiRoot);
		wndCtrl.transform.localPosition = Vector3.zero;
//		wndCtrl.transform.position = Vector3.zero;
//		wndCtrl.transform.localScale = Vector3.one;
		wndCtrl.layer = LayerMask.NameToLayer ("UI");

		wnd.Init (wndID, wndCtrl);
		wnd.OnCreate (param);
		m_dicWnd.Add (wndID, wnd);
		return true;
	}

	public void CloseWindow(WndID wndID)
	{
		Window wnd;
		if (m_dicWnd.TryGetValue (wndID, out wnd)) {
			wnd.Destroy ();
			m_dicWnd.Remove (wndID);
		}
	}

	public Window GetWindow(WndID wndID)
	{
		Window wnd = null;
		m_dicWnd.TryGetValue(wndID, out wnd);
		return wnd;
	}
}

