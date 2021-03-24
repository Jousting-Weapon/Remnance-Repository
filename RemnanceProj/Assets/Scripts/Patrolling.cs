using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour{

    public Transform[] points;
    int current;
    private NavMeshAgent agent;

   void Start()
   {
       agent = GetComponent<NavMeshAgent>();
       current = 0;
       agent.speed = 300;
   }

   void Update()
   {
       agent.destination = points[current].position;
       if (agent.transform.position == points[current].position)
        {
           current = (current + 1) % points.Length;
           agent.destination = points[current].position;
        }
   }
}