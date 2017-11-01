using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ItsHarshdeep.LoadingScene.Controller;

public class MainMenu : MonoBehaviour {

    //public Animator animator;

    public string facebookLink;
    public string twitterLink;

	public GameObject mainMenuCanvas;
	public GameObject aboutCanvas;
	public GameObject optionsCanvas;







    public void PlayClick()
    {
		SceneController.LoadLevel ("Level 1");
    }

    public void OptionsClick()
    {
        //animator.SetBool("Options", true);
		mainMenuCanvas.SetActive(false);
		optionsCanvas.SetActive (true);
    }

    public void AboutClick()
    {
        //animator.SetBool("About", true);
		aboutCanvas.SetActive (true);
		mainMenuCanvas.SetActive (false);
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    public void facebookClick()
    {
        Application.OpenURL(facebookLink);
    }

    public void twitterClick()
    {
        Application.OpenURL(twitterLink);
    }



    public void options_BackClick()
    {
        //animator.SetBool("Options", false);
		mainMenuCanvas.SetActive(true);
		optionsCanvas.SetActive (false);
    }



    public void about_BackClick()
    {
        //animator.SetBool("About", false);
		aboutCanvas.SetActive (false);
		mainMenuCanvas.SetActive (true);
    }


}
