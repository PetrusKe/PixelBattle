using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class switchScenedaynight : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}

	void switchsc1()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("CityDark");
	}

}
