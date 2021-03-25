using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public int cameraOutCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            interactAction.Invoke();
            //FindObjectOfType<GameManager>().CompleteLevel();
        }
    }

    public void ToggleVisiblity()
    {
        Renderer[] allRenderedmesh = transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allRenderedmesh)
            if (r.enabled)
            {
                r.enabled = false;
                cameraOutCount--;
                Debug.Log("this is r: " + r);
                Debug.Log("this is cameraOut: " + cameraOutCount);

            }
            else
            {
                r.enabled = true;
                cameraOutCount++;
                Debug.Log("this is r: " + r);
                Debug.Log("this is cameraOut: " + cameraOutCount);
            }

    }
