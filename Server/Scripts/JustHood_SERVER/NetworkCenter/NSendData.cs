using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustHood_SERVER.NetworkCenter
{
    class NetworkSendData
    {
        public void SendDataTo(int index, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            byte[] array = buffer.ToArray();
            
            Datas.GetClientFromIndex(index).myStream.BeginWrite(array, 0, array.Length, null, null);
           // Program.BroadcastConsole(ConsoleType.DEBUG, "Packet "+buffer.ReadInteger()+" sended to " + index);
        }

        public void SendDataToAll(byte[]data)
        {
            for(int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if(Datas.Clients[i].Socket != null)
                {
                    SendDataTo(i, data);
                }
            }
        }

        public void SendDataToAllBut(int index, byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Datas.Clients[i].Socket != null)
                {
                    if (i != index)
                    {
                        SendDataTo(i, data);
                    }
                }
            }
        }

        public void SendAlertMsg(int index,string alertMsg)
        {
            ByteBuffer buffer = new ByteBuffer();

            buffer.WriteInteger((int) ServerPackets.SAlertMsg);
            buffer.WriteString(alertMsg);

            SendDataTo(index, buffer.ToArray());
        }

        public async void SendAllDatas(int index)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                NetworkClient client2 = Datas.Clients[i];
                if (i != index && client2 != null && client2.Socket != null)
                {
                    await Task.Delay(25);
                    SendDataTo(index, client2.ToDatas());
                    Program.BroadcastConsole(ConsoleType.DEBUG, "Player '"+i+" init to player "+index);
                }
            }  
        }
        
        public void SendPlayerIntoTheGame(int index)
        {
            NetworkClient client = Datas.GetClientFromIndex(index);
            SendDataToAll(client.ToDatas());
            
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                NetworkClient client2 = Datas.Clients[i];
                if (i != index && client2 != null && client2.Socket != null)
                {
                    SendDataTo(index, client2.ToDatas());
                    Program.BroadcastConsole(ConsoleType.DEBUG, "Player '"+i+" init to player "+index);
                }
            }

            //Program.BroadcastConsole(ConsoleType.DEBUG, "Player '"+index+"' has started playing.");
        }
        
        public void SendPlayerMovement(int index, float x,float y,float z,float rotX,float rotY,float rotZ,float rotW)
        {
            ByteBuffer buffer = new ByteBuffer();
            
            buffer.WriteInteger((int) ServerPackets.SPlayerMovement);
            
            //player info
            buffer.WriteInteger(index);
            
            //player position
            buffer.WriteFloat(x);
            buffer.WriteFloat(y);
            buffer.WriteFloat(z);
            
            //player rotation
            buffer.WriteFloat(rotX);
            buffer.WriteFloat(rotY);
            buffer.WriteFloat(rotZ);
            buffer.WriteFloat(rotW);
            
            SendDataToAllBut(index, buffer.ToArray());
 
            buffer = null;
        }
        
    }
}
