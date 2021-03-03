using System;
using System.Net;
using System.Net.Sockets;
using JustHood_SERVER.Events;

namespace JustHood_SERVER.NetworkCenter
{
    public class Network
    {
        public TcpListener ServerSocket;

        public void InitTCP()
        {
            ServerSocket = new TcpListener(IPAddress.Any, 35565);
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);
            
            Program.BroadcastConsole(ConsoleType.INFO, "Query running on 37.187.9.46:35565 !");
        }

        void OnClientConnect(IAsyncResult result)
        {
            TcpClient netclient = ServerSocket.EndAcceptTcpClient(result);
            netclient.NoDelay = false;
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

            int i = Datas.instance.Player_HighIndex;
            
            NetworkClient client = Datas.Clients[i];
            client.Reset();
                    
            Datas.Clients[i].Socket = netclient;
            Datas.Clients[i].Index = i;
            Datas.Clients[i].IP = netclient.Client.RemoteEndPoint.ToString();
            Datas.Clients[i].Start();
            Program.BroadcastConsole(ConsoleType.INFO, "Incoming Connection from " + Datas.Clients[i].IP + " || Index: " + i);
                    
            onJoinDisconnectEvents.onJoin(i);
                    
            Datas.instance.Player_HighIndex++;
        }
    }
}
