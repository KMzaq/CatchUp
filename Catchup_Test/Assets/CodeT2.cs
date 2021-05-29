using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodeT2 : MonoBehaviour
{
    GetToTouchPoint gtp = new GetToTouchPoint();
    Vector2 mp; //MousePosition

    Dictionary<KeyCode, Action> m_keyCallFunc;

    [SerializeField]
    private float speed;

    int JumpCount;
    int MaxJumpCount;

    [SerializeField]
    GameObject sidsid;

    Animator CAanim;

    //애니메이션관련
    int State = 3;//1 = 바닥, 2 = 벽, 3 = 공중
    bool IsMove;
    Vector2 State_wall; //1234상하좌우
    
    private void Awake()
    {
        gtp.MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        m_keyCallFunc = new Dictionary<KeyCode, Action>()
        {
            // { KeyCode.A, KeyDown_A },
            //{ KeyCode.W, KeyDown_W },
            //{ KeyCode.S, KeyDown_S },
            //{ KeyCode.D, KeyDown_D }
        };

        if(this.GetComponent<Animator>() != null)
            CAanim = this.GetComponent<Animator>();
    }
    void Start()
    {
        MaxJumpCount = 3;
        JumpCount = MaxJumpCount;
        CAanim.SetInteger("_state", State);
    }

    // Update is called once per frame
    void Update()
    {
        CA_move();
        print(State);
        if (Input.anyKeyDown)
        {
            foreach (var dic in m_keyCallFunc)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }

        //점프
        if (Input.GetMouseButton(1))
        {
            Time.timeScale = 0.5f;
            sidsid.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            sidsid.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (State != 1 && JumpCount != 0)
            {
                mp = gtp.GetMousePos();
                
                State = 3;
                CAanim.SetInteger("_state", State);

                this.GetComponent<Rigidbody2D>().velocity = new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y).normalized * speed;

                JumpCount--;

                StopCoroutine("Jump");
                StartCoroutine("Jump");
            }
        }

        if (this.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            CAanim.SetFloat("_DirX", 0.0f);
        }
        else if (this.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            CAanim.SetFloat("_DirX", 1.0f);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (this.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            CAanim.SetFloat("_DirX", 1.0f);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    float jumptime;
    float velX;
    float velY;
    IEnumerator Jump()
    {
        jumptime = 0;

        velX = this.GetComponent<Rigidbody2D>().velocity.x;
        velY = this.GetComponent<Rigidbody2D>().velocity.y;

        yield return new WaitForSeconds(1.00f);
        while (State == 3)
        {
            yield return new WaitForSeconds(0.01f);
            jumptime += 0.01f;

            if (jumptime >= 1)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                State = 1;
                CAanim.SetInteger("_state", State);
                yield break;
            }
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(velX * (float)(1 - jumptime), velY * (float)(1 - jumptime));
        }
        yield break;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StopCoroutine("Jump");

            ContactPoint2D Contact = collision.contacts[0];
            Vector2 Wallpos = collision.collider.bounds.center;
            Vector2 Wallsize = collision.collider.bounds.size * 0.5f;

            //print(Contact.point.x);
            //print(collision.collider.bounds.size);
            //print(Wallpos);//소수점 첫자리까지만 나옴
            //print(collision.collider.bounds.center);//콜라이더의 중심

            //벽부위
            if (Contact.point.y >= Wallpos.y + Wallsize.y && Contact.point.x > Wallpos.x - Wallsize.x && Contact.point.x < Wallpos.x + Wallsize.x)//상
            {
                print("상");
                State_wall = Vector2.up;
                sidsid.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if(Contact.point.y <= Wallpos.y - Wallsize.y && Contact.point.x > Wallpos.x - Wallsize.x && Contact.point.x < Wallpos.x + Wallsize.x) //하
            {
                print("하");
                State_wall = Vector2.down;
                sidsid.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
            else if(Contact.point.x <= Wallpos.x - Wallsize.x && Contact.point.y > Wallpos.y - Wallsize.y && Contact.point.y < Wallpos.y + Wallsize.y) //좌
            {
                print("좌");
                State_wall = Vector2.left;
                sidsid.transform.localEulerAngles = new Vector3(0, 0, 90);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(Contact.point.x >= Wallpos.x + Wallsize.x && Contact.point.y > Wallpos.y - Wallsize.y && Contact.point.y < Wallpos.y + Wallsize.y) //우
            {
                print("우");
                State_wall = Vector2.right;
                sidsid.transform.localEulerAngles = new Vector3(0, 0, 270);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }


            

            if (State == 3 || State == 1)
            {
                State = 2;
                JumpCount = MaxJumpCount;
                CAanim.SetInteger("_state", State);
                CAanim.SetFloat("_DirX", State_wall.x);
                CAanim.SetFloat("_DirY", State_wall.y);
            }
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {


        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        
    }

    //private void KeyDown_A()
    //{
    //    Debug.Log("A");
    //    if (State == 1)
    //    {
    //       this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 0.001f, ForceMode2D.Impulse);
    //    }
    //}
    //private void KeyDown_W()
    //{
    //    Debug.Log("W");
        
    //    if (State == 1)
    //        this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 0.001f, ForceMode2D.Impulse);
    //}
    //private void KeyDown_S()
    //{
    //    Debug.Log("S");
    //    if (State == 1)
    //        this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 0.001f, ForceMode2D.Impulse);
    //}
    //private void KeyDown_D()
    //{
    //    Debug.Log("D");
    //    if (State == 1)
    //        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.001f, 0), ForceMode2D.Impulse);
    //}
    private void CA_move()
    {
        if(State == 1)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * speed);
            CAanim.SetFloat("_DirX", inputX);
            CAanim.SetFloat("_DirY", inputY);
        }
    }

    private void FlipY()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipY = !this.gameObject.GetComponent<SpriteRenderer>().flipY;
    }
}
