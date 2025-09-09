using UnityEngine;

public class ShieldBuff : Buff
{
    private float rechargeTime;
    private float rechargeTimer;
    private bool shieldActive;
    public bool IsShieldActive => shieldActive;
    public ShieldBuff(float shieldDuration, float rechargeTime) : base(shieldDuration)
    {
        this.rechargeTime = rechargeTime;
        rechargeTimer = 0f;
        shieldActive = true;
    }

    public override void OnActivate()
    {
        base.OnActivate();
        shieldActive = true;
        rechargeTimer = 0f;
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (!shieldActive)
        {
            rechargeTimer += deltaTime;
            if (rechargeTimer >= rechargeTime)
            {
                shieldActive = true;
                rechargeTimer = 0f;
            }
        }
    }
    public override bool OnCollision(GameObject collider)
    {
        if (!shieldActive)
            return false;
        if (collider.CompareTag("Enemy"))
        {
            shieldActive = false;
            rechargeTimer = 0f;
            Debug.Log("Щит поглотил столкновение с метеоритом");
            return true;
        }
        return false;
    }
    public void UpdateParameters(float newRechargeTime)
    {
        this.rechargeTime = newRechargeTime;
    }
}