using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] float Speed;
    [SerializeField] float Delaytime;
    [SerializeField] GameObject Sprite;
    [SerializeField] bool On_barrage;

    SubState substate;
    SlimeState slimestate;
    Direction direction;
    Animator anime;
    float Movespeed;
    Vector3 Moveval;

    enum SlimeState
    {
        Move,
        Wall,
    }

    enum SubState
    {
        Start,
        Delay,
        EndDelay,
    }

    enum Direction
    {
        Right,
        Left,
        Up,
        Down,
    }

    void Start()
    {

        slimestate = SlimeState.Move;
        Sprite = this.gameObject.transform.GetChild(0).gameObject;
        anime = Sprite.gameObject.GetComponent<Animator>();
        anime.SetBool("MOVE", true);
        Moveval = new Vector3(0, 0, 1);
    }

    void Update()
    {
        if(Hp <= 0)
        {
            StartCoroutine(Die());
        }
        if( slimestate == SlimeState.Move)
        {
            SlimeMove();
        }
        if(slimestate == SlimeState.Wall)
        {
            SlimeWall();
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Wall"))
        {
            Cheak_WallDirection(other);
            slimestate = SlimeState.Wall;
            substate = SubState.Start;
            anime.SetBool("MOVE", false);
        }
        else
        {

        }
    }

    void Cheak_WallDirection(Collider Wallcollision)//붙은 벽 방향 체크 함수
    {
        //부딫힌 점과 벽의 중심을 이용해 각도를 구해 붙은 방향을 체크함
                float x = 0, z = 0;

                x = this.transform.position.x - Wallcollision.bounds.center.x;
                z = this.transform.position.z - Wallcollision.bounds.center.z;
        
        
        float degree = Mathf.Atan2(z, x) * 180 / Mathf.PI;
        float a = Wallcollision.bounds.size.x / Wallcollision.bounds.size.z;
        float DegreeStandardAngle = 90 / (a + 1);
        float DegreeStandardAngle2 = 180 - DegreeStandardAngle;

        if (degree >= DegreeStandardAngle && degree <= DegreeStandardAngle2) // 위
        {
            direction = Direction.Up;
            anime.SetBool("Vertical", true);
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 0);
        }
        else if (degree <= -DegreeStandardAngle && degree >= -DegreeStandardAngle2) //아래
        {
            direction = Direction.Down;
            anime.SetBool("Vertical", true);
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 180);
        }
        else if (degree < -DegreeStandardAngle2 || degree > DegreeStandardAngle2) //왼쪽
        {
            direction = Direction.Left;
            anime.SetBool("Vertical", false);
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 180);
            transform.localScale =  new Vector3(1, 1, 1);
        }
        else if (degree < DegreeStandardAngle || degree > -DegreeStandardAngle) //오른쪽
        {
            direction = Direction.Right;
            anime.SetBool("Vertical", false);
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 180);
            transform.localScale = new Vector3(-1,1,1);

        }
    }


    void SlimeMove()
    {
        Movespeed = Time.deltaTime * Speed;
        transform.Translate(Moveval * Movespeed);
    }

    void SlimeWall()
    {
        if(substate == SubState.Start)
        {
            StartCoroutine(TimeDelay());
        }
        else if (substate == SubState.EndDelay && On_barrage)
        {

        }
        else if(substate == SubState.EndDelay && !(On_barrage))
        {
            slimestate = SlimeState.Move;
            substate = SubState.Delay;
            anime.SetBool("MOVE", true);

        }

    }

    void MovePointCreate()
    {
        int rand, Sub_index;
        float[] MovePoint_Val = { 0.1f, -0.1f };
        float SubVal;

        Sub_index = Random.Range(0, 2);
        rand = Random.Range(0, 10);

        SubVal = MovePoint_Val[Sub_index] * rand;

        if (direction == Direction.Up)
        {
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 0 + SubVal *-50);
            Moveval = new Vector3(SubVal, 0, 1 - 0.1f * rand);
        }
        if(direction == Direction.Down)
        {
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 180 + SubVal * 50);
            Moveval = new Vector3(SubVal, 0, -1 + 0.1f * rand);
        }
        if(direction == Direction.Left)
        {
            Sprite.transform.rotation = Quaternion.Euler(45, 0, 90 - SubVal*50);
            Moveval = new Vector3(-1 + 0.1f * rand, 0, SubVal);
        }
        if(direction == Direction.Right)
        {
            Sprite.transform.rotation = Quaternion.Euler(45, 0, -90 + SubVal * 50);
            Moveval = new Vector3(1 - 0.1f * rand, 0, SubVal);
        }
    }
    IEnumerator TimeDelay()
    {
        substate = SubState.Delay;
        transform.Translate(0, 0, 0);
        yield return new WaitForSeconds(Delaytime);
        substate = SubState.EndDelay;
        MovePointCreate();
    }


    IEnumerator Die()
    {
        anime.SetBool("DIE", true);
        yield return new WaitForSeconds(0.45f);
        Destroy(this.gameObject);
    }

}
