using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMove : MonoBehaviour {
      public Rigidbody2D rb2D;
      public float speed = 10f;
      Vector3 movement;

//populate the rigidbody2D component in the Start() function:
      void Start(){
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

//capture Inputs in the Update() function:
      void Update(){
           movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
      }

//use the FixedUpdate() function to move the character (all physics go in FixedUpdate()):
      void FixedUpdate(){
            rb2D.position = transform.position + movement * speed * Time.fixedDeltaTime;
      }

}