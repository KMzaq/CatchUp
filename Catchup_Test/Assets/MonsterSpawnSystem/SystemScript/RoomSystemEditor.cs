using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomSystem))]
public class RoomSystemEditor : Editor
{
    RoomSystem roomSystem;
  

    private void OnEnable()
    {
        roomSystem = target as RoomSystem;

    }
    // List<GameObject> Creators = new List<GameObject>();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("ADD Spawn Point"))
        {
            GameObject Base;
            Base = Instantiate(roomSystem.shapeCreatorBase, roomSystem.transform);
           // Creators.Add(Base);
          
            roomSystem.MonsterSpawnPaze.Add(Base.GetComponent<shapeCreator>());

        }


        if (GUILayout.Button("Clear Spawn Point"))
        {
            roomSystem.MonsterSpawnPaze.RemoveAt(roomSystem.MonsterSpawnPaze.Count-1);
        
            DestroyImmediate(roomSystem.transform.GetChild(roomSystem.MonsterSpawnPaze.Count).gameObject);

           // Creators.RemoveAt(Creators.Count-1);

        }


    }





}


