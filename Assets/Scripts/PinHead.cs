﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHead : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PinHead")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
