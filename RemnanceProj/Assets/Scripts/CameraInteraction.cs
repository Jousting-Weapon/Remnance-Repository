using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CameraInteraction : MonoBehaviour
{
    public GameObject CameraUI;
    public Flash FlashScript;
    public GameObject MainCamera;
    private GameObject pictureCamera;
    SkinnedMeshRenderer[] renderArray;
    public Animator armAnimator;

    private bool pictureBool = false;

    private DialogueManager dialogueManager;

    private void Awake()
    {
        CameraUI.SetActive(false);
        renderArray = MainCamera.transform.GetChild(1).GetChild(1).GetComponentsInChildren<SkinnedMeshRenderer>();

        pictureCamera = GameObject.FindGameObjectWithTag("PictureCamera");
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // When the player presses 'F,' play the Camera animation
        if (!dialogueManager.inTutorial && Input.GetKeyDown(KeyCode.F))
        {
            armAnimator.SetBool("CameraEquip", !armAnimator.GetBool("CameraEquip"));

            CameraUI.SetActive(false);

            if (pictureBool == false)
            {
                pictureCamera.SetActive(true);
                pictureBool = true;
            }
            else if (pictureBool == true)
            {
                StartCoroutine(AnimationTimer());
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && CameraUI.activeInHierarchy == false && pictureBool == true)
        {
            foreach (SkinnedMeshRenderer renderer in renderArray)
            {
                renderer.enabled = false;
            }

            pictureCamera.SetActive(false);

            pictureBool = false;

            CameraUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && CameraUI.activeInHierarchy == true && pictureBool == false)
        {
            foreach (SkinnedMeshRenderer renderer in renderArray)
            {
                renderer.enabled = true;
            }

            pictureCamera.SetActive(true);

            pictureBool = true;

            CameraUI.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && CameraUI.activeInHierarchy == true)
        {
            FlashScript.doCameraFlash = true;
        }
    }

    IEnumerator AnimationTimer()
    {
        pictureBool = false;
        yield return new WaitForSeconds(1.5f);
        pictureCamera.SetActive(false);
    }
}