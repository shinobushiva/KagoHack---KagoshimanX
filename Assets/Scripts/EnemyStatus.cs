using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour
{
	
	public int hp = 1;
	public GameObject deadEffect;
	public int point = 100;
	
	void MoveToLayer (Transform root, int layer)
	{
		root.gameObject.layer = layer;
		foreach (Transform child in root)
			MoveToLayer (child, layer);
	}
	
	public IEnumerator ShowScoreAndDestroy (float sec, int score, Vector3 pos)
	{
		
		TextMesh tm = (TextMesh)Instantiate (ScoreManager.i.text3DPrefab, pos, Quaternion.identity);
		tm.text = "" + score;
		
		yield return new WaitForSeconds(sec);
		Destroy (tm.gameObject);
		Destroy (this);
		
		SendMessage ("Dead", SendMessageOptions.DontRequireReceiver);
		
	}
	
	public void AddDamage (int n)
	{
		hp -= n;
		
		if (hp <= 0) {
			
			/*
			if (deadEffect)
				Instantiate (deadEffect, transform.position, transform.rotation);			
			Destroy (gameObject);
			*/
			
			Transform t = transform.FindChild ("AttackRange");
			if (t)
				Destroy (t.gameObject);
			
			Destroy (GetComponent<Collider> ());
			Destroy (GetComponent<CharacterMotor> ());
			Destroy (GetComponent<CharacterController> ());
			Destroy (GetComponent<EnemyAttackRange> ());
			Destroy (GetComponent<EnemyMove> ());
			
			MoveToLayer (gameObject.transform, LayerMask.NameToLayer ("Dead"));
			if (!gameObject.GetComponent<Rigidbody> ())
				gameObject.AddComponent<Rigidbody> ();
			rigidbody.AddRelativeForce (Vector3.back * 200);
			
			ScoreManager.i.AddPoint (point);
			
			StartCoroutine (ShowScoreAndDestroy (2f, point, transform.position + Vector3.up));
			
		}
	}
}
