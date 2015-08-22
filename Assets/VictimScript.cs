using UnityEngine;
using System.Collections;

public class VictimScript : MonoBehaviour {

	public float meal = 1.0f;
	bool dead = false;
	
	public Sprite deadSprite;
	
	public void Grab(bool grab)
	{
		GetComponent<DistanceJoint2D>().enabled = grab;
	}
	
	public void Kill()
	{
		dead = true;
		GetComponent<SpriteRenderer>().sprite = deadSprite;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead)
		{
			Vector2 horizontalLimits = Camera.main.transform.GetComponent<CameraScript>().horizontalLimits;
			if ((transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
				|| (transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize)
				|| (transform.position.x < horizontalLimits[0]) || (transform.position.x > horizontalLimits[1]))
			{
				// yeti loses
				Destroy(gameObject);
				return;
			}
		}
		
		if (meal <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
