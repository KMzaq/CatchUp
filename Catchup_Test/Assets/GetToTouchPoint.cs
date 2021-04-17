using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetToTouchPoint
{
    Vector2 MousePos;

    [SerializeField]
    public Camera MainCamera;

    //void Start()
    //{
    //    MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        //MousePos = Input.mousePosition;
    //        //MousePos = MainCamera.ScreenToWorldPoint(MousePos);
            
    //        MousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);

    //        Debug.Log(MousePos);
    //        Debug.Log(MousePos.normalized);
    //        //this.GetComponent<Rigidbody2D>().velocity = new Vector2(MousePos.x - this.transform.position.x, MousePos.y - this.transform.position.y).normalized;
          
    //    }
    //}
    public Vector2 GetMousePos()
    {

        Vector2 sls;
        sls = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        return sls;
    }

}
