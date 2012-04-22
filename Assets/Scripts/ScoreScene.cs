using UnityEngine;
using System.Collections;

public class ScoreScene : MonoBehaviour
{
	
	public GUIText score;
	public GUIText credit;
	public string c1 = "Kagoshiman X";
	public string c2 = "Kagoshiman X";
	
	// Use this for initialization
	void Start ()
	{
		if (ScoreManager.i)
			score.text = "Score : " + ScoreManager.i.point;
		
		StartCoroutine (Credit ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Space)) {
			ScoreManager.i.BackToStart ();
		}
		
		if (Input.GetMouseButtonUp (0)) {
			ScoreManager.i.BackToStart ();
		}
	
	}
	
	private IEnumerator Credit ()
	{
		
		while (true) {
			credit.text = c1;
		
			yield return new WaitForSeconds(2f);
		
		
			credit.text = c2;
		
			yield return new WaitForSeconds(2f);
		}
		
		
		
	}
}
