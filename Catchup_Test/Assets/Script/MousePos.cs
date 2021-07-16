using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos
{
    Vector3 Pos;

    public Camera MainCamera;

    public Vector3 GetPos()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f))
        {
            Pos = hit.point;
            //Debug.Log(Pos);
        }
        return Pos;

    }

}
