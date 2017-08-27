using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelMove : MonoBehaviour {

	public AnimationCurve cur_move;

	[Range(1,100)]
	public float num_radius;

	[Range(1,100)]
	public float num_Reatch;
	public float cnt_Reatch;

	private int tgl_direction = 1;
	private Vector2 pos_init;

	//private SpriteRenderer spr_;
	private Vector3 pos_;
	private Rigidbody2D rig_;

	// Use this for initialization
	void Start () {
		//spr_ = GetComponent<SpriteRenderer> ();
		pos_ = transform.position;
		rig_ = GetComponent<Rigidbody2D> ();
		pos_init = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		cnt_Reatch++;
		if (cnt_Reatch != 0) {
			rig_.MovePosition(new Vector2 (
				pos_init.x + (cur_move.Evaluate (Mathf.PingPong (cnt_Reatch / num_Reatch, 1)) * num_radius),
				pos_init.y + 0));
		}
	}
}
