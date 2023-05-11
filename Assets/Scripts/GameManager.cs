using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int paddle1Color = 0;
    public int paddle2Color = 0;
    public int gameMode = 0;
    public int numberGoals = 0;
    public float difficultyLevel = 0f;
    public AudioSource player1Sound;
    public AudioSource player2Sound;
    [Header("Colors")]
    [SerializeField] private Color32 blueColor = new Color32(29, 67, 243, 255);
    [SerializeField] private Color32 redColor = new Color32(204, 0, 0, 255);
    [SerializeField] private Color32 greenColor = new Color32(0, 173, 24, 255);
    [SerializeField] private Color32 yellowColor = new Color32(234, 251, 39, 255);
    [SerializeField] private Color32 pinkColor = new Color32(241, 0, 235, 255);
    [SerializeField] private Color32 whiteColor = new Color32(255, 255, 255, 255);
    [SerializeField] private GameObject Paddle1Paddel;
    [SerializeField] private GameObject Paddle2Paddel;

    [SerializeField] private TextMeshProUGUI winnerMessage;
    [SerializeField] private TextMeshProUGUI optionsMessage;

    public SpriteRenderer LeftPlayer;
    public SpriteRenderer RightPlayer;
    private Ball ballScript;
    public GameObject ballObject;
    public SpriteRenderer leftGoal;
    public SpriteRenderer rightGoal;
    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        SetGameConfig();
    }

    void Start()
    {
        LeftPlayer = Paddle1Paddel.GetComponent<SpriteRenderer>();
        RightPlayer = Paddle2Paddel.GetComponent<SpriteRenderer>();
        ballObject = GameObject.FindGameObjectWithTag("Ball");
        ballScript = ballObject.GetComponent<Ball>();
    }

    void SetGameConfig()
    {
        gameMode = PlayerPrefs.HasKey("GameMode") ? PlayerPrefs.GetInt("GameMode") : 0;
        var mainRight = rightParticles.main;
        var mainLeft = leftParticles.main;
        switch (PlayerPrefs.GetInt("NumberGoals"))
        {
            case 0:
                numberGoals = 2;
                break;
            case 1:
                numberGoals = 5;
                break;
            case 2:
                numberGoals = 10;
                break;
            default:
                numberGoals = 2;
                break;
        }
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 0:
                difficultyLevel = 10f;
                break;
            case 1:
                difficultyLevel = 15f;
                break;
            case 2:
                difficultyLevel = 20f;
                break;
            default:
                difficultyLevel = 10f;
                break;
        }
        switch (PlayerPrefs.GetInt("Paddle1Color"))
        {
            case 0:
                LeftPlayer.color = redColor;
                rightGoal.color = redColor;
                mainLeft.startColor = Color.red;
                break;
            case 1:
                LeftPlayer.color = blueColor;
                rightGoal.color = blueColor;
                mainLeft.startColor = Color.blue;
                break;
            case 2:
                LeftPlayer.color = yellowColor;
                rightGoal.color = yellowColor;
                mainLeft.startColor = Color.yellow;
                break;
            case 3:
                LeftPlayer.color = greenColor;
                rightGoal.color = greenColor;
                mainLeft.startColor = Color.green;
                break;
            case 4:
                LeftPlayer.color = whiteColor;
                rightGoal.color = whiteColor;
                mainLeft.startColor = Color.white;
                break;
            case 5:
                LeftPlayer.color = pinkColor;
                rightGoal.color = pinkColor;
                mainLeft.startColor = Color.black;
                break;
            default:
                LeftPlayer.color = redColor;
                rightGoal.color = redColor;
                mainLeft.startColor = Color.red;
                break;
        }
        switch (PlayerPrefs.GetInt("Paddle2Color"))
        {
            case 0:
                RightPlayer.color = redColor;
                leftGoal.color = redColor;
                mainRight.startColor = Color.red;
                break;
            case 1:
                RightPlayer.color = blueColor;
                leftGoal.color = blueColor;
                mainRight.startColor = Color.blue;
                break;
            case 2:
                RightPlayer.color = yellowColor;
                leftGoal.color = yellowColor;
                mainRight.startColor = Color.yellow;
                break;
            case 3:
                RightPlayer.color = greenColor;
                leftGoal.color = greenColor;
                mainRight.startColor = Color.green;
                break;
            case 4:
                RightPlayer.color = whiteColor;
                leftGoal.color = whiteColor;
                mainRight.startColor = Color.white;
                break;
            case 5:
                RightPlayer.color = pinkColor;
                leftGoal.color = pinkColor;
                mainRight.startColor = Color.black;
                break;
            default:
                RightPlayer.color = blueColor;
                leftGoal.color = blueColor;
                mainRight.startColor = Color.blue;
                break;
        }
    }

    public void DisplayWinner(bool player)
    {
        ballScript.ResetBall();
        ballObject.GetComponent<SpriteRenderer>().enabled = false;
        winnerMessage.enabled = true;
        optionsMessage.enabled = true;
        winnerMessage.faceColor = player ? LeftPlayer.color : RightPlayer.color;
        winnerMessage.outlineColor = winnerMessage.faceColor;
        winnerMessage.text = player ? "Paddle 1 Wins" : "Paddle 2 Wins";
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
}
