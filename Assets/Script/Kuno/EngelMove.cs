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
	bool flg_fall = false;

	public float num_fallTime = 0;
	public float spd_fall = 0.0005f;

	Coroutine baf_cor;
	IEnumerator cor_down;

	// Use this for initialization
	protected void Start () {
		//spr_ = GetComponent<SpriteRenderer> ();
		pos_ = transform.position;
		rig_ = GetComponent<Rigidbody2D> ();
		pos_init = transform.position;
		cor_down = Down ();
	}

	// Update is called once per frame
	public void Update () {

		if(!flg_fall)
			cnt_Reatch++;

		if (cnt_Reatch != 0) {
			rig_.MovePosition(new Vector2 (
				pos_init.x + (cur_move.Evaluate (Mathf.PingPong (cnt_Reatch / num_Reatch, 1)) * num_radius),
				pos_init.y + 0));
		}
	}

	IEnumerator Down(){
		yield return new WaitForSeconds (num_fallTime);

		while (flg_down) {

			flg_fall = true;
			cnt_down += spd_fall;
			rig_.MovePosition (new Vector2 (transform.position.x, transform.position.y - cnt_down));
			yield return null;
		}

		yield break;
	}

	protected override void RaycastHit (){
		if (!flg_down && baf_cor == null) {
			baf_cor = StartCoroutine (cor_down);
			flg_down = true;
		}
	}

	protected override void RaycastExit ()
	{
		if (flg_down) {
			cnt_down = 0;
			flg_down = false;
			pos_init.y = transform.position.y;
			StopCoroutine (cor_down);
			cor_down = null;
			cor_down = Down();
			baf_cor = null;

			flg_fall = false;
		}
	}
}
