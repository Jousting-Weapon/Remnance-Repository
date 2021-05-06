using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePatrol : MonoBehaviour
{
    public FinalChaseTrigger FinalChaseTrigger;
    Patrolling patrolling;
    public GameObject chasetrigger;
    public GuardianSight GuardianSight;
    public bool chasing;
    void Start()
    {
        patrolling = GetComponent<Patrolling>();
        GuardianSight = GetComponent<GuardianSight>();
        chasetrigger = GameObject.Find("FinalChaseTrigger");
        FinalChaseTrigger = chasetrigger.GetComponent<FinalChaseTrigger>();
    }

    void Update()
    {
        if (FinalChaseTrigger.finalchase)
        {
            GetComponent<Patrolling>().enabled = false;
            GetComponent<GuardianSight>().isChasing = true;
        }
    }
}
