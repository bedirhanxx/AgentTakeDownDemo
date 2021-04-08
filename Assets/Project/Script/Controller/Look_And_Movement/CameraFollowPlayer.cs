using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Objects")]
    public GameObject PlayerBase;
    void Update()
    {
        if (!GameManager.Instance.winLevel)
            transform.position = PlayerBase.transform.position;
        else
        {
            transform.parent = PlayerBase.transform;
            transform.localPosition = new Vector3(0f, 0.93f, -4.18f);
            transform.localRotation = Quaternion.Euler(12.5f, 0f, 0f);
            GetComponent<MouseLook>().enabled = false;
            GetComponent<FPSCamShooting>().Weapon.SetActive(false);
        }
    }
}
