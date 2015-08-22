using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	//    1
	// 2     0
	//    3
	int direction = 3;
	float speed = 100.0f;
	
	public Sprite[] yetiDirectionSprites = new Sprite[4];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float inputV = Input.GetAxis("Vertical");
		float inputH = Input.GetAxis("Horizontal");
		float inputDeadZone = 0.1f;
		if (Mathf.Abs(inputV) >= inputDeadZone)
		{
			direction = (inputV > 0) ? 1 : 3;
		}
		else if (Mathf.Abs(inputH) >= inputDeadZone)
		{
			direction = (inputH > 0) ? 0 : 2;
		}
		
		GetComponent<SpriteRenderer>().sprite = yetiDirectionSprites[direction];
		
		transform.position = transform.position + Time.deltaTime * speed * (new Vector3(inputH, inputV, 0.0f));
	}
}
