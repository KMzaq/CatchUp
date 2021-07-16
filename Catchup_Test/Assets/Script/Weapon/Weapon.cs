using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public delegate void del_Attack();
    public del_Attack Func_attack { get; protected set; }

    public int _damage { get; protected set; }
    public int _maxBulletNum { get; protected set; }
    public float _attackSpeed { get; protected set; }
    public float _reloadTime { get; protected set; }

}
