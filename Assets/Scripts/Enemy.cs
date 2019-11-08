using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Enemy : MonoBehaviour
{
	public float speed;
	public bool facingRight;
	public float stopTime;
	Vector2 velocity = new Vector2();
	MovementController movementController;
	SpriteRenderer spriteRenderer;
	Coroutine FlipCoro;
	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		movementController = GetComponent<MovementController>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		velocity.x = speed;
	}

	private void Update()
	{
		UpdateMove();
		UpdateFlip();
	}

	private void UpdateMove()
	{

		movementController.Move(velocity * Time.deltaTime);
	}

	private void UpdateFlip()
	{
		if ((velocity.x > 0 && movementController.collisions.right) || (velocity.x < 0 && movementController.collisions.left))
		{
			//spriteRenderer.flipX = !spriteRenderer.flipX;
			//velocity.x = (velocity.x * -1);
			if (FlipCoro == null)
			{
				FlipCoro = StartCoroutine(FlipCoroutine());
			}
		}
		else if (movementController.collisions.frontPit)
		{
			if (FlipCoro == null)
			{
				FlipCoro = StartCoroutine(FlipCoroutine());
			}
			//movementController.collisions.frontPit = false;
			//spriteRenderer.flipX = !spriteRenderer.flipX;
			//velocity.x = (velocity.x * -1);
		}
	}

	IEnumerator FlipCoroutine()
	{
		float actualVelocity = velocity.x;
		velocity.x = 0;
		animator.Play("ChickenIdle");
		yield return new WaitForSeconds(stopTime);
		movementController.collisions.frontPit = false;
		spriteRenderer.flipX = !spriteRenderer.flipX;
		velocity.x = (actualVelocity * -1);
		animator.Play("ChickenRun");
		FlipCoro = null;

	}
}
