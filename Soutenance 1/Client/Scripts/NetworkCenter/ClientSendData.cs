using System;
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

    public void SendDataToServer(byte[] data)
    {
        try
        {
            if (Network.instance.myStream != null)
            {
                ByteBuffer buffer = new ByteBuffer();
                buffer.WriteBytes(data);

                Network.instance.myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
                buffer = null;
            }
        }
        catch (Exception _ex)
        {
            Debug.Log("Error sending data: " + _ex);
        }
    }

    public void SendMovement(float x, float y, float z, float rotX, float rotY, float rotZ, float rotW)
    {
        ByteBuffer buffer = new ByteBuffer();

        buffer.WriteInteger((int) ClientPackets.CHandleMovement);

        //player position
        buffer.WriteFloat(x);
        buffer.WriteFloat(y);
        buffer.WriteFloat(z);

        //player rotation
        buffer.WriteFloat(rotX);
        buffer.WriteFloat(rotY);
        buffer.WriteFloat(rotZ);
        buffer.WriteFloat(rotW);
        
        SendDataToServer(buffer.ToArray());
        
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
    }*/
}
