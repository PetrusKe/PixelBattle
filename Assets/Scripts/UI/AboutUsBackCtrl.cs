using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutUsBackCtrl : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject aboutUs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void back() 
	{
		mainMenu.SetActive (true);
		aboutUs.SetActive (false);
	}
}
