using UnityEngine;
using System.Collections;
using XEngine;

public class GameComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		LXEngine.Instance.OnRun (Time.deltaTime);
	}
}
