using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingCtrl : MonoBehaviour {

	public GameObject settings;
	public GameObject mainMenu;
	// Use this for initialization
	void Start () {
		settings.SetActive (false);
	}
	
	// Update is called once per frame
	IEnumerator startSettings()
	{
		settings.SetActive (true);
		settings.GetComponent<Animator> ().enabled = true;
		mainMenu.SetActive(false);
		yield return new WaitForSeconds (1.0f);
		Time.timeScale = 0;
	}
}
