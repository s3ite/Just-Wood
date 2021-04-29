using System;
using System.Collections.Generic;

namespace JustHood_SERVER.NetworkCenter
{
    class NetworkHandleData
    {
        private delegate void Packet(int Index, byte[] Data);
        private Dictionary<int, Packet> Packets;

        public void InitMessages()
        {
            Packets = new Dictionary<int, Packet>();
            Packets.Add(1, HandleNewAccount);
            Packets.Add((int) ClientPackets.CHandleMovement, HandleMovement);
        }

        public void HandleMovement(int index, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInteger(); //packetnum

            float posX = buffer.ReadFloat();
            float posY = buffer.ReadFloat();
            float posZ = buffer.ReadFloat();

            float rotX = buffer.ReadFloat();
            float rotY = buffer.ReadFloat();
            float rotZ = buffer.ReadFloat();
            float rotW = buffer.ReadFloat();
            
            NetworkClient client = Datas.GetClientFromIndex(index);
            client.posX = posX;
            client.posY = posY;
            client.posZ = posZ;
            
            buffer = null;
  
            Datas.networkSendData.SendPlayerMovement(index,posX,posY,posZ,rotX,rotY,rotZ,rotW);
            
        }
        
        public void HandleData(int index, byte[] data)
        {
            int packetnum;
            Packet Packet;
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            packetnum = buffer.ReadInteger();
            buffer = null;

            if (packetnum == 0)
                return;

            if (Packets.TryGetValue(packetnum, out Packet))
            {
                Packet.Invoke(index, data);
            }
            
           // Program.BroadcastConsole(ConsoleType.DEBUG, "Packet "+packetnum+" received from " + index);
        }

        void HandleNewAccount(int index, byte[]data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetNum = buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();

            Datas.database.AddAccount(username,password);
        }

        void HandleLogin(int index, byte[]data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetNum = buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();

            if (!Datas.database.AccountExist(index,username))
            {
                //SendUserNotExists
                return;
            }

            if(!Datas.database.PasswordOK(index,username, password))
            {
                //SendPasswordwasNotCorrect
                return;
            }

            Console.WriteLine("Player " + username + " logged in succesfully.");
            //SendPlayerIntoTheGame
        }

      
    }
}
