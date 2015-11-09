using UnityEngine;
using System.Collections;

public class NoiseSourceScript : MonoBehaviour {

	public Sprite uniformSpreadSprite;
	public Sprite windSpreadSprite;

	public enum NoiseType { None, Uniform, WindLeft, WindRight };

	GameObject indicator;
	SpriteRenderer renderer;

	public void SetNoiseType(NoiseType noiseType)
	{
		switch (noiseType) {
		case NoiseType.None:
			renderer.enabled = false;
			break;
		case NoiseType.Uniform:
			renderer.enabled = true;
			renderer.sprite = uniformSpreadSprite;
			indicator.transform.localScale = Vector2.one;
			break;
		case NoiseType.WindLeft:
			renderer.enabled = true;
			renderer.sprite = windSpreadSprite;
			indicator.transform.localScale = new Vector2 (-1, 1);
			break;
		case NoiseType.WindRight:
			renderer.enabled = true;
			renderer.sprite = windSpreadSprite;
			indicator.transform.localScale = Vector2.one;
			break;
		default:
			break;
		}
	}

	// Use this for initialization
	void Awake () {
		indicator = new GameObject();
		indicator.transform.SetParent(transform);
		indicator.transform.localPosition = Vector2.zero;
		renderer = indicator.AddComponent<SpriteRenderer> ();
		renderer.color = new Color (1f, 1f, 1f, 0.2f);

		SetNoiseType(NoiseType.None);
		//SetNoiseType(NoiseType.Uniform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
