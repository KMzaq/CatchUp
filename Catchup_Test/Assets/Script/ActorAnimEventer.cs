using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventer : MonoBehaviour
{
    public UnityEvent WallClimbing;
    void FuncTest()
    {
        print("애니메이션");
        WallClimbing.Invoke();
    }

    
}
