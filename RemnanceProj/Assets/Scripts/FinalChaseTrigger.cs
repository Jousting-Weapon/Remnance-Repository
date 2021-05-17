using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChaseTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public GameObject guardian;
    private GameObject[] guardianlist;
    public GameObject finalguardians;
    public bool finalchase;

    void Awake()
    {
        player = GameObject.Find("First Person Player");
        finalguardians = GameObject.Find("FinalGuardians(DR)");
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
