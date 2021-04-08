using UnityEngine;
using System.Collections;


public class TouchLook : MonoBehaviour
{
    [Header("Look Settings")]
    public float sensitivityX = 5.0f;
    public float sensitivityY = 5.0f;
    [Header("Invert Settings")]
    public bool invertX = false;
    public bool invertY = false;
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.touches[0].deltaPosition;
                float rotationZ = delta.x * sensitivityX * Time.deltaTime;
                rotationZ = invertX ? rotationZ : rotationZ * -1;
                float rotationX = delta.y * sensitivityY * Time.deltaTime;
                rotationX = invertY ? rotationX : rotationX * -1;

                transform.localEulerAngles += new Vector3(rotationX, rotationZ, 0);
            }
        }
    }
}
