using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetKeyUp (KeyCode.Space)) {
			Application.LoadLevel ("Game");
		}
		
		if (Input.GetMouseButtonUp (0)) {
			Application.LoadLevel ("Game");
		}
	
	}
}
