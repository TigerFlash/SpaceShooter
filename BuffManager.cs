using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private List<Buff> activeBuffs = new List<Buff>();
    public GameObject shieldVisual;
    private ShieldBuff activeShieldBuff;
    [Header("Shield Settings")]
    public float shieldDuration = 999999f;
    public float shieldRechargeTime = 10f;
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            Buff buff = activeBuffs[i];
            buff.OnUpdate(deltaTime);
            if (!buff.IsActive)
            {
                buff.OnDeactivate();
                activeBuffs.RemoveAt(i);
            }
        }
        if (activeShieldBuff != null)
        {
            UpdateShieldBuffParameters();
            var spriteRenderer = shieldVisual.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = activeShieldBuff.IsShieldActive ? new Color(0.45f, 0.5f, 0.85f, 0.1f) : new Color(0.8f, 0.45f, 0.45f, 0.1f);
            }
        }
        else
        {
            //shieldVisual.SetActive(false);
        }
    }
    public void AddBuff(Buff buff)
    {
        buff.OnActivate();
        activeBuffs.Add(buff);

        if (buff is ShieldBuff shieldBuff)
        {
            activeShieldBuff = shieldBuff;
            shieldVisual.SetActive(true);
        }
    }
    public bool HandleCollision(GameObject collider)
    {
        foreach (var buff in activeBuffs)
        {
            if (buff.OnCollision(collider))
                return true;
        }
        return false;
    }
    public void ActivateShield()
    {
        ShieldBuff shield = new ShieldBuff(shieldDuration, shieldRechargeTime);
        AddBuff(shield);
    }
    public void UpdateShieldBuffParameters()
    {
        if (activeShieldBuff != null)
        {
            activeShieldBuff.UpdateParameters(shieldRechargeTime);
        }
    }
}