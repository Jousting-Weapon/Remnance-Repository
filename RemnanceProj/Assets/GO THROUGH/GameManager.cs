using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public bool pickedUpFinalObject = false;

    public void CompleteLevel()
    {
        if(pickedUpFinalObject == true)
        {
            completeLevelUI.SetActive(true);
        }
    }

    public void FinalObjectTrigger()
    {
        pickedUpFinalObject = true;
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
