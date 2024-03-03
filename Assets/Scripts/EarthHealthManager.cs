using UnityEngine;

public class EarthHealthManager : MonoBehaviour
{
    public int baseHealth = 3; // The starting health
    private int currentHealth;
    private int permanentHealthUpgrades = 0; // Permanent upgrades applied outside of gameplay

    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = baseHealth + permanentHealthUpgrades;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead.");
            // Handle player death (e.g., restart level, show game over screen)
        }
    }

    public void ApplyTemporaryHealthUpgrade(int upgradeAmount)
    {
        // Increase current health as a temporary in-game upgrade
        currentHealth += upgradeAmount;
    }

    public void ApplyPermanentHealthUpgrade(int upgradeAmount)
    {
        // Increase permanent health upgrades and reset health to reflect this
        permanentHealthUpgrades += upgradeAmount;
        ResetHealth();
    }

    // Example usage
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) // Press D to simulate damage
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.U)) // Press U to apply a temporary health upgrade
        {
            ApplyTemporaryHealthUpgrade(1);
        }

        // Permanent upgrades would typically be triggered by UI events or game milestones, not directly in Update()
    }
}
