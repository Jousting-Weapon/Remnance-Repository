using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public bool cameraOut;

    public KeyCode interactKey;
    public UnityEvent interactAction;

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        if(isInRange && GameObject.Find("OldCamera").GetComponent<Equipment>().cameraOutCount == 77)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                FindObjectOfType<GameManager>().CompleteLevel();
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("player now in range");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("player not in range");
        }
    }
}
