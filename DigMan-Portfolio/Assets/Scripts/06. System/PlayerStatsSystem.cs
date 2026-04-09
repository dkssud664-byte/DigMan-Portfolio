using System;
using UnityEngine;

public class PlayerStatsSystem
{
    public int MaxHP { get; private set; }
    public float CurrentHP { get; private set; }

    public float MaxStamina { get; private set; }
    public float CurrentStamina { get; private set; }
    private float recoverTime = 12f;

    public bool IsDead => CurrentHP <= 0f;

    public event Action<float> OnHPChanged;
    public event Action<float> OnStaminaChanged;
    public event Action OnDead;

    public void Init(PlayerInfo info)
    {
        MaxHP = info.MaxHp;
        CurrentHP = info.Hp;

        MaxStamina = info.MaxStamina;
        CurrentStamina = info.Stamina;
    }

    public bool UseStamina(float amount)
    {
        if (CurrentStamina < amount)
        {
            return false;
        }

        CurrentStamina -= amount;
        OnStaminaChanged?.Invoke(CurrentStamina / MaxStamina);
        {
            return true;
        }
    }

    public void RecoverStamina(float deltaTime)
    {
        float recoverPerSecond = MaxStamina / recoverTime;
        CurrentStamina = Mathf.Min(CurrentStamina + recoverPerSecond * deltaTime, MaxStamina);
        OnStaminaChanged?.Invoke(CurrentStamina / MaxStamina);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        CurrentHP -= damage;
        OnHPChanged?.Invoke(CurrentHP / MaxHP);

        if (CurrentHP <= 0f)
            OnDead?.Invoke();
    }

    public void ResetHp(PlayerStatType type, PlayerInfo info)
    {
        if(type != PlayerStatType.hp)
        {
            return;
        }

        MaxHP = info.MaxHp;
        CurrentHP = MaxHP;
        OnHPChanged?.Invoke(CurrentHP / MaxHP);
    }
}
