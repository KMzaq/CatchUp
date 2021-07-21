using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeT3 : MonoBehaviour
{
    //enum State {NO, GROUND, WALL, AIR}
    //enum Dir { LEFT, RIGHT, UP, DOWN};

    //[SerializeField]
    //private float speed;
 
    //MousePos gtp = new MousePos();

    //int JumpCount;
    //int MaxJumpCount;

    //[SerializeField]
    //State _State;
    //[SerializeField]
    //Dir _WallDir;

    //Animator CAanim;
    //Rigidbody m_rigidbody;

    //public GameObject image;

    ////무기관련
    //public GameObject hand;

    ////
    //[SerializeField]
    //Collider colll;

    //void Awake()
    //{
        
    //}

    //void Start()
    //{
    //    gtp.MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    //    m_rigidbody = this.GetComponent<Rigidbody>();
        
    //    if (this.GetComponentInChildren<Animator>() != null)
    //    {
    //        CAanim = this.GetComponentInChildren<Animator>();
    //    }

    //    //상태관련
    //    _State = State.GROUND;
    //    Debug.Log((int)_State);

    //    CAanim.SetInteger("_state", (int)_State);


    //}


    //Vector2 MouseDir;



    //void Update()
    //{
    //    //마우스 방향 화면 중심 기준 //캐릭터 위치를 화면 위치로 바꾸고 계산하면 이상해지나?
    //    MouseDir = new Vector2(Input.mousePosition.x - Screen.width * 0.5f, Input.mousePosition.y - Screen.height * 0.5f).normalized;

    //    //손 각도와 위치
    //    //hand.transform.localPosition = new Vector3(MouseDir.x * 0.1f, MouseDir.y * 0.1f, 0);
        
    //    if(MouseDir.x <0)
    //    {//이벤트 발생 개념으로 수정
    //        image.transform.rotation = Quaternion.Euler(-45, 180, 0);
    //        hand.transform.rotation = Quaternion.Euler(225, 0, -Mathf.Atan2(MouseDir.y, MouseDir.x) * Mathf.Rad2Deg);
    //    }
    //    else
    //    {
    //        image.transform.rotation = Quaternion.Euler(45, 0, 0);
    //        hand.transform.rotation = Quaternion.Euler(45, 0, Mathf.Atan2(MouseDir.y, MouseDir.x) * Mathf.Rad2Deg);
    //    }

    //    switch (_State)
    //    {
    //        case State.GROUND:
    //            CA_move();
    //            break;
    //        case State.WALL:
    //            if (Input.GetMouseButtonDown(0))
    //            {
    //                StopCoroutine("Jump");
    //                StartCoroutine(Jump(MouseDir));
    //            }
    //            break;
    //        case State.AIR:

    //            break;
    //    }
    //}

    //private void CA_move()
    //{

    //    float inputX = Input.GetAxisRaw("Horizontal");
    //    float inputY = Input.GetAxisRaw("Vertical");

    //    transform.Translate(new Vector3(inputX, 0, inputY) * Time.deltaTime * speed);
    //    CAanim.SetFloat("_DirX", inputX);
    //    CAanim.SetFloat("_DirY", inputY);
    //}


    //IEnumerator CA_WallClimbing(Collision wall)
    //{
    //    CAanim.SetFloat("_DirY", -1);
    //    float ChapPosY = wall.collider.bounds.size.y * 0.8f;
    //    m_rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    //    Debug.Log("벽타기");
    //    float count = 0;
    //    Vector3 wasPos = this.transform.position;
    //    while (true)
    //    {
    //        count += Time.deltaTime;
    //        this.transform.position = Vector3.Lerp(wasPos, new Vector3(wasPos.x, 0.37f, wasPos.z), count * 5f); //올라가는데 걸리는 시간 스피드 비례해서 스피드 * 0.n 하면 좋을듯
    //        if(count * 5f >= 1)
    //        {
    //            this.transform.position = new Vector3(wasPos.x, 0.37f, wasPos.z);
    //            Debug.Log("벽타기끝");
    //            break;
    //        }
    //        yield return null;
    //    }
        
    //    //20%\만큼 줄임
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == "Wall" && _State != State.WALL)
    //    {
    //        _State = State.WALL;
    //        CAanim.SetInteger("_state", (int)_State);
    //        StartCoroutine(CA_WallClimbing(collision));
    //        Cheak_WallDirection(collision);
    //    }
    //    if(collision.collider.tag == "Floor" &&_State == State.AIR)
    //    {
    //        _State = State.GROUND;
    //        CAanim.SetInteger("_state", (int)_State);
    //        m_rigidbody.velocity = Vector3.zero;
    //        Debug.Log("추락함");
    //    }
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.collider.tag == "Wall" && _State == State.WALL)
    //    {
    //        WallMove();
    //    }
    //}

    //IEnumerator Jump(Vector2 dir)
    //{
    //    Debug.Log(dir);
    //    WaitForSeconds wait = new WaitForSeconds(0.1f);
    //    float count = 3f;

    //    this.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x * 100, 0, dir.y * 100));
    //    _State = State.AIR;
    //    CAanim.SetInteger("_state", (int)_State);
    //    CAanim.SetFloat("_DirX", 0);
    //    CAanim.SetFloat("_DirY", -1);

    //    yield return new WaitForSeconds(1.0f);
    //    Debug.Log("추락");
    //    if(_State != State.WALL)
    //        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    //    while (count > 0)
    //    {
    //        count -= 0.1f;
    //        m_rigidbody.velocity *= 0.8f;
    //        //속도 줄어듬
    //        yield return wait;
    //    }
    //    yield break;
    //}

    //float _wallMoveMin, _wallMoveMax;
    //public void WallMove()
    //{
    //    ///벽에 닿았을때 크기의 몇퍼센트만큼 벽의 위치 +-로 이동 가능 범위를 구함 
    //    /////상하냐 좌우냐로 x냐 z냐 결정
    //    ///
    //    /// 이동 가능 범위를 벗어날 때 누른 키에 따라 해당 방향으로 레이를 쏴서 벽이 있는지 없는지 알아내고 있으면 이동 없으면 이동 불가 이런식으로 레이 O-/
    //    /////문제 옆 벽으로 넘어갔을때 : 다시 거리를 잼
    //    /// //


    //    //조건
    //    //1 벽에 붙어있을 때               벽 콜리전이 닿아있을때 이 함수 실행 또는 bool 값 따로 받아오기? // 손에 콜리전 추가해서 재기? 그럼 옆면에서 이상해짐 범위로 잡아야함
    //    //2 벽타기 애니메이션이 끝난 이후  나중
    //    //3 벽의 방향에 따라 이동 방향 다름 O

    //    bool _canMoveAtWall = true;

    //    float inputX = Input.GetAxisRaw("Horizontal");
    //    float inputY = Input.GetAxisRaw("Vertical");

    //    if (_canMoveAtWall)
    //    {
    //        if(_WallDir == Dir.UP || _WallDir == Dir.DOWN)
    //        {
    //            transform.Translate(new Vector3(0, 0, inputY) * Time.deltaTime * speed);
    //        }
    //        else if(_WallDir == Dir.LEFT || _WallDir == Dir.RIGHT)
    //        {
    //            transform.Translate(new Vector3(inputX, 0, 0) * Time.deltaTime * speed);
    //        }
    //    }
    //}

    //public void Cheak_WallDirection(Collision Wallcol)//붙은 벽 방향 체크
    //{
    //    //부딫힌 점과 벽의 중심을 이용해 각도를 구해 붙은 방향을 체크함
    //    float x = Wallcol.contacts[0].point.x - Wallcol.collider.bounds.center.x;
    //    float z = Wallcol.contacts[0].point.z - Wallcol.collider.bounds.center.z;
    //    float degree = Mathf.Atan2(z, x) * 180 / Mathf.PI;

    //    print(Wallcol.collider.bounds.center.z);
    //    print(Wallcol.contacts[0].point);
    //    print(degree); //벽 크기비율에 따라 각도 달라지게 해야함

    //    float a;
    //    float b;
    //    a = Wallcol.collider.bounds.size.x >= Wallcol.collider.bounds.size.z ? Wallcol.collider.bounds.size.x / Wallcol.collider.bounds.size.z : 1f;
    //    b = Wallcol.collider.bounds.size.x >= Wallcol.collider.bounds.size.z ? 1f : Wallcol.collider.bounds.size.z / Wallcol.collider.bounds.size.x;
    //    print((Mathf.Round(a * 10) * 0.1f));
    //    print((Mathf.Round(b * 10) * 0.1f));


    //    if (degree >= 45 && degree <= 135) //90도 +-
    //    {
    //        print("위");
    //    }
    //    else if(degree <= -45 && degree >= -135)
    //    {
    //        print("아래");
    //    }
    //    else if (degree < -135 || degree > 135)
    //    {
    //        print("왼쪽");
    //    }
    //    else if (degree < 45 || degree > -45)
    //    {
    //        print("오른쪽");
    //    }
    //}

    //public void FuncTest()
    //{
    //    transform.Translate(0, 110, 0);
    //    print("sid");
    //}
}
