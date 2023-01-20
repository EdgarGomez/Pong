using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI bluePlayerGoalsText;
    [SerializeField] private TextMeshProUGUI redPlayerGoalsText;
    [SerializeField] private TextMeshProUGUI winnerMessage;
    [SerializeField] private TextMeshProUGUI optionsMessage;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip wallsAudio;
    [SerializeField] private AudioClip goalAudio;
    [SerializeField] private AudioClip paddelsAudio;
    [Header("Particles")]
    [SerializeField] private ParticleSystem blueParticles;
    [SerializeField] private ParticleSystem redParticles;
    [SerializeField] private ParticleSystem ballParticles;
    [Header("Game Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int maxGoalsToWin = 2;
    [Header("Colors")]
    [SerializeField] private Color32 blueColor = new Color32(29, 67, 243, 255);
    [SerializeField] private Color32 redColor = new Color32(204, 0, 0, 255);
    [SerializeField] private Color32 greenColor = new Color32(0, 173, 24, 255);
    [SerializeField] private Color32 yellowColor = new Color32(234, 251, 39, 255);
    [SerializeField] private Color32 pinkColor = new Color32(241, 0, 235, 255);
    [SerializeField] private Color32 whiteColor = new Color32(255, 255, 255, 255);
    [SerializeField] private GameObject LeftPaddel;
    [SerializeField] private GameObject RightPaddel;
    // Components
    private Rigidbody2D rb;
    private AudioSource gameSounds;
    // Variables
    private int bluePlayerGoalsCounter = 0;
    private int redPlayerGoalsCounter = 0;

    [SerializeField] RectTransform optionsPanel;


    private void Start()
    {
        if (PlayerPrefs.HasKey("SoundOn"))
        {
            SetInitialPlayerPrefs();
        }
        rb = GetComponent<Rigidbody2D>();
        gameSounds = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckForWinning();
    }

    private void CheckForWinning()
    {
        if (bluePlayerGoalsCounter == maxGoalsToWin)
        {
            DisplayWinner("Blue");
        }
        if (redPlayerGoalsCounter == maxGoalsToWin)
        {
            DisplayWinner("Red");
        }
    }

    private void DisplayWinner(string color)
    {
        ResetBall();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        winnerMessage.enabled = true;
        optionsMessage.enabled = true;
        winnerMessage.faceColor = color == "Blue" ? blueColor : redColor;
        winnerMessage.outlineColor = winnerMessage.faceColor;
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
        Vector2[] directions = { Vector2.up + Vector2.right, Vector2.up + -Vector2.right, -Vector2.up + Vector2.right, -Vector2.up + -Vector2.right };
        int rand = Random.Range(0, directions.Length);
        rb.velocity = directions[rand].normalized * speed;
    }

    private void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    private void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleGoal(
            other.tag == "LeftGoalLine" ? "blue" : "red",
            ref (other.tag == "LeftGoalLine" ? ref bluePlayerGoalsCounter : ref redPlayerGoalsCounter),
            other.tag == "LeftGoalLine" ? bluePlayerGoalsText : redPlayerGoalsText,
            other.tag == "LeftGoalLine" ? blueParticles : redParticles
        );
    }

    private void HandleGoal(string team, ref int goalsCounter, TextMeshProUGUI goalsText, ParticleSystem particles)
    {
        gameSounds.PlayOneShot(goalAudio);
        particles.Play();
        goalsCounter++;
        goalsText.text = goalsCounter.ToString();
        RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.gameObject.tag;
        if (tag == "TopBottomWalls" || tag == "BluePlayer" || tag == "RedPlayer")
        {
            gameSounds.PlayOneShot(tag == "TopBottomWalls" ? wallsAudio : paddelsAudio);
            ballParticles.Play();
        }
    }

    private void SetInitialPlayerPrefs()
    {
        PlayerPrefs.SetInt("GameMode", 0);
        PlayerPrefs.SetInt("VictoryGoals", 0);
        PlayerPrefs.SetInt("Difficulty", 0);
        PlayerPrefs.SetInt("SoundOn", 0);
        PlayerPrefs.SetInt("PowerOn", 0);
        PlayerPrefs.SetInt("Player1Color", 0);
        PlayerPrefs.SetInt("Player2Color", 1);
    }

    public void SaveVictoryGoals(int value)
    {
        PlayerPrefs.SetInt("VictoryGoals", value);
    }
    public void SaveGameMode(int value)
    {
        PlayerPrefs.SetInt("GameMode", value);
    }
    public void SaveDifficulty(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
    }
    public void SaveSoundOn(int value)
    {
        PlayerPrefs.SetInt("SoundOn", value);
    }
    public void SavePowerOn(int value)
    {
        PlayerPrefs.SetInt("PowerOn", value);
    }
    public void SavePlayer1Color(int value)
    {
        PlayerPrefs.SetInt("Player1Color", value);
    }
    public void SavePlayer2Color(int value)
    {
        PlayerPrefs.SetInt("Player2Color", value);
    }
    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SpriteRenderer LeftPlayer = LeftPaddel.GetComponent<SpriteRenderer>();
        SpriteRenderer RightPlayer = RightPaddel.GetComponent<SpriteRenderer>();

        switch (PlayerPrefs.GetInt("VictoryGoals"))
        {
            case 0:
                maxGoalsToWin = 2;
                break;
            case 1:
                maxGoalsToWin = 5;
                break;
            case 2:
                maxGoalsToWin = 10;
                break;
            default:
                maxGoalsToWin = 2;
                break;
        }
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 0:
                speed = 10f;
                break;
            case 1:
                speed = 15f;
                break;
            case 2:
                speed = 20f;
                break;
            default:
                speed = 10f;
                break;
        }
        switch (PlayerPrefs.GetInt("GameMode"))
        {
            case 0:

                break;
            case 1:

                break;
            default:

                break;
        }


        switch (PlayerPrefs.GetInt("Player1Color"))
        {
            case 0:
                LeftPlayer.color = redColor;
                break;
            case 1:
                LeftPlayer.color = blueColor;
                break;
            case 2:
                LeftPlayer.color = yellowColor;
                break;
            case 3:
                LeftPlayer.color = greenColor;
                break;
            case 4:
                LeftPlayer.color = whiteColor;
                break;
            case 5:
                LeftPlayer.color = pinkColor;
                break;
            default:
                LeftPlayer.color = redColor;
                break;
        }
        switch (PlayerPrefs.GetInt("Player2Color"))
        {
            case 0:
                RightPlayer.color = redColor;
                break;
            case 1:
                RightPlayer.color = blueColor;
                break;
            case 2:
                RightPlayer.color = yellowColor;
                break;
            case 3:
                RightPlayer.color = greenColor;
                break;
            case 4:
                RightPlayer.color = whiteColor;
                break;
            case 5:
                RightPlayer.color = pinkColor;
                break;
            default:
                RightPlayer.color = blueColor;
                break;
        }

        optionsPanel.localScale = new Vector3(0, 0, 0);
        Invoke("GoBall", 1);
    }
}
