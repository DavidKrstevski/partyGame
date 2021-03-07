using Mirror;
using Steamworks;

public class LobbyManager : NetworkBehaviour
{
    private MyNetworkManager networkManager;
    private void Start()
    {
        networkManager = FindObjectOfType<MyNetworkManager>();
    }

    public void OpenFriendList() => SteamFriends.ActivateGameOverlay("friends");

    public void LeaveLobby()
    {
        SteamMatchmaking.LeaveLobby(SteamLobby.LobbyId);
        if (isServer)
            networkManager.StopHost();
        else
            networkManager.StopClient();
    }
}
