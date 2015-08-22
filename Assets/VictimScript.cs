using UnityEngine;
using System.Collections;

public class VictimScript : MonoBehaviour {

	public float meal = 10.0f;
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
		if (meal <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
