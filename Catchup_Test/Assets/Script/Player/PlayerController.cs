using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController
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


    //스테이터스 나중에 값 가져오게 설정
    [SerializeField]
    private float speed;

    #region Components
    public static InputControls Player_Controls;
    public static Animator Player_Animator;
    PlayerWeaponManager M_PWeaponManager;
    PWall_Action M_WallAction;

    Rigidbody M_Rigidbody;

    public SpriteRenderer mImage;
    #endregion


    private void Awake()
    {
        Player_Controls = new InputControls();
        Player_Animator = Util.FindChild<Animator>(this.gameObject);

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
        mImage.flipX = false;
        Player_Animator.SetInteger("_state", (int)_State);

        Player_Controls.PlayerInput.Attack.performed += _ => {
            if (blockAttack || blockAllAction) return;
            M_PWeaponManager.Func_attack.Invoke();
        };
        Player_Controls.PlayerInput.Jump.performed += _ => {
            if (_State != State.WALL || blockAllAction) return;
            { M_WallAction.AJump(MouseDir); _State = State.AIR;


                if(MouseDir.x > 0.5f)
                    Player_Animator.SetFloat("_DirX", 1);
                else if(MouseDir.x < -0.5f)
                    Player_Animator.SetFloat("_DirX", -1);
                else Player_Animator.SetFloat("_DirX", MouseDir.x);

                if (MouseDir.y > 0.5f)
                    Player_Animator.SetFloat("_DirY", 1);
                else if (MouseDir.y < -0.5f)
                    Player_Animator.SetFloat("_DirY", -1);
                else Player_Animator.SetFloat("_DirY", MouseDir.y);


                Player_Animator.SetInteger("_state", (int)_State);
                mImage.flipX = false;
            }
        };

        Player_Controls.PlayerInput.UseActive.performed += _ => Inventory.Instance.Inventory_Active.UseSkill();
        Player_Controls.PlayerInput.MousePosition.performed += _ => M_PWeaponManager.AimmingWeapon();
        Player_Controls.PlayerInput.ChangeWeapon.performed += _ => M_PWeaponManager.SetUsedWeapon(0);
        Player_Controls.PlayerInput.ChangeWeapon2.performed += _ => M_PWeaponManager.SetUsedWeapon(1);

    }

    void Update()
    {
        MousePos = Player_Controls.PlayerInput.MousePosition.ReadValue<Vector2>(); 
        MouseDir = (MousePos - new Vector2(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

        //수정필수코드
        if (blockAllAction)
            M_PWeaponManager.Hand.SetActive(false);
        else
            M_PWeaponManager.Hand.SetActive(true);

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
            _WallDir = (Dir)M_WallAction.Cheak_WallDirection(collision);
            M_WallAction.AWallClimb(collision);
            Player_Animator.SetInteger("_state", (int)_State);
            switch(_WallDir)
            {
                case (Dir)0:
                    Player_Animator.SetFloat("_DirX", 0);
                    Player_Animator.SetFloat("_DirY", 1);
                    break;
                case (Dir)1:
                    Player_Animator.SetFloat("_DirX", 0);
                    Player_Animator.SetFloat("_DirY", -1);
                    break;
                case (Dir)2:
                    Player_Animator.SetFloat("_DirX", -1);
                    Player_Animator.SetFloat("_DirY", 0);
                    break;
                case (Dir)3:
                    Player_Animator.SetFloat("_DirX", 1);
                    Player_Animator.SetFloat("_DirY", 0);
                    break;
            }
           
            
        }
        if (collision.collider.tag == "Floor" && _State == State.AIR)
        {
            _State = State.GROUND;
            Player_Animator.SetInteger("_state", (int)_State);
            PlayerController.Player_Animator.SetTrigger("_IsLanded");
            M_Rigidbody.velocity = new Vector3(0, 0, 0);
            Debug.Log("추락함");
        }
    }
    private void Amove()
    {
        if (blockMove)
            return;

        Vector2 inputVelue = Player_Controls.PlayerInput.Movement.ReadValue<Vector2>();

        if (inputVelue != Vector2.zero)
        {
            if (MouseDir.x > 0.5f)
                Player_Animator.SetFloat("_DirX", 1);
            else if (MouseDir.x < -0.5f)
                Player_Animator.SetFloat("_DirX", -1);
            else Player_Animator.SetFloat("_DirX", MouseDir.x);

            if (MouseDir.y > 0.5f)
                Player_Animator.SetFloat("_DirY", 1);
            else if (MouseDir.y < -0.5f)
                Player_Animator.SetFloat("_DirY", -1);
            else Player_Animator.SetFloat("_DirY", MouseDir.y);
        }
        else Player_Animator.SetFloat("_DirX", 0);
      M_Rigidbody.MovePosition(transform.position + new Vector3(inputVelue.x * 0.01f * speed, 0, inputVelue.y * 0.01f * speed));

    }
}
