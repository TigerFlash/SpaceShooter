using UnityEngine;

public abstract class Meteor : MonoBehaviour
{
    public float speed = 0.4f;
    public float rotationSpeed = 40f;
    public int damage;
    public int hp;
    protected virtual void Start()
    {
        Destroy(gameObject, 15f);
    }
    protected virtual void Update()
    {
        Move();
        Rotate();
    }
    protected virtual void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    protected virtual void Rotate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            BuffManager buffManager = playerController.GetComponent<BuffManager>();
            if (buffManager.HandleCollision(this.gameObject))
            {
                Destroy(gameObject);
                return;
            }
            playerController.TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}