using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingCollision : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    private GuardianSight GuardianSight;


    void Awake()
    {
        player = GameObject.Find("First Person Player");
        GuardianSight = GetComponent<GuardianSight>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            GuardianSight.isChasing = true;
        }
    }
}
