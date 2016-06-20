using System;
using System.IO;

namespace XEngine
{
	public class XStream : MemoryStream
	{
		public XStream()
		{
		}

		public XStream(byte[] bytes) : base(bytes)
		{
		}

		public void Write(byte value)
		{
			this.WriteByte(value);
		}

		public void Write(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.Write(bytes, 0, bytes.Length);
		}

		public new byte ReadByte()
		{
			return (byte)base.ReadByte();
		}
	}
}

