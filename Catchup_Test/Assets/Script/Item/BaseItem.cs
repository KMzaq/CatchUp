using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BaseItemData
{
    public int index;
    public ItemType itemType;
    public string name;
    public ItemTear itemTear;
    public string itemDescription;

}

public enum ItemType
{
    MELEEWEAPON,
    RANGEDWEAPON,
    ACTIVEITEM,
    PASSIVEITEM,
}

public enum ItemTear
{
    NORMAL,
    EPIC,
    UNIQUE,
    LEGENDERY,
}

//:arrow_forward: 근접무기: 아이템번호 / 아이템종류 / 이름 / 등급 / 공격력(최소 / 최대) / 공격속도 / 설명글 / 효과
//아이템번호, 아이템 종류, 이름, 등급, 분류, 공격력, 탄창, 탄약, 탄속/s, 공격딜레이/s, 사거리, , 넉백, 집탄률, 효과


//무기별 데이터의 이름
// 저 분류나 종류 같은 이넘으로 표시할 수 있는것들의 이름

[System.Serializable]
public struct MeleeWeaponData
{
    public float minDamage;
    public float maxDamage;
    public float attackSpeed;
    public EffectType effectType;

    public enum EffectType
    {
        NORMAL,
        SMITE,
        AMPLIFICATION,
        ACTIVATIOM,

    }
}

[System.Serializable]
public struct RangedWeaponData
{
    public float damage;
    public int maxBulletNum;
    public int loadedBullet; // 한번에 장전 가능한 총알 수
    public float bulletSpeed;
    public float reloadTime;
    public float delayTime;
    public float attackRange;
    public float knockBack;
    public float bulletSpread;
    public EffectType effectType;

    public enum EffectType
    {
        SINGLE,
        REPEAT,
        SINGLEANDREPEAT,
        MULTIFUL, //샷건

    }
}

[System.Serializable]
public struct ActiveItemData
{
    public float coolTime;
    public EffectType effectType;
    public enum EffectType
    {
        INFINITY,
        PRODUCTION,
        CONSUMABLE, //소모
        BUFF, // 버프

    }
}

public abstract class BaseItem : MonoBehaviour
{
    public BaseItemData itemData;

    private void Start()
    {
        Init();
    }
    public virtual void Init()
    {
        //GetitemData
    }

}


