using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public List<GameObject> orbPrefabs;

    [HideInInspector]
    public Orb[] orbs;

    private bool rightShift;

    [SerializeField]
    private List<GameObject> arrows;

    // Start is called before the first frame update
    void Start()
    {
        rightShift = true;
        orbs = new Orb[5];
        RandomizeDirection();
        Refill();
    }

    public void Refill()
    {
        for (int i = 0; i < 5; i++)
        {
            if (orbs[i] == null)
            {
                orbs[i] = Instantiate(orbPrefabs[Random.Range(0, orbPrefabs.Count)], new Vector2(-32 + (i * 16), transform.position.y), Quaternion.identity).GetComponent<Orb>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (orbs[i] != null)
            {
                orbs[i].transform.position = Vector2.Lerp(orbs[i].transform.position, new Vector2(-32 + (i * 16), transform.position.y), 10f * Time.deltaTime);
            }
        }

        for (int j = 0; j < arrows.Count; j++)
        {
            arrows[j].transform.localScale = Vector2.Lerp(arrows[j].transform.localScale, new Vector2(rightShift ? 1f : -1f, 1f), 10f * Time.deltaTime);
        }
    }

    public void RandomizeDirection()
    {
        rightShift = Random.Range(0, 2) == 0;
    }

    public void Shift()
    {
        if (rightShift)
        {
            Orb placeholder = orbs[4];
            for (int i = 3; i >= 0; i--)
            {
                orbs[i + 1] = orbs[i];
                orbs[i + 1].GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            orbs[0] = placeholder;
            orbs[0].GetComponent<SpriteRenderer>().sortingOrder = -10;
        }
        else
        {
            Orb placeholder = orbs[0];
            for (int i = 1; i <= 4; i++)
            {
                orbs[i - 1] = orbs[i];
                orbs[i - 1].GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            orbs[4] = placeholder;
            orbs[4].GetComponent<SpriteRenderer>().sortingOrder = -10;
        }


    }
}
