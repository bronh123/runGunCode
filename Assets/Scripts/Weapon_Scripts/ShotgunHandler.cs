using UnityEngine;

public class ShotgunHandler : MonoBehaviour
{

    private PublicMover Player;
    private AmmoTracker Weapon;
    public GameObject ShotgunPellet;
    private Transform ProjectileSpawner;
    public float spread;
    public float recoilIntensity;
    private bool hasTouchedGround = false;
    public int projectileCount = 8;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PublicMover>();
        Weapon = GameObject.Find("Shotgun").GetComponent<AmmoTracker>();
        // ShotgunPellet = GameObject.Find("ShotgunPellet");
        ProjectileSpawner = GameObject.Find("ShotgunProjectileSpawner").transform;
        Weapon.onEnemyHitEffect = (float a) => { if (a == -1f) { Weapon.reload(1); } };
        Weapon.onHitEffect = (Vector3 pos) => { };
    }

    // Update is called once per frame
    void Update()
    {
        // shoots the gun
        if (Input.GetKeyDown(KeyCode.Mouse0) && Weapon.getAmmo() > 0)
        {
            Fire();
        }

        // checks if the player has touched the ground
        if (Player.isGrounded())
        {
            hasTouchedGround = true;
        }

        /*  
        *   The shotgun is intended to also act as the player's double jump, so
        *   it can't be reloaded until the player has touched the ground.
        *
        *   Once non-instant reload animations are added, consider reloading
        *   automatically on landing.
        */

        // Reloads the gun if the player has touched the ground since they last fired a shot
        if (Input.GetKeyDown(KeyCode.R) && hasTouchedGround)
        {
            Weapon.reload();
        }
    }


    // consolidates all functions called upon firing
    void Fire()
    {
        ApplyRecoil();
        Weapon.fireAmmo(1);
        for (int i = 0; i < projectileCount; i++)
        {
            GameObjectPoolManager.SpawnObject(ShotgunPellet, ProjectileSpawner.position, ProjectileSpawner.rotation * Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread)));
            hasTouchedGround = false;
        }

    }

    // applies the recoil force of the shotgun
    void ApplyRecoil()
    {
        Player.ApplyImpulse(-transform.parent.forward, recoilIntensity);
    }

    #region "weapon stat setters"
    public void SetSpread(float spread)
    {
        this.spread = Mathf.Clamp(spread, 0, 360);
    }

    public void SetRecoilIntensity(float recoilIntensity)
    {
        this.recoilIntensity = Mathf.Max(0, recoilIntensity);
    }

    public void SetProjectileCount(int projectileCount)
    {
        this.projectileCount = Mathf.Max(1, projectileCount);
    }

    public void SetProjectileSpeed(float projectileSpeed)
    {
        ShotgunPellet.GetComponent<GenericProjectileHandler>().projectile_speed = Mathf.Clamp(projectileSpeed, 1, 100);
        GameObjectPoolManager.DestroyAllOfObject(ShotgunPellet);
    }

    public void SetProjectileDamage(float damageAdj)
    {
        ShotgunPellet.GetComponent<GenericProjectileHandler>().damage = Mathf.Max(damageAdj, 1);
        GameObjectPoolManager.DestroyAllOfObject(ShotgunPellet);
    }

    public void SetProjectileScale(Vector3 projectileScale)
    {
        ShotgunPellet.transform.localScale = projectileScale;
        GameObjectPoolManager.DestroyAllOfObject(ShotgunPellet);
    }
    #endregion

}
