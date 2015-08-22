using UnityEngine;
using System.Collections;

public class RendererSortingScript : MonoBehaviour {

	void Sort()
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.FloorToInt(-transform.position.y);
	}

	// Use this for initialization
	void Start () {
		Sort();
	}

	// Update is called once per frame
	void Update () {
		Sort();
	}
}
