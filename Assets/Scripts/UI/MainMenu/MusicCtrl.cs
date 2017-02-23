using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicCtrl : MonoBehaviour {
	private Scrollbar bar;
	public volume volume;
	// Use this for initialization
	void Start () {
		bar = this.gameObject.GetComponent<Scrollbar> ();

        // fixme(Petrus): need instance Volume
	}
	
	// Update is called once per frame
	void Update () {
		volume.vol = bar.value;	
	}
}
