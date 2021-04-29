using System;
using JustHood_SERVER.NetworkCenter;

namespace JustHood_SERVER
{
    class General
    {
        public void InitServer()
        {
            //Datas.mysql.MySQLInit();
            InitGameData();
            Datas.network.InitTCP();
        }

        public void InitGameData()
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                Datas.Clients[i] = new NetworkClient();
            }
        }
    }
}
