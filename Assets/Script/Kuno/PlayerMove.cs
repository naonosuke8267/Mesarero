using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayableBase {

	private Rigidbody2D rig_;

	public float spd_horizontalMove;
	public float spd_varticalMove;

	public int num_maxJumpCharge;
	int cnt_jumpCharge;

	[SerializeField]
	Sprite spr_ground;
	[SerializeField]
	Sprite spr_air;
	[SerializeField]
	Sprite spr_shot;

	SpriteRenderer spr_;

	enum Status{
		neutoral,
		jump,
	}

	Status enu_status;

	// Use this for initialization
	public void Start () {
		base.Start ();
		enu_status = Status.neutoral;

		//各コンポーネントの取得
		spr_ = GetComponent<SpriteRenderer>();
		rig_ = GetComponent<Rigidbody2D> ();

	}

	public void Update(){
		base.Update ();

		switch (enu_status) {
		case Status.jump:
			ChargeJump ();
			break;
		}
	}

	protected override void GroundMove (){
		spr_.sprite = spr_ground;

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			enu_status = Status.jump;
			rig_.velocity = new Vector2(0,0);
		}
	}

	protected override void AirMove (){
		spr_.sprite = spr_air;

		if (Input.GetKey (KeyCode.RightArrow)) {
			rig_.AddForce (new Vector2(spd_horizontalMove, 0));
		}else if (Input.GetKey (KeyCode.LeftArrow)) {
			rig_.AddForce (new Vector2(-spd_horizontalMove, 0));
		} else {
			rig_.velocity = new Vector2 (0,rig_.velocity.y);
		}
	}

	void ChargeJump(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			rig_.AddForce (new Vector2 (0, -spd_varticalMove));
			cnt_jumpCharge++;

			if (cnt_jumpCharge > num_maxJumpCharge) {
				cnt_jumpCharge = 0;
				enu_status = Status.neutoral;
			}
		} else {
			cnt_jumpCharge = 0;
			enu_status = Status.neutoral;
		}
	}


}
