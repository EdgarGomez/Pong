using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI paddle2PlayerGoalsText;
    [SerializeField] private TextMeshProUGUI paddle1PlayerGoalsText;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip wallsAudio;
    [SerializeField] private AudioClip goalAudio;
    [SerializeField] private AudioClip paddelsAudio;
    [Header("Particles")]
    [SerializeField] private ParticleSystem paddle2Particles;
    [SerializeField] private ParticleSystem paddle1Particles;
    [SerializeField] private ParticleSystem ballParticles;

    [SerializeField] private GameObject Paddle1Paddel;
    [SerializeField] private GameObject Paddle2Paddel;

    // Components
    private Rigidbody2D rb;
    private AudioSource gameSounds;
    // Variables
    private int paddle2PlayerGoalsCounter = 0;
    private int paddle1PlayerGoalsCounter = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameSounds = GetComponent<AudioSource>();

        Invoke("GoBall", 1);
    }

    private void Update()
    {
        CheckForWinning();
    }

    private void CheckForWinning()
    {
        if (paddle2PlayerGoalsCounter == GameManager.Instance.numberGoals)
        {
            GameManager.Instance.DisplayWinner(false);
        }
        if (paddle1PlayerGoalsCounter == GameManager.Instance.numberGoals)
        {
            GameManager.Instance.DisplayWinner(true);
        }
    }

    void GoBall()
    {
        Vector2[] directions = { Vector2.up + Vector2.right, Vector2.up + -Vector2.right, -Vector2.up + Vector2.right, -Vector2.up + -Vector2.right };
        int rand = Random.Range(0, directions.Length);
        rb.velocity = directions[rand].normalized * GameManager.Instance.difficultyLevel;
    }

    public void ResetBall()
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
            ref (other.tag == "LeftGoalLine" ? ref paddle2PlayerGoalsCounter : ref paddle1PlayerGoalsCounter),
            other.tag == "LeftGoalLine" ? paddle2PlayerGoalsText : paddle1PlayerGoalsText,
            other.tag == "LeftGoalLine" ? paddle2Particles : paddle1Particles
        );
    }

    private void HandleGoal(string team, ref int goalsCounter, TextMeshProUGUI goalsText, ParticleSystem particles)
    {
        particles.Play();
        goalsCounter++;
        goalsText.text = goalsCounter.ToString();
        gameSounds.PlayOneShot(goalAudio);
        if (gameObject.CompareTag("Ball"))
        {
            RestartGame();
        }
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

}
