﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	public Vector2 horizontalLimits;

	/*void Awake ()
	{
		
	}*/

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Camera>().orthographicSize = Screen.width / 4;
		//GetComponent<Camera>().pixelRect = new Rect(Screen.width - 1000, Screen.height - 200, 1000, 200);
		//GetComponent<Camera>().orthographicSize = 100; //Screen.height / 4;
		float horizontalSize = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
		
		float x = player.transform.position.x;
		if (x < horizontalLimits[0] + horizontalSize)
			x = horizontalLimits[0] + horizontalSize;
		if (x > horizontalLimits[1] - horizontalSize)
			x = horizontalLimits[1] - horizontalSize;
		/*if (x < horizontalLimits[0] + GetComponent<Camera>().pixelWidth / 2)
			x = horizontalLimits[0] + GetComponent<Camera>().pixelWidth / 2;
		if (x > horizontalLimits[1] - GetComponent<Camera>().pixelWidth / 2)
			x = horizontalLimits[1] - GetComponent<Camera>().pixelWidth / 2;*/
		//x = Mathf.Max(horizontalLimits[0] + Screen.width / 2, x);
		//x = Mathf.Min(horizontalLimits[1] - Screen.width / 2, x);
		transform.position = new Vector3(x, transform.position.y, -10.0f);
	}
}
