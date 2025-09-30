using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage;


    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player!");
            if (other.gameObject.TryGetComponent<PlayerStats>(out var playerStats))
            {
                playerStats.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}