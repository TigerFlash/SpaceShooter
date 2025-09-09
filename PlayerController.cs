using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  
    public float autoSpeed = 2f;
    public int maxHp = 100;
    public int hp = 100;
    public float currentSpeed;
    private Camera cam;
    private float minX, maxX, minY, maxY;
    private float halfWidth, halfHeight;
    public float speedIncrement = 1f; // на сколько увеличивается скорость
    public float increaseInterval = 20f; // интервал увеличения скорости в секундах
    private float timer = 0f;
    private List<float> slowEffects = new List<float>(); // список множителей замедления
    public int score = 0;
    public Weapon currentWeapon;
    [SerializeField] 
    private WeaponManager weaponManager;
    [SerializeField]
    private BuffManager buffManager;
    [SerializeField]
    private UIScript _UIScript;
    void Start()
    {
        buffManager = GetComponent<BuffManager>();
        _UIScript.UpdateUI();
        currentSpeed = autoSpeed;
        cam = Camera.main;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Vector3 size = sr.bounds.size;
            halfWidth = size.x / 2f;
            halfHeight = size.y / 2f;
        }
        else
        {
            halfWidth = 0f;
            halfHeight = 0f;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentWeapon.Fire();
        }
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(0, moveY).normalized;
        Vector3 newPosition = transform.position + (Vector3)(movement * speed * Time.deltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, minX + halfWidth, maxX - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minY + halfHeight, maxY - halfHeight);
        transform.position = newPosition;
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            autoSpeed += speedIncrement; 
            timer = 0f; 
        }
    }
    public void ApplySlow(float slowAmount, float duration)
    {
        StartCoroutine(SlowEffect(slowAmount, duration));
    }

    private IEnumerator SlowEffect(float slowAmount, float duration)
    {
        slowEffects.Add(slowAmount);
        UpdateSpeed();

        yield return new WaitForSeconds(duration);

        slowEffects.Remove(slowAmount);
        UpdateSpeed();
    }
    private void UpdateSpeed()
    {
        float multiplier = 1f;
        foreach (float effect in slowEffects)
        {
            multiplier += effect;
        }
        currentSpeed = autoSpeed + multiplier;
    }
    public void AddPoints(int points)
    {
        score += points;
        _UIScript.UpdateUI();
    }
    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        _UIScript.UpdateUI();
        if (hp <= 0)
        {
            Debug.Log("Died");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponPickup"))
        {
            WeaponPickup pickup = collision.GetComponent<WeaponPickup>();
            if (pickup != null)
            {
                weaponManager.PickUpWeapon(pickup.weaponIndex);
                Destroy(collision.gameObject);
            }
        }
    }
}
