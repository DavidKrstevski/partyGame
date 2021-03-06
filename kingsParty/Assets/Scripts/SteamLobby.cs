﻿using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamLobby : MonoBehaviour
{
    [SerializeField]
    private GameObject hostButton;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private const string HOST_ADDRESS_KEY = "HostAddress";

    NetworkManager networkManager;

    public static CSteamID LobbyId { get; private set; }

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized)
            return;

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        hostButton.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, networkManager.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            hostButton.SetActive(true);
            return;
        }

        LobbyId = new CSteamID(callback.m_ulSteamIDLobby);
        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(LobbyId, HOST_ADDRESS_KEY, SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active)
            return;

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HOST_ADDRESS_KEY);
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        hostButton.SetActive(false);
    }
}