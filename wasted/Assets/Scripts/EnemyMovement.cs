using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody enemybody;
    public GameObject Player;


    void Start()
    {
        enemybody = this.GetComponent<Rigidbody>();
        Player = GameObject.Find("MainChar");
    }

    // Update is called once per frame
    void Update()
    {
       

        Vector3 directiontoPlayer = Player.transform.position - enemybody.transform.position;

        enemybody.transform.Rotate(directiontoPlayer.normalized);
        enemybody.AddForce(directiontoPlayer.normalized * 2);

        



        
    }
}
