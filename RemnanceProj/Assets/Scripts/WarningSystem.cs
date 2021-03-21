using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningSystem : MonoBehaviour
{
    //----------------Singleton-----------

    public static WarningSystem Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //------------------------------------

    public Text warningText;
    Animator warningAnim;

    // Start is called before the first frame update
    void Start()
    {
        warningAnim = warningText.GetComponent<Animator>();
    }

    public void ShowMessage(string message)
    {
        warningText.text = message;
        warningAnim.SetTrigger("show");
    }
}
