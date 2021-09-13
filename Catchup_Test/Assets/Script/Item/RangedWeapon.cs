using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    public RangedWeaponData rangedWeaponData;
    public override void Init()
    {
        base.Init();
        itemData.itemType = ItemType.RANGEDWEAPON;
    }
}

