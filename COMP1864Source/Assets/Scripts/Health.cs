// KHOGDEN 001115381
using audio;
using System.Collections;
using System.Collections.Generic;
using managers;
using UnityEngine;

namespace entity
{
    public class Health : MonoBehaviour
    {
        private float health = 100f;
        [SerializeField] float maxHealth = 100f;

        private bool dead;

        // Add or remove health from 'health' by the inputted value in 'amount'.
        public void ChangeHealth(float amount)
        {
            // Store the value of health before altering it.
            float prevHealth = health;

            // Add the inputted amount of health being given to 'health'.
            health += amount;

            // Check that health went down or up.
            if (prevHealth > health)
            {
                // Entity was damaged.
                
            }
            else
            {
                // Entity was healed.

            }

            // Check that the health change has caused the health value to go over the maximum/minimum it's allowed up/down to.
            if (health > maxHealth)
                health = maxHealth;
            else if (health < 0f)
                health = 0f;

            // Check that the entity died/got destroyed.
            if(health == 0f && !dead)
                dead = true;
        }

        // Use 'ChangeHealth()' to instantly kill the entity.
        public void Kill()
        {
            ChangeHealth(-health);
        }

        // Method to get the value of 'health'.
        public float GetHealth()
        {
            return health;
        }

        // Method to get the value of 'maxHealth'.
        public float GetMaxHealth()
        {
            return maxHealth;
        }
    }
}
