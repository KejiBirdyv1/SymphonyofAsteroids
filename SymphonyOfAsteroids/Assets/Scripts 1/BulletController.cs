using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15f; 
    public float lifetime = 0.3f; 
    public float trailDuration = 0.3f; 

    private Vector3 direction;

    private TrailRenderer trailRenderer;

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
        Destroy(gameObject, lifetime);

        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
