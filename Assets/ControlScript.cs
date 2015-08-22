using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	//    1
	// 2     0
	//    3
	int direction = 3;
	float speed = 100.0f;
	
	public float hp = 100.0f;
	
	readonly float mealToHp = 1.0f;   // meal = hp * mealToHp; hp = meal / mealToHp 
	readonly float hpDropShelter = 0.1f; // per second
	readonly float hpDropOutside = 0.3f; // per second
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
		GetComponent<Rigidbody2D>().velocity = speed * (new Vector2(inputH, inputV));
		
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
		
		if (hp <= 0.0f) ;
	}
}
