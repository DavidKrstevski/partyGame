﻿using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteamLobby : MonoBehaviour
{
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<LobbyChatUpdate_t> playerLeftLobby;

    private const string HOST_ADDRESS_KEY = "HostAddress";

    public static CSteamID LobbyId { get; private set; }

    private void Start()
    {
        if (!SteamManager.Initialized)
            return;

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        playerLeftLobby = Callback<LobbyChatUpdate_t>.Create(OnPlayerLeftLobby);
    }

    public void HostLobby() => SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, MyNetworkManager.singleton.maxConnections);

    public void LeaveGame() => Application.Quit();

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
            return;

        LobbyId = new CSteamID(callback.m_ulSteamIDLobby);
        MyNetworkManager.singleton.StartHost();

        SteamMatchmaking.SetLobbyData(LobbyId, HOST_ADDRESS_KEY, SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback) => SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active)
            return;

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HOST_ADDRESS_KEY);
        MyNetworkManager.singleton.networkAddress = hostAddress;
        MyNetworkManager.singleton.StartClient();
    }

    private void OnPlayerLeftLobby(LobbyChatUpdate_t callback)
    {
        var cSteamId = new CSteamID(callback.m_ulSteamIDUserChanged);

        if (cSteamId == SteamMatchmaking.GetLobbyOwner(LobbyId))
            SteamMatchmaking.LeaveLobby(LobbyId);
    }
}
