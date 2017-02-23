using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolutionBackCtrl : MonoBehaviour {

	public GameObject Resolution;
	public GameObject Settings;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void pressed() {
		Resolution.SetActive(false);
		Settings.SetActive (true);
		Settings.GetComponent<Animator> ().enabled = true;
	}
}
