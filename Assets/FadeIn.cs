using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sr;
    private float alpha = 0f;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Min(alpha + (2f * Time.deltaTime), 0.65f);
        sr.color = new Color(1f, 1f, 1f, alpha);
    }
}
