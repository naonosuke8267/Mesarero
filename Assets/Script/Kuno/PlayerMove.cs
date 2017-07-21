using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayableBase {

	private Rigidbody2D rig_;

	//縦横の移動量
	[SerializeField]
	float spd_horizontalMove;
	[SerializeField]
	float spd_varticalMove;
	//ためジャンプを何フレームまで有効にするか
	[SerializeField]
	int num_maxJumpCharge;
	int cnt_jumpCharge;

	//各ステート用スプライト
	[SerializeField]
	Sprite spr_ground;
	[SerializeField]
	Sprite spr_air;
	[SerializeField]
	Sprite spr_shot;

	SpriteRenderer spr_;

	//ネモ
	[SerializeField]
	GameObject obj_shot;
	[SerializeField]
	GameObject obj_carry;
	SpriteRenderer spr_carry;

	Sprite spr_nemo;

	bool flg_shot;
	int cnt_shot;
	int num_shotMotion = 20;

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

		spr_carry = obj_carry.GetComponent<SpriteRenderer> ();

	}

	public void Update(){
		base.Update ();

		switch (enu_status) {
		case Status.jump:
			ChargeJump ();
			break;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) && spr_carry.enabled) {
			Instantiate (obj_shot,transform.position,Quaternion.identity);
			flg_shot = true;
			spr_carry.enabled = false;
		}

		if (flg_shot) {
			cnt_shot++;
			spr_.sprite = spr_shot;

			if (cnt_shot > num_shotMotion) {
				cnt_shot = 0;
				flg_shot = false;
			}
		}
	}

	protected override void GroundMove (){
		if (!flg_shot) {
			spr_.sprite = spr_ground;
		}

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			enu_status = Status.jump;
			rig_.velocity = new Vector2(0,0);
		}
	}

	protected override void AirMove (){
		if (!flg_shot) {
			spr_.sprite = spr_air;
		}

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

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag != "Nemo")
			return;

		spr_carry.enabled = true;
		GameObject.Destroy (col.gameObject);
	}

}
