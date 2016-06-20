using System;

using System.Collections.Generic;

namespace XEngine
{
	public class XProtocolMgr : XSingleton<XProtocolMgr>
	{
		private Dictionary<int, XProtocol> m_dicProtocol;

		public XProtocolMgr ()
		{
			m_dicProtocol = new Dictionary<int, XProtocol> ();
		}

		public void Register(XProtocol pt, Action callback)
		{
			int ptID = pt.GetProtocolID ();
			XProtocol protocol;
			if (!m_dicProtocol.TryGetValue (ptID, out protocol)) {
				m_dicProtocol.Add (ptID, pt);
			}
			if (callback != null) {
				pt.RegisterCallback (callback);
			}
		}

		public void Unregister(XProtocol pt, Action callback)
		{
			int ptID = pt.GetProtocolID ();
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

		public void OnProtocol(int ptID, byte[] stream)
		{
			XProtocol pt;
			if (m_dicProtocol.TryGetValue (ptID, out pt)) {
				pt.OnAnalyze (stream);
				pt.OnCallback ();
			}
		}
	}
}

