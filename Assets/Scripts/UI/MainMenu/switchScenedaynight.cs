using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class switchScenedaynight : MonoBehaviour {

	private Resolution Res;
	private GLOBAL_SCREEN globalScreen;

	// Use this for initialization
	void Start () 
	{
		globalScreen = GameObject.Find ("ApplicationManager").GetComponent<GLOBAL_SCREEN> ();
	}

	void switchsc1()
	{
		Res = Screen.currentResolution;//record the current resolution
		globalScreen.newRes = Res;//file info written
		if (Screen.fullScreen)
			globalScreen.full_screen = true;
		else
			globalScreen.full_screen = false;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("CityDark");
	}

}
