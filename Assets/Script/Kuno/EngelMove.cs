using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelMove : PlayableBase {

	public AnimationCurve cur_move;

	[Range(0,5)]
	public float num_radius;

	[Range(1,500)]
	public float num_Reatch;
	public float cnt_Reatch;

	private int tgl_direction = 1;
	protected Vector2 pos_init;

	//private SpriteRenderer spr_;
	private Vector3 pos_;
	protected Rigidbody2D rig_;

	float cnt_down = 0;
	bool flg_down = false;

	// Use this for initialization
	public void Start () {
		//spr_ = GetComponent<SpriteRenderer> ();
		pos_ = transform.position;
		rig_ = GetComponent<Rigidbody2D> ();
		pos_init = transform.position;
	}
	
	// Update is called once per frame
	public void Update () {

		cnt_Reatch++;
		if (cnt_Reatch != 0) {
			rig_.MovePosition(new Vector2 (
				pos_init.x + (cur_move.Evaluate (Mathf.PingPong (cnt_Reatch / num_Reatch, 1)) * num_radius),
				pos_init.y + 0));
		}
	}

	IEnumerator Down(){
		yield return new WaitForSeconds (1.0f);

		while (flg_down) {
			cnt_down += 0.0005f;
			rig_.MovePosition (new Vector2 (transform.position.x, transform.position.y - cnt_down));
			yield return null;
		}
	}

	protected override void RaycastHit (){
		if (!flg_down) {
			StartCoroutine (Down ());
			flg_down = true;
		}
	}

	protected override void RaycastExit ()
	{
		if (flg_down) {
			StopCoroutine ("Down");
			flg_down = false;
			pos_init = transform.position;
			cnt_down = 0;
		}
	}
}
