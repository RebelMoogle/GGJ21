﻿using System;
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

	[SerializeField]
	public GameObject groundPosition;

	[SerializeField]
	public GameObject AttackPosition;

	private fx_Spawner spawnFX;
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
	string attackInput2 = "Kick";



    private void Start() {
		walksound = GetComponent<StudioEventEmitter>();
        controller.OnLandEvent.AddListener(this.OnLanding);
        controller.OnCrouchEvent.AddListener(this.OnCrouching);
		receiveDamage.damageEvent.AddListener(this.OnDamage);

		
        spawnFX = this.GetComponent<fx_Spawner>();

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

		if (walksound) {
					if (horizontalMove == 0 || walksound.IsPlaying())
			{
				walksound.Stop();
			}
			else
			{
				walksound.Play();
			}
		}

		if (Input.GetButtonDown(jumpInput))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
			AudioController.Instance.PlayOneshotClip("jump");
			spawnFX.SpawnFXJump(groundPosition.transform);
		}

		if (Input.GetButtonDown(crouchInput))
		{
			crouch = true;
			AudioController.Instance.PlayOneshotClip("crouch");
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

        if (Input.GetButtonDown(attackInput1))
		{
			punchEm.DoAttack("Punch", controller.IsFacingRight(), animator, crossFade);
		}
		if (Input.GetButtonDown(attackInput2))
		{
			punchEm.DoAttack("Kick", controller.IsFacingRight(), animator, crossFade);
		}

        if (Input.GetButtonDown("Pause"))
        {
			GameManager.Instance.GetPauseMenu();
        }
	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
		spawnFX.SpawnFXLand(groundPosition.transform);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	private void OnDamage(HealthState healthState, Transform impact)
    {
        animator.CrossFade("Damaged", crossFade, -1, 0f);
		if (healthState == HealthState.Knockout) {
			animator.CrossFade("KnockOut", crossFade);
			// TODO: turn colliders off.
			spawnFX.SpawnFXFall(groundPosition.transform);
		} else {
			spawnFX.SpawnFXHit(impact);
		}
    }

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
