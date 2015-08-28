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
		if (allDead)
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
					
					dialog.transform.Find("Text").GetComponent<Text>().text = "[The End]\n\nThis is the extended post-compo version of Ludum Dare #33 entry that was originally made in 48 hrs by Scorched (@IgorsGames)";
					dialog.GetComponent<DialogScript>().Show();
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
