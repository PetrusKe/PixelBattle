using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {
	public GameObject Music;
	public GameObject Settings;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void pressed () {
		Music.SetActive (true);
		Music.GetComponent<Animator> ().enabled = true;
		Settings.SetActive (false);
	}
}
