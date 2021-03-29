using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWarning : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E) && GameObject.Find("OldCamera").GetComponent<Equipment>().cameraOutCount == 0)
        {
            WarningSystem.Instance.ShowMessage("Equip camera to interact! 'F' ");
        }
    }
}
