using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 defaultPos;

    private float shakeAmount;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, defaultPos, 10f * Time.deltaTime);

        transform.position = transform.position + (Vector3)(new Vector2(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount)));

        shakeAmount = Mathf.Max(shakeAmount - (3f * Time.deltaTime), 0f);
    }

    public void Bump(float amount)
    {
        transform.position = transform.position + (Vector3)(Vector2.up * amount);
    }

    public void Shake(float intensity)
    {
        shakeAmount = intensity;
    }
}
