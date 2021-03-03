using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            FindObjectOfType<GameManager>().EndGame();
        }
        if (other.GetComponent<Collider>().tag == "Point")
        {
            Debug.Log("Point Found");
        }
        
    }
}
