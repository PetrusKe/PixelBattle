using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	public int countDownSecs;
	public Text timeText;
	private int minutesLeft;
	private int secondsLeft;

	private string minutesLeftFormed;
	private string secondsLeftFormed;

	private int time_secs;
	public Image panelImage;

	void Awake()
	{
		panelImage.gameObject.GetComponent<Animator> ().enabled = false;
	}

	// Use this for initialization
	void Start () {
		time_secs = countDownSecs;
	}
	
	// Update is called once per frame
	void Update () {

		if (time_secs > 0) {
			time_secs = countDownSecs - (int)Time.time;
			minutesLeft = time_secs / 60;
			secondsLeft = time_secs % 60;

			if (minutesLeft < 10)
				minutesLeftFormed = "0" + minutesLeft.ToString ();
			else
				minutesLeftFormed = minutesLeft.ToString ();

			if (secondsLeft < 10)
				secondsLeftFormed = "0" + secondsLeft.ToString ();
			else
				secondsLeftFormed = secondsLeft.ToString ();

			timeText.text = "Time: " + minutesLeftFormed + ":" + secondsLeftFormed;
		} 
		else
			timeText.text = "Time: 00:00";

		
	}

	void LateUpdate()
	{
		if ((int)Time.time == countDownSecs)//如果系统时间与预设倒计时精确相等
			
			panelImage.gameObject.GetComponent<Animator> ().enabled = true;
			
	}
}
