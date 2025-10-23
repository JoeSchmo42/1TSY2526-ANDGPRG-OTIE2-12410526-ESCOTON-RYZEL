using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float lifetime = 5f;
    public bool isPlayerBullet = true;

    void Start()
    {
        // Destroy bullet after lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only process collisions for player bullets hitting enemies
        if (isPlayerBullet && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}