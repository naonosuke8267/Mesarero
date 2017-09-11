using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject obj_player;

	public float pos_playerY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pos_playerY = transform.position.y;

		transform.position = new Vector3 (
			transform.position.x,
			Mathf.Clamp(obj_player.transform.position.y,pos_playerY,float.MaxValue),
			-10
		);


	}
}
