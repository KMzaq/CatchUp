using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Hand;
    [SerializeField]
    private GameObject Used_Weapon;

    void Start()
    {
        
    }

    void Update()
    {
        handposchange();
        AimmingHand();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    WeaponFlip(); //플립 조건 설정 필요 ex엔터더건전
        //}
    }

    private void handposchange()
    {
        //Vector3 MouseDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        //Hand.transform.localPosition = MouseDir * 0.1f;
    }
    private void AimmingHand()
    {
        //Vector3 MouseDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        //Hand.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(MouseDir.y, MouseDir.x) * Mathf.Rad2Deg);
    }

    private void WeaponFlip()
    {
        //Used_Weapon.GetComponent<SpriteRenderer>().flipY = !Used_Weapon.GetComponent<SpriteRenderer>().flipY;
    }
}
