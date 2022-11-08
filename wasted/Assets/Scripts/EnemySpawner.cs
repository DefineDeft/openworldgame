using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int EnemyCap;
    public GameObject EnemyType;
    public float spawnfrequency = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawner());
    }

    // Update is called once per frame
    IEnumerator spawner()
    {
        while (true)
        {
            if (this.transform.childCount < EnemyCap)
            {
                
                GameObject newEnemy = Instantiate(EnemyType, this.transform.position, Quaternion.identity);
                newEnemy.transform.SetParent(this.transform);
                
         
            }
            yield return new WaitForSeconds(spawnfrequency);
        }
    }
}
