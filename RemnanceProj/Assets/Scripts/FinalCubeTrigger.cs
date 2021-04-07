using UnityEngine;

public class FinalCubeTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter()
    {
        gameManager.FinalObjectTrigger();

        FindObjectOfType<DialogueManager>().TriggerArtifactDialogue();

        Destroy(gameObject);
    }

}
