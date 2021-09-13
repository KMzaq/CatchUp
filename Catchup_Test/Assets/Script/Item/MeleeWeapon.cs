using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public MeleeWeaponData meleeWeaponData;
    public override void Init()
    {
        base.Init();
        itemData.itemType = ItemType.MELEEWEAPON;
    }
}
