using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_TutorSword : MeleeWeapon
{
    public GameObject weapon;
    public GameObject effect;
    
    public override void Attack()
    {
        GameObject go = Instantiate(effect);
        Debug.Log(go.GetComponent<Effect>().damage = Random.Range((int)meleeWeaponData.minDamage, (int)meleeWeaponData.maxDamage));
        
        go.transform.rotation = this.transform.rotation;
        Debug.Log((go.transform.forward * 10f));
        Vector2 vector = (PlayerController.Player_Controls.PlayerInput.MousePosition.ReadValue<Vector2>() - new Vector2(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;
        go.transform.position = PlayerController.Player_Animator.gameObject.transform.position + (new Vector3(vector.x, 0, vector.y) * 0.25f);
        weapon.transform.localEulerAngles = new Vector3(weapon.transform.localEulerAngles.x, weapon.transform.localEulerAngles.y,180 - weapon.transform.localEulerAngles.z);
    }
}
