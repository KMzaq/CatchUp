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

    //
    Vector2 MousePos = Vector2.zero;
    Vector2 MouseDir = Vector2.zero;
    bool blockAllAction = false;
    bool blockMove = false;
    bool blockAttack = false;

    //스테이터스 나중에 값 가져오게 설정
    [SerializeField]
    private float speed;

    #region Components
    public static InputControls Player_Controls;
    PlayerWeaponManager M_PWeaponManager;
    PWall_Action M_WallAction;

    Rigidbody M_Rigidbody;
    #endregion


    private void Awake()
    {
        Player_Controls = new InputControls();
        M_PWeaponManager = Util.GetorAddComponent<PlayerWeaponManager>(this.gameObject);
        M_WallAction = Util.GetorAddComponent<PWall_Action>(this.gameObject);

        M_Rigidbody = Util.GetorAddComponent<Rigidbody>(this.gameObject);
    }


    private void OnEnable()
    {
        Player_Controls.Enable();
    }

    private void OnDisable()
    {
        Player_Controls.Disable();
    }
    void Start()
    {
        _State = State.GROUND;


        Player_Controls.PlayerInput.Attack.performed += _ => {
            if (blockAttack || blockAllAction) return;
            M_PWeaponManager.Func_attack.Invoke();
        };
        Player_Controls.PlayerInput.Jump.performed += _ => {
            if (_State != State.WALL || blockAllAction) return;
            { M_WallAction.AJump(MouseDir); _State = State.AIR; Debug.Log("as"); }
        };
        Player_Controls.PlayerInput.MousePosition.performed += _ => M_PWeaponManager.AimmingWeapon();

    }

    void Update()
    {
        MousePos = Player_Controls.PlayerInput.MousePosition.ReadValue<Vector2>(); 
        MouseDir = (MousePos - new Vector2(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;
        //Debug.Log(MouseDir);
        switch (_State)
        {
            case State.GROUND:
                Amove();
                break;
            case State.WALL:

                break;
            case State.AIR:

                break;
        }
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
        if (blockMove)
            return;

        Vector2 inputVelue = Player_Controls.PlayerInput.Movement.ReadValue<Vector2>();
        M_Rigidbody.MovePosition(transform.position + new Vector3(inputVelue.x * 0.01f * speed, 0, inputVelue.y * 0.01f * speed));

    }
}
