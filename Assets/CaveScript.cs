using UnityEngine;
using System.Collections;

public class CaveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public float meal = 0;

	void OnTriggerEnter2D(Collider2D other)
	{
		ControlScript yeti = other.gameObject.GetComponent<ControlScript>();
		if (yeti != null)
		{
			// this is the yeti, heal him
			yeti.ActivateShelter(this);
		}
		else
		{
			// convert to meal
		
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		ControlScript yeti = other.gameObject.GetComponent<ControlScript>();
		if (yeti != null)
		{
			yeti.DeactivateShelter();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
