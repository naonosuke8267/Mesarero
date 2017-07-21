using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableBase : MonoBehaviour {


	public GameObject obj_pipot;

	Vector3 pos_pipot;
	Vector2 num_size;


	// Use this for initialization
	protected void Start () {
		//サイズの取得
		if (obj_pipot.GetComponent<BoxCollider2D> ()) {
			num_size = obj_pipot.GetComponent<BoxCollider2D> ().bounds.size;
		} else if (obj_pipot.GetComponent<CircleCollider2D> ()) {
			num_size = obj_pipot.GetComponent<CircleCollider2D> ().bounds.size;
		} else if (obj_pipot.GetComponent<SpriteRenderer> ()) {
			num_size = obj_pipot.GetComponent<SpriteRenderer> ().bounds.size;
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

		Vector3 baf_vec = new Vector3 (pos_pipot.x + num_size.x / 2,pos_pipot.y,0);
		Debug.DrawRay (baf_vec,arg_direction * num_size.y / 2,Color.magenta,0.01f);

		int layerMask = 1 << LayerMask.NameToLayer ("Default");
		if (Physics2D.Raycast (baf_vec, arg_direction, num_size.y / 2 + 0.1f,layerMask)){
			return true;
		}

		baf_vec = new Vector3 (pos_pipot.x - num_size.x / 2,pos_pipot.y,0);
		Debug.DrawRay (baf_vec,arg_direction * num_size.y / 2,Color.magenta,0.01f);
		if (Physics2D.Raycast (baf_vec, arg_direction, num_size.y / 2 + 0.1f,layerMask)){
			return true;
		}

		return false;
	}

	protected virtual void GroundMove(){
	}

	protected virtual void AirMove(){
	}
}
