using System;
using System.Collections.Generic;

namespace XEngine
{
	public class XObjectPool<T> where T : class, IXResetable, new()
	{
		private Stack<T> m_objectStack;
		
		private Action<T> m_resetAction;
		private Action<T> m_onetimeInitAction;

		public XObjectPool(int size, Action<T> resetAction=null, Action<T> onetimeInitAction=null)
		{
			m_objectStack = new Stack<T>(size);
			m_resetAction = resetAction;
			m_onetimeInitAction = onetimeInitAction;
		}

        public void Destroy()
        {
            m_objectStack.Clear();
            m_objectStack = null;
            m_resetAction = null;
            m_onetimeInitAction = null;
        }

		public T Acquire()
		{
			T t;
			if (m_objectStack.Count > 0) 
			{
				t = m_objectStack.Pop();
				t.Reset();
				if(m_resetAction != null)
				{
					m_resetAction(t);
				}
			}
			else
			{
				t = new T();
				if(m_onetimeInitAction != null)
				{
					m_onetimeInitAction(t);
				}
			}
			return t;
		}
		
		public void Relase(T obj)
		{
			m_objectStack.Push(obj);			
		}
	}
}

