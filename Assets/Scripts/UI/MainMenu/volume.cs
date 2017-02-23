using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class volume : MonoBehaviour {

	public float vol;

	public virtual void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

}
