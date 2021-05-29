using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeT3 : MonoBehaviour
{
    enum State {NO, GROUND, WALL, AIR}

    [SerializeField]
    private float speed;
 
    GetToTouchPoint gtp = new GetToTouchPoint();

    int JumpCount;
    int MaxJumpCount;

    [SerializeField]
    State _State;

    Animator CAanim;
    Rigidbody rigidbody;


    void Awake()
    {
        
    }

    void Start()
    {
        gtp.MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rigidbody = this.GetComponent<Rigidbody>();
        
        if (this.GetComponentInChildren<Animator>() != null)
        {
            CAanim = this.GetComponentInChildren<Animator>();
        }
        _State = State.GROUND;
        CAanim.SetInteger("_state", (int)_State);
        Debug.Log((int)_State);
    }

    Vector3 TCPoint;
    Vector2 MoveDir;
    void Update()
    {
        switch (_State)
        {
            case State.GROUND:
                CA_move();
                break;
            case State.WALL:
                if (Input.GetMouseButtonDown(0))
                {
                    TCPoint = gtp.GetMousePos();
                    MoveDir = new Vector2(TCPoint.x - this.transform.position.x, TCPoint.z - this.transform.position.z).normalized;
                    CA_jump(MoveDir);
                    StartCoroutine(Jump(MoveDir));

                }
                break;
            case State.AIR:

                break;
        }
    }

    private void CA_move()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(inputX, 0, inputY) * Time.deltaTime * speed);
        CAanim.SetFloat("_DirX", inputX);
        CAanim.SetFloat("_DirY", inputY);
    }

    private void CA_jump(Vector2 dir)
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x * 100, 10, dir.y * 100));
        _State = State.AIR;
        CAanim.SetInteger("_state", (int)_State);
        CAanim.SetFloat("_DirX", 0);
        CAanim.SetFloat("_DirY", -1);

    }

    IEnumerator CA_WallClimbing(Collision wall)
    {
        CAanim.SetFloat("_DirY", -1);
        float ChapPosY = wall.collider.bounds.size.y * 0.8f;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        Debug.Log("벽타기");
        float count = 0;
        Vector3 wasPos = this.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            this.transform.position = Vector3.Lerp(wasPos, new Vector3(wasPos.x, 0.37f, wasPos.z), count);
            if(count >= 1)
            {
                this.transform.position = new Vector3(wasPos.x, 0.37f, wasPos.z);
                break;
            }
            yield return null;
        }
        
        //20%\만큼 줄임
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Wall")
        {
            Debug.Log("sid");
            _State = State.WALL;
            CAanim.SetInteger("_state", (int)_State);
            StartCoroutine(CA_WallClimbing(collision));
        }
        if(collision.collider.tag == "Floor" &&_State == State.AIR)
        {
            _State = State.GROUND;
            CAanim.SetInteger("_state", (int)_State);
        }
    }
    IEnumerator Jump(Vector2 dir)
    {

        //this.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x * 100, 10, dir.y * 100));
        Debug.Log("코루틴1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("코루틴 1초뒤");
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        yield break;
        //코루틴 실험 해봐야 할듯
        //하나 돌던중에 다시 실행 시키면 어케되는지
        //http://theeye.pe.kr/archives/2725 여기서 사용한 방법 관련해서 안에 대기시간 같은 값 가져올수 있는건지

    }
}
