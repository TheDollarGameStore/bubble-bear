using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.newHighscore)
        {
            highscoreText.text = "Previous Best:\n" + PlayerPrefs.GetInt("Highscore", 0).ToString();
        }

        scoreText.text = GameManager.instance.score.ToString();
    }
}
