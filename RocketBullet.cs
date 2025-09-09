using UnityEngine;

public class RocketBullet : Bullet
{
    public float rotateSpeed = 200f;
    private Transform target;
    [Range(0, 100)]
    public float minSpeed = 10f;
    [Range(0, 100)]
    public float maxSpeed = 20f;
    protected override void Start()
    {
        base.Start();
        speed = Random.Range(minSpeed, maxSpeed);
    }
    protected override void Update()
    {
        if (target == null)
        {
            FindTarget();
            transform.Translate(Vector2.right * (speed + playerController.currentSpeed) * Time.deltaTime);
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            direction.z = 0;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.Translate(Vector2.right * (speed + playerController.currentSpeed) * Time.deltaTime);
        }
    }
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;
        float playerX = playerController.transform.position.x;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.x <= playerX)
                continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearestEnemy = enemy.transform;
            }
        }
        target = nearestEnemy;
    }
}
