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
    InputControls M_Controls;

    Rigidbody M_Rigidbody;

    PWall_Action M_WallAction;

    private void Awake()
    {
        M_Controls = new InputControls();
    }

    private void OnEnable()
    {
        M_Controls.Enable();
    }

    private void OnDisable()
    {
        M_Controls.Disable();
    }
    void Start()
    {
        _State = State.GROUND;

        //M_Controls.PlayerInput.Movement.performed += _ => Amove();

        M_Rigidbody = GetComponent<Rigidbody>();
        M_WallAction = GetComponent<PWall_Action>();

        
        
    }

    Vector2 MouseDir;
    void Update()
    {
        //Debug.Log("컨트롤러 처음 반복 시작");
        //MouseDir = new Vector2(Input.mousePosition.x - Screen.width * 0.5f, Input.mousePosition.y - Screen.height * 0.5f).normalized;
        //switch (_State)
        //{
        //    case State.GROUND:
        //        Amove();
        //        break;
        //    case State.WALL:
        //        if (Input.GetMouseButtonDown(1))
        //        {
        //            M_WallAction.AJump(MouseDir);
        //            _State = State.AIR;
        //        }
        //        break;
        //    case State.AIR:

        //        break;
        //}
        //Debug.Log("컨트롤러  반복 종료");
        Amove();
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
            M_Rigidbody.velocity = new Vector3(0, 0, 0);
            Debug.Log("추락함");
        }
    }
    private void Amove()
    {
        
        Vector2 inputVelue = M_Controls.PlayerInput.Movement.ReadValue<Vector2>();
        M_Rigidbody.MovePosition(transform.position + new Vector3(inputVelue.x * 0.01f * speed, 0, inputVelue.y * 0.01f * speed));
        Debug.Log(inputVelue);
    }
}
