using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleSteamIdUpdate))]
    private ulong steamId;

    [SerializeField]
    private float speed;
    [SerializeField]
    private TMP_Text username;


    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!isLocalPlayer)
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        gameObject.transform.position = new Vector2(transform.position.x + (h * speed),
           transform.position.y + (v * speed));
    }

    #region Server

    public void SetSteamId(ulong steamId)
    {
        this.steamId = steamId;
    }

    #endregion

    #region Client
    private void HandleSteamIdUpdate(ulong oldSteamId, ulong newSteamId)
    {
        var cSteamId = new CSteamID(newSteamId);
        username.text = SteamFriends.GetFriendPersonaName(cSteamId);
    }

    #endregion

}
