using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public GameObject chasetrigger;
    public FinalChaseTrigger FinalChaseTrigger;


    void Awake()
    {
        player = GameObject.Find("First Person Player");
        chasetrigger = GameObject.Find("FinalChaseTrigger");
        FinalChaseTrigger = chasetrigger.GetComponent<FinalChaseTrigger>();
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

        if (other.GetComponent<Collider>().tag == "Player" && FinalChaseTrigger.finalchase)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(-4, -33, 960);
            player.GetComponent<CharacterController>().enabled = true;
        }


        if (other.GetComponent<Collider>().tag == "Point")
        {
            Debug.Log("Point Found");
        }
        
    }
}
