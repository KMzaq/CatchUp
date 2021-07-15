using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{

    //몬스터를 풀링 방식으로 전체 방의 몬스터계체 마다의 최대 개수를 구하고 생성이 아니라 
    //다시 활동으로 제작하는건 어떨까?
    
    // 두번째 방법
    //가까운 몬스터부터 미리생성해둔다. 이후 플레이어가 갈수있는 방을 기준으로 몹을 미리 생성시킨다.



    private int MonsterSpawnPazeNum = 0;
    public List<shapeCreator> MonsterSpawnPaze = new List<shapeCreator>();
    [SerializeField]
    private GameObject BaseMonster; //몬스터가 없을때 나올 몬스터임

    public GameObject shapeCreatorBase;
    private void StartMap() //다죽이고 실행시키면 monsterspawnpasenum 으로 전체적인 paze를 넘길것
    {
        GameObject MBase;
        IList list = MonsterSpawnPaze[MonsterSpawnPazeNum]._Monster;
        print(list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                MBase = Instantiate(MonsterSpawnPaze[MonsterSpawnPazeNum]._Monster[i]);
                MBase.transform.position = MonsterSpawnPaze[MonsterSpawnPazeNum].points[i] + this.gameObject.transform.position;
            }
            catch (UnassignedReferenceException)
            {
                MBase = Instantiate(BaseMonster);
                MBase.transform.position = MonsterSpawnPaze[MonsterSpawnPazeNum].points[i] + this.gameObject.transform.position;

                Debug.LogError("몬스터가 안들어간 오류입니다. 페이즈" + MonsterSpawnPazeNum + "번째의 " + i + "몬스터가 지정안되어있습니다. BaseMonster로 대체되었습니다.");
            }
        }
       
    }
    private void Start()
    {
     
        StartMap();
    }


}
