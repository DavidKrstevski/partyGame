using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaveLobbyButton : MonoBehaviour
{
    private void Start()
    {
        SteamLobby steamLobby = FindObjectOfType<SteamLobby>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(steamLobby.LeaveLobby);
    }
}
