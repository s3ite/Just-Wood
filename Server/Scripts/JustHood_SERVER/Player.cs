using System;
using JustHood_SERVER.NetworkCenter;

namespace JustHood_SERVER
{
    public class Player
    {
        private NetworkClient NClient;
        private Guid GUID;
        
        public Player(NetworkClient nClient, Guid guid)
        {
            NClient = nClient;
            GUID = guid;
        }

        public static Player getPlayerFromGUID(Guid guid)
        {
            return (Datas.PlayerList.ContainsKey(guid)) ? Datas.PlayerList[guid] : null;
        }
        
        public static Player getPlayerFromGUID2(Guid guid)
        {
            return null;
        }
    }
}