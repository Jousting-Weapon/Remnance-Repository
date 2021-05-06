using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChaseTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public FinalCubeTrigger FinalCubeTrigger;
    public GameObject guardian;
    private GameObject[] guardianlist;
    public GameObject finalguardians;
    public bool finalchase;
    public Patrolling patrolling;

    void Awake()
    {
        guardianlist = GameObject.FindGameObjectsWithTag("Guardian");
        guardian = GameObject.FindGameObjectWithTag("Guardian");
        player = GameObject.Find("First Person Player");
        FinalCubeTrigger = GetComponent<FinalCubeTrigger>();
        finalguardians = GameObject.Find("FinalGuardians(DR)");
        patrolling = GetComponent<Patrolling>();
    }

    void Start()
    {
        finalguardians.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            finalguardians.SetActive(true);

            finalchase = true;
        }
    }    
}
