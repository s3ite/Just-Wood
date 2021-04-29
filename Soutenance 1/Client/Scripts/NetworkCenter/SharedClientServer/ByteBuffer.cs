using System;
using System.Collections.Generic;
using System.Text;

namespace JustHood_SERVER.NetworkCenter
{
    public class ByteBuffer : IDisposable
    {
        List<byte> Buff;
        byte[] readBuff;
        int readpos;
        bool buffUpdate = false;

        public ByteBuffer()
        {
            Buff = new List<byte>();
            readpos = 0;
        }

        public int GetReadPos()
        {
            return readpos;
        }

        public byte[] ToArray()
        {
            return Buff.ToArray();
        }

        public int Count()
        {
            return Buff.Count;
        }

        public int Length()
        {
            return Count() - readpos;
        }

        public void Clear()
        {
            Buff.Clear();
            readpos = 0;    
        }
        
        /*
         *
         *  WRITE DATAS
         * 
         */

        public void WriteByte(byte Inputs)
        {
            Buff.Add(Inputs);
            buffUpdate = true;
        }

        public void WriteBytes(byte[] Input)
        {
            Buff.AddRange(Input);
            buffUpdate = true;
        }

        public void WriteShort(short Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input));
            buffUpdate = true;
        }

        public void WriteInteger(int Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input));
            buffUpdate = true;
        }
        
        public void WriteBoolean(bool b)
        {
            int i = b ? 1 : 0;
            Buff.AddRange(BitConverter.GetBytes(i));
            buffUpdate = true;
        }


        public void WriteFloat(float Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input));
            buffUpdate = true;
        }

        public void WriteString(string Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input.Length));
            Buff.AddRange(Encoding.ASCII.GetBytes(Input));
            buffUpdate = true;
        }

         /*
          *
          *  READ DATAS
          * 
          */

        public string ReadString(bool Peek = true)
        {
            int len = ReadInteger(true);
            if(buffUpdate)
            {
                readBuff = Buff.ToArray();
                buffUpdate = false;
            }

            string ret = Encoding.ASCII.GetString(readBuff, readpos, len);
            if(Peek & Buff.Count > readpos)
            {
                if(ret.Length > 0)
                {
                    readpos += len;
                }
            }
            return ret;
        }

        public  byte ReadByte(bool Peek = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                byte ret = readBuff[readpos];
                if (Peek & Buff.Count > readpos)
                {
                    readpos += 1;
                }
                return ret;
            }

            else
            {
                throw new Exception("Byte Buffer Past Limit!");
            }
        }

        public byte[] ReadBytes(int Length, bool Peek = true)
        {
            if(buffUpdate)
            {
                readBuff = Buff.ToArray();
                buffUpdate = false;
            }

            byte[] ret = Buff.GetRange(readpos, Length).ToArray();
            if(Peek)
            {
                readpos += Length;
            }
            return ret;
        }

        public bool ReadBool(bool Peek = true)
        {
            int i = ReadInteger(Peek);
            return i == 1;
        }
        
        public float ReadFloat(bool Peek = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                float ret = BitConverter.ToSingle(readBuff, readpos);
                if (Peek & Buff.Count > readpos)
                {
                    readpos += 4;
                }
                return ret;
            }

            else
            {
                throw new Exception("Byte Buffer is Past its Limit!");
            }
        }

        public int ReadInteger(bool Peek = true)
        {
            if(Buff.Count > readpos)
            {
                if(buffUpdate)
                {
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                int ret = BitConverter.ToInt32(readBuff, readpos);
                if(Peek & Buff.Count > readpos)
                {
                    readpos += 4;
                }
                return ret;
            }

            else
            {
                throw new Exception("Byte Buffer is Past its Limit!");
            }
        }

        private bool disposedValue = false;

        //IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposedValue)
            {
                if(disposing)
                {
                    Buff.Clear();
                }

                readpos = 0;
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
