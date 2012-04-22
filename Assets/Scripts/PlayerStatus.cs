using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
	
	public int hp = 10;
	public AudioClip damageSound;
	public AudioClip deadSound;
	
	private void EnablePlayer (bool b)
	{
		Renderer[] rs =
				gameObject.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs)
			r.enabled = b;
		gameObject.GetComponent<ThirdPersonController> ().enabled = b;
		gameObject.GetComponent<Collider> ().enabled = b;
		gameObject.GetComponent<PlayerAttack> ().enabled = b;
		
		gameObject.GetComponent<ThirdPersonCamera> ().enabled = b;
	}
	
	private IEnumerator WaitAndRevive (int sec)
	{
		Vector3 cang = Camera.main.transform.localEulerAngles;
		Vector3 org = Camera.main.transform.position;
		Vector3 dest = org + Vector3.forward * 10;
		
		
		float dul = sec;
		float t = 0;
		while (t < dul) {
			cang.y = Mathf.Lerp (0, 120, t / sec);
			Camera.main.transform.localEulerAngles = cang;
			Camera.main.transform.position = Vector3.Lerp (org, dest, t / sec);
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		//yield return new WaitForSeconds(sec);
		
		//hp = 10;
		//EnablePlayer (true);
		ScoreManager.i.PlayerDead ();
	}
	
	void Update ()
	{
		ScoreManager.i.playerHp = hp;
	}
	
	public void AddDamage (int n)
	{
		hp -= n;
		if (hp <= 0) {
			//Dead!
			
			EnablePlayer (false);
			AudioSource.PlayClipAtPoint (deadSound, transform.position);
			transform.position = Vector3.one * 10000;
			StartCoroutine (WaitAndRevive (3)); //Multi-threading
			
		} else {
			AudioSource.PlayClipAtPoint (damageSound, transform.position);
		}
	}
	
	
	
}
