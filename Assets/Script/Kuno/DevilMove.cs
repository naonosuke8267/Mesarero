﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilMove : EngelMove {

	public float cnt_down = 0;
	bool flg_down = false;

	IEnumerator Down(){
		while (true) {
			cnt_down += 0.001f;
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
}
