using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event 시스템에 필수로 넣어줘야함
// 여기서 맵을 받아서 시작 죽음 페이즈 등 관리 할거
public class WoldMapSystem : MonoBehaviour
{
    public RoomSystem room;
    // Start is called before the first frame update
    void Start()
    {
        room = null;
    }

    // Update is called once per frame
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