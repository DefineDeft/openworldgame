using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public int BulletDamage;
    public bool isEnemyBullet;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void SetDamage(int damage)
    {
        BulletDamage = damage;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision otherCollision)
    {

       Collider other = otherCollision.collider;

        if (isEnemyBullet == true)
        {

            HealthScript health = other.GetComponentInChildren(typeof(HealthScript)) as HealthScript;

            // Check if we hit something we can damage.
            if (health != null)
            {
                // If so, damage it and destroy ourselves!
                health.TakeDamage(BulletDamage);
                Destroy(gameObject);
            }
        } else
        {
            EnemyHealth health = other.GetComponentInChildren(typeof(EnemyHealth)) as EnemyHealth;

            // Check if we hit something we can damage.
            if (health != null)
            {
                // If so, damage it and destroy ourselves!
                health.TakeDamage(BulletDamage);
                Destroy(gameObject);
            }
        }

        // Depending on your game, you might also want to
        // destroy the projectile if you hit an indestructible wall.
    }
}
