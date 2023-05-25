using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Collectible : MonoBehaviour 
{

	public enum CollectibleTypes {Coin, PowerUp};

	public CollectibleTypes CollectibleType;


	private float rotationSpeed = 80;

	public AudioClip collectSound;

	public GameObject collectEffect;

	public Collectible()
    {

    }

    private void Awake()
    {
        
    }

    void Update () 
	{
		transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public virtual void Collect()
	{
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		
		// Collect Effect
		Instantiate(collectEffect, transform.position, Quaternion.identity);

		Destroy (gameObject);
	}
}
