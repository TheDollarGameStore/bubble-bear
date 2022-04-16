using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int weightsLeft;

    //For Next Weight
    private int requiredProgress; 
    private int currentProgress;

    [SerializeField]
    private List<Row> rows;

    private float gameSpeed = 1.25f;

    public static GameManager instance = null;

    public Player player;

    [HideInInspector]
    public bool paused;

    [HideInInspector]
    public int score;
    private int displayScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float timeLimit;
    private float currentTime;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI weightsText;

    [SerializeField]
    private Image progressCircle;

    [SerializeField]
    private GameObject addWeightsPrefab;

    [SerializeField]
    private AudioClip newWeightSound;

    [SerializeField]
    private AudioClip gameOverSound;

    [SerializeField]
    private GameObject gameOverScreen;

    private void FixedUpdate()
    {
        if (displayScore != score)
        {
            displayScore += 25;
            scoreText.text = displayScore.ToString();
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void SubtractWeight()
    {
        weightsLeft -= 1;
        weightsText.text = weightsLeft.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        requiredProgress = 3000;
        currentProgress = 0;
        weightsLeft = 1;
        weightsText.text = weightsLeft.ToString();
        timeLimit = 10f;
        currentTime = timeLimit;
        Invoke("Shift", gameSpeed);
    }

    private void Update()
    {
        if (!paused)
        {
            currentTime = Mathf.Max(currentTime - Time.deltaTime, 0f);

            timeText.text = currentTime.ToString("F1");

            if (currentTime == 0f)
            {
                player.MakeMove();
            }
        }

        progressCircle.fillAmount = Mathf.Lerp(progressCircle.fillAmount, (float)currentProgress / (float)requiredProgress, 10f * Time.deltaTime);
    }

    void Shift()
    {
        Invoke("Shift", gameSpeed);
        for (int i = 0; i < rows.Count; i++)
        {
            rows[i].Shift();
        }
    }

    public void RandomizeDirections()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            rows[i].RandomizeDirection();
        }
    }

    public void Pause()
    {
        paused = true;
        CancelInvoke("Shift");
    }

    public void Resume()
    {
        if (weightsLeft != 0)
        {
            paused = false;
            Invoke("Shift", gameSpeed);
        }
        else
        {
            Invoke("ShowGameOver", 1.5f);
        }
    }

    void ShowGameOver()
    {
        SoundManager.instance.PlayRandomized(gameOverSound);
        gameOverScreen.SetActive(true);
    }

    public void AddScore(int amount)
    {
        score += amount;

        currentProgress += amount;

        if (currentProgress >= requiredProgress)
        {
            currentProgress -= requiredProgress;
            weightsLeft += 5;
            weightsText.text = weightsLeft.ToString();
            requiredProgress += 500;
            Instantiate(addWeightsPrefab, new Vector3(110f, 8f, 0f), Quaternion.identity).GetComponent<ScoreText>().score = 5;
            SoundManager.instance.PlayRandomized(newWeightSound);
        }
    }

    public List<Orb> SeparateColumn()
    {
        List<Orb> separatedOrbs = new List<Orb>();

        for (int i = 0; i < rows.Count; i++)
        {
            separatedOrbs.Add(rows[i].orbs[player.position]);
            separatedOrbs[separatedOrbs.Count - 1].inMatchingState = true;
            separatedOrbs[separatedOrbs.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = -10 - i;
            rows[i].orbs[player.position] = null;
        }

        return separatedOrbs;
    }

    public void RefillAll()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            rows[i].Refill();
        }
        RandomizeDirections();
        Resume();
    }
    
    public void ResetTimer()
    {
        currentTime = timeLimit;
    }
}
