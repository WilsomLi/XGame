using System;
using System.Collections;
using System.Collections.Generic;

namespace XEngine
{
	public class LXEngine : XSingleton<LXEngine>
	{
		private Dictionary<XTimer, XTimer> m_dicTimer;
		private Dictionary<int, List<XEvent>> m_dicEvent;

		private float m_runSecond = 0.0f;

		public LXEngine ()
		{
			m_dicTimer = new Dictionary<XTimer, XTimer> ();
			m_dicEvent = new Dictionary<int, List<XEvent>> ();
		}

		public void OnRun(float runSecond)
		{
			m_runSecond = runSecond;
			OnRunTimer ();
		}

		public XTimer CreateTimer(float second, Action<object> callback, object param)
		{
			XTimer timer = new XTimer ();
			m_dicTimer.Add (timer, timer);
			timer.Reset (m_runSecond, second, callback, param);
			return timer;
		}

		private void OnRunTimer()
		{
			XTimer timer;
			var enumerator = m_dicTimer.GetEnumerator ();
			while (enumerator.MoveNext ()) {
				timer = enumerator.Current.Value;
				if (timer.IsStop ()) {
					m_dicTimer [timer] = null;
				} else {
					timer.OnRun (m_runSecond);
				}
			}
		}

		public XEvent RegisterEvent(int eventID, Action callback)
		{
			List<XEvent> lisEvent;
			if (!m_dicEvent.TryGetValue (eventID, out lisEvent)) {
				lisEvent = new List<XEvent> ();
				m_dicEvent.Add (eventID, lisEvent);
			}
			XEvent evt = new XEvent(eventID, callback);
			lisEvent.Insert(0,evt);
			return evt;
		}

		public void UnregisterEvent(XEvent evt)
		{
			int eventID = evt.GetID ();
			List<XEvent> lisEvent;
			if (m_dicEvent.TryGetValue (eventID, out lisEvent)) {
				lisEvent.Remove (evt);
				if (lisEvent.Count <= 0) {
					m_dicEvent.Remove (eventID);
				}
			}
		}

		public void SetEvent(int eventID)
		{
			List<XEvent> lisEvent;
			if (m_dicEvent.TryGetValue (eventID, out lisEvent)) {
				XEvent evt;
				for (int i = lisEvent.Count - 1; i >= 0; i--) {
					evt = lisEvent [i];
					evt.OnEvent ();
				}
			}
		}
	}
}

