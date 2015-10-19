using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelScript : MonoBehaviour {

	public WindScript wind;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		
		VictimScript[] victims = Object.FindObjectsOfType<VictimScript>();
		for (int i = 0; i < victims.Length; ++i)
		{
			victims[i].wind = wind;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Cancel") >= 0.1f)
		{
			// go to menu on Escape
			Application.LoadLevel (0);
			return;
		}

		//VictimScript[] victims = (VictimScript[]) Object.FindObjectsOfType(typeof(VictimScript));
		VictimScript[] victims = Object.FindObjectsOfType<VictimScript>();
		bool allDead = true;
		for (int i = 0; i < victims.Length; ++i)
		{
			if (!victims[i].IsDead())
			{
				allDead = false;
				break;
			}
		}
	
		// all victims are killed
		if (allDead && (Time.timeScale > 0))
		{
			this.enabled = false;
			Debug.Log("all dead");
			if (Application.loadedLevel == Application.levelCount - 1)
			{
				// this is last level
				GameObject dialog = GameObject.Find("Dialog");
				if (dialog.GetComponent<DialogScript>().IsHidden() && ((Input.GetAxisRaw("Action") < 0.1f)))
				{
					Debug.Log("last level");
					
					dialog.transform.Find("Text").GetComponent<Text>().text = 
						@"BELLIGERANT MADNESS
A true type font by P.D. Magnus
www.fontmonkey.com

LICENSE

This font is copyright 2008 by P.D. Magnus. Like all the Fontmonkey fonts, it is free for for all commercial or non-commercial use.
To be clear: They do not cost anything.";
					
					dialog.GetComponent<DialogScript>().Show();
					dialog.GetComponent<DialogScript> ().gotoMenuAfterClosing = true;
				}
				else
				{
					this.enabled = true;
				}
			}
			else
			{
				// go to next level
				Debug.Log("next level");
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}
	}
}
