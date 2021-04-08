using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<LineFollower>().stopPlayer();
            LevelNumberManager.instance.LevelNumber++;
            GameManager.Instance.saveLevel();
            GameManager.Instance.FinishLevel();
        }
    }
}
