using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthImage;

    [SerializeField] private int damage;

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            playerHealth -= damage;

            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        healthImage.fillAmount = playerHealth / maxHealth;
    }

}
