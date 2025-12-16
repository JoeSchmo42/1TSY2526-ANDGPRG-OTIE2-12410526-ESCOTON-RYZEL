using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] int damage = 25;
    [SerializeField] float lifetime = 10f;

    private float timer = 0f;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                OnHit(enemy);
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnHit(EnemyScript enemy)
    {
        enemy.TakeDamage(damage);
    }
}
