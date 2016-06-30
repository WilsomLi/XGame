using System;
using System.IO;
using System.Text;

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

		private byte[] ReadBytes(int size)
		{
			byte[] bytes = new byte[size];
			base.Read(bytes, 0, size);
			return bytes;
		}

		public bool ReadBool()
		{
			byte[] bytes = ReadBytes(sizeof(bool));
			return BitConverter.ToBoolean(bytes, 0);
		}

		public new byte ReadByte()
		{
			return (byte)base.ReadByte();
		}

		public int ReadInt()
		{
			byte[] bytes = ReadBytes(sizeof(int));
			return BitConverter.ToInt32(bytes, 0);
		}

		public float ReadFloat()
		{
			byte[] bytes = ReadBytes(sizeof(float));
			return BitConverter.ToSingle(bytes, 0);
		}

		public double ReadDouble()
		{
			byte[] bytes = ReadBytes(sizeof(double));
			return BitConverter.ToDouble(bytes, 0);
		}

		public string ReadString()
		{
			int length = ReadInt();
			byte[] bytes = ReadBytes(length);
			return Encoding.UTF8.GetString(bytes, 0, length);
		}

		public void WriteBytes(byte[] bytes)
		{
			int length = bytes.Length;
			for (int i = 0; i < length; i++)
			{
				WriteByte(bytes[i]);
			}
		}

		public void WriteBool(bool value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.Write(bytes, 0, bytes.Length);
		}

		public void WriteInt(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.Write(bytes, 0, bytes.Length);
		}

		public void WriteFloat(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.Write(bytes, 0, bytes.Length);
		}

		public void WriteDouble(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.Write(bytes, 0, bytes.Length);
		}
	}
}

