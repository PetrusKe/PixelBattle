using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour {
	public Dropdown resolutions;
	public Text setToFull;
	public Text setToWindowed;
	private int resValue;
	//private bool fullscreen2;


	// Use this for initialization
	void Start () {
		setToFull.enabled = false;
		setToWindowed.enabled = true;
		//“全屏”；
		resolutions.value = 5;
		resValue = 5;
		optionset ();
		/*setToFull.enabled = false;
		setToWindowed.enabled = true;*/
	}


	// Update is called once per frame
	void Update () 
	{

		//resValue = resolutions.value;

	}

	void press()//When the On or Off button is pressed
	{

		bool a;
		if (setToFull.enabled && !setToWindowed.enabled)//if the [On] button is pressed
 {			//set the resolutions recording to the dropdown. and the screen condition will relate to the button which was just pressed
			a = true;
			setToFull.enabled = false;
			setToWindowed.enabled = true;
		} 
		else 
		{
			a = false;
			setToFull.enabled = true;
			setToWindowed.enabled = false;
		}

		resValue = resolutions.value;
		switch (resValue)
		{
		case 0:
			Screen.SetResolution (640, 480, a);
			break;
		case 1:
			Screen.SetResolution (800, 600, a);
			break;
		case 2:
			Screen.SetResolution (1024, 768, a);
			break;
		case 3:
			Screen.SetResolution (1280, 720, a);
			break;
		case 4:
			Screen.SetResolution (1440, 900, a);
			break;
		case 5:
			Screen.SetResolution (1920, 1080, a);
			break;
		}
	}

	void optionset()//When the On or Off button is pressed
	{

		//bool a;
		/*if (setToFull.enabled && !setToWindowed.enabled)//if the [On] button is pressed
		{			//set the resolutions recording to the dropdown. and the screen condition will relate to the button which was just pressed
			a = true;
			setToFull.enabled = false;
			setToWindowed.enabled = true;
		} 
		else 
		{
			a = false;
			setToFull.enabled = true;
			setToWindowed.enabled = false;
		}*/
		bool b;
		if (setToFull.enabled && !setToWindowed.enabled)//if the [On] button is pressed
		{			//set the resolutions recording to the dropdown. and the screen condition will relate to the button which was just pressed
			b = false;
					} 
		else 
		{
			b = true;

		}

		resValue = resolutions.value;
		switch (resValue)
		{
		case 0:
			Screen.SetResolution (640, 480, b);
			break;
		case 1:
			Screen.SetResolution (800, 600, b);
			break;
		case 2:
			Screen.SetResolution (1024, 768, b);
			break;
		case 3:
			Screen.SetResolution (1280, 720, b);
			break;
		case 4:
			Screen.SetResolution (1440, 900, b);
			break;
		case 5:
			Screen.SetResolution (1920, 1080, b);
			break;
		}
	}



}
