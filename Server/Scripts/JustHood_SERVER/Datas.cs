using System;
using JustHood_SERVER.NetworkCenter;
using JustHood_SERVER.DataBase;
using System.Collections.Generic;

namespace JustHood_SERVER
{
    class Datas
    {
        public static Datas instance = new Datas();

        //Global instances of classes.
        public static General general = new General();
        public static Network network = new Network();
        //public static Database database = new Database();
        //public static MySQL mysql = new MySQL();
        public static NetworkHandleData networkHandleData = new NetworkHandleData();
        public static NetworkSendData networkSendData = new NetworkSendData();
        public static NetworkClient[] Clients = new NetworkClient[Constants.MAX_PLAYERS];

        //
        public int Player_HighIndex = 1;
        public static Dictionary<Guid, Player> PlayerList = new Dictionary<Guid,Player>();

        public static NetworkClient GetClientFromIndex(int i)
        {
            return Clients[i];
        }
    }
}
