using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class PlayerStats : MonoBehaviour

{


    // -- Health and Shield Settings --
    public int maxHealth = 100; 
    private int currentHealth; 
    public int maxShield = 50; 
    private int currentShield; 
    private int shieldDamageReduction; // Percentage of damage reduced when shield is active (e.g., 50 means 50% damage reduction), set at 0 and affected by upgrades and gear

    public int CurrentShield => currentShield; // Public property to access current shield value
    public int CurrentHealth => currentHealth; // Public property to access current health value

    // -- Basic Stats --

    public int strength = 10;
    public int dexterity = 10;
    public int constitution = 10;
    public int intelligence = 10;
    public int wisdom = 10;
    public int charisma = 10;

    // -- XP and Leveling --
    public int currentXP = 0;
    public int xpToNextLevel;
    public int level;
    private bool isDead = false;


    // -- Initialization --
    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum at the start
        currentShield = maxShield; // Initialize current shield to maximum at the start
        level = 1; // Start at level 1
        StartCoroutine(ShieldRechargeCoroutine(5f, maxShield * 0.25f)); 
    }



// -- Health and Shield Management --
    public void TakeDamage(int damage)
    {
        if (currentShield > 0) // If the player has shield, damage is applied to the shield first
        {

            float reducedDamage = damage * (1f - shieldDamageReduction / 100f);

            if (currentShield >= reducedDamage)
            {
                currentShield -= Mathf.RoundToInt(reducedDamage);
                return;
            }
            else
            {
                float leftoverDamage = reducedDamage - currentShield;
                currentShield = 0;
                damage = Mathf.CeilToInt(leftoverDamage);
            }
        }   
            currentHealth -= damage;

            if (currentHealth <= 0) // Check if health has dropped to zero or below
            {
                Die(); // Call the Die method if the player has died
            }
    }

    private void Die()
    {
        Debug.Log("Player has died."); // Log a message when the player dies
        // Additional death logic can be added here (e.g., respawn, game over screen, etc.)
        isDead = true; // Set the isDead flag to true
    }

    public void Heal(int amount) // 
    {
        if (isDead) return; // If the player is dead, do not allow healing
        
        
        if (currentShield > 0)
        {
            int shieldHeal = Mathf.Min(amount, maxShield - currentShield); // Calculate how much healing can be applied to the shield
            currentShield += shieldHeal; // Increase the current shield by the healed amount
            amount -= shieldHeal; // Reduce the remaining healing amount by the amount applied to the shield
        } else
        {
            int healthHeal = Mathf.Min(amount, maxHealth - currentHealth); // Calculate how much healing can be applied to health
            currentHealth += healthHeal; // Increase the current health by the healed amount
        }

    }

    public void HealOverTime(int amount, float duration, float speed)
    {
        if (isDead) return; // If the player is dead, do not allow healing
        // Start the healing over time, healing in chunks of the total amount every 1 second until the total amount is healed or the duration is over
        StartCoroutine(HealOverTimeCoroutine(amount, duration, speed));        
    }

    IEnumerator HealOverTimeCoroutine(int totalAmount, float duration, float speed)
        {
            int amountHealed = 0; // Track the total amount healed so far
            float timeElapsed = 0f; // Track the time elapsed since the healing started

            while (amountHealed < totalAmount && timeElapsed < duration)
            {
                int ticks = Mathf.CeilToInt(duration / speed);
                int healPerTick = Mathf.CeilToInt((float)totalAmount / ticks); // Calculate how much to heal per tick based on the total amount and duration
                int healChunk = Mathf.Min(healPerTick, totalAmount - amountHealed); // Heal in chunks of the calculated amount or the remaining amount if less than the calculated amount
                Heal(healChunk); // Apply the healing chunk
                amountHealed += healChunk; // Update the total amount healed
                yield return new WaitForSeconds(speed); // Wait for the specified speed (e.g., 1 second) before applying the next healing chunk
                timeElapsed += speed; // Update the time elapsed
            }
        }


    public void TakeDamageOverTime(int damage, float duration, float speed)
    {
        if (isDead) return; // If the player is dead, do not allow taking damage
        // Start the damage over time, taking damage in chunks of the total damage every 1 second until the total damage is taken or the duration is over
        StartCoroutine(TakeDamageOverTimeCoroutine(damage, duration, speed));
    }

    IEnumerator TakeDamageOverTimeCoroutine(int totalDamage, float duration, float speed)
    {
        int damageTaken = 0; 
        float timeElapsed = 0f; 

        while (damageTaken < totalDamage && timeElapsed < duration)
        {
            int damageChunk = Mathf.Min(5, totalDamage - damageTaken);
            TakeDamage(damageChunk); 
            damageTaken += damageChunk; 
            yield return new WaitForSeconds(speed); 
            timeElapsed += speed; 
        }
    }

    private IEnumerator ShieldRechargeCoroutine(float delay, float rechargeRate)
    {
        while (!isDead) 
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay before starting to recharge

            if (currentHealth == maxHealth)
            {
            currentShield += Mathf.RoundToInt(rechargeRate); // Recharge the shield by the specified amount
            currentShield = Mathf.Min(currentShield, maxShield); // Ensure the shield does not exceed its maximum value    
            }
            
        }
        
        
    }
}

