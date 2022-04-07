using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public PlayerController2D controller;

	public float runSpeed;
	public bool canMove = true;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	public Rigidbody2D myRigidbody2D;
	public Animator anim;

	private void Start()
    {
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update () 
	{
		if(canMove == true)
        {
			runSpeed = 40;
		}
		if (canMove == false)
		{
			runSpeed = 0;

		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
			anim.SetBool("isJump", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
		
	}
  

    void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
        if (horizontalMove != 0)
        {
			anim.SetBool("isRun", true);
		}
		else
        {
			anim.SetBool("isRun", false);
		}

		if (myRigidbody2D.velocity.y < 0)
		{
			anim.SetBool("isJump", false);
			anim.SetBool("isFall", true);
		}
		if (myRigidbody2D.velocity.y == 0)
		{
			anim.SetBool("isJump", false);
			anim.SetBool("isFall", false);
		}
	}
}
