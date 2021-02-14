using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float speed;
    public string playerName { get; private set; }
    private TMP_Text nameText;
    //private MenuManager menuManager;

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

    public void SetPlayerName()
    {
        //Fehler
        //playerName = menuManager.playernameInputField.text;
        nameText = GetComponentInChildren<TMP_Text>();
        playerName = MenuManager.inputName;
        Debug.Log("Playername: " + playerName);
        nameText.text = playerName;
    }
}
