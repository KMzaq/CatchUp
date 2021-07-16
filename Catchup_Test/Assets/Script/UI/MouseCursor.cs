using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D CursorTexture;
    private Vector2 HotSpot;
    void Start()
    {
        StartCoroutine(ChangeCursor());
    }

    IEnumerator ChangeCursor()
    {
        yield return new WaitForEndOfFrame();

        HotSpot.x = CursorTexture.width * 0.5f;
        HotSpot.y = CursorTexture.height * 0.5f;

        Cursor.SetCursor(CursorTexture, HotSpot, CursorMode.Auto);
    }


}
