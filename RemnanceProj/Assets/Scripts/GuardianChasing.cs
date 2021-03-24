using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianChasing : MonoBehaviour
{
    public Transform[] points;
    int current;
    private UnityEngine.AI.NavMeshAgent agent;
    public GameManager gameManager;
    public bool isChasing = false;
    public GameObject player;
    //public Transform playerpos;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player");
        //playerpos = player.GetComponent<CharacterController>().transform.position;
    }

    void Start()
    {

    }
    
    void Update()
    {
       RaycastCheck();

       if (isChasing)
        {
            Chasing();
        }
    }

    private void RaycastCheck()
    {
        Vector3 origin = transform.position;
        origin.y = origin.y + 115;
        Vector3 direction = transform.forward;
        float maxDistance = 1000f;


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

    public void Chasing()
    {
        //agent.destination = playerpos;
        Debug.Log("is chasing");
    }
}
