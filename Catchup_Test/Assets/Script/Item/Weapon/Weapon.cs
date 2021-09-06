using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{

    public int _damage { get; protected set; }
    public int _maxBulletNum { get; protected set; }
    public float _attackSpeed { get; protected set; }
    public float _reloadTime { get; protected set; }

    public abstract void Attack(); 
}
