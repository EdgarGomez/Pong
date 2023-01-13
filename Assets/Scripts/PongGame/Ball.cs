using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rb;
    public TextMeshProUGUI bluePlayerGoalsText;
    public TextMeshProUGUI redPlayerGoalsText;
    int bluePlayerGoalsCounter = 1;
    int redPlayerGoalsCounter = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 1);
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
            bluePlayerGoalsText.text = (bluePlayerGoalsCounter++).ToString();
            ResetBall();
            RestartGame();
        }

        if (other.CompareTag("RightGoalLine"))
        {
            redPlayerGoalsText.text = (redPlayerGoalsCounter++).ToString();
            ResetBall();
            RestartGame();
        }
    }
}
