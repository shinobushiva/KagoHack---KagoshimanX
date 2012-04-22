using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public float speed = 190f;
	public string word = "Hello Unity";
	
	public Vector3 vec = Vector3.zero;
	
	public int[] array;
	
	// Use this for initialization
	void Start () {
		//Debug.Log (rigidbody);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * speed);	
	}
}
