using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : BaseItem
{
    
    public override void Init()
    {
        base.Init();
        itemData.itemType = ItemType.PASSIVEITEM;
    }

    public abstract void OnEffect();
}
