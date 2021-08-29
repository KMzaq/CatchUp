﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapST : MonoBehaviour
{
    
    [SerializeField]
     private CapsuleCollider playercollider;
    [SerializeField]
  //  private WoldMapSystem system;
    private GameObject Map;
    private void Awake()
    {
       WoldMapSystem.Insternse.room =null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Map")&& WoldMapSystem.Insternse.room == null) {
            Map = other.gameObject;
            WoldMapSystem.Insternse.room = Map.gameObject.GetComponent<RoomSystem>();
            WoldMapSystem.Insternse.room.StageStart();
        }
    }


}
