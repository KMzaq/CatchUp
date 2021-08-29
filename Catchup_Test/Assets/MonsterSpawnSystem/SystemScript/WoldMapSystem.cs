using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event 시스템에 필수로 넣어줘야함
// 여기서 맵을 받아서 시작 죽음 페이즈 등 관리 할거
public class WoldMapSystem : MonoBehaviour
{   
    
    private static WoldMapSystem insternse;
    public static WoldMapSystem Insternse
    {
        get
        {
            if (insternse == null)
            {

                GameObject gameObject1 = GameObject.Find("RoomSystem");
                if (gameObject1 == null)
                {
                    gameObject1 = new GameObject { name = "RoomSystem" };
                    gameObject1.AddComponent<WoldMapSystem>();

                }
                DontDestroyOnLoad(gameObject1);
                insternse = gameObject1.GetComponent<WoldMapSystem>();


            }
            return insternse;
        }

    }
    public RoomSystem room;



    void Update()
    {

        if (room != null)
        {
            for (int i = 0; i < room.PazeMonsters.Count && room.PazeMonsters[i] == null; i++)
            {

                room.PazeMonsters.Remove(room.PazeMonsters[i]);
            }
            if (room.PazeMonsters.Count <= 0)
            {
                room.NextPaze();
                print("넘어간다~"+ room.EndMap);
                if (room.EndMap)
                {
                    Destroy(room.gameObject);
                    room = null;
                    print("끝");
                }
            }

        }







    }
}