using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine.Rendering.Universal;

namespace Script.NetworkCenter
{
    internal static class NetworkReceive
    {
        internal static void PacketRouter()
        {
        //    Network.socket.PacketId[(int) ServerPackets.SAlertMsg] = new Client.DataArgs(Packet_AlertMsg);
       //     Network.socket.PacketId[(int) ServerPackets.SPlayerOwnDatas] = new Client.DataArgs(HandleOwnPlayerDatas);
        //    Network.socket.PacketId[(int) ServerPackets.SPlayerDatas] = new Client.DataArgs(HandlePlayerDatas);
        //    Network.socket.PacketId[(int) ServerPackets.SPlayerMovement] = new Client.DataArgs(HandleMovement);
        }

        private static void Packet_AlertMsg(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            
            string AlertMsg = buffer.ReadString();

            Debug.Log(AlertMsg);
            
            buffer.Dispose();
        }
        
        public static void HandleOwnPlayerDatas(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            
            int ID = buffer.ReadInteger();
        
            Player p = Player.GetPlayerFromEntityID(ID);
        
            p.EntityID = ID;
        
            p.posX = buffer.ReadFloat();
            p.posY = buffer.ReadFloat();
            p.posZ = buffer.ReadFloat();

            if (p.obj != null)
                Object.Destroy(p.obj);
        
            p.obj = Object.Instantiate(NetworkManager.instance.playerPrefab, new Vector3(p.posX, p.posY, p.posZ), Quaternion.identity);
           // p.obj.GetComponent<PlayerMovementInputController>().EntityID = ID;
            p.obj.name = "Player: " + ID;

            NetworkManager.instance.MyEntityID = ID;
            
            buffer.Dispose();
        }
        
        public static void HandlePlayerDatas(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            
            int ID = buffer.ReadInteger();
        
            Player p = Player.GetPlayerFromEntityID(ID);
        
            p.EntityID = ID;
        
            p.posX = buffer.ReadFloat();
            p.posY = buffer.ReadFloat();
            p.posZ = buffer.ReadFloat();

            if (p.obj != null)
                Object.Destroy(p.obj);
        
            p.obj = Object.Instantiate(NetworkManager.instance.playerPrefab, new Vector3(p.posX, p.posY, p.posZ), Quaternion.identity);
         //   p.obj.GetComponent<PlayerMovementInputController>().EntityID = ID;
            p.obj.name = "Player: " + ID;
            p.obj.GetComponentInChildren<Camera>().enabled = false;
            p.obj.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            p.obj.GetComponentInChildren<CinemachineImpulseListener>().enabled = false;
            p.obj.GetComponentInChildren<CinemachineVolumeSettings>().enabled = false;
            p.obj.GetComponentInChildren<Camera>().enabled = false;
            p.obj.GetComponentInChildren<AudioListener>().enabled = false;
            p.obj.GetComponentInChildren<CinemachineBrain>().enabled = false;
            p.obj.GetComponentInChildren<UniversalAdditionalCameraData>().enabled = false;
           
            buffer.Dispose();
        }
        
            
        public static void HandleMovement(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);

            //Player Info
            int EntityID = buffer.ReadInteger();
            
            //Player Pos
            float x = buffer.ReadFloat();
            float y = buffer.ReadFloat();
            float z = buffer.ReadFloat();
        
            //Player Rotation
            float rotX = buffer.ReadFloat();
            float rotY = buffer.ReadFloat();
            float rotZ = buffer.ReadFloat();
            float rotW = buffer.ReadFloat();
        
            //Player Animation
            float horizontalPress = buffer.ReadFloat();
            float verticalPress = buffer.ReadFloat();
            float mouseX = buffer.ReadFloat();
            float mouseY = buffer.ReadFloat();
            bool speed = buffer.ReadBool();
            bool spacePress = buffer.ReadBool();

            Player p = Player.GetPlayerFromEntityID(EntityID);
            GameObject obj = p.obj;
        
            if ((System.Object) obj != null)
            {
              // p.obj.GetComponent<PlayerMovementInputController>().MoveTo(new Vector3(x,y,z), new Quaternion(rotX, rotY, rotZ, rotW), horizontalPress, verticalPress, mouseX, mouseY, speed, spacePress);
            }
            
            buffer.Dispose();
        }
    }
}