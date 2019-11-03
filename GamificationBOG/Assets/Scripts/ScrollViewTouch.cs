using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(GraphicRaycaster))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollViewTouch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0, 1f)]
    public float sensitivity = 0.5f;
    [Range(0, 0.99f)]
    public float inertia = 0.5f;
    public Camera mainCamera;

    ScrollRect scroll;
    GraphicRaycaster gRay;
    PointerEventData pData;
    //EventSystem eventSystem;

    Vector3 oldMousePosition;
    Vector3 delta;
    RaycastHit rayHit;

    bool inertialMovement = false;
    bool isActive = false;

    void Awake()
    {
        if(!mainCamera)
            mainCamera = FindObjectOfType<Camera>();

        scroll = GetComponent<ScrollRect>();
        gRay = GetComponent<GraphicRaycaster>();
        //eventSystem = GetComponent<EventSystem>();

        if (!mainCamera)
        {
            Debug.LogError("Cmaera controller: camera not found");
            Destroy(this.gameObject);
        }

        //Input.simulateMouseWithTouches = true;
    }

    private void ProcessMouseInput()
    {
        if (!inertialMovement)
            delta = Input.mousePosition - oldMousePosition;

        oldMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
            inertialMovement = false;

        if (Input.GetMouseButtonUp(0))
            inertialMovement = true;

        if (!Input.GetMouseButton(0) && !inertialMovement)
            return;

        if (inertialMovement && inertia > 0)
        {
            delta *= inertia;

            if (delta.magnitude <= 0.0001f)
                inertialMovement = false;
        }
        else
            inertialMovement = false;

        scroll.verticalScrollbar.value -= delta.y * sensitivity * 0.01f;
    }

    private void ProcessTouchInput()
    {
        if (Input.touches.Length == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                inertialMovement = false;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                delta = Input.touches[0].deltaPosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                inertialMovement = true;                
            }
        }
        else if(inertialMovement && inertia > 0)
        {
            delta *= inertia;

            if (delta.magnitude <= 0.0001f)
                inertialMovement = false;
        }

        if(Input.touches.Length == 0 && !inertialMovement)
            delta = Vector3.zero;

        scroll.verticalScrollbar.value -= delta.y * sensitivity * 0.01f;
    }

    void Update()
    {
        if (Input.mousePresent)
        {
            if ((!EventSystem.current.IsPointerOverGameObject() || !isActive) && !inertialMovement)
                return;

            ProcessMouseInput();
        }
        else
        {
            if (Input.touches.Length > 0)
            {
                pData = new PointerEventData(EventSystem.current);
                pData.position = Input.touches[0].position;

                List<RaycastResult> hits = new List<RaycastResult>();

                gRay.Raycast(pData, hits);

                Transform scrollTransform = hits.FirstOrDefault(h => h.gameObject.transform == scroll.transform).gameObject.transform;

                if(scrollTransform)
                    ProcessTouchInput();

                //foreach (RaycastResult hit in hits)
                //{
                //    if (hit.gameObject.transform == scroll.transform)
                //    {
                        
                //        break;
                //    }
                //}
            }
            else if (inertialMovement)
                ProcessTouchInput();
        }
    }

    public RectTransform Content
    {
        get { return scroll.content; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isActive = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
    }
}
