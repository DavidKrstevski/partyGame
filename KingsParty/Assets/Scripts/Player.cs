using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float speed;
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
}
