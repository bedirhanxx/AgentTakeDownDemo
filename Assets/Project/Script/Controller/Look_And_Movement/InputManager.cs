using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NonDrawingGraphic))]
public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Objects")]
    public GameObject cam;
    public GameObject Draw;
    public PointerEventData eventData;
    public Draw draw;
    public bool drawReset;
    public bool pressed;
    public bool cliclable = false;
    [Header("Raycast Settings")]
    public RaycastHit hit;
    public Vector2 rayOffset = new Vector2(0, 0); //change this if need to offset

    #region Singleton
    public static InputManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Draw = GameObject.Find("Draw");
        draw = Draw.GetComponent<Draw>();
    }
    public void callDraw()
    {
        Draw = GameObject.Find("Draw");
        draw = Draw.GetComponent<Draw>();
    }

    #region RaycastFromTouchPoint
    public void OnPointerDown(PointerEventData _eventData)
    {
        if (cliclable)
        {
            eventData = _eventData;
            drawReset = false;
        }
    }
    public void OnPointerUp(PointerEventData _eventData)
    {
        if (cliclable)
        {
            if (!drawReset)
            {
                draw.GetComponent<Draw>().ResetLine();
                drawReset = true;
            }
        }
    }

    private void Update()
    {

        if (draw.GetComponent<Draw>().posIndex > 200)
        {
            draw.GetComponent<Draw>().ResetLine();
            draw.GetComponent<Draw>().posIndex = 0;
            drawReset = true;
        }

        if (eventData == null) return;

        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(eventData.position + rayOffset);
        Physics.Raycast(ray, out hit);
        if (draw != null && hit.transform != null)
        {
            if (hit.transform.gameObject.tag == "ResetArea")
            {
                draw.ResetLine();
                drawReset = true;
            }
            if (hit.transform.gameObject.tag == "Enemy")
            {
                draw.ResetLine();
                drawReset = true;
            }
            if (hit.transform.gameObject.tag == "Player")
            {
                draw.ResetLine();
                drawReset = true;
            }
            if (hit.transform.gameObject.tag == "FinishDraw")
            {
                drawReset = true;
                eventData = null;
                draw.CompleteDrawing();
                drawReset = true;
                cliclable = false;
            }
            if (!drawReset)
            {
                draw.DrawLine(hit.point + (hit.normal * 0.05f));
            }
        }
    }
    #endregion
}