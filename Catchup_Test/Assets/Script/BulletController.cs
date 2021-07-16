using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int _damage { get; private set; }
    float _speed;
    Vector3 _moveDir;
    void Update()
    {
        transform.Translate(_moveDir * 0.01f * _speed);
    }

    public void ShootSetting(int damage, float speed, Vector3 Dir)
    {
        _damage = damage;
        _speed = speed;
        _moveDir = new Vector3(Dir.x, 0, Dir.y);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }
}
