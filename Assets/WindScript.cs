using UnityEngine;
using System.Collections;

public class WindScript : MonoBehaviour {

	public float speed = 50.0f;
	public float baseSpeed = 50.0f;
	public float changePeriod = 5.0f;
	
	SpriteRenderer[] treeRenderers;
	public Sprite treeCalm;
	public Sprite treeWindLeft;
	public Sprite treeWindRight;
	
	float startTime;

	// Use this for initialization
	void Start () {
		GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
		treeRenderers = new SpriteRenderer[trees.Length];
		for (int i = 0; i < trees.Length; ++i)
		{
			treeRenderers[i] = trees[i].GetComponent<SpriteRenderer>();
		}
		
		startTime = Time.time;
		//GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float phase = Time.time - startTime;
		if (phase % (2 * changePeriod) < changePeriod)
			speed = 0.0f;
		else if (phase % (4 * changePeriod) < 2 * changePeriod)
			speed = baseSpeed;
		else
			speed = -baseSpeed;
	
		Vector2 offset = (Time.time % 100.0f) * 
			new Vector2((Mathf.Abs(baseSpeed) < Mathf.Epsilon) ? 0.0f : -speed / baseSpeed, 
			Mathf.Abs(Mathf.Sin((Time.time % (2 * Mathf.PI)) / 100.0f)) * 1.0f);
		GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
		
		for (int i = 0; i < treeRenderers.Length; ++i)
		{
			if (Mathf.Abs(speed) < Mathf.Epsilon)
				treeRenderers[i].sprite = treeCalm;
			else if (speed < 0.0f)
				treeRenderers[i].sprite = treeWindLeft;
			else 
				treeRenderers[i].sprite = treeWindRight;
		}
	}
}
