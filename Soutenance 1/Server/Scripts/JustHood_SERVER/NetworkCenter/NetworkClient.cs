using System;
using System.Net.Sockets;
using JustHood_SERVER.Events;

namespace JustHood_SERVER.NetworkCenter
{
    public class NetworkClient
    {
        public int Index;
        public string IP;
        public TcpClient Socket;
        public NetworkStream myStream;
        private byte[] readBuff;

        public float posX;
        public float posY = 64;
        public float posZ;

        public void Reset()
        {
            Index = 0;
            IP = null;
            posX = 0;
            posY = 64;
            posZ = 0;
            readBuff = null;
            Socket = null;
            myStream = null;
        }
        public void Start()
        {
            Socket.SendBufferSize = 4096;
            Socket.ReceiveBufferSize = 4096;
            myStream = Socket.GetStream();
            Array.Resize(ref readBuff, Socket.ReceiveBufferSize);
            myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
        }

        void CloseConnection()
        {
            onJoinDisconnectEvents.onDisconnect(this);
            Socket.Close();
            Socket = null;
        }

        public bool isPlaying()
        {
            return Socket != null;
        }

        public byte[] ToDatas()
        {
            ByteBuffer buffer = new ByteBuffer();
            
            buffer.WriteInteger((int) ServerPackets.SPlayerDatas);
            buffer.WriteInteger(Index);
            buffer.WriteFloat(posX);
            buffer.WriteFloat(posY);
            buffer.WriteFloat(posZ);

            return buffer.ToArray();
        }
        
        void OnReceiveData(IAsyncResult result)
        {
            try
            {
                int readBytes = myStream.EndRead(result);
                if (Socket == null)
                {
                    return;
                }
                if (readBytes <= 0)
                {
                    CloseConnection();
                    return;
                }

                byte[] newBytes = null;
                Array.Resize(ref newBytes, readBytes);
                Buffer.BlockCopy(readBuff, 0, newBytes, 0, readBytes);

                Datas.networkHandleData.HandleData(Index, newBytes);

                if (Socket == null)
                {
                    return;
                } 
                
                myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceiveData, null);
                
            }
            
            catch (Exception ex)
            {
                CloseConnection();
                return;
            }
        }
    }
}
