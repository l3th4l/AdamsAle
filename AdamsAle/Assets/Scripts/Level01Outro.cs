using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItsHarshdeep.LoadingScene.Controller;

public class Level01Outro : MonoBehaviour {

	public void OutroSkip()
	{
		SceneController.LoadLevel ("Level 3");
	}
}
