using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using ItsHarshdeep.LoadingScene.Controller;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuPanel;
	public GameObject controlsPanel;

	public GameObject player;
	public GameObject diedPanel;

	void Start()
	{
		Camera cam = Camera.main;
		PostProcessingBehaviour effects = cam.GetComponent<PostProcessingBehaviour> ();
		if (effects != null) 
		{
			effects.enabled = SettingManager.effects;
		}

	}

    private void Update()
    {
		if (player == null) {

			diedPanel.SetActive (true);
		}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if(pauseMenuPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

	public void Restart()
	{
		SceneController.LoadLevel (SceneManager.GetActiveScene ().name);
	}

    public void TogglePauseMenu()
    {
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
    }

    public void ControlsClick()
    {
		pauseMenuPanel.SetActive (false);
		controlsPanel.SetActive (true);
    }

	public void Controls_BackClick()
	{
		controlsPanel.SetActive (false);
		pauseMenuPanel.SetActive (true);
	}

    public void ExitClick()
    {
		SceneController.LoadLevel ("MainMenu");
    }
}
