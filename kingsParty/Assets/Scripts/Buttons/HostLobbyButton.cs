using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HostLobbyButton : MonoBehaviour
{
    private void Start()
    {
        SteamLobby steamLobby = FindObjectOfType<SteamLobby>();
        Debug.Log("Steam Lobby: " + steamLobby);
        Button button = GetComponent<Button>();
        Debug.Log("Button: " + button);
        button.onClick.AddListener(steamLobby.HostLobby);
    }
}
