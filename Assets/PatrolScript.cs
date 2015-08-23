using UnityEngine;
using System.Collections;

public class PatrolScript : MonoBehaviour {

	public Vector2 patrolToPos;
	public float speed;
	Vector2 patrolFromPos;
	
	Rigidbody2D body;
	float startTime;

	// Use this for initialization
	void Start () {
		patrolFromPos = transform.position;
		body = GetComponent<Rigidbody2D>();
		
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		//if (body.velocity.sqrMagnitude < Mathf.Epsilon)
		{
			float phase = ((Time.time - startTime) * speed / Vector2.Distance(patrolFromPos, patrolToPos)) % 2.0f;
			if (phase > 1.0f) phase = 2.0f - phase;
			body.position = new Vector2(
				Mathf.Lerp(patrolFromPos.x, patrolToPos.x, phase),
				Mathf.Lerp(patrolFromPos.y, patrolToPos.y, phase));
		}
	}
}
