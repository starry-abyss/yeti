using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictimScript : MonoBehaviour {

	public float meal = 1.0f;
	bool dead = false;
	
	float speed = 60.0f;
	
	Vector2 currentVelocity = new Vector2(0, 0);
	
	public AudioClip killSound;
	//public LayerMask layerVictim;
	
	public Sprite deadSprite;
	public WindScript wind;
	
	public bool IsDead()
	{
		return dead;
	}
	
	public void ScareEvent(Vector3 position)
	{
		if (dead) return;
		PatrolScript patrolScript = GetComponent<PatrolScript>();
		if (patrolScript != null)
		{
			patrolScript.enabled = false;
		}
		
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
		
		if (wind.enabled)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 100, 1 << gameObject.layer);
			for (int i = 0; i != colliders.Length; ++i)
			{
				if (colliders[i].gameObject != this)
				{
					float directionSound = colliders[i].transform.position.x - transform.position.x;
					float directionWind = wind.speed;
					
					// victim has seen us
					if (/*(Vector2.Distance(transform.position, colliders[i].transform.position) <= 50.0f)
					    // victim has heard us
					    ||*/ (((directionSound > 0.0f) && (directionWind > 0.0f)) || ((directionSound < 0.0f) && (directionWind < 0.0f))))
					{
						VictimScript victim = colliders[i].GetComponent<VictimScript>();
						victim.ScareEvent(transform.position);
					}
				}
			}
		}
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
				
				GameObject dialog = GameObject.Find("Dialog");
				dialog.transform.Find("Text").GetComponent<Text>().text = "It ran away!\n\n[Don't let anybody escape]";
				dialog.GetComponent<DialogScript>().restartLevelAfterClosing = true;
				dialog.GetComponent<DialogScript>().Show();
				
				//Application.LoadLevel(Application.loadedLevel);
				return;
			}
		}
		else
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 50, 1 << gameObject.layer);
			for (int i = 0; i != colliders.Length; ++i)
			{
				if (colliders[i].gameObject != this)
				{
					VictimScript victim = colliders[i].GetComponent<VictimScript>();
					victim.ScareEvent(transform.position);
				}
			}
		}
		
		if (meal <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
