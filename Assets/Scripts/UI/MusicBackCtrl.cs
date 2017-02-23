using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBackCtrl : MonoBehaviour {

	public GameObject Music;
	public GameObject Settings;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void pressed() {
		Music.SetActive (false);
		Settings.SetActive (true);
	}
}
