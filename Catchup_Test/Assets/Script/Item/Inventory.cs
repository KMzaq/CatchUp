using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public Weapon[,] Inventory_Scroll = new Weapon[5,2];
    public ActiveItem Inventory_Active;
    public List<PassiveItem> Inventory_Passive = new List<PassiveItem>();

    public GameObject FirstWeapon;
    public GameObject SecondWeapon;


    void Start()
    {
        if (Instance == null)
            Instance = this;


        GameObject go1 = Instantiate(FirstWeapon);
        DontDestroyOnLoad(go1);
        go1.SetActive(false);
        Inventory_Scroll[0, 0] = go1.GetComponent<Weapon>();

        GameObject go2 = Instantiate(SecondWeapon);
        DontDestroyOnLoad(go2);
        go1.SetActive(false);
        Inventory_Scroll[0, 1] = go2.GetComponent<Weapon>();
    }

    public bool GetItem(BaseItem item)
    {
        if (item is Weapon) {
            for (var i = 0; i < Inventory_Scroll.GetLength(0); i++)
            {
                if (!Instance.Inventory_Scroll[i, 0])
                {
                    Instance.Inventory_Scroll[i, 0] = (Weapon)item;
                    DontDestroyOnLoad(item.gameObject);
                    item.gameObject.SetActive(false);

                    return true;
                }
            }
            for (var i = 0; i < Inventory_Scroll.GetLength(1); i++)
            {
                if (!Instance.Inventory_Scroll[i, 1])
                {
                    Instance.Inventory_Scroll[i, 1] = (Weapon)item;
                    DontDestroyOnLoad(item.gameObject);
                    item.gameObject.SetActive(false);

                    return true;
                }
            }
        }
        else if (item is ActiveItem) {
            if (!Inventory_Active)
            {
                Inventory_Active = item as ActiveItem;
                DontDestroyOnLoad(item.gameObject);
                item.gameObject.SetActive(false);

                return true;
            }
        }
        else if (item is PassiveItem) {
            Inventory_Passive.Add(item as PassiveItem);
            return true;
        }
        return false;
    }
}
