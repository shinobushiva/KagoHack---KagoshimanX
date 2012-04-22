using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	
	public static ScoreManager i;
	public int point = 0;
	public TextMesh text3DPrefab;
	public int playerHp = 10;
	public GUISkin skin;
	public Texture2D barTex;
	public int life = 3;
	
	public bool noGUI = false;

	// Use this for initialization
	void Start ()
	{
		if (!i) {
			i = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI ()
	{
		if(noGUI)
			return;
		
		GUI.skin = skin;
		
		GUI.Box (new Rect (-100, 0, Screen.width + 200, 100), "");
		GUI.Label (new Rect (20, 60, 100, 40), "" + point + " Points");	
		
		GUI.DrawTexture (new Rect (0, 20, 200f * (playerHp / 10f), 30), barTex);
		GUI.Label (new Rect (20, 20, 100, 40), "HP : " + playerHp);
		//GUI.Label (new Rect (Screen.width - 100, 20, 100, 40), "Life : " + life);
		
	}
	
	public void AddPoint (int n)
	{
		point += n;
	}
	
	public void PlayerDead ()
	{
		if (life > 1) {
			//life--;
			point = 0;
			Application.LoadLevel (Application.loadedLevel);
		} else {
			ScoreManager.i.noGUI = true;
			Application.LoadLevel ("Score");
		}
	}
	
	public void BackToStart ()
	{
		noGUI = false;
		i = null;
		Destroy (gameObject);
		Application.LoadLevel ("Start");
	
	}
}
