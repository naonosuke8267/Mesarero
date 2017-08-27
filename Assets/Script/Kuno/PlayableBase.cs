using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableBase : MonoBehaviour {


	public GameObject obj_pipot;

	Vector3 pos_pipot;
	Vector2 num_size;

	public PhysicsMaterial2D phy_air;
	public PhysicsMaterial2D phy_gro;

	protected bool flg_CollisionActive = true;

	// Use this for initialization
	protected void Start () {
		//サイズの取得
		if (GetComponent<BoxCollider2D> ()) {
			num_size = GetComponent<BoxCollider2D> ().bounds.size;
		} else if (GetComponent<CircleCollider2D> ()) {
			num_size = GetComponent<CircleCollider2D> ().bounds.size;
		} else if (GetComponent<SpriteRenderer> ()) {
			num_size = GetComponent<SpriteRenderer> ().bounds.size;
		}


	}
	
	// Update is called once per frame
	protected void Update () {
		pos_pipot = obj_pipot.transform.position;

		if (RayCast (Vector2.down)) {
			GroundMove ();
		} else {
			AirMove ();
		}
	}

	bool RayCast(Vector2 arg_direction){
		if (flg_CollisionActive) {
			Vector3 baf_vec = new Vector3 (pos_pipot.x + num_size.x / 2, pos_pipot.y, 0);
			Debug.DrawRay (baf_vec, arg_direction * num_size.y / 2, Color.magenta, 0.01f);

			int layerMask = 1 << LayerMask.NameToLayer ("Ground");
			if (Physics2D.Raycast (baf_vec, arg_direction, num_size.y / 1.5f, layerMask)) {
				return true;
			}

			baf_vec = new Vector3 (pos_pipot.x - num_size.x / 2, pos_pipot.y, 0);
			Debug.DrawRay (baf_vec, arg_direction * num_size.y / 2, Color.magenta, 0.01f);
			if (Physics2D.Raycast (baf_vec, arg_direction, num_size.y / 1.5f, layerMask)) {
				return true;
			}

			return false;
		}

		return false;
	}

	protected virtual void GroundMove(){
		GetComponent<Rigidbody2D> ().sharedMaterial = phy_gro;
	}

	protected virtual void AirMove(){
		GetComponent<Rigidbody2D> ().sharedMaterial = phy_air;
	}
}
