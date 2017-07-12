using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject obj_player;

	public Vector3 pos_;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pos_ = transform.position;


		transform.position = new Vector3 (
			transform.position.x,
			obj_player.transform.position.y,
			-10

		);
	}
}
