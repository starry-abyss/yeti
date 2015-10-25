using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictimScript : MonoBehaviour {

	public float meal = 1.0f;
	bool dead = false;
	
	float speed = 60.0f;

	public bool heavy = false;
	
	Vector2 currentVelocity = new Vector2(0, 0);
	
	public AudioClip killSound;
	//public LayerMask layerVictim;
	
	public Sprite deadSprite;
	public Sprite aliveSprite;
	public Sprite deadSprite_Heavy;
	public Sprite aliveSprite_Heavy;
	public WindScript wind;

    AudioSource audioSource;

    public bool IsDead()
	{
		return dead;
	}
	
	public void ScareEvent(Vector3 position)
	{
		if (dead) return;
		PatrolScript patrolScript = GetComponent<PatrolScript>();
		if (patrolScript != null)
		{
			patrolScript.enabled = false;
		}
		
		Vector3 direction3 = transform.position - position;
		Vector2 direction2 = new Vector2(direction3.x, direction3.y).normalized;
		currentVelocity = direction2 * speed;
	}
	
	public void Grab(bool grab)
	{
		GetComponent<DistanceJoint2D>().enabled = grab;
	}
	
	public void Kill()
	{
		if (dead) return;
		dead = true;
		GetComponent<SpriteRenderer>().sprite = heavy ? deadSprite_Heavy : deadSprite;
		currentVelocity = new Vector2(0, 0);

        //AudioSource.PlayClipAtPoint(killSound, transform.position);
        //audioSource.PlayOneShot(killSound);
        audioSource.Play();

        if (wind.enabled)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 100, 1 << gameObject.layer);
			for (int i = 0; i != colliders.Length; ++i)
			{
				if (colliders[i].gameObject != this)
				{
					// victim has heard us
					if (wind.windBlowsFromTo(transform.position, colliders[i].transform.position))
					{
						VictimScript victim = colliders[i].GetComponent<VictimScript>();
						victim.ScareEvent(transform.position);
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = killSound;
        audioSource.loop = false;
        audioSource.spatialBlend = 0.0f;
        audioSource.dopplerLevel = 0.0f;

		GetComponent<SpriteRenderer>().sprite = heavy ? aliveSprite_Heavy : aliveSprite;
    }
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = currentVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead)
		{
			Vector2 horizontalLimits = Camera.main.transform.GetComponent<CameraScript>().horizontalLimits;
			if ((transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
				|| (transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize)
				|| (transform.position.x < horizontalLimits[0]) || (transform.position.x > horizontalLimits[1]))
			{
				// yeti loses
				//Destroy(gameObject);
				
				GameObject dialog = GameObject.Find("Dialog");
				dialog.transform.Find("Text").GetComponent<Text>().text = "It ran away!\n\n[Don't let anybody escape]";
				dialog.GetComponent<DialogScript>().restartLevelAfterClosing = true;
				dialog.GetComponent<DialogScript>().Show();
				
				//Application.LoadLevel(Application.loadedLevel);
				return;
			}
		}
		else
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 50, 1 << gameObject.layer);
			for (int i = 0; i != colliders.Length; ++i)
			{
				if ((colliders[i].gameObject != this) && GetComponent<Collider2D>().enabled)
				{
					VictimScript victim = colliders[i].GetComponent<VictimScript>();
					victim.ScareEvent(transform.position);
				}
			}
		}

        if (meal <= 0.0f)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
		}
	}
}
