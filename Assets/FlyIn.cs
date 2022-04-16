using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour
{
    private Vector2 targetPos;

    [SerializeField]
    private Vector2 offset;

    private bool flying;

    // Start is called before the first frame update
    void Start()
    {
        flying = false;
        targetPos = transform.position;
        transform.position += (Vector3)offset;
        Invoke("StartFly", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (flying)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, 10f * Time.deltaTime);
        }
    }

    void StartFly()
    {
        flying = true;
    }
}
