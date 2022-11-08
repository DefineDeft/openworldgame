using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private double playerHealth;
    [SerializeField] private double maxHealth;
    [SerializeField] private Image healthImage;

    [SerializeField] private int damage;
    [SerializeField] private double regenRate = 0.5;

    public Transform respawn;
    

    public void TakeDamage(int damageAmount)
    {
        playerHealth = playerHealth - damageAmount;

        if (playerHealth <= 0)
        {
            this.transform.parent.transform.position = respawn.position;
            playerHealth = maxHealth;
        }


        UpdateHealth();
    }



    private void FixedUpdate()
    {
        playerHealth += regenRate * Time.deltaTime;
        UpdateHealth();
    }

    private void UpdateHealth()
    {

       
            healthImage.fillAmount = (float)(playerHealth / maxHealth);


    }

}
