using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MealIndicatorScript : MonoBehaviour {

	void UpdateValue(int newValue)
	{
		//hud.GetComponent<>();
		if (value != newValue)
		{
			value = newValue;
			
			//hud.GetComponent<RawImage>().material.SetTextureScale("_MainTex", new Vector2(value, 1));
			Rect uvRect = hud.GetComponent<RawImage>().uvRect;
			uvRect.width = value;
			hud.GetComponent<RawImage>().uvRect = uvRect;
			
			Vector3 scale = hud.GetComponent<RectTransform>().localScale;
			scale.x = value;
			hud.GetComponent<RectTransform>().localScale = scale;
		}
	}
	
	int value;
	
	public Vector2 offset;
	public GameObject hudPrefab;
	GameObject hud;
	
	CaveScript cave;
	VictimScript victim;

	// Use this for initialization
	void Start () {
		hud = (GameObject) Instantiate(hudPrefab);//, 
			//transform.position + new Vector3(offset.x, offset.y, 0.0f), 
			//Quaternion.identity);
		GameObject canvas = GameObject.Find("Canvas");
		hud.transform.SetParent(canvas.transform, true);
		
		value = -1;
		UpdateValue(0);
		
		cave = GetComponent<CaveScript>();
		victim = GetComponent<VictimScript>();
	}
	
	// Update is called once per frame
	void Update () {
		hud.transform.position = transform.position + new Vector3(offset.x, offset.y, 0.0f);
		if (cave != null)
		{
			UpdateValue(Mathf.CeilToInt(cave.meal / 10.0f));
		}
		else if (victim != null)
		{
			UpdateValue(Mathf.CeilToInt(victim.meal / 10.0f));
		}
	}
}
