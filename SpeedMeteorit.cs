using UnityEngine;

public class SlowMeteor : Meteor
{
    public float speedAmount = 0.5f;
    public float speedDuration = 3f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                player.ApplySlow(speedAmount, speedDuration);
            }      
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}