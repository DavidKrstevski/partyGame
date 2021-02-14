using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : NetworkManager
{
    private static RoomManager instance;

    public static RoomManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoomManager>();
            }

            return instance;
        }
    }

    public List<Player> playerList = new List<Player>();

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        var player = conn.identity.GetComponent<Player>();
        playerList.Remove(player);
        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        var player = conn.identity.GetComponent<Player>();
        playerList.Add(player);
        player.SetPlayerName();
    }
}
