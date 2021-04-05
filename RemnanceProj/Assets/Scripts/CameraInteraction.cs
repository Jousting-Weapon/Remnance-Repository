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
    public GameObject MainCamera;
    private GameObject pictureCamera;
    SkinnedMeshRenderer[] renderArray;

    private void Awake()
    {
        Camera.SetActive(false);
        renderArray = MainCamera.transform.GetChild(1).GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();

        pictureCamera = GameObject.FindGameObjectWithTag("PictureCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Camera.activeInHierarchy == false && pictureCamera.activeInHierarchy == true)
        {
            foreach (SkinnedMeshRenderer renderer in renderArray)
            {
                renderer.enabled = false;
            }

            pictureCamera.SetActive(false);

            Camera.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && Camera.activeInHierarchy == true && pictureCamera.activeInHierarchy == false)
        {
            foreach (SkinnedMeshRenderer renderer in renderArray)
            {
                renderer.enabled = true;
            }

            pictureCamera.SetActive(true);

            Camera.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && Camera.activeInHierarchy == true)
        {
            FlashScript.doCameraFlash = true;
        }
    }
}