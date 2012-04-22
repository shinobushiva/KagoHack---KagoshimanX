using UnityEngine;
using System.Collections;

public class Kinfe : MonoBehaviour
{
	
	public float speed;
	public int damage = 2;
	

	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}
	
	void OnTriggerEnter (Collider c)
	{
		if (c.gameObject.tag == "Player") {
			PlayerStatus ps = 
				c.gameObject.GetComponent<PlayerStatus> ();
			if (ps)
				ps.AddDamage (damage);
			Destroy (gameObject);
		}
	}
}
