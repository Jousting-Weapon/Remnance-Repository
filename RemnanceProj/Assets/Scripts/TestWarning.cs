using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWarning : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.T))
        {
            WarningSystem.Instance.ShowMessage("Some message");
        }
    }
}
