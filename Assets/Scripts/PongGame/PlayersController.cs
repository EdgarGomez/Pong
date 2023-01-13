using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    private float playerSpeed = 10.0f;

    void Update()
    {
        float redPlayerMovement = Input.GetAxis("Vertical");
        float bluePlayerMovement = Input.GetAxis("Debug Vertical");
        if (gameObject.tag == "RedPlayer")
        {
            transform.Translate(0f, redPlayerMovement * playerSpeed * Time.deltaTime, 0f);
        }
        else
        {
            transform.Translate(0f, bluePlayerMovement * playerSpeed * Time.deltaTime, 0f);
        }

    }
}
