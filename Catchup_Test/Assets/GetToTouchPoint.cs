using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetToTouchPoint
{
    Vector3 MousePos;

    [SerializeField]
    public Camera MainCamera;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                MousePos = hit.point;
                Debug.Log(MousePos);
            }
        }
        Debug.Log("1");
    }
    public Vector3 GetMousePos()
    {

        Vector3 sls;
        //sls = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f))
        {
            MousePos = hit.point;
            Debug.Log(MousePos);
        }
        sls = MousePos;
        return sls;
    }

}
