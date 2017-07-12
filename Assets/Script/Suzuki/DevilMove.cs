﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilMove : MonoBehaviour {

    public AnimationCurve cur_move;

    [Range(1, 100)]
    public float num_radius;

    [Range(1, 100)]
    public float num_Reatch;
    private float cnt_Reatch;

    private int tgl_direction = 1;
    private bool tgl_fall = false;

    private SpriteRenderer spr_;
    private Vector3 pos_;
    private Rigidbody2D rig_;

    // Use this for initialization
    void Start()
    {
        spr_ = GetComponent<SpriteRenderer>();
        pos_ = transform.position;
        rig_ = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        cnt_Reatch++;
        if (cnt_Reatch != 0)
        {
            rig_.velocity = new Vector2(
                tgl_direction * (cur_move.Evaluate(Mathf.PingPong(cnt_Reatch / num_Reatch, 1)) * num_radius),
            0);
        }

        if (cnt_Reatch % num_Reatch == 0)
        {
            tgl_direction *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "inu"&&tgl_fall==false)
        {
            Debug.Log("落ちるよ");
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        Debug.Log("実行開始");
        yield return new WaitForSeconds(3.0f);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        yield return new WaitForSeconds(1.0f);
        GetComponent<Rigidbody2D>().gravityScale = 2;
    }
}