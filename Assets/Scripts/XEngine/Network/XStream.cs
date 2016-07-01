using System;
using System.IO;
using System.Text;

namespace XEngine
{
	public class XStream : MemoryStream, IXResetable
	{
		public XStream()
		{
		}

		public XStream(byte[] bytes) : base(bytes)
		{
		}

		public void Reset()
		{
			SetLength(0);
			Seek(0, SeekOrigin.Begin);
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
			base.Write(bytes, 0, bytes.Length);
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

		public void WriteString(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			int length = bytes.Length;
			WriteInt(bytes.Length);
			base.Write(bytes, 0, length);
		}
	}
}

