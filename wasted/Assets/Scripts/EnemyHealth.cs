using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private double playerHealth;
    [SerializeField] private double maxHealth;

    [SerializeField] private int damage;



    public void TakeDamage(int damageAmount)
    {
        playerHealth = playerHealth - damageAmount;

        if (playerHealth <= 0)
        {
            Destroy(this.transform.gameObject);
        }


        UpdateHealth();
    }



    private void Update()
    {


    }

    private void UpdateHealth()
    {


        //Material original = GetComponent<MeshRenderer>().material;
        Color newColor = new Color(255,0, 0, (float)(playerHealth / maxHealth));

        GetComponent<MeshRenderer>().material.color = newColor;


    }

}
