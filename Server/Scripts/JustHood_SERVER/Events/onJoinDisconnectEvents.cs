using JustHood_SERVER.NetworkCenter;

namespace JustHood_SERVER.Events
{
    public class onJoinDisconnectEvents
    {
        public static void onJoin(int i)
        {
            Datas.networkSendData.SendPlayerIntoTheGame(i);
        }

        public static void onDisconnect(NetworkClient client)
        {
            Program.BroadcastConsole(ConsoleType.INFO, "Connection from " + client.IP + " got disconnect || Index was : " + client.Index);
        }
    }
}