using UnityEngine;

public class UnoRay : MonoBehaviour
{
    public int Damage;
    public int DamageStay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.TakeDamage(Damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.TakeDamage(DamageStay);
        }
    }
}
