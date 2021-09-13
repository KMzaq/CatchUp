using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float _damage;
    public float _speed;
    Vector3 _moveDir;
    float curtime;
    void Update()
    {
        transform.position += (_moveDir * 0.001f * _speed);

        curtime += Time.deltaTime;
        if (curtime >= 5)
            Destroy(this.gameObject);
    }

    public void ShootSetting(float damage, float speed, Vector3 Dir)
    {
        _damage = damage;
        _speed = speed;
        _moveDir = new Vector3((Dir.x), 0, (Dir.z));
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //에너미의 체력빼는함수 실행 데미지 매개변수로 넣어서

            Destroy(this.gameObject);
        }
        if (collision.collider.tag == "Wall")
            Destroy(this.gameObject);
    }
}
