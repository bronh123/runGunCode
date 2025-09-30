using UnityEngine;

/// <summary>
/// Manages weapon statistics and upgrades for different weapon types.
/// Currently handles shotgun modifications with planned support for gravity launcher.
/// </summary>
public class WeaponStats : MonoBehaviour
{
    // References to weapon handlers
    public ShotgunHandler shotgunHandler;
    public GravLauncherHandler gravLauncherHandler;

    #region "Shotgun Stat Variables
    
    // Base shotgun statistics
    [Header("Shotgun Stats")]
    [SerializeField] private float sgSpread = 10;                    // Spread angle of shotgun pellets in degrees
    [SerializeField] private float sgProjSpeed = 50;                 // Speed of projectiles
    [SerializeField] private int sgProjCount = 10;                   // Number of pellets per shot
    [SerializeField] private float sgRecoilIntensity = 50;           // Intensity of weapon recoil
    [SerializeField] private float sgDamage = 1;                     // Damage per pellet
    [SerializeField] private Vector3 sgProjectileScale = new(.1f, .1f, .1f);  // Size of projectiles

    // Shotgun upgrade modifiers
    [Header("Shotgun Upgrade Values")]
    
    // Slug upgrade - Converts shotgun to fire fewer more powerful, larger rounds
    [Header("Shotgun Slug Upgrade")]
    [SerializeField] private float sgSlugSpreadAdj = -2f;            // Reduces spread for accuracy
    [SerializeField] private float sgSlugDamageAdj = 5f;             // Increases damage significantly
    [SerializeField] private int sgSlugProjectileCountAdj = -5;      // Reduces projectile count (division in code)
    [SerializeField] private float sgSlugRecoilAdj = -10;            // Reduces recoil slightly
    [SerializeField] private float sgSlugProjectileSpeedAdj = 10f;   // Increases projectile speed
    [SerializeField] private Vector3 sgSlugProjectileSizeAdj = new(.1f, .1f, .1f);  // Increases projectile size

    // Blast upgrade - Increases spread and pellet count for crowd control
    [Header("Shotgun Blast Upgrade")]
    [SerializeField] private float sgBlastSpreadAdj = 2f;            // Increases spread
    [SerializeField] private float sgBlastDamageAdj = -2f;           // Reduces damage per pellet
    [SerializeField] private int sgBlastProjectileCountAdj = 10;     // Multiplies projectile count
    [SerializeField] private float sgBlastRecoilAdj = 10f;           // Increases recoil
    [SerializeField] private float sgBlastProjectileSpeedAdj = 10f;  // Increases projectile speed

    #endregion

    #region "Grav Launcher Stat Variables
    
    // Gravity launcher statistics (placeholder for future implementation)
    [Header("Grav Launcher Stats - Currently Useless")]
    [SerializeField] private float glProjectileSpeed;     // Speed of gravity projectile
    [SerializeField] private float glEffectFieldRadius;   // Radius of gravity effect field
    [SerializeField] private float glDamagePerTick;       // Damage dealt per tick within field
    [SerializeField] private float glPullForce;           // Force pulling objects toward center
    [SerializeField] private float glPushForce;           // Force pushing objects away from center
    #endregion
    
    /// <summary>
    /// Initialize weapon handlers with base statistics
    /// </summary>
    void Start()
    {
        // Set initial shotgun statistics if handler exists
        if (shotgunHandler != null)
        {
            shotgunHandler.SetSpread(sgSpread);
            shotgunHandler.SetProjectileCount(sgProjCount);
            shotgunHandler.SetRecoilIntensity(sgRecoilIntensity);
            shotgunHandler.SetProjectileSpeed(sgProjSpeed);
        }
    }

    /// <summary>
    /// Applies slug upgrade modifications to shotgun
    /// Fewer projectiles with increased size and damage.
    /// </summary>
    public void ModifyShotgunSlugQualities()
    {
        // Apply slug modifications
        sgSpread += sgSlugSpreadAdj;                    // Reduce spread for accuracy
        sgProjCount /= sgSlugProjectileCountAdj;        // Divide projectile count (note: dividing by negative number)
        sgDamage += sgSlugDamageAdj;                    // Increase damage
        sgRecoilIntensity += sgSlugRecoilAdj;           // Adjust recoil
        sgProjSpeed += sgSlugProjectileSpeedAdj;        // Increase speed
        sgProjectileScale += sgSlugProjectileSizeAdj;   // Increase projectile size

        // Clamp values to prevent invalid states
        sgSpread = Mathf.Clamp(sgSpread, 0, 360);              // Keep spread within valid angle range
        sgProjCount = Mathf.Clamp(sgProjCount, 1, 200);        // Ensure at least 1 projectile
        sgDamage = Mathf.Max(1, sgDamage);                     // Ensure minimum damage
        sgRecoilIntensity = Mathf.Clamp(sgRecoilIntensity, 0, 500);  // Keep recoil manageable
        sgProjSpeed = Mathf.Clamp(sgProjSpeed, 1, 100);        // Keep speed within reasonable limits

        // Update shotgun handler with new values
        shotgunHandler.SetRecoilIntensity(sgRecoilIntensity);
        shotgunHandler.SetSpread(sgSpread);
        shotgunHandler.SetProjectileCount(sgProjCount);
        shotgunHandler.SetProjectileDamage(sgDamage);
        shotgunHandler.SetProjectileSpeed(sgProjSpeed);
        shotgunHandler.SetProjectileScale(sgProjectileScale);
    }

    /// <summary>
    /// Applies blast upgrade modifications to shotgun
    /// Increases spread and projectile count for crowd control
    /// </summary>
    public void ModifyShotgunBlastQualities()
    {
        // Apply blast modifications
        sgSpread += sgBlastSpreadAdj;                   // Increase spread
        sgProjCount *= sgBlastProjectileCountAdj;       // Multiply projectile count
        sgDamage += sgBlastDamageAdj;                   // Reduce damage per pellet
        sgRecoilIntensity += sgBlastRecoilAdj;          // Increase recoil
        sgProjSpeed += sgBlastProjectileSpeedAdj;       // Increase projectile speed

        // Clamp values to prevent invalid states
        sgSpread = Mathf.Clamp(sgSpread, 0, 360);              // Keep spread within valid angle range
        sgProjCount = Mathf.Clamp(sgProjCount, 1, 200);        // Limit maximum projectiles
        sgDamage = Mathf.Max(1, sgDamage);                     // Ensure minimum damage
        sgRecoilIntensity = Mathf.Clamp(sgRecoilIntensity, 0, 500);  // Keep recoil manageable
        sgProjSpeed = Mathf.Clamp(sgProjSpeed, 1, 100);        // Keep speed within reasonable limits

        // Update shotgun handler with new values
        shotgunHandler.SetSpread(sgSpread);
        shotgunHandler.SetProjectileCount(sgProjCount);
        shotgunHandler.SetProjectileDamage(sgDamage);
        shotgunHandler.SetRecoilIntensity(sgRecoilIntensity);
        shotgunHandler.SetProjectileSpeed(sgProjSpeed);
    }
}