using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : NetworkManager
{
    public static event EventHandler OnClientCon;

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientCon?.Invoke(this, EventArgs.Empty);
    }

}
