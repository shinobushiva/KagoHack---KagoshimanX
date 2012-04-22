using UnityEngine;
using System.Collections;

public class CubeCreator : MonoBehaviour {
	
	public float power = 1000f;
	public Transform prefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown ("p")){
			Transform t = 
				(Transform)Instantiate(prefab, transform.position, transform.rotation);
			if(t.gameObject.rigidbody)
				t.gameObject.rigidbody.
					AddRelativeForce(Vector3.forward * power);
			Destroy(t.gameObject, 2);
		}
	}
}
