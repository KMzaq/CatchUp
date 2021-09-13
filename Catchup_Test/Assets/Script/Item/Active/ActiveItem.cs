using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItem : BaseItem
{
    public ActiveItemData activeItemData;
    public override void Init()
    {
        base.Init();
        itemData.itemType = ItemType.ACTIVEITEM;
    }

    public abstract void UseSkill();
}
