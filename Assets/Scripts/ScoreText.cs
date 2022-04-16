using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [HideInInspector]
    public int score;

    private float alpha;

    [SerializeField]
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        alpha = 1f;
        scoreText.text = "+" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3)(direction * Time.deltaTime);

        if (alpha >= 0f)
        {
            alpha -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        scoreText.color = new Color(1f, 1f, 1f, alpha);
    }
}
