using UnityEngine;
using System.Collections;

public class VictimScript : MonoBehaviour {

	public float meal = 1.0f;
	bool dead = false;
	
	float speed = 60.0f;
	
	Vector2 currentVelocity = new Vector2(0, 0);
	
	public AudioClip killSound;
	
	public Sprite deadSprite;
	
	public bool IsDead()
	{
		return dead;
	}
	
	public void ScareEvent(Vector3 position)
	{
		if (dead) return;
		Vector3 direction3 = transform.position - position;
		Vector2 direction2 = new Vector2(direction3.x, direction3.y).normalized;
		currentVelocity = direction2 * speed;
	}
	
	public void Grab(bool grab)
	{
		GetComponent<DistanceJoint2D>().enabled = grab;
	}
	
	public void Kill()
	{
		if (dead) return;
		dead = true;
		GetComponent<SpriteRenderer>().sprite = deadSprite;
		currentVelocity = new Vector2(0, 0);
		
		AudioSource.PlayClipAtPoint(killSound, transform.position);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = currentVelocity;
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
				//Destroy(gameObject);
				
				Application.LoadLevel(Application.loadedLevel);
				return;
			}
		}
		
		if (meal <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
