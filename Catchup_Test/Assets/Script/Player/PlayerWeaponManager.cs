using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Hand;
    [SerializeField]
    private GameObject Used_Weapon;

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

    void Start()
    {
        SetUsedWeapon();
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

        Used_Weapon.transform.localRotation = Quaternion.Euler(RotX, 0, WeaponAngle * SpinDir);

    }

    private void SetUsedWeapon()
    {
        Func_attack -= Used_Weapon.GetComponent<Weapon>().Attack;
        //무기가 변경된 후 -를 하면 전에것이 빠지는가 아님 무기가 달라서 안빠지는가 실험 필요

        Func_attack += Used_Weapon.GetComponent<Weapon>().Attack;
    }

}
