using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : NetworkBehaviour
{
    [SerializeField]
    private NetworkManager manager;
    public TMP_InputField playernameInputField;
    public static string inputName;
    [SerializeField]
    private TMP_InputField ipAddressInputField;
    [SerializeField]
    private GameObject joinPanel;

    [SerializeField]
    private Button hostButton;
    [SerializeField]
    private Button joinButton;

    public void HostGame()
    {
        if (!CheckIfNameIsValid())
            return;

        manager.StartHost();
        manager.ServerChangeScene("LobbyScene");
    }

    public void JoinGame()
    {
        manager.networkAddress = ipAddressInputField.text;
        manager.StartClient();
        joinButton.interactable = false;
        Invoke("ActivateJoinButton", 5);
    }

    public void ActivateJoinPanel()
    {
        if (!CheckIfNameIsValid())
            return;

        joinPanel.SetActive(true);
    }

    public void DeactivateJoinPanel() => joinPanel.SetActive(false);

    public void ExitGame() => Application.Quit();

    private void ActivateJoinButton() => joinButton.interactable = true;

    private bool CheckIfNameIsValid()
    {
        if (string.IsNullOrEmpty(playernameInputField.text))
            return false;

        inputName = playernameInputField.text;
        return true;
    }
}
