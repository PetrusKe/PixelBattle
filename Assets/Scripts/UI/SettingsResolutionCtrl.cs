using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsResolutionCtrl : MonoBehaviour {

	public GameObject Resolution;
	public GameObject Settings;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void pressed() {
		Resolution.SetActive (true);
		Resolution.GetComponent<Animator> ().enabled = true;
		Settings.SetActive (false);
	}
}
