using UnityEngine;

public abstract class Buff
{
    public float Duration { get; protected set; } // Длительность баффа
    protected float timer;

    public bool IsActive => timer < Duration;

    public Buff(float duration)
    {
        Duration = duration;
        timer = 0f;
    }

    public virtual void OnActivate()
    {
        timer = 0f;
    }
    public virtual void OnUpdate(float deltaTime)
    {
        timer += deltaTime;
    }
    public virtual bool OnCollision(GameObject collider)
    {
        return false; // Возвращает true, если бафф поглотил столкновение
    }
    public virtual void OnDeactivate()
    {
        Debug.Log("Deactivated");
    }
}