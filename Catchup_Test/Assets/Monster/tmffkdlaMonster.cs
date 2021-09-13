using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmffkdlaMonster : MonoBehaviour
{
    public GameObject player;
    GameObject playerSet { set { player = value; } }
    GameObject sprite;
    MonsterBase monsterBaseState;
    Animator anime;

    private void Start()
    {
        monsterBaseState = new MonsterBase();
        sprite = this.gameObject.transform.GetChild(0).gameObject;
        anime = sprite.gameObject.GetComponent<Animator>();
        Debug.Log("몬스터 스폰");
        MonsterRoationTrans(monsterBaseState.monsterRotation);

    }

    public void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "대충 벽 테그")
        {
            //벽과의 위치 계산후 방향 계산후 monsterBaseState.monsterRotation 변경

            MonsterRoationTrans(monsterBaseState.monsterRotation);
            anime.SetBool("JUMP", false);
        }

    }

    void DieMonster()
    {
        anime.SetBool("DIE", false);
    }
    void jump(Vector3 player)
    {
        if (monsterBaseState.monsterRotation == MonsterBase.MonsterRotation.noob)
        {
            //첫스폰이라 벽으로 점프 y축 이동

        }
        anime.SetBool("JUMP", true);

        //플레이어 쪽으로 점프해야하지만 플레이어쬭 벽에 붙어있으면 반대로 점프 ㄱㄱ
    }
    void MonsterRoationTrans(MonsterBase.MonsterRotation roation)
    {
        int MonsterRoationing = (int)roation * 90;
        sprite.transform.Rotate(0, 0, MonsterRoationing);
    }
}

class MonsterBase
{
    enum MonsterStateEnum
    {
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

    public MonsterRotation monsterRotation = MonsterRotation.noob;
    int MonsterHP = 10;
    float MonsterSpeed = 20;
    int MonsterState = (int)MonsterStateEnum.Idle;

    //============================================
    void Init(int _hp, float _monsterspeed)
    {
        MonsterHP = _hp;
        MonsterSpeed = _monsterspeed;
        MonsterState = (int)MonsterStateEnum.Idle;

    }

}