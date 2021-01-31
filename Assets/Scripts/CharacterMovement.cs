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

	public float crossFade = 1.0f;

	public float runSpeed = 40f;

	SpriteRenderer sprite;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    private void Start() {
        controller.OnLandEvent.AddListener(this.OnLanding);
        controller.OnCrouchEvent.AddListener(this.OnCrouching);
		receiveDamage.damageEvent.AddListener(this.OnDamage);

		sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {

		
		if (receiveDamage.GetHealthState() == HealthState.Knockout) {
			// do not move when knocked out
			return;
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
			RuntimeManager.PlayOneShot("event:/Character/Jump");
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
			RuntimeManager.PlayOneShot("event:/Character/crouch");
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

        if (Input.GetButtonDown("Fire1"))
		{
			animator.CrossFade("Punch", crossFade, -1, 0f);
			punchEm.DoAttack("Punch", controller.IsFacingRight());
		}

		if (Input.GetButtonDown("Fire2"))
		{
			receiveDamage.receiveDamage(100);
		}

        if (Input.GetButtonDown("Pause"))
        {
			GameManager.Instance.GetPauseMenu();
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
		if (healthState == HealthState.Low) {
			sprite.color = new Color(1, 0, 0, 1);
		} else if (healthState == HealthState.Knockout) {
			animator.CrossFade("KnockOut", crossFade);
		}
    }

	void FixedUpdate ()
	{
		if(GameManager.Instance.gameState != GameManager.GameState.PauseMenu)
        {
			// Move our character
			controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
			jump = false;
		}
	}
}
