using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField]
    public GameObject Hand;
    [SerializeField]
    private GameObject Used_Weapon;
    public GameObject Weapons;

    [SerializeField]
    private SpriteRenderer Image;

    public delegate void del_Attack();
    public del_Attack Func_attack { get; protected set; }

    enum HANDDIR { LEFT, RIGHT }
    HANDDIR _HandDir = HANDDIR.RIGHT; //enum dir 공유할수있는 방법 있으면 좋을거같음
    Vector2 MouseDir;
    float RotX = 0;
    float WeaponAngle = 0;
    int SpinDir = 1;

    int curInventoryIndex = 0;
    int curItemIndex = 0;

    void Start()
    {
        if (!Used_Weapon)
            Used_Weapon = Inventory.Instance.Inventory_Scroll[0, 0].gameObject;
        Func_attack += Used_Weapon.GetComponent<Weapon>().Attack;
    }

    void Update()
    {
        if (_HandDir == HANDDIR.LEFT) //30보다 작음
        {
            if (Mathf.Abs(WeaponAngle) < 50)
            {
                _HandDir = HANDDIR.RIGHT;
                Hand.transform.localPosition = new Vector3(0.1f, 0, 0);
                RotX = 0;
                SpinDir = 1;
                Image.flipX = false;
            }
        }
        else if (_HandDir == HANDDIR.RIGHT)
        {
            if (Mathf.Abs(WeaponAngle) > 130)
            {
                _HandDir = HANDDIR.LEFT;
                Hand.transform.localPosition = new Vector3(-0.1f, 0, 0);
                RotX = 180;
                SpinDir = -1;
                Image.flipX = true;
            }
        }
    }
    public void AimmingWeapon()
    {
        MouseDir = (PlayerController.Player_Controls.PlayerInput.MousePosition.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(transform.position)).normalized;
        WeaponAngle = Mathf.Atan2(MouseDir.y, MouseDir.x) * Mathf.Rad2Deg;

        Weapons.transform.localRotation = Quaternion.Euler(RotX, 0, WeaponAngle * SpinDir);

    }


    public void SetUsedWeapon(int value)
    {
        Weapon T = null;

        switch(value)
        {
            case 0:
                Func_attack -= Used_Weapon.GetComponent<Weapon>().Attack;
                Inventory.Instance.Inventory_Scroll[curItemIndex, curInventoryIndex].gameObject.SetActive(false);
                curInventoryIndex = value;
                curItemIndex++;
                if (curItemIndex >= 5)
                    curItemIndex = 0;

                if (Inventory.Instance.Inventory_Scroll[curItemIndex, curInventoryIndex] == null)
                {
                    for (var i = 0; i < Inventory.Instance.Inventory_Scroll.GetLength(curInventoryIndex); i++)
                    {
                        var index = i + curItemIndex;
                        if (index >= 5)
                            index -= 5;
                        T = Inventory.Instance.Inventory_Scroll[index, curInventoryIndex];
                        if (T != null)
                        {
                            curItemIndex = index;
                            break;
                        }
                    }
                } else T = Inventory.Instance.Inventory_Scroll[curItemIndex, curInventoryIndex];

                T.gameObject.transform.SetParent(Weapons.transform);
                Used_Weapon = T.gameObject;
                T.gameObject.SetActive(true);
                Func_attack += Used_Weapon.GetComponent<Weapon>().Attack;

                break;
            case 1:

                Func_attack -= Used_Weapon.GetComponent<Weapon>().Attack;
                Inventory.Instance.Inventory_Scroll[curItemIndex, curInventoryIndex].gameObject.SetActive(false);
                curInventoryIndex = value;
                curItemIndex++;
                if (curItemIndex >= 5)
                    curItemIndex = 0;


                T = Inventory.Instance.Inventory_Scroll[curItemIndex, curInventoryIndex];

                if (T == null)
                {
                    for (var i = 0; i < Inventory.Instance.Inventory_Scroll.GetLength(curInventoryIndex); i++)
                    {
                        var index = i + curItemIndex;
                        if (index >= 5)
                            index -= 5;
                        T = Inventory.Instance.Inventory_Scroll[index, curInventoryIndex];
                        if (T != null)
                            break;
                    }
                }

                T.gameObject.transform.SetParent(Weapons.transform);
                Used_Weapon = T.gameObject;
                T.gameObject.SetActive(true);
                Func_attack += Used_Weapon.GetComponent<Weapon>().Attack;

                break;
            default:
                SetUsedWeapon(curInventoryIndex);
                return;
        }

    }
}
