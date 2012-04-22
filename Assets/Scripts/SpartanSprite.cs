using UnityEngine;
using System.Collections;

public class SpartanSprite : MonoBehaviour
{
	
	public Texture2D kick;
	public Texture2D duck;
	public Texture2D punch;
	public Texture2D punch2;
	public Texture2D jump;
	public Texture2D[] walk;
	private float nextWalkTime = 0;
	public float walkDuration = 0.5f;
	private int walkIdx = 0;
	private bool isDucking;
	public CharacterController cc;
	public ThirdPersonController tpc;
	private float attackDuration = 0.5f;
	private float nextAttack = 0;
	private float ccHeight;
	private Vector3 ccCenter;
	public Joystick joystick;
	public Joystick touchPad;

	// Use this for initialization
	void Start ()
	{
		//tpc = GetComponent<ThirdPersonController> ();
		//cc = GetComponent<CharacterController>();
		nextWalkTime = 0;
		nextAttack = 0;
		
		
		if (cc) {
			ccHeight = cc.height;
			ccCenter = cc.center;
			
			if (cc.GetComponent<PlayerAttack> ())
				attackDuration = cc.GetComponent<PlayerAttack> ().attackDuration;
		}
		
	
	}
	
	private bool Attacking ()
	{
		if (touchPad) {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
				return touchPad.tapCount >= 1 ;
			} else {
				return  Input.GetButton ("Fire1");
			}
		}
		return false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (nextAttack < Time.time && nextAttack > Time.time - attackDuration / 2f) {
			if (!isDucking)
				renderer.material.mainTexture = punch2;
			return;
		} else if (nextAttack > Time.time) {
			return;
		}
		
		if (!cc)
			return;
		
		bool e = (gameObject.tag == "Player");
		
		if (!cc.isGrounded && Mathf.Abs (cc.velocity.y) > 0.01f) {
			if (Attacking () && e) {
				renderer.material.mainTexture = kick;
				nextAttack = Time.time + attackDuration;
			} else {
				renderer.material.mainTexture = jump;
			}
		} else {
			
			if (Input.GetAxis ("Vertical") == -1 || (joystick && joystick.position.y < -0.5f)) {
				isDucking = true;
				if (tpc) {
					tpc.canMove = false;
					cc.height = ccHeight / 2;
					
					cc.center = ccCenter - Vector3.up * ccHeight / 4f;
				}
			} else {
				isDucking = false;
				if (tpc) {
					tpc.canMove = true;
					cc.height = ccHeight;
					cc.center = ccCenter;
				}
			}
			
			if (isDucking && e) {
				if (Attacking ()) {
					renderer.material.mainTexture = kick;
					nextAttack = Time.time + attackDuration;
				} else {
					renderer.material.mainTexture = duck;
				}
			} else if (tpc && !tpc.IsMoving ()) {
				if (Attacking () && e) {
					renderer.material.mainTexture = punch;
					nextAttack = Time.time + attackDuration;
				} else {
					renderer.material.mainTexture = walk [0];
				}
			} else {
				if (Attacking () && e) {
					renderer.material.mainTexture = punch;
					nextAttack = Time.time + attackDuration;
				} else {
					if (nextWalkTime < Time.time) {
						walkIdx = (walkIdx + 1) % walk.Length;
						renderer.material.mainTexture = walk [walkIdx];
				
						nextWalkTime = Time.time + walkDuration;
					}
				}
			}
		}
		
	
	
	}
}
