using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
	public ReceiveDamage receiveDamage;
	public PunchEm punchEm;
	public Animator animator;
	public StudioEventEmitter walksound;

	[Range(1,2)]
	public int playerNumber = 1;

	public float crossFade = 1.0f;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	string moveInput = "Horizontal";
	string jumpInput = "Jump";
	string crouchInput = "Crouch";
	string attackInput1 = "Punch";



    private void Start() {
		//walksound = GetComponent<StudioEventEmitter>();
        controller.OnLandEvent.AddListener(this.OnLanding);
        controller.OnCrouchEvent.AddListener(this.OnCrouching);
		receiveDamage.damageEvent.AddListener(this.OnDamage);

		if (playerNumber != 1) {
			controller.Flip();
			moveInput = $"{moveInput}-{playerNumber}";
			jumpInput = $"{jumpInput}-{playerNumber}";
			crouchInput = $"{crouchInput}-{playerNumber}";
			attackInput1 = $"{attackInput1}-{playerNumber}";
		}
    }

    // Update is called once per frame
    void Update () {

		
		if (receiveDamage.GetHealthState() == HealthState.Knockout) {
			// do not move when knocked out
			return;
		}


		horizontalMove = Input.GetAxisRaw(moveInput) * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown(jumpInput))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
			AudioController.Instance.PlayOneshotClip("jump");
		}

		if (Input.GetButtonDown(crouchInput))
		{
			crouch = true;
		} else if (Input.GetButtonUp(crouchInput))
		{
			crouch = false;
		}

        if (Input.GetButtonDown(attackInput1))
		{
			animator.CrossFade("Punch", crossFade, -1, 0f);
			punchEm.DoAttack("Punch", controller.IsFacingRight());
			AudioController.Instance.PlayOneshotClip("punch");
		}
	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	private void OnDamage(HealthState healthState)
    {
        animator.CrossFade("Damaged", crossFade, -1, 0f);
		if (healthState == HealthState.Knockout) {
			animator.CrossFade("KnockOut", crossFade);
		}
    }

	void FixedUpdate ()
	{

		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
