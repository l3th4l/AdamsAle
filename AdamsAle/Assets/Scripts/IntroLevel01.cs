using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevel01 : MonoBehaviour {

	public GameObject plot;
	public GameObject skipPlot;
	public GameObject story;
	public GameObject skipStory;

	public void SkipPlot()
	{
		plot.SetActive (false);
		story.SetActive (true);
		skipStory.SetActive (true);
		skipPlot.SetActive (false);
	}

	public void SkipStory()
	{
		Animator anim = GetComponent<Animator> ();
		anim.enabled = true;
	}
}
