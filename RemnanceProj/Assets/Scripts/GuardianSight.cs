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
    public Patrolling Patrolling;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("First Person Player");
        Patrolling = GetComponent<Patrolling>();
    }

    void RaycastCheck()
    {
        Vector3 origin = transform.position;
        origin.y = origin.y + 2;
        Vector3 direction = transform.forward;
        float maxDistance = 100f;


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

    void Chasing()
    {
        agent.speed = 30;
        agent.angularSpeed = 300;
        agent.destination = player.transform.position;

    }

    void Update()
    {
        RaycastCheck();

        if (isChasing)
        {
            Chasing();
        }

        if (Mathf.Abs(Vector3.Distance(agent.transform.position, player.transform.position)) > 70)
        {
            isChasing = false;
            agent.speed = 10;
            Patrolling.Patrol();

        }
    } 

}
