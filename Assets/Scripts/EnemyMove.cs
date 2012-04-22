using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
	
	public float speed = 1;
	private CharacterController cc;
	private Transform playerTrans;
	public EnemyType type;
	
	public Transform knifePrefab;
	private float nextKinfeTime;
	public float kinfeDuration = 1;
	
	public float range = 10;
	
	public enum EnemyType
	{
		Normal,
		KnifeUp,
		KnifeDown
	}

	// Use this for initialization
	void Start ()
	{
		//Get a reference for the player transform.
		GameObject p = GameObject.FindGameObjectWithTag ("Player");
		playerTrans = p.transform;
		
		//Get a reference for the Character Controller
		cc = GetComponent<CharacterController> ();
		
		if(type == EnemyType.KnifeDown){
			Renderer[] rs = GetComponentsInChildren<Renderer>();
			foreach(Renderer r in rs)
				r.material.color = Color.green;
		}else if(type == EnemyType.KnifeUp){
			Renderer[] rs = GetComponentsInChildren<Renderer>();
			foreach(Renderer r in rs)
				r.material.color = Color.blue;
		} 
		
		nextKinfeTime = 0;
	}
	
	  void OnDrawGizmos() {
		Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
	
	// Update is called once per frame
	void Update ()
	{
		
		if(Vector3.Distance(playerTrans.position, transform.position) > range){
			return;
		}
		
		
		//Move towards Player
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		if (cc) {
			
			//Knife Guy
			if (type == EnemyType.KnifeUp || type == EnemyType.KnifeDown) {
				if (Vector3.Distance (playerTrans.position, transform.position) < 5) {
					if(nextKinfeTime < Time.time){
						nextKinfeTime = Time.time + kinfeDuration;
						
						if(type == EnemyType.KnifeUp){
							Instantiate(knifePrefab, transform.position+Vector3.up/2, transform.rotation);
						}else{
							Instantiate(knifePrefab, transform.position-Vector3.up/2, transform.rotation);
						} 
					}
				} else {
					cc.SimpleMove (forward * Time.deltaTime * speed);
				}
			}
			//Normal Guy
			else {
				cc.SimpleMove (forward * Time.deltaTime * speed);
			}
			
		
			//Look at Player position, not working somehow...
			transform.LookAt (playerTrans.position);
			Vector3 ang = transform.localEulerAngles;
			ang.x = 0;
			ang.z = 0;
			transform.localEulerAngles = ang;
				
			
		}
	
	}
}
