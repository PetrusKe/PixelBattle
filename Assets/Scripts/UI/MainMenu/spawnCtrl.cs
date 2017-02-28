using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawnCtrl : MonoBehaviour {

	public Transform player_1;
	public Transform player_2;
	public GameObject[] heroes;
	public Image switcherPanel;

	public Image hero1_left;
	public Image hero1_right;
	public Image hero2_left;
	public Image hero2_right;

	private int heroID_1 = 0;
	private int heroID_2 = 0;

	private GLOBAL_SCREEN globalScreen; 

	public TimeText timer;

	void Awake()
	{
		globalScreen = GameObject.Find ("ApplicationManager").GetComponent<GLOBAL_SCREEN> ();
		Debug.Log(globalScreen.newRes.ToString ());
		Screen.SetResolution (globalScreen.newRes.width, globalScreen.newRes.height, globalScreen.full_screen);
	}

	void Start()
	{
		left_1 ();
		right_1 ();
	}
	// Use this for initialization
	void StartGame () 
	{
		if (true) 
		{
			player_1 = Instantiate<GameObject>(heroes[heroID_1].gameObject).transform;
			player_2 = Instantiate<GameObject>(heroes[heroID_2].gameObject).transform;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
		
	void left_1()
	{
		heroID_1 = (heroID_1 == 0) ? 1 : heroID_1 - 1;
		judge ();
	}

	void right_1()
	{
		heroID_1 = (heroID_1 == 1) ? 0 : heroID_1 + 1;
		judge ();
	}

	void left_2()
	{
		heroID_2 = (heroID_2 == 0) ? 1 : heroID_2 - 1;
		judge ();
	}

	void right_2()
	{
		heroID_2 = (heroID_2 == 1) ? 0 : heroID_2 + 1;
		judge ();
	}

	void Enter()
	{
		timer.SendMessage ("begin");
		switcherPanel.gameObject.SetActive (false);
		StartGame ();
	}

	void judge()
	{
		switch (heroID_1) 
		{
		case 0:
			_0X ();
			break;
		case 1:
			_1X ();
			break;
		}

		switch (heroID_2) 
		{
		case 0:
			_X0 ();
			break;
		case 1:
			_X1 ();
			break;
		}

	}


	void _0X()
	{
		hero1_left.enabled = true;
		hero2_left.enabled = false;
	}
	void _1X()
	{
		hero2_left.enabled = true;
		hero1_left.enabled = false;
	}
	void _X0()
	{
		hero1_right.enabled = true;
		hero2_right.enabled = false;
	}
	void _X1()
	{
		hero2_right.enabled = true;
		hero1_right.enabled = false;
	}
}
