using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int MaxHP;

    public int CurrentHP { get; set; }

    public bool OnDie;

    void Start()
    {
        CurrentHP = MaxHP;
    }

    public void DamagedHP(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
            OnDie = true;
    }

    public void HealedHP(int heal)
    {
        CurrentHP += heal;

        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;
    }

}
