using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public PlayerController playerController;
    public int Damage;
    protected virtual void Start()
    {
        Destroy(gameObject, 15f);
    }
    protected virtual void Update()
    {
        transform.Translate(Vector2.right * (speed + playerController.currentSpeed) * Time.deltaTime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Meteor enemy = collision.GetComponent<Meteor>();
            enemy.TakeDamage(Damage);
        }
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Star"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}