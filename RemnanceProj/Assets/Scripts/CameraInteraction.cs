using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CameraInteraction : MonoBehaviour
{
    public GameObject Camera;
    public Flash FlashScript;
    public Equipment EquipmentScript;

    private void Awake()
    {
        Camera.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && Camera.activeInHierarchy == false && EquipmentScript.isRendered == true)
        {
            Camera.SetActive(true);
            EquipmentScript.ToggleVisiblity();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && Camera.activeInHierarchy == true)
        {
            Camera.SetActive(false);
            EquipmentScript.ToggleVisiblity();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && Camera.activeInHierarchy == true)
        {
            FlashScript.doCameraFlash = true;
        }
    }
}