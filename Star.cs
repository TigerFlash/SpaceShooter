using UnityEngine;

public abstract class Star : MonoBehaviour
{
    public int points = 10;
    protected virtual void Start()
    {
        Destroy(gameObject, 15f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerScore = collision.GetComponent<PlayerController>();
        if (playerScore != null)
        {
            GivePoints(playerScore);
            Destroy(gameObject);
        }
    }
    protected abstract void GivePoints(PlayerController playerScore);
}