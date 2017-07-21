using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemoMove : PlayableBase {

	//移動量
	[SerializeField]
	float spd_rotate;
	[SerializeField]
	float spd_shot;

	//キャラのステートごとのsprite
	[SerializeField]
	Sprite spr_ground;
	[SerializeField]
	Sprite spr_air;

	//各コンポーネント
	SpriteRenderer spr_;
	Rigidbody2D rig_;
	[SerializeField]
	BoxCollider2D box_col;
	[SerializeField]
	BoxCollider2D box_tri;

	int cnt_enableCollision = 10;

	// Use this for initialization
	public void Start () {
		base.Start ();
		spr_ = GetComponent<SpriteRenderer> ();
		rig_ = GetComponent<Rigidbody2D> ();

		box_col.enabled = false;
		box_tri.enabled = false;

		rig_.velocity = new Vector2 (0,spd_shot);
	}

	public void Update(){
		base.Update ();

		if (cnt_enableCollision == 0) {
			box_col.enabled = true;
			box_tri.enabled = true;
		} else {
			cnt_enableCollision--;
		}
	}

	protected override void AirMove (){
		spr_.sprite = spr_air;
		transform.eulerAngles += new Vector3 (0,0,spd_rotate);

		rig_.velocity = new Vector2 (0,rig_.velocity.y);
	}

	protected override void GroundMove (){
		spr_.sprite = spr_ground;
		transform.rotation = Quaternion.identity;
	}
}
