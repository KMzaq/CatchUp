using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_TutorGun : Weapon
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject muzzle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Attack();
        //}
    }

    public override void Attack()
    {
        Debug.Log("Attack");
        //GameObject Instance = Instantiate(bullet, muzzle.transform.position, Quaternion.identity);
        //Instance.GetComponent<BulletController>().ShootSetting(1, 1f, (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized);
    }
}
