using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private LobbyManager manager;
    [SerializeField]
    private TMP_InputField playernameInputField;
    [SerializeField]
    private TMP_InputField ipAddressInputField;

    [SerializeField]
    private GameObject hostButton;
    [SerializeField]
    private GameObject joinButton;
    [SerializeField]
    private GameObject cancleButton;


    private void Start()
    {
        LobbyManager.OnClientCon += ClientConnected;
    }

    public void HostGame()
    {
        if (!CheckIfNameIsValid())
            return;

        manager.StartHost();
        cancleButton.SetActive(true);
        joinButton.SetActive(false);
        hostButton.SetActive(false);

    }

    public void JoinGame()
    {
        if (!CheckIfNameIsValid())
            return;

        manager.networkAddress = ipAddressInputField.text;
        manager.StartClient();
        joinButton.SetActive(false);
        cancleButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CancleConnection()
    {
        manager.StopClient();
        cancleButton.SetActive(false);
        hostButton.SetActive(true);
        joinButton.SetActive(true);
    }

    private bool CheckIfNameIsValid()
    {
        if (string.IsNullOrEmpty(playernameInputField.text)){
            return false;
        }
        return true;
    }

    private void ClientConnected(object sender, EventArgs e)
    {
        manager.ServerChangeScene("LobbyScene");
    }

}
