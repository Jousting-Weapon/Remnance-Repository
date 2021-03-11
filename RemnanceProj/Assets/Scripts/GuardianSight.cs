using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSight : MonoBehaviour
{
    public LayerMask Ground;
    public GameManager gameManager;

    void Update()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        Vector3 origin = transform.position;
        origin.y = origin.y + 115;
        Vector3 direction = transform.forward;
        float maxDistance = 1000f;


        Debug.DrawRay(origin, direction * maxDistance, Color.red);
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, ~Ground))
        {
            if (raycastHit.collider.tag == "Player")
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }
}
