using Mirror;
using Steamworks;

public class LobbyManager : NetworkBehaviour
{
    public void OpenFriendList() => SteamFriends.ActivateGameOverlay("friends");

    public void LeaveLobby()
    {
        SteamMatchmaking.LeaveLobby(SteamLobby.LobbyId);
        if (isServer)
        {           
            SteamMatchmaking.LeaveLobby(SteamLobby.LobbyId);
            MyNetworkManager.singleton.StopHost();
        }
        else
        {
            SteamMatchmaking.LeaveLobby(SteamLobby.LobbyId);
            MyNetworkManager.singleton.StopClient();
        }          
    }
}
