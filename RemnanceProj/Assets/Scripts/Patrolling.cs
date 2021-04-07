using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{

    public Transform[] points;
    int current;
    private NavMeshAgent agent;

   void Awake()
   {
       agent = GetComponent<NavMeshAgent>();
       //Debug.Log(agent);
       current = 0;
       agent.speed = 600;
   }

   void Update()
   {
       agent.destination = points[current].position;
       //Debug.Log(agent.transform.position);
       if (Mathf.Abs(Vector3.Distance(agent.transform.position, points[current].position)) < 1)
        {
           current = (current + 1) % points.Length;
           agent.destination = points[current].position;
           //Debug.Log(agent.transform.position);
        }
   }
}