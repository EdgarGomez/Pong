using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    private float playerSpeed = 10.0f;
    private int gameMode = 0;
    [SerializeField] GameObject ball;

    void Start()
    {
        gameMode = GameManager.Instance.gameMode;
        Debug.Log(gameMode);
    }

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
            if (gameMode == 0)
            {
                transform.Translate(0f, bluePlayerMovement * playerSpeed * Time.deltaTime, 0f);
            }
            else
            {
                PlayAI();
            }
        }
    }

    private void PlayAI()
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 ballPosition = ball.transform.position;
        float distance = Mathf.Abs(ballPosition.y - playerPosition.y);

        if (distance >= 2)
        {
            Vector2 newVelocity = new Vector2(0, 0);
            if (ballPosition.y > playerPosition.y)
            {
                newVelocity.y = 1;
            }
            else if (ballPosition.y < playerPosition.y)
            {
                newVelocity.y = -1;
            }
            newVelocity *= playerSpeed;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
    }
}
