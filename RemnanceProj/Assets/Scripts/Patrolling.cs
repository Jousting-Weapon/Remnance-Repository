using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{

    public Transform[] points;
    int current;
    private NavMeshAgent agent;

    private GuardianSight GuardianSight;

   void Awake()
   {
       agent = GetComponent<NavMeshAgent>();
       current = 0;
       agent.speed = 10;
       GuardianSight = GetComponent<GuardianSight>();
   }

   public void Patrol()
   {
       agent.destination = points[current].position;
       if (Mathf.Abs(Vector3.Distance(agent.transform.position, points[current].position)) < 1)
        {
           current = (current + 1) % points.Length;
           agent.destination = points[current].position;
        }
   }

   void Update()
   {
       if (GuardianSight.isChasing)
       {
           return;
       }

       Patrol();
       
       //agent.destination = points[current].position;
       //if (Mathf.Abs(Vector3.Distance(agent.transform.position, points[current].position)) < 1)
        //{
        //   current = (current + 1) % points.Length;
        //   agent.destination = points[current].position;
        //}
   }
}