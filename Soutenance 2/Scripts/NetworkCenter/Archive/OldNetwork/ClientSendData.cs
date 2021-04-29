/*using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JustHood_SERVER.NetworkCenter;

public class ClientSendData : MonoBehaviour
{
    public static ClientSendData instance;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public void SendDataToServer(ByteBuffer buffer)
    {
        Network._client.Send(buffer.ToArray());
    }

    public void SendMovement(Vector3 position, Quaternion rotation, float horizontalPress, float verticalPress, bool spacePress)
    {
        ByteBuffer buffer = new ByteBuffer();

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
        buffer.WriteBoolean(spacePress);

        SendDataToServer(buffer);
        
    }

    /*
    public void SendNewAccount()
    {
        ByteBuffer buffer = new ByteBuffer();

        buffer.WriteInteger(1);
        buffer.WriteString(_username.text);
        buffer.WriteString(_password.text);

        SendDataToServer(buffer.ToArray());
        buffer = null;
    }

    public void SendLogin()
    {
        ByteBuffer buffer = new ByteBuffer();
        
        buffer.WriteInteger(2);
        buffer.WriteString(_loginUser.text);
        buffer.WriteString(_loginPass.text);

        SendDataToServer(buffer.ToArray());
        buffer = null;
    }
}
*/