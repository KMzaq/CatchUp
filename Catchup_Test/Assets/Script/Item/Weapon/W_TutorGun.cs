using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_TutorGun : RangedWeapon
{
    public GameObject bullet;
    public GameObject bulletSpawnPos;
    public override void Init()
    {
        base.Init();
    }

    public override void Attack()
    {
        GameObject go = Instantiate(bullet);
        go.transform.position = bulletSpawnPos.transform.position;
        go.transform.rotation = bulletSpawnPos.transform.rotation;
        Vector2 vector = (PlayerController.Player_Controls.PlayerInput.MousePosition.ReadValue<Vector2>() - new Vector2(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;
        Debug.Log(vector);
        go.GetComponent<BulletController>().ShootSetting(rangedWeaponData.damage, rangedWeaponData.bulletSpeed, new Vector3(vector.x, 0, vector.y));
        
        var T = go.GetComponent<BulletController>();
        T._damage = rangedWeaponData.damage;
        
    }
}
