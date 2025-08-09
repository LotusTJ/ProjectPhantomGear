using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DoorEnterExitScript : MonoBehaviour
{
    public float waitTime = 2f;
    public string nextSceneName;
    public KeyCode actionKey = KeyCode.E;

    private bool playerInTrigger = false;
    public GameObject promptUI;
    public TextMeshProUGUI promptText;

    private bool isControllerConnected;

    void Start()
    {
        isControllerConnected = Input.GetJoystickNames().Length > 0 && !string.IsNullOrEmpty(Input.GetJoystickNames()[0]);
    }

    void Update()
    {
        if (playerInTrigger)
        {
            promptUI.SetActive(true);

            if (isControllerConnected)
                promptText.text = "Press A to Enter";
            else
                promptText.text = $"Press {actionKey} to Enter";

            if (Input.GetKeyDown(actionKey) || Input.GetButtonDown("Submit"))
            {
                Invoke(nameof(NextScene), waitTime);
            }
        }
        else
        {
            promptUI.SetActive(false);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
