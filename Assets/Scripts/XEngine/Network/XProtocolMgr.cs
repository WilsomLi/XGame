using System;

using System.Collections.Generic;

namespace XEngine
{
	public class XProtocolMgr : XSingleton<XProtocolMgr>
	{
		private Dictionary<Type, int> m_dicPtType;
		private Dictionary<int, XProtocol> m_dicProtocol;

		public XProtocolMgr ()
		{
			m_dicPtType = new Dictionary<Type, int> ();
			m_dicProtocol = new Dictionary<int, XProtocol> ();
		}

		public void Register<T>(Action<XProtocol> callback) where T : XProtocol,new()
		{
			XProtocol protocol = new T() as XProtocol;
			int ptID = protocol.GetProtocolID ();
			Type type = typeof(T);
			m_dicPtType [type] = ptID;
			if (!m_dicProtocol.ContainsKey (ptID)) {
				m_dicProtocol.Add (ptID, protocol);
			}	
			if (callback != null) {
				protocol.RegisterCallback (callback);
			}
		}

		public void Unregister(int ptID, Action<XProtocol> callback)
		{
			XProtocol protocol;
			if (m_dicProtocol.TryGetValue (ptID, out protocol)) {
				protocol.UnregisterCallback (callback);
			}
		}

		public void Unregister<T>(Action<XProtocol> callback) where T : XProtocol
		{
			int ptID = 0;
			m_dicPtType.TryGetValue (typeof(T), out ptID);
			XProtocol protocol;
			if (m_dicProtocol.TryGetValue (ptID, out protocol)) {
				protocol.UnregisterCallback (callback);
			}
		}

		public XProtocol FindProtocol(int ptID)
		{
			XProtocol protocol;
			m_dicProtocol.TryGetValue (ptID, out protocol);
			return protocol;
		}

		public void OnProtocol(int ptID, XStream stream)
		{
			XProtocol pt;
			if (m_dicProtocol.TryGetValue (ptID, out pt)) {
				pt.OnAnalyze (stream);
				pt.OnCallback ();
			}
		}

		public int GetProtocolID(Type type)
		{
			return m_dicPtType [type];
		}
	}
}

