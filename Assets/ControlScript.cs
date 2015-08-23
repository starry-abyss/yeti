using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	//    1
	// 2     0
	//    3
	int direction = 3;
	float speed = 70.0f;
	
	Vector2 currentVelocity;
	
	public WindScript wind;
	
	public float hp = 100.0f;
	public LayerMask layerVictim;
	
	readonly float mealToHp = 0.1f;   // meal = hp * mealToHp; hp = meal / mealToHp 
	readonly float hpDropShelter = 0.0f; // per second
	readonly float hpDropOutside = 1.0f; // per second
	//readonly float mealPerVictim = 10.0f;
	int maxHp = 100;
	
	CaveScript shelter = null;
	
	public Sprite[] yetiDirectionSprites = new Sprite[4];
	
	public void ActivateShelter(CaveScript newShelter)
	{
		shelter = newShelter;
		//SpriteRenderer renderer = other.gameObject.GetComponent<SpriteRenderer>();
		//if (renderer != null)
		//{
		GetComponent<SpriteRenderer>().enabled = false;
		//}
	}
	
	public void DeactivateShelter()
	{
		shelter = null;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	void EatAndHeal(ref float meal)
	{
		float mealToEat = Mathf.Min((maxHp - hp) * mealToHp, meal);
		meal -= mealToEat;
		hp += mealToEat / mealToHp;
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		VictimScript victim = coll.gameObject.GetComponent<VictimScript>();
		if (victim != null)
		{
			victim.Kill();
			EatAndHeal(ref victim.meal);
		}
	}
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = currentVelocity;
		
		Vector3 position = GetComponent<Rigidbody2D>().position;
		Vector2 horizontalLimits = Camera.main.transform.GetComponent<CameraScript>().horizontalLimits;
		if (position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
			position.y = Camera.main.transform.position.y - Camera.main.orthographicSize;
		if (position.y > Camera.main.transform.position.y + Camera.main.orthographicSize)
			position.y = Camera.main.transform.position.y + Camera.main.orthographicSize;
		if (position.x < horizontalLimits[0])
			position.x = horizontalLimits[0];
		if (position.x > horizontalLimits[1])
			position.x = horizontalLimits[1];
		GetComponent<Rigidbody2D>().position = position;
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
		
		//if (Mathf.Abs(inputH) < inputDeadZone) inputH = 0.0f;
		//if (Mathf.Abs(inputV) < inputDeadZone) inputV = 0.0f;
		
		GetComponent<SpriteRenderer>().sprite = yetiDirectionSprites[direction];
		
		//transform.position = transform.position + Time.deltaTime * speed * (new Vector3(inputH, inputV, 0.0f));
		Vector2 velocity = speed * (new Vector2(inputH, inputV)).normalized;
		if ((((wind.speed > 0) && (velocity.x > 0))
			|| ((wind.speed < 0) && (velocity.x < 0)))
			&& (Mathf.Abs(wind.speed) >= Mathf.Abs(velocity.x)))
				velocity.x = 0.0f;
		else if (Mathf.Abs(velocity.x) > 0)
			velocity.x += wind.speed;
		currentVelocity = velocity;
	
		// grabbing victims
		bool grab = Input.GetAxis("Action") >= inputDeadZone;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20, layerVictim);
		for (int i = 0; i != colliders.Length; ++i)
		{
			VictimScript victim = colliders[i].GetComponent<VictimScript>();
			victim.Grab(grab);
			if (grab) victim.Kill();
		}
		
		// scaring victims by our appearance
		colliders = Physics2D.OverlapCircleAll(transform.position, 50, layerVictim);
		for (int i = 0; i != colliders.Length; ++i)
		{
			VictimScript victim = colliders[i].GetComponent<VictimScript>();
			victim.ScareEvent(transform.position);
		}
		
		if (shelter == null)
		{
			
			hp -= Time.deltaTime * hpDropOutside;
		}
		else
		{
			if (hp < maxHp)
			{
				EatAndHeal(ref shelter.meal);
			}
			hp -= Time.deltaTime * hpDropShelter;
		}
		
		// yeti loses
		if (hp <= 0.0f) 
		{
			;
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
