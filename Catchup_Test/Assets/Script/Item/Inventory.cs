using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public ItemData[] Inventory_Scroll_1 = new ItemData[5];
    public ItemData[] Inventory_Scroll_2 = new ItemData[5];
    public ItemData Inventory_Active;
    public List<ItemData> Inventory_Passive = new List<ItemData>();

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public bool GetItem(ItemData item)
    {
        switch (item._ItemType)
        {
            case ItemType.Weapon:
                for (var i = 0; i < Inventory_Scroll_1.Length; i++)
                {
                    if (!Instance.Inventory_Scroll_1[i])
                    {
                        Instance.Inventory_Scroll_1[i] = item;
                        return true;
                    }
                }
                for (var i = 0; i < Inventory_Scroll_2.Length; i++)
                {
                    if (!Instance.Inventory_Scroll_2[i])
                    {
                        Instance.Inventory_Scroll_2[i] = item;
                        return true;
                    }
                }
                break;

            case ItemType.Active:
                if(!Inventory_Active)
                    Inventory_Active = item;
                break;

            case ItemType.Passive:
                Inventory_Passive.Add(item);
                break;

            default:
                break;
        }
        return false;
    }
}
