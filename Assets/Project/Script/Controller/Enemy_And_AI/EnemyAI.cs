using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Player;
    public GameObject PlayerFPSCAM;
    public GameObject EnemyWeapon;
    [Header("Settings for hit player")]
    public int shootCorret = 3;
    private int shootCount;
    public float shootDistance;
    private float dist;
    private float time = 0.0f;
    [Header("Settings for shoot time")]
    public float shootTime = 0.1f;
    [Header("Settings for rotation speed")]
    public float speed;
    [Header("Mask Settings For Raycast")]
    public LayerMask LayerMask;
    

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    IEnumerator delayRestart()
    {
        yield return
        LevelNumberManager.instance.gameRestarted = false;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if(hit.transform.gameObject.tag == "Player")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                if (time >= shootTime)
                {
                    time = 0.0f;
                    shoot();
                }
            }
            
            //Debug.Log("Did Hit Wall");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Shoot");
        }
    }


    void Update()
    {
        PlayerFPSCAM = GameObject.FindGameObjectWithTag("PlayerFPSCam");
        if (Player != null)
        {
    
            time += Time.deltaTime;
            dist = Vector3.Distance(Player.transform.position, transform.position);
            //Debug.Log("Distance to Player: " + dist);
            if(dist < shootDistance)
            {

                Quaternion OriginalRot = transform.rotation;
                transform.LookAt(Player.transform);
                Quaternion NewRot = transform.rotation;
                transform.rotation = OriginalRot;
                transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, speed * Time.deltaTime);
            }
                
        }
    }

    private void shoot()
    {
        // if shoot count equal to shoot correct hit player other then miss player.
        shootCount++;
        EnemyWeapon.GetComponent<Animator>().SetTrigger("hit");
        if (shootCount >= shootCorret)
        {
            Debug.Log("Hit Player");
            Player.GetComponent<LineFollower>().stopPlayer();
            PlayerFPSCAM.GetComponent<MouseLook>().enabled = false;
            PlayerFPSCAM.GetComponent<TouchLook>().enabled = false;
            PlayerFPSCAM.GetComponent<FPSCamShooting>().Weapon.SetActive(false);
            LevelNumberManager.instance.gameRestarted = true;
            GameManager.Instance.GameOver();
            shootCount = 0;
        }
    }
}
