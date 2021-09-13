using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            //에너미의 체력빼는함수 실행 데미지 매개변수로 넣어서
            
        }
    }
    public void EndAnim()
    {
        Destroy(this.gameObject);
    }
}
