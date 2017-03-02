using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour {
	public volume Volume;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<AudioSource> ().volume = GameObject.Find ("UIcontroller").GetComponent<volume>().vol;
	}
}
