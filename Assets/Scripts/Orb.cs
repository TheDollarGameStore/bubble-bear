using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orb : MonoBehaviour
{
    [HideInInspector]
    public bool inMatchingState;

    [HideInInspector]
    public float targetY;

    [SerializeField]
    private TextMeshProUGUI valueText;

    [SerializeField]
    private TextMeshProUGUI valueTextBG;

    [SerializeField]
    private GameObject popPrefab;

    [SerializeField]
    private GameObject scorePrefab;

    [SerializeField]
    private AudioClip merge;

    public enum OrbColor
    {
        Blue,
        Green,
        Red,
        Yellow
    }

    public OrbColor color;

    [HideInInspector]
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        targetY = transform.position.y;
        value = 1;
    }

    public void IncreaseValue()
    {
        SoundManager.instance.PlayPitched(merge, value);
        value++;
        valueText.text = value.ToString();
        valueTextBG.text = value.ToString();
    }

    private void Update()
    {
        if (inMatchingState)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, targetY), 20f * Time.deltaTime);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void Pop()
    {
        Instantiate(popPrefab, transform.position, Quaternion.identity);
        ScoreText score = Instantiate(scorePrefab, transform.position, Quaternion.identity).GetComponent<ScoreText>();
        score.score = GetScore();
        GameManager.instance.AddScore(GetScore());
        Destroy(gameObject);
    }

    private int GetScore()
    {
        switch(value)
        {
            case 1:
                return 100;
            case 2:
                return 250;
            case 3:
                return 500;
            case 4:
                return 800;
            case 5:
                return 1300;
            case 6:
                return 2000;
            case 7:
                return 3500;
            default:
                return 100;
        }
    }
}
