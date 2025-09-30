using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Tooltip("Amount of damage dealt to player per attack")]
    public int damage = 10;
    
    [Tooltip("Maximum health points of the enemy")]
    public float max_health = 5;
    
    [Tooltip("Current health points of the enemy")]
    public float health;
    
    [Tooltip("Reference to the player's stats component for dealing damage")]
    public PlayerStats playerStats;
    
    [Tooltip("Reference to the EnemyDrops component")]
    private EnemyDrops enemyDrops;
    
    protected bool dead = false;
    
    protected virtual void Awake()
    {
        enemyDrops = GetComponent<EnemyDrops>();
    }
    
    protected virtual void OnEnable()
    {
        health = max_health;
        dead = false;
    }
    
    public virtual float TakeDamage(float dmg)
    {
        if (playerStats != null)
        {
            dmg = playerStats.ApplyStrength(dmg);
        }
        
        health -= dmg;
        
        if (health <= 0 && !dead)
        {
            KillEnemy();
            return -1;
        }
        
        return Mathf.Max(health, 0f);
    }
    
    public virtual void KillEnemy()
    {
        dead = true;
        
        // Handle drops before deactivating
        if (enemyDrops != null)
        {
            enemyDrops.DropCommons();
        }
        
        // Deactivate the GameObject using pool manager
        GameObjectPoolManager.Deactivate(gameObject);
    }
    
    public virtual int DealDamageToPlayer()
    {
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
            return damage;
        }
        return 0;
    }
    
    public bool IsDead()
    {
        return dead;
    }
    
    public float GetHealthPercentage()
    {
        return Mathf.Clamp01(health / max_health);
    }
}