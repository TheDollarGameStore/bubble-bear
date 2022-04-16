using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    private bool falling;

    private bool matching;

    private Vector2 impactPos;

    private List<Orb> orbs;

    [SerializeField]
    private AudioClip pop;

    [SerializeField]
    private AudioClip impact;

    [SerializeField]
    private AudioClip spawn;

    [SerializeField]
    private AudioClip woosh;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Fall", 0.6f);
        SoundManager.instance.PlayRandomized(spawn);
    }

    // Update is called once per frame
    void Fall()
    {
        SoundManager.instance.PlayRandomized(woosh);
        falling = true;
        transform.parent = null;
        impactPos = new Vector2(transform.position.x, 64f);
    }

    private void Update()
    {
        if (falling)
        {
            transform.position = Vector2.MoveTowards(transform.position, impactPos, 100f * Time.deltaTime);
        }

        if (!matching && (Vector2)transform.position == impactPos)
        {
            SoundManager.instance.PlayRandomized(impact);
            //Start Cascade
            falling = false;
            matching = true;

            Camera.main.transform.GetComponent<CameraBehaviour>().Bump(5f);
            Camera.main.transform.GetComponent<CameraBehaviour>().Shake(0.5f);

            orbs = GameManager.instance.SeparateColumn();
            transform.parent = orbs[0].transform;
            Invoke("CheckMatch", 0.6f);
        }
    }

    private void CheckMatch()
    {
        int index = 0;
        bool madeMatch = false;
        while (index < orbs.Count - 1)
        {
            if (orbs[index].color == orbs[index + 1].color)
            {
                madeMatch = true;
                for (int i = index; i >= 0; i--)
                {
                    orbs[i].targetY -= 16f;
                }

                orbs[index].IncreaseValue();
                orbs[index + 1].Invoke("DestroySelf", 0.3f);
                orbs.RemoveAt(index + 1);
                Invoke("CheckMatch", 0.3f);
                Camera.main.transform.GetComponent<CameraBehaviour>().Bump(1.5f);
                break;
            }

            index++;
        }

        if (!madeMatch)
        {
            Invoke("PopAll", 0.45f);
        }
    }

    private void PopAll()
    {
        SoundManager.instance.PlayRandomized(pop);
        Camera.main.transform.GetComponent<CameraBehaviour>().Shake(1.5f);
        for (int i = 0; i < orbs.Count; i++)
        {
            orbs[i].Pop();
        }
        GameManager.instance.Invoke("RefillAll", 0.8f);
        GameManager.instance.ResetTimer();
        Destroy(gameObject);
    }
}
