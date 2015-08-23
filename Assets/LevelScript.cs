using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
				Debug.Log("last level");
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
