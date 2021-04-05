using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MenuElementHover : MonoBehaviour
{

    CameraMovement cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if(cameraMovement.inTransition || cameraMovement.inOptionsMenu)
        {
            return;
        }
        this.transform.GetComponent<TextMeshPro>().color = Color.yellow;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (cameraMovement.inTransition || cameraMovement.inOptionsMenu)
        {
            return;
        }
        this.transform.GetComponent<TextMeshPro>().color = Color.white;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (cameraMovement.inTransition || cameraMovement.inOptionsMenu)
        {
            return;
        }
        if (this.transform.name == "Start Button")
        {
            SceneManager.LoadScene("Final_Build");
        }
        else if(this.transform.name == "Options Button")
        {
            cameraMovement.optionsCutscene = true;
            //Debug.LogError("Lance has not implemented the Options Menu yet. Don't worry ;)");
        }
        else if (this.transform.name == "Exit Button")
        {
            Application.Quit();
            if(Application.isEditor)
            {
                EditorApplication.isPlaying = false;
            }
        }
    }

}
