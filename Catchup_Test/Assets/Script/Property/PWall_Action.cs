using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PWall_Action : MonoBehaviour
{
    public enum Dir : int { UP, DOWN, LEFT, RIGHT }
    Dir _WallAttachDir;

    private Rigidbody A_rigidbody;

    [SerializeField]
    private int speed;
    [SerializeField]
    private int JumpSpeed;
    [SerializeField]
    private float JumpTime;
    [SerializeField]
    private float JumpDistance;

    public bool IsJumping = false;

    public ActorController actorController;
    void Start()
    {
        A_rigidbody = GetComponent<Rigidbody>();

        actorController = Util.GetorAddComponent<ActorController>(this.gameObject);

        //점프 횟수와 속도
    }
    private void Update()
    {
        if (IsJumping)
        {
            JumpTime += Time.deltaTime;
            if(JumpTime >= 1.0f * JumpDistance)
            {
                A_rigidbody.velocity = new Vector3(A_rigidbody.velocity.x * 0.99f, A_rigidbody.velocity.y, A_rigidbody.velocity.z * 0.99f);
                A_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
            if (A_rigidbody.velocity == Vector3.zero)
            {
                IsJumping = false;
            }
        }

    }

    public int Cheak_WallDirection(Collision Wallcollision)//붙은 벽 방향 체크 함수
    {
        //부딫힌 점과 벽의 중심을 이용해 각도를 구해 붙은 방향을 체크함
        float x = 0, z = 0;
        foreach (ContactPoint Cpoint in Wallcollision.contacts)
        {
            if(Cpoint.thisCollider.tag == "Player")
            {
                x = Cpoint.point.x - Wallcollision.collider.bounds.center.x;
                z = Cpoint.point.z - Wallcollision.collider.bounds.center.z;
                break;
            }
        }
        float degree = Mathf.Atan2(z, x) * 180 / Mathf.PI;
        float a = Wallcollision.collider.bounds.size.x / Wallcollision.collider.bounds.size.z;
        float DegreeStandardAngle = 90 / (a+1);
        float DegreeStandardAngle2 = 180 - DegreeStandardAngle;

        int dir = 0;

        if (degree >= DegreeStandardAngle && degree <= DegreeStandardAngle2)
        {
            print("위");
            dir = 0;
            //Player_Animator.SetFloat("_DirX", MouseDir.x);
        }
        else if (degree <= -DegreeStandardAngle && degree >= -DegreeStandardAngle2)
        {
            print("아래");
            dir = 1;
        }
        else if (degree < -DegreeStandardAngle2 || degree > DegreeStandardAngle2)
        {
            print("왼쪽");
            dir = 2;
        }
        else if (degree < DegreeStandardAngle || degree > -DegreeStandardAngle)
        {
            print("오른쪽");
            dir = 3;
        }
        _WallAttachDir = (Dir)dir;
        return dir;
    }

    public void AJump(Vector2 dir)
    {
        IsJumping = true;
        JumpTime = 0;
        this.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * JumpSpeed, 0, dir.y * JumpSpeed); 
    }

    public void AWallClimb(Collision wall)
    {
        StopCoroutine("WallClimbing");
        StartCoroutine("WallClimbing", wall);
    }

    IEnumerator WallClimbing(Collision wall)
    {
        actorController.blockAllAction = true;
        float ChapPosY = wall.collider.bounds.size.y - 0.13f;
        float count = 0;
        Vector3 wasPos = this.transform.position;

        A_rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        while (true)
        {
            count += 0.01f * speed;
            this.transform.position = Vector3.Lerp(wasPos, new Vector3(wasPos.x, ChapPosY, wasPos.z), count); //올라가는데 걸리는 시간 스피드 비례해서
            if (count >= 1) // count / speed 이동속도 반영되야함
            {
                print("벽타기끝");
                actorController.blockAllAction = false;
                PlayerController.Player_Animator.SetTrigger("_IsLanded");
                this.transform.position = new Vector3(wasPos.x, ChapPosY, wasPos.z);
                break;
            }
            yield return new WaitForSeconds(0.01f * speed);
        }
    }

    public void AWallMove()
    {
        ///벽에 닿았을때 크기의 몇퍼센트만큼 벽의 위치 +-로 이동 가능 범위를 구함 
        /////상하냐 좌우냐로 x냐 z냐 결정
        ///
        /// 이동 가능 범위를 벗어날 때 누른 키에 따라 해당 방향으로 레이를 쏴서 벽이 있는지 없는지 알아내고 있으면 이동 없으면 이동 불가 이런식으로 레이 O-/
        /////문제 옆 벽으로 넘어갔을때 : 다시 거리를 잼
        /// //


        //조건
        //1 벽에 붙어있을 때               벽 콜리전이 닿아있을때 이 함수 실행 또는 bool 값 따로 받아오기? // 손에 콜리전 추가해서 재기? 그럼 옆면에서 이상해짐 범위로 잡아야함
        //2 벽타기 애니메이션이 끝난 이후  나중
        //3 벽의 방향에 따라 이동 방향 다름 O

        bool _canMoveAtWall = true;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        if (_canMoveAtWall)
        {
            if (_WallAttachDir == Dir.UP || _WallAttachDir == Dir.DOWN)
            {
                //transform.Translate(new Vector3(0, 0, inputZ) * Time.deltaTime * speed);
                GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3( 0, 0, inputZ * 0.01f * speed));
            }
            else if (_WallAttachDir == Dir.LEFT || _WallAttachDir == Dir.RIGHT)
            {
                //transform.Translate(new Vector3(inputX, 0, 0) * Time.deltaTime * speed);
                GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(inputX * 0.01f * speed, 0, 0));
            }
        }
    }
}
