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
	
	void AdjustTextureOffset()
	{
		Material material = GetComponent<MeshRenderer>().material;
		float width = material.mainTexture.width;
		float height = material.mainTexture.height;
		
		Vector2 offset = material.mainTextureOffset;
		offset.x = Mathf.RoundToInt(offset.x * width) / width;
		offset.y = Mathf.RoundToInt(offset.y * height) / height;
		GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
	}

	public bool windBlowsFromTo(Vector2 from, Vector2 to)
	{
		/*float directionSound = to.x - from.x;
		float directionWind = this.speed;
		return ((directionSound > 0.0f) && (directionWind > 0.0f)) || ((directionSound < 0.0f) && (directionWind < 0.0f));*/

		Vector2 directionTarget = to - from;
		Vector2 directionWind = new Vector2(this.speed, 0.0f);
		directionTarget.Normalize();
		//directionWind.Normalize();

		return (((directionTarget.x > 0.0f) && (directionWind.x > 0.0f)) || ((directionTarget.x < 0.0f) && (directionWind.x < 0.0f))) && (Mathf.Abs(directionTarget.x) > Mathf.Abs(directionTarget.y));
	}

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
	
		Vector2 offset = 
			new Vector2((phase % 100.0f) * ((Mathf.Abs(baseSpeed) < Mathf.Epsilon) ? 
			Mathf.Sin((phase % (2 * Mathf.PI))) * 0.002f : -speed / baseSpeed), 
			//Mathf.Abs(Mathf.Sin((Time.time % (2 * Mathf.PI)) /*/ 100.0f*/)) * 1.0f);
			((phase / 5.0f) % 1.0f));
		GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
		
		AdjustTextureOffset();
		
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
