using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

	public float attackDist = 3;
	public AudioClip attackSound;
	public float attackDuration = 0.5f;
	private float nextAttackTime = 0;
	public Joystick touchPad;
	
	private bool Attacking ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
			return touchPad.tapCount >= 1 ;
		} else {
			return  Input.GetButton ("Fire1");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		Debug.DrawRay (transform.position, transform.forward * 10, Color.red);
	
		if (Attacking () && nextAttackTime < Time.time) {
			//Attack!!
			nextAttackTime = Time.time + attackDuration;
			
			GameObject[] es = 
				GameObject.FindGameObjectsWithTag ("Enemy");
			GameObject nearest = null;
			foreach (GameObject e in es) {
				if (nearest == null) {
					nearest = e;
				} else if (
				Vector3.Distance (transform.position, nearest.transform.position)
					>
				Vector3.Distance (transform.position, e.transform.position)
					) {
					nearest = e;
				}
			}		
			
			if (nearest && Vector3.Distance (transform.position, nearest.transform.position) < attackDist) {
				
				//Vector3 forward = transform.TransformDirection (Vector3.forward);
				//GetComponent<CharacterController> ().SimpleMove (forward * Time.deltaTime);
				transform.LookAt (nearest.transform.position);
				Vector3 ang = transform.localEulerAngles;
				ang.x = 0;
				ang.z = 0;
				transform.localEulerAngles = ang;
				
				if (attackSound)
					AudioSource.PlayClipAtPoint (attackSound, transform.position);
				// The Enemy must dieeeeeeee!!!!!
				nearest.SendMessage ("AddDamage", 1,
					SendMessageOptions.DontRequireReceiver);
			}
			
		}
	}
}
