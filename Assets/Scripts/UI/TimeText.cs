using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {

	private Text timeText;
	private int minutes;
	private int seconds;

	private string minutesFormed;
	private string secondsFormed;

	// Use this for initialization
	void Start () {
		timeText = this.gameObject.GetComponent<Text> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		seconds = (int)Time.fixedTime;
		minutes = ((int)Time.fixedTime / 60);

		minutesFormed = minutes < 10 ? "0" + minutes.ToString () : minutes.ToString ();

		secondsFormed = seconds < 10 ? "0" + seconds.ToString () : seconds.ToString();

		timeText.text = "Time:" + minutesFormed + ":" + secondsFormed;
	}
}
