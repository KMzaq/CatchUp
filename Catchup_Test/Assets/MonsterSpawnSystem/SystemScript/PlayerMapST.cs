using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapST : MonoBehaviour
{
    
    [SerializeField]
     private CapsuleCollider playercollider;
    [SerializeField]
    private WoldMapSystem system;
    private GameObject Map;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Map")) {
            Map = other.gameObject;
            system.room = Map.gameObject.GetComponent<RoomSystem>();
            system.room.StageStart();
        }
    }


}
