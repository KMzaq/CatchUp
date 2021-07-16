using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private bool _IsBlockInput;

    [SerializeField]
    Dictionary<KeyCode, Action> m_KeyDictionary;

    void Start()
    {
        m_KeyDictionary = new Dictionary<KeyCode, Action>
        {
            
        };
    }

    
    void Update()
    {
        Debug.Log("인풋핸들러 반복");
    }
    private bool CanInput()
    {
        return !_IsBlockInput;//움직일 수 없게되는 조건들 추가
    }
    public Vector3 GetMoveInput()
    {
        if (CanInput())
        {
            Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            return move;
        }
        return Vector3.zero;
    }

    public bool GetJumpInputDown()
    {
        if(CanInput())
            return Input.GetMouseButtonDown(1);
        return false;
    }




}
