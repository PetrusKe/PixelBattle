using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicCtrl : MonoBehaviour {
	private Scrollbar bar;
	public volume Volume;
	// Use this for initialization
	void Start () {
		bar = this.gameObject.GetComponent<Scrollbar> ();
	}
	
	// Update is called once per frame
	void Update () {
		Volume.vol = bar.value;	
	}
}
