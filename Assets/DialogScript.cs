using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogScript : MonoBehaviour {

	bool hidden = false;
	public bool restartLevelAfterClosing = false;

	// Use this for initialization
	void Start () {
		Show();
	}
	
	public bool IsHidden()
	{
		return hidden;
	}
	
	public void Hide()
	{
		hidden = true;
		Time.timeScale = 1.0f;
		
		//Debug.Log("Hide");
		
		GetComponent<Image>().enabled = false;
		transform.Find("Text").GetComponent<Text>().enabled = false;
		transform.Find("Text (1)").GetComponent<Text>().enabled = false;
	}
	
	public void Show()
	{
		hidden = false;
		
		transform.SetAsLastSibling();
		GetComponent<Image>().enabled = true;
		transform.Find("Text").GetComponent<Text>().enabled = true;
		transform.Find("Text (1)").GetComponent<Text>().enabled = true;
		
		Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = Camera.main.transform.position;
		//position.y = position.y - Camera.main.orthographicSize + 10;
		position.z = -9;
		GetComponent<RectTransform>().position = position;
		
		if (!hidden && (Input.GetAxisRaw("Action") >= 0.1f))
		{
			if (restartLevelAfterClosing)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				Hide();
			}
		}
	}
}
