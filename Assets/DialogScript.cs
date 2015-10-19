using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogScript : MonoBehaviour {

	bool hidden = false;
	public bool restartLevelAfterClosing = false;
	public bool gotoMenuAfterClosing = false;
    public bool showOnLevelStart = true;

    AudioSource musicHunt;
	AudioSource musicConcentrate;

	// Use this for initialization
	void Start () {
		musicHunt = GameObject.Find("music_hunt").GetComponent<AudioSource>();
		musicConcentrate = GameObject.Find("music_concentrate").GetComponent<AudioSource>();
        if (showOnLevelStart)
            Show();
        else
            Hide();
	}
	
	public bool IsHidden()
	{
		return hidden;
	}
	
	public void Hide()
	{
		hidden = true;
		
		musicHunt.enabled = true;
		musicConcentrate.enabled = false;
		Time.timeScale = 1.0f;
		
		//Debug.Log("Hide");
		
		GetComponent<Image>().enabled = false;
		transform.Find("Text").GetComponent<Text>().enabled = false;
		transform.Find("Text (1)").GetComponent<Text>().enabled = false;
	}
	
	public void Show()
	{
		hidden = false;
		
		musicHunt.enabled = false;
		musicConcentrate.enabled = true;
		
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
		
		float offset = 15.0f;
		Vector2 size = GetComponent<RectTransform>().sizeDelta;
		size.x = Camera.main.orthographicSize * Camera.main.aspect * 2 - offset * 2;
		size.y = Camera.main.orthographicSize * 2 - offset * 2;
		GetComponent<RectTransform>().sizeDelta = size;
		
		if (!hidden && (Input.GetAxisRaw("Action") >= 0.1f))
		{
			if (gotoMenuAfterClosing)
				Application.LoadLevel(0);
			else if (restartLevelAfterClosing)
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
