using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject pauseMenu;

    private bool isPaused;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 24;
        }
        else
        {
            speed = 12;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.Confined;
                
            }
            else if (isPaused)
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -100 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "SiteEntranceTrigger")
        {
            FindObjectOfType<DialogueManager>().TriggerSiteEntrance();
            Destroy(other.gameObject);
        }
        else if (other.name == "CaveEntranceTrigger")
        {
            FindObjectOfType<DialogueManager>().TriggerCaveEntrance();

            Destroy(other.transform.parent.gameObject);
        }
    }

    public void onExitButtonClick()
    {
        Application.Quit();
        /**if(Application.isEditor)
            {
                EditorApplication.isPlaying = false;
            }*/
    }

    public void onMenuButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_Menu_Scene");
    }
}
