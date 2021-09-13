using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_adit : MonoBehaviour
{
    [SerializeField] GameObject[] Map;
    [SerializeField] Vector3[] MapSpawnPoint;
    [SerializeField] Vector3[] LoadSpawnPoint;
    [SerializeField] GameObject Load;
    [SerializeField] GameObject[] MapLevel;

    List<GameObject> MapList = new List<GameObject>();
    List<GameObject> LoadList = new List<GameObject>();

    void Start()
    {
        Start_Create();
        Init();
    }

    void Update()
    {
        
    }

    void Init()
    {

    }

    void Start_Create()
    {
        for (int i = 0; i < 4; i++) // map add
        {
            GameObject MapInstantiate;
            GameObject LoadInstantiate;

            MapInstantiate = Instantiate(Map[i]);
            MapInstantiate.transform.position = new Vector3(MapSpawnPoint[i].x, MapSpawnPoint[i].y, MapSpawnPoint[i].z);
            MapInstantiate.transform.SetParent(this.gameObject.transform, false);
            MapList.Add(MapInstantiate);

            LoadInstantiate = Instantiate(Load);
            LoadInstantiate.transform.position = new Vector3(LoadSpawnPoint[i].x, LoadSpawnPoint[i].y, LoadSpawnPoint[i].z);
            LoadInstantiate.transform.Rotate(new Vector3(0f, -90f * i, 0f));
            LoadList.Add(LoadInstantiate);
        }

        for(int j = 0; j < 4; j++)
        {
            Level_Create(j);
        }

}

    void Level_Create(int Mapindex)
    {
        int rand;
        GameObject Level_Instantiate;
        rand = Random.Range(0, 3);
        Level_Instantiate = Instantiate(MapLevel[rand + 3 * Mapindex]);
        Level_Instantiate.transform.SetParent(MapList[Mapindex].transform, false);
    }
    
    void Level_Cls(int Mapindex)
    {
        for(int i = 1; i <= MapList[Mapindex].transform.childCount; i++)
        {
            Destroy(MapList[Mapindex].transform.GetChild(i).gameObject);
        }
    }

}

