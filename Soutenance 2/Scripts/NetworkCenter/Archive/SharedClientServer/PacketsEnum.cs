namespace Script.NetworkCenter
{
    public enum ServerPackets
    {
        SAlertMsg = 1,
        SPlayerOwnDatas = 2,
        SPlayerDatas = 3,
        SPlayerMovement = 4
    }
    
    public enum ClientPackets
    {
        CPing = 1,
        CHandleMovement = 4
    }
}