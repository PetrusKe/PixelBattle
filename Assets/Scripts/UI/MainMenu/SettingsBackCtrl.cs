using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBackCtrl : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject Settings;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void back() {
		Time.timeScale = 1.0f;
		mainMenu.SetActive (true);
		Settings.SetActive (false);

	}
}
