using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 targetPos;

    private SpriteRenderer sr;

    [SerializeField]
    private GameObject weightPrefab;

    [HideInInspector]
    public int position;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        targetPos = transform.position;
        position = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, 10f * Time.deltaTime);

        if (GameManager.instance.paused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetPos.x > -32)
        {
            GameManager.instance.StartTimer();
            targetPos += Vector2.left * 16f;
            sr.flipX = true;
            position -= 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && targetPos.x < 32)
        {
            GameManager.instance.StartTimer();
            targetPos += Vector2.right * 16f;
            sr.flipX = false;
            position += 1;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.instance.StartTimer();
            MakeMove();
        }
    }

    public void MakeMove()
    {
        GameManager.instance.SubtractWeight();
        GameObject weight = Instantiate(weightPrefab, transform.position, Quaternion.identity);
        weight.transform.parent = transform;
        GameManager.instance.Pause();
    }
}
