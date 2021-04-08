using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCount : MonoBehaviour
{
    [Header("Alive Enemy List")]
    public GameObject[] AllEnemysAlive;
    public int EnemyCount;
    [Header("Objects")]
    public GameObject Crosshair;

    public void CheckEnemys()
    {
        AllEnemysAlive = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyCount = AllEnemysAlive.Length;
        if (EnemyCount == 0)
        {
            Debug.Log("Complete Mission");
            Crosshair.SetActive(false);
            GameManager.Instance.winLevel = true;
        }
    }
    void Update()
    {
        CheckEnemys();
    }
}
