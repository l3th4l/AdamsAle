using UnityEngine;
using System.Collections;
using ItsHarshdeep.LoadingScene.Constants;
using ItsHarshdeep.LoadingScene.Controller;

public class LoadRequestedScene : MonoBehaviour
{
	private LoadingSceneController loadingSC = null;

	void Start ()
	{
		Invoke ("LoadRequestedScenee", Constants.LOADING_SCENE_WAIT_TIME);
	}

	private void LoadRequestedScenee ()
	{
		loadingSC = new LoadingSceneController ();
	}
}
