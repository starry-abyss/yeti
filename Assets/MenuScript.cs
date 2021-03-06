﻿using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	void Awake()
	{
		Cursor.visible = true;
		Input.ResetInputAxes();
#if UNITY_WEBGL || UNITY_EDITOR
		GameObject.Find("ExitButton").SetActive(false);
#endif
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void Credits()
	{
		Application.LoadLevel("credits");
	}

	public void Episode1()
	{
		Application.LoadLevel("level1");
	}

	public void Episode2()
	{
		Application.LoadLevel("level6");
	}

	public void Episode3()
	{
		Application.LoadLevel("level9");
	}

	public void Episode4()
	{
		Application.LoadLevel("level13");
	}
}
