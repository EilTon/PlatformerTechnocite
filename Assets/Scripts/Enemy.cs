﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Enemy : MonoBehaviour
{
	public float speed;
	public bool facingRight;
	public float stopTimeOnFlip;
	public float pushBackForce;
	[HideInInspector]
	public bool dangerous;

	MovementController movementController;
	SpriteRenderer spriteRenderer;
	Vector2 velocity = new Vector2();
	Coroutine flipCoroutine;
	Animator anim;
	AnimationsTimes animationTimes;

	// Start is called before the first frame update
	void Start()
	{
		dangerous = true;
		movementController = GetComponent<MovementController>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		animationTimes = GetComponent<AnimationsTimes>();
		StartFacing();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMove();
		UpdateFlip();
	}

	void UpdateMove()
	{
		movementController.Move(velocity * Time.deltaTime);
	}

	void UpdateFlip()
	{
		// Si on se déplace vers la droite, et touche un mur vers la droite OU BIEN
		// si on se déplace vers la gauche, et touche un mur vers la gauche
		if ((velocity.x > 0 && movementController.collisions.right) ||
		   (velocity.x < 0 && movementController.collisions.left))
		{
			Flip();
		}
		// Si j'atteind un ravin, je flip
		else if (movementController.collisions.frontPit)
		{
			Flip();
		}
	}

	void StartFacing()
	{
		if (facingRight)
		{
			velocity.x = speed;
		}
		else
		{
			velocity.x = -speed;
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
	}

	/// <summary>
	/// Retourne le sprite et la velocity
	/// </summary>
	void Flip()
	{
		if (flipCoroutine == null)
			flipCoroutine = StartCoroutine(FlipCoroutine());
	}

	IEnumerator FlipCoroutine()
	{
		float actualVelocity = velocity.x;
		velocity.x = 0;
		anim.Play("ChickenIdle");
		yield return new WaitForSeconds(stopTimeOnFlip);
		anim.Play("ChickenRun");
		spriteRenderer.flipX = !spriteRenderer.flipX;
		velocity.x = actualVelocity * -1f;
		flipCoroutine = null;
	}

	Coroutine dieCoroutine;
	public void Die()
	{
		if (dieCoroutine == null)
			dieCoroutine = StartCoroutine(DieCoroutine());
	}

	IEnumerator DieCoroutine()
	{
		dangerous = false;
		anim.Play("ChickenHit");
		velocity.x = 0;
		// Wait for the time of the FrogHit animation to be finished
		yield return new WaitForSeconds(0.25f);
		Destroy(gameObject);
		dieCoroutine = null;
		//yield return null;
	}
}
