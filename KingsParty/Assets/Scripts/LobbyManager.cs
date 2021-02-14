using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private RoomManager manager;
    void Start()
    {
        manager = FindObjectOfType<RoomManager>();
    }

    public void BackToMainMenu()
    {
        manager.StopHost();
    }
}
