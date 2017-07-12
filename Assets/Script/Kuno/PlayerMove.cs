using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	private Rigidbody2D rig_;

	public float spd_horizontalMove;
	public float spd_varticalMove;

	// Use this for initialization
	void Start () {
		rig_ = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(new Vector3 (spd_horizontalMove, 0,0));
		}else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(new Vector3 (-spd_horizontalMove, 0,0));
		} else {
			rig_.velocity = new Vector2 (0,rig_.velocity.y);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			rig_.velocity = new Vector2(rig_.velocity.x,-spd_varticalMove);
		}
	}
}
