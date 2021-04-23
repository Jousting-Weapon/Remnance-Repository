using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter()
    {
        gameManager.CompleteLevel();
    }

}
