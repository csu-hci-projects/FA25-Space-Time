using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private PlayerInputActions pInput;
    private GameManager gameManager;
    private TimeManager timeManager;
    private InputAction pause;
    public TMP_Text timeText;
    public GameObject pauseMenu;
    public Canvas canvas;
    private bool isPaused;

    void OnEnable()
    {
        pInput.Enable();
        pInput.UI.Enable();
        pause.Enable();
        pause.started += OnPause; 
        Debug.Log(pInput.UI);
    }

    void OnDisable()
    {
        pInput.Disable();
        pInput.UI.Disable();
        pause.Disable();
        pause.started -= OnPause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        // Debug.Log("Hearing: " + timeManager.IsPaused);
        timeManager.IsPaused = !timeManager.IsPaused;
        if(timeManager.IsPaused)
        {
            timeManager.IsPaused = true;
            pauseMenu.SetActive(true);
            pInput.Player.Disable();
        }
        else
        {
            timeManager.IsPaused = false;
            pauseMenu.SetActive(false);
            pInput.Player.Enable();
        }
    }

    void FixedUpdate()
    {
        if(!timeManager.IsPaused)
        {
            timeText.text = timeManager.GetTimeString();
        }
    }
    public void AssignInput(PlayerInputActions input)
    {
        pInput = input;
        pause = pInput.UI.Cancel;
    }

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        timeManager = GetComponent<TimeManager>();
        
    }
}