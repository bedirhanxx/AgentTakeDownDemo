using UnityEngine;

public class FPSCamShooting : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Weapon;
    [Header("Mask Settings For Raycast")]
    public LayerMask LayerMasks;
    [Header("Shoot Time Settings")]
    private float time = 0.0f;
    public float shootTime = 0.1f;
    [Header("Shoot Damage Settings")]
    public float shootDamage;

    void FixedUpdate()
    {
        time += Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMasks))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                if (time >= shootTime)
                {
                    hit.transform.GetComponent<EnemyHealth>().damageEnemy(shootDamage);
                    time = 0.0f;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Weapon.GetComponent<Animator>().SetBool("hit", true);
            }
            else
            {
                Weapon.GetComponent<Animator>().SetBool("hit", false);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Weapon.GetComponent<Animator>().SetBool("hit", false);
        }
    }
}
