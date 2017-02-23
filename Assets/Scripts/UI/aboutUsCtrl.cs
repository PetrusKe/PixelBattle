using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aboutUsCtrl : MonoBehaviour {

	public GameObject aboutUs;
	public GameObject mainMenu;
	// Use this for initialization
	void Start () {
		aboutUs.SetActive (false);
	}

	// Update is called once per frame
	void startAboutUs()
	{
		aboutUs.SetActive (true);
		aboutUs.GetComponent<Animator> ().enabled = true;
		mainMenu.SetActive(false);
	}
}
