using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    void Update()
    {
        if (player != null)
        {
            Vector3 currentPosition = transform.position;
            float targetX = player.position.x;
            Vector3 targetPosition = new Vector3(targetX, currentPosition.y, currentPosition.z);
            transform.position = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
