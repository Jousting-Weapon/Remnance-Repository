using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;


    void Awake()
    {
        player = GameObject.Find("First Person Player");
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            Debug.Log("player collision");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(-467, 7, -526);
            player.GetComponent<CharacterController>().enabled = true;
        }
        if (other.GetComponent<Collider>().tag == "Point")
        {
            Debug.Log("Point Found");
        }
        
    }
}
