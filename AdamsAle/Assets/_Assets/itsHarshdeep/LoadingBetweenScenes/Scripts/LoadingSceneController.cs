using UnityEngine;
using System.Collections;

#if (!UNITY_5_2 ||  !UNITY_5_2_3)
using UnityEngine.SceneManagement;
#endif

namespace ItsHarshdeep.LoadingScene.Controller
{
/// <summary>
/// This class is for the loading scene. When you need to call load a method
/// from SceneController. It will help loading screen to load the requested scene. 
/// But you dont need to call this method will exrenally. 
/// This method need to  be only call from the Loaing scene  
/// </summary>
	public class LoadingSceneController : SceneController
	{

		/// <summary>
		/// To be only call from the loading Scene
		/// Initializes a new instance of the <see cref="LoadingSceneController"/> class.
		/// </summary>
		public LoadingSceneController ()
		{
			LoadScene ();
		}

		private void LoadScene ()
		{
			#if (!UNITY_5_2 ||  !UNITY_5_2_3)
			SceneManager.LoadSceneAsync (scene);
			#else
			Application.LoadLevel (scene);
			#endif
		}
	}

}