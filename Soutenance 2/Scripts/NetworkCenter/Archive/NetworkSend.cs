using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Script.NetworkCenter
{

    internal static class NetworkSend
    {

        public static void Ping()
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInteger((int) ClientPackets.CPing);
            buffer.WriteString("Ping");
            
         //   Network.socket.SendData(buffer.Data, buffer.Head);
            
            buffer.Dispose();
        }

        public static void Movement(Vector3 position, Quaternion rotation, float horizontalPress, float verticalPress, float mouseX, float mouseY, bool speed, bool spacePress)
        {
            ByteBuffer buffer = new ByteBuffer(4);

            buffer.WriteInteger((int) ClientPackets.CHandleMovement);

            //player position
            buffer.WriteFloat(position.x);
            buffer.WriteFloat(position.y);
            buffer.WriteFloat(position.z);

            //player rotation
            buffer.WriteFloat(rotation.x);
            buffer.WriteFloat(rotation.y);
            buffer.WriteFloat(rotation.z);
            buffer.WriteFloat(rotation.w);
        
            //player animation
            buffer.WriteFloat(horizontalPress);
            buffer.WriteFloat(verticalPress);
            buffer.WriteFloat(mouseX);
            buffer.WriteFloat(mouseY);
            buffer.WriteBoolean(speed);
            buffer.WriteBoolean(spacePress);

          //  Network.socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }
    }
}