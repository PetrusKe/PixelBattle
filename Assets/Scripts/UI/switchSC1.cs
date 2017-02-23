using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class switchSC1 : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}

	void switchsc()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainMenu");
	}

}
