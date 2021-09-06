using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "DataFile/ItemData")]
public class ItemData : ScriptableObject
{
    public string _Name;
    public Sprite _Image;
    public ItemType _ItemType;

    public List<Component> ItemList = new List<Component>();
}
public enum ItemType
{
    Weapon,
    Passive,
    Active,
}