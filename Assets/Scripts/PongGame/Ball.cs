using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bluePlayerGoalsText;
    [SerializeField]
    private TextMeshProUGUI redPlayerGoalsText;
    [SerializeField]
    private TextMeshProUGUI winnerMessage;
    [SerializeField]
    private TextMeshProUGUI optionsMessage;
    [SerializeField]
    private ParticleSystem blueParticles;
    [SerializeField]
    private ParticleSystem redParticles;
    private float speed = 10f;
    private Rigidbody2D rb;
    private int bluePlayerGoalsCounter = 0;
    private int redPlayerGoalsCounter = 0;
    private int maxGoalsToWin = 2;
    private Color32 blueColor = new Color32(29, 67, 243, 255);
    private Color32 redColor = new Color32(204, 0, 0, 255);

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
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }

    void GoBall()
    {
        float rand = Random.Range(0, 4);
        switch (rand)
        {
            case 1:
                rb.velocity = (-Vector2.up + Vector2.right).normalized * speed;
                break;
            case 2:
                rb.velocity = (Vector2.up + -Vector2.right).normalized * speed;
                break;
            case 3:
                rb.velocity = (Vector2.up + Vector2.right).normalized * speed;
                break;
            case 4:
                rb.velocity = (-Vector2.up + -Vector2.right).normalized * speed;
                break;
            default:
                rb.velocity = (-Vector2.up + Vector2.right).normalized * speed;
                break;
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
            blueParticles.Play();
            bluePlayerGoalsCounter = bluePlayerGoalsCounter + 1;
            bluePlayerGoalsText.text = (bluePlayerGoalsCounter).ToString();
            RestartGame();
        }

        if (other.CompareTag("RightGoalLine"))
        {
            redParticles.Play();
            redPlayerGoalsCounter = redPlayerGoalsCounter + 1;
            redPlayerGoalsText.text = (redPlayerGoalsCounter).ToString();
            RestartGame();
        }
    }
}
