using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmffkdlaMonster : MonoBehaviour
{
    public GameObject player;
    GameObject playerSet { set { player = value; } }
    GameObject sprite;
    MonsterBase monsterBaseState;


    private void Start()
    {
         monsterBaseState = new MonsterBase();
        sprite =this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log("몬스터 스폰");
        InitMonsterRoation(monsterBaseState.monsterRotation);
        
    }


    void jump(Vector3 player)
    {
        if(monsterBaseState.monsterRotation == MonsterBase.MonsterRotation.noob) 
        {
         

        }
        //플레이어 쪽으로 점프해야하지만 플레이어쬭 벽에 붙어있으면 반대로 점프 ㄱㄱ
    }
    void InitMonsterRoation(MonsterBase.MonsterRotation roation)
    {
        int MonsterRoationing = (int)roation * 90;
        sprite.transform.Rotate(0, 0, MonsterRoationing);
    }
}
class MonsterBase
{
    enum MonsterStateEnum{
        Idle,
        attck,
    
    }
    public enum MonsterRotation
    {
        noob,
        Left,
        Under,
        Ringt,
        Up
    }

   public MonsterRotation monsterRotation= MonsterRotation.noob;
    int MonsterHP=10; 
    float MonsterSpeed =20;
    int MonsterState = (int)MonsterStateEnum.Idle;
    
    //============================================  
    void Init(int _hp,float _monsterspeed)
    {
        MonsterHP = _hp;
        MonsterSpeed = _monsterspeed;
         MonsterState = (int)MonsterStateEnum.Idle;
        
    }

}
