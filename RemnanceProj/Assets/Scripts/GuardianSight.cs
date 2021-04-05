using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardianSight : MonoBehaviour
{
    public GameManager gameManager;
    public bool isChasing = false;
    public NavMeshAgent agent;
    public GameObject player;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("First Person Player");
    }

    private void RaycastCheck()
    {
        Vector3 origin = transform.position;
        origin.y = origin.y + 350;
        Vector3 direction = transform.forward;
        float maxDistance = 3000f;


        Debug.DrawRay(origin, direction * maxDistance, Color.red);
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance))
        {
            if (raycastHit.collider.tag == "Player")
            {
                isChasing = true;
            }
        }
    }

    void Update()
    {
        RaycastCheck();

        if (isChasing)
        {
            Chasing();
        }
    }
     
    public void Chasing()
    {
        agent.speed = 400;
        agent.destination = player.transform.position;

    }
}
