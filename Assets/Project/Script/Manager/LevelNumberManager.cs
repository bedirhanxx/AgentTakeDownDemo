using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNumberManager : MonoBehaviour
{
    public static LevelNumberManager instance;
    public int LevelNumber = 1;
    public bool gameRestarted;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
