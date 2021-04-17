using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;   //action에 필요

public class CodeTest : MonoBehaviour
{
    // Start is called before the first frame update
    GetToTouchPoint gtp = new GetToTouchPoint();
    Vector2 mp; //MousePosition


    //JUMP
    bool IsJumping;

    int State = 3;//1 = 바닥, 2 = 벽, 3 = 공중

    [SerializeField]
    int JumpCount;
    int MaxJumpCount;

    float Distance;
    Vector2 PastPos;
    
    [SerializeField]
    private float speed;


    Dictionary<KeyCode, Action> m_keyCallFunc;



    void Start()
    {
        gtp.MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        MaxJumpCount = 3;
        JumpCount = MaxJumpCount;


        m_keyCallFunc = new Dictionary<KeyCode, Action>()
        {
             { KeyCode.A, KeyDown_A },
            { KeyCode.W, KeyDown_W },
            { KeyCode.S, KeyDown_S },
            { KeyCode.D, KeyDown_D }
        };

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Animator>().SetInteger("_state", State);
        print(this.GetComponent<Rigidbody2D>().velocity);
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
        
        if (Input.GetMouseButton(1))
        {
            Time.timeScale = 0.5f;
        }
        else Time.timeScale = 1;
        if (Input.GetMouseButtonUp(1))    
        {
            if (JumpCount != 0)
            {
                State = 3;

                PastPos = new Vector2(this.transform.position.x, this.transform.position.y);

                mp = gtp.GetMousePos();
                

                this.GetComponent<Rigidbody2D>().velocity = new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y).normalized * speed;
                //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y).normalized * speed, ForceMode2D.Force);

                IsJumping = true;
                JumpCount--;
                jumptime = 0;

                velX = this.GetComponent<Rigidbody2D>().velocity.x;
                velY = this.GetComponent<Rigidbody2D>().velocity.y;

                StopCoroutine("Jump");
                StartCoroutine("Jump");
            }
        }

        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    print("냥냥");
        //}
        
        
    }

    float jumptime;
    float velX;
    float velY;
    IEnumerator Jump()
    {
        //mp = gtp.GetMousePos();

        //this.GetComponent<Rigidbody2D>().velocity = new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y).normalized * speed;
        //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y).normalized * speed, ForceMode2D.Force);

        //IsJumping = true;
        //JumpCount--;
        //jumptime = 0;

        yield return new WaitForSeconds(1.00f);
        while (IsJumping == true)
        {
            yield return new WaitForSeconds(0.01f);
            jumptime += 0.01f;

            if (jumptime >= 1) yield break;

            //Distance = Vector2.Distance(PastPos, new Vector2(this.transform.position.x, this.transform.position.y));
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(velX *(float)(1-jumptime), velY *(float)(1-jumptime));
            //Debug.Log(JumpCount);

            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(mp.x - PastPos.x, mp.y - PastPos.y).normalized * speed * (-3f*jumptime*jumptime + 6f * jumptime);

            Debug.Log(-3f*jumptime*jumptime + 6f * jumptime);
            
        }
        yield break;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Wall")
        {
            State = 2;
            if (IsJumping == true)
            {
                IsJumping = false;
                JumpCount = MaxJumpCount;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StopCoroutine("Jump");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Wall")
        {
            if (IsJumping == true)
            {
                IsJumping = false;
                JumpCount = MaxJumpCount;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //StopCoroutine("Jump");
            }
            Debug.Log(JumpCount);
            State = 2;


        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y).normalized * speed;

        IsJumping = true;
        JumpCount--;
        jumptime = 0;

        //StartCoroutine("Jump");
    }

    private void KeyDown_A()
    {
        Debug.Log("A");
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 0.001f, ForceMode2D.Impulse);
    }
    private void KeyDown_W()
    {
        Debug.Log("W");
        print(KeyCode.W);
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 0.001f, ForceMode2D.Impulse);
    }
    private void KeyDown_S()
    {
        Debug.Log("S");
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 0.001f, ForceMode2D.Impulse);
    }
    private void KeyDown_D()
    {
        Debug.Log("D");
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.001f,0), ForceMode2D.Impulse);
    }
}
