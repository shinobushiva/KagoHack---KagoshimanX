using UnityEngine;
using System.Collections;

public class EnemyAttackRange : MonoBehaviour
{
	public int damage = 2;
	private float nextDamage = 0;
	
	void OnTriggerEnter (Collider c)
	{
		if (c.gameObject.tag == "Player") {
			if (nextDamage > Time.time)
				return;
			
			nextDamage = Time.time + 1;
			
			PlayerStatus ps = 
				c.gameObject.GetComponent<PlayerStatus> ();
			if (ps)
				ps.AddDamage (damage);
		}
	}
}
