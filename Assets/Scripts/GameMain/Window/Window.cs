using System;
using System.Collections.Generic;
using XEngine;
using UnityEngine;

public class Window
{
	private Dictionary<int,XEvent> m_dicEvent;
	private Dictionary<Type,List<Action<XProtocol>>> m_dicProto;

	private WndID m_wndID;
	protected GameObject m_wndCtrl;

	public Window()
	{
	}

	public WndID GetWndID()
	{
		return m_wndID;
	}

	public virtual void Destroy()
	{
		if (m_dicEvent != null) {
			foreach (XEvent evnt in m_dicEvent.Values) {
				LXEngine.Instance.UnregisterEvent (evnt);
			}
			m_dicEvent.Clear ();
			m_dicEvent = null;
		}
		if (m_dicProto != null) {
			List<Action<XProtocol>> lisCallback;
			int ptID;
			foreach(Type type in m_dicProto.Keys) {
				lisCallback = m_dicProto[type];
				ptID = XProtocolMgr.Instance.GetProtocolID (type);
				foreach (Action<XProtocol> callback in lisCallback) {
					XProtocolMgr.Instance.Unregister(ptID, callback);
				}
				lisCallback.Clear();
			}
			m_dicProto.Clear ();
			m_dicProto = null;
		}
		GameObject.Destroy (m_wndCtrl);
	}

	public void Init (WndID wndID, GameObject wndCtrl)
	{
		m_wndID = wndID;
		m_wndCtrl = wndCtrl;
		InitCtrl (m_wndCtrl.transform);
		Register ();
	}

	public virtual bool OnCreate(object param)
	{
		return true;
	}

	protected virtual void InitCtrl(Transform tf)
	{
	}

	protected virtual void Register()
	{
	}

	protected void RegisterEvent(int eventID,Action callback)
	{
		if (m_dicEvent == null) {
			m_dicEvent = new Dictionary<int, XEvent> ();
		}
		XEvent evnt = LXEngine.Instance.RegisterEvent (eventID,callback);
		m_dicEvent.Add (eventID, evnt);
	}

	protected void RegisterProto<T>(Action<XProtocol> callback) where T:XProtocol,new()
	{
		if (m_dicProto == null) {
			m_dicProto = new Dictionary<Type, List<Action<XProtocol>>> ();
		}
		Type type = typeof(T);
		List<Action<XProtocol>> listCallback;
		if (!m_dicProto.TryGetValue(type, out listCallback))
		{
			listCallback = new List<Action<XProtocol>>();
			m_dicProto[type] = listCallback;
		}
		if (listCallback.IndexOf(callback) == -1)
		{
			listCallback.Add(callback);
			XProtocolMgr.Instance.Register<T>(callback);
		}
	}
}

