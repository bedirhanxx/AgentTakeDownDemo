using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{

    [Header("Objects")]
    public Transform par;
    LineRenderer lineR;
    GameObject LineR;
    public GameObject FPSCAM;
    public GameObject Crosshair;
    public GameObject AfterClickArea;
    public Transform Player;
    [Header("Position Settings")]
    public Vector3 playerStartPos;
    public Quaternion playerStartRot;
    [Header("Control For Max Line")]
    public int posIndex;


    private void OnEnable()
    {
        lineR = GetComponent<LineRenderer>();
        LineR = this.gameObject;
        playerStartRot = Player.localRotation;
        playerStartPos = Player.transform.position = new Vector3(0f, 1f, 0f);
    }

    private void Start()
    {

        lineR = GetComponent<LineRenderer>();
        LineR = this.gameObject;
        playerStartRot = Player.localRotation;
            
    }

    public void ResetLine()
    {
        AfterClickArea.GetComponent<MeshCollider>().enabled = false;
        Vector3[] poses = new Vector3[2];

        Player.localRotation = playerStartRot;
        Player.localPosition = playerStartPos;
        InputManager.instance.drawReset = false;

        poses[0] = par.position;
        poses[1] = par.position + par.forward * 0.01f;
        lineR.SetVertexCount(2);
        lineR.SetPositions(poses);
    }

    public void DrawLine(Vector3 worldPos)
    {
        AfterClickArea.GetComponent<MeshCollider>().enabled = true;
        posIndex = lineR.positionCount - 1;
        Vector3 lastPos = lineR.GetPosition(posIndex);
        if((worldPos - lastPos).sqrMagnitude > 0.01f)
        {
            lineR.positionCount += 1;
            lineR.SetPosition(posIndex + 1, worldPos);
        }
    }

    public void CompleteDrawing()
    {

            FPSCAM.SetActive(true);
            Crosshair.SetActive(true);
//            Cursor.visible = false;
            GetComponent<LineRenderer>().Simplify(0.05f);
            Player.GetComponent<LineFollower>().Follow(lineR);
    }

}
