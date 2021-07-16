using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    //상태
    enum State : int{ GROUND, WALL, AIR, DEAD }
    public enum Dir : int{ UP, DOWN, LEFT, RIGHT }
    [SerializeField]
    State _State;
    [SerializeField]
    Dir _WallDir;

    //스테이터스 나중에 값 가져오게 설정
    [SerializeField]
    private float speed;

    //기능들
    Rigidbody M_rigidbody;

    PWall_Action M_WallAction;

    void Start()
    {
        _State = State.GROUND;
        M_rigidbody = GetComponent<Rigidbody>();
        M_WallAction = GetComponent<PWall_Action>();

        
        
    }

    Vector2 MouseDir;
    void Update()
    {
        Debug.Log("컨트롤러 처음 반복 시작");
        MouseDir = new Vector2(Input.mousePosition.x - Screen.width * 0.5f, Input.mousePosition.y - Screen.height * 0.5f).normalized;
        switch (_State)
        {
            case State.GROUND:
                Amove();
                break;
            case State.WALL:
                if (Input.GetMouseButtonDown(1))
                {
                    M_WallAction.AJump(MouseDir);
                    _State = State.AIR;
                }
                break;
            case State.AIR:

                break;
        }
        Debug.Log("컨트롤러  반복 종료");
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Wall" && _State != State.WALL)
        {
            _State = State.WALL;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            M_WallAction.AWallClimb(collision);
            _WallDir = (Dir)M_WallAction.Cheak_WallDirection(collision);
            print(_WallDir);
            
        }
        if (collision.collider.tag == "Floor" && _State == State.AIR)
        {
            _State = State.GROUND;
            M_rigidbody.velocity = new Vector3(0, 0, 0);
            Debug.Log("추락함");
        }
    }
    private void Amove()
    {
        //이동 벨로시티이동으로 변경
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        //transform.Translate(new Vector3(inputX, 0, inputZ) * Time.deltaTime * speed);
        //M_rigidbody.velocity = new Vector3(inputX * speed, 0, inputZ * speed);
        M_rigidbody.MovePosition(transform.position + new Vector3(inputX * 0.01f * speed, 0, inputZ * 0.01f * speed));
    }
}
