using System;

namespace XEngine
{
	public class XSingleton<T> where T : class, new()
	{
		private static T m_instance;

		public static T Instance
		{
			get
			{
				if(XSingleton<T>.m_instance == null) {
					XSingleton<T>.m_instance = Activator.CreateInstance<T>();
				}
				return XSingleton<T>.m_instance;
			}
		}
	}
}

