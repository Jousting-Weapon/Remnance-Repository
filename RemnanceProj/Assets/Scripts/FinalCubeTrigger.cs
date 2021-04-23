using UnityEngine;

public class FinalCubeTrigger : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter()
    {
        gameManager.FinalObjectTrigger();

        FindObjectOfType<DialogueManager>().TriggerArtifactDialogue();

        Destroy(gameObject);
    }

}
