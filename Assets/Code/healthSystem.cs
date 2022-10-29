using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHeal;

    public int HealthAmountMax => healthAmountMax;
    public int HealthAmount => healthAmount;

    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void TakeDamege(int damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
            
    }

    public void HealFullHP()
    {
        healthAmount = healthAmountMax;
        OnHeal?.Invoke(this, EventArgs.Empty);
    }

    public void HealSomeHP(int healAmount)
    {
        healthAmount += healAmount;
        OnHeal?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public bool IsFullHP()
    {
        if (healthAmount == healthAmountMax)
        {
            return true;
        }           
        return false;
    }

    public bool IsDead()
    {
        if (healthAmount == 0)
        {
            return true;
        }            
        return false;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }
    }
}
