using UnityEngine;
using System.Collections;

public class BossDead : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	IEnumerator WL (float sec)
	{
		/*
		Vector3 cang = Camera.main.transform.localEulerAngles;
		Vector3 org = Camera.main.transform.position;
		Vector3 dest = org + Vector3.forward * 10;
		
		
		float dul = sec;
		float t = 0;
		while (t < dul) {
			cang.y = 360 - Mathf.Lerp (0, 120, t / sec);
			Camera.main.transform.localEulerAngles = cang;
			Camera.main.transform.position = Vector3.Lerp (org, dest, t / sec);
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		*/
		yield return new WaitForEndOfFrame();
		Application.LoadLevel ("Score");
	}
	
	public void Dead ()
	{
		StartCoroutine (WL (3));
	}
}
