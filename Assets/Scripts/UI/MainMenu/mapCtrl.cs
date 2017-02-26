using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCtrl : MonoBehaviour {

	public GameObject maps;
	public GameObject mainMenu;
	// Use this for initialization
	void Start () {
		maps.SetActive (false);
	}

	// Update is called once per frame
	void startMaps()
	{
		maps.SetActive (true);
		maps.GetComponent<Animator> ().enabled = true;
		mainMenu.SetActive(false);
	}
}
