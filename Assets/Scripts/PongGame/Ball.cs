using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rb;
    public TextMeshProUGUI bluePlayerGoalsText;
    public TextMeshProUGUI redPlayerGoalsText;
    public TextMeshProUGUI winnerMessage;
    public TextMeshProUGUI optionsMessage;
    int bluePlayerGoalsCounter = 0;
    int redPlayerGoalsCounter = 0;
    int maxGoalsToWin = 2;
    Color32 blueColor = new Color32(29, 67, 243, 255);
    Color32 redColor = new Color32(204, 0, 0, 255);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 1);
    }

    void Update()
    {
        if (bluePlayerGoalsCounter == maxGoalsToWin)
        {
            Winner("Blue");
        }
        if (redPlayerGoalsCounter == maxGoalsToWin)
        {
            Winner("Red");
        }
    }

    void Winner(string color)
    {
        ResetBall();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        winnerMessage.enabled = true;
        optionsMessage.enabled = true;
        if (color == "Blue")
        {
            winnerMessage.faceColor = blueColor;
            winnerMessage.outlineColor = blueColor;
        }
        else
        {
            winnerMessage.faceColor = redColor;
            winnerMessage.outlineColor = redColor;
        }
        winnerMessage.text = color + " Player Wins";
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb.velocity = (-Vector2.up + Vector2.right).normalized * speed;
        }
        else
        {
            rb.velocity = (Vector2.up + -Vector2.right).normalized * speed;
        }
    }

    void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftGoalLine"))
        {
            bluePlayerGoalsCounter = bluePlayerGoalsCounter + 1;
            bluePlayerGoalsText.text = (bluePlayerGoalsCounter).ToString();
            ResetBall();
            RestartGame();
        }

        if (other.CompareTag("RightGoalLine"))
        {
            redPlayerGoalsCounter = redPlayerGoalsCounter + 1;
            redPlayerGoalsText.text = (redPlayerGoalsCounter).ToString();
            ResetBall();
            RestartGame();
        }
    }
}
