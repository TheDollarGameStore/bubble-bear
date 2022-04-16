using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOne : MonoBehaviour
{
    public static OnlyOne instance = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
