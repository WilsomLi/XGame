using UnityEngine;
using System.Collections;
using XEngine;

public class GameComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (this);
		GameMain.Instance.StartUp ();
	}
	
	// Update is called once per frame
	void Update () {
		GameMain.Instance.OnRun (Time.deltaTime);
	}

	void OnApplicationQuit()
	{

	}

	void OnApplicationPause(bool isPause)
	{

	}
}
