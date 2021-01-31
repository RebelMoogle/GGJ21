using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public CharacterController2D controller;
	public ReceiveDamage receiveDamage;
    public PunchEm punchEm;
	public Animator animator;

	public float crossFade = 0.2f;

	public float runSpeed = 40f;

    public bool isJumping = false;
    public bool isPunching = false;

    public bool startFlipped = true;

    public float stepSpeed = 1.0f;

    public float RepeatActionTime = 1.0f;

    float timePassed = 0.0f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    private void Start() {
        controller.OnLandEvent.AddListener(this.OnLanding);
        controller.OnCrouchEvent.AddListener(this.OnCrouching);
		receiveDamage.damageEvent.AddListener(this.OnDamage);

        if(startFlipped) {
            controller.Flip();
        }
    }

    // Update is called once per frame
    void Update ()
    {

        if (receiveDamage.GetHealthState() == HealthState.Knockout)
        {
            // do not move when knocked out
            return;
        }

        timePassed += Time.deltaTime;
        if (timePassed > RepeatActionTime)
        {
            timePassed = 0.0f;
            HandleMovement();
        }

    }

    private void HandleMovement()
    {
        horizontalMove = stepSpeed * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (isJumping)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (isPunching)
        {
            punchEm.DoAttack("Punch", controller.IsFacingRight(), animator, crossFade);
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
