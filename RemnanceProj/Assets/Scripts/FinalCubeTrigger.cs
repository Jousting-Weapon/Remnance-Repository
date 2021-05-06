using UnityEngine;

public class FinalCubeTrigger : MonoBehaviour
{
    GameManager gameManager;

    public bool checktrigger;
    public GameObject finalchasetrigger;
    private Collider collidercheck; 

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        finalchasetrigger = GameObject.Find("FinalChaseTrigger");
        collidercheck = finalchasetrigger.GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter()
    {
        checktrigger = true;

        gameManager.FinalObjectTrigger();

        FindObjectOfType<DialogueManager>().TriggerArtifactDialogue();

        Destroy(gameObject);

        collidercheck.enabled = true;
    }

}
