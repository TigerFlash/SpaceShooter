using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1f; 
    public PlayerController playerController;
    protected float lastFireTime;
    public Image reloadImage;
    public virtual void Fire()
    {
        if (Time.time - lastFireTime >= 1f / fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
            StartCoroutine(ReloadCoroutine());
        }
    }
    protected abstract void Shoot();
    protected IEnumerator ReloadCoroutine()
    {
        float reloadDuration = 1f / fireRate;
        float elapsed = 0f;
        reloadImage.fillAmount = 0f;
        while (elapsed < reloadDuration)
        {
            elapsed += Time.deltaTime;
            reloadImage.fillAmount = Mathf.Clamp01(elapsed / reloadDuration);
            yield return null;
        }
        reloadImage.fillAmount = 1f;
    }
}