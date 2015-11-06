using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	void UpdateValue(float newValue)
	{
		//hud.GetComponent<>();
		//if (value != newValue)
		{
			value = newValue;
			
			//hud.GetComponent<RawImage>().material.SetTextureScale("_MainTex", new Vector2(value, 1));
			/*Rect uvRect = hud.GetComponent<RawImage>().uvRect;
			uvRect.width = value / 100.0f * (Screen.width - 20);
			hud.GetComponent<RawImage>().uvRect = uvRect;*/
			
			Vector2 size = hud.GetComponent<RectTransform>().sizeDelta;
			//size.x = value / 100.0f * (Screen.width - 20);
			size.x = value /*/ 100.0f * (Camera.main.pixelWidth - 20)*/;
			hud.GetComponent<RectTransform>().sizeDelta = size;
		}
	}
	
	float value;
	
	public GameObject hudPrefab;
	GameObject hud;
	Text overeaten;
	
	ControlScript yeti;
	
	// Use this for initialization
	void Start () {
		hud = (GameObject) Instantiate(hudPrefab);//, 
		//transform.position + new Vector3(offset.x, offset.y, 0.0f), 
		//Quaternion.identity);
		GameObject canvas = GameObject.Find("Canvas");
		hud.transform.SetParent(canvas.transform, true);

		overeaten = hud.GetComponentInChildren<Text> ();
		overeaten.enabled = false;
		
		//value = -1;
		UpdateValue(0);

		yeti = GetComponent<ControlScript>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 position = Camera.main.transform.position;
		position.y = position.y - Camera.main.orthographicSize + 10;
		position.z = -8;
		hud.GetComponent<RectTransform>().position = position;

		overeaten.enabled = yeti.hp > yeti.maxHp * 0.99f;
		
		UpdateValue(yeti.hp);
	}
}
