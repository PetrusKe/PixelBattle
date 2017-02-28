using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {

	private Text timeText;
	private int secs_60n;
	private int minutes;
	private int seconds;
	private string minutesFormed;
	private string secondsFormed;
	private float enterTime = 0.0f;


	// Use this for initialization
	void Start () {
		timeText = this.gameObject.GetComponent<Text> ();
		timeText.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {


		secs_60n = (int)(Time.fixedTime - enterTime);
		seconds = secs_60n % 60;
		minutes = ((int)Time.fixedTime / 60);

		minutesFormed = minutes < 10 ? "0" + minutes.ToString () : minutes.ToString ();

		secondsFormed = seconds < 10 ? "0" + seconds.ToString () : seconds.ToString();

		timeText.text = "Time:" + minutesFormed + ":" + secondsFormed;
	}

	void begin()
	{
		enterTime = Time.time;//records the time you press "Enter"
		timeText.enabled = true;
	}
}
