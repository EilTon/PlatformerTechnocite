using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
	[Tooltip("Number of meter by second")]
	public float maxSpeed;
	public float timeToMaxSpeed;
	float acceleration;
	float minSpeedThreshold;
	int jumpCount;
	public int maxJump;
	Animator animator;
	Coroutine hitEnemy;
	[Tooltip("Unity value of max jump height")]
	public float jumpHeight;
	[Tooltip("Time in seconds to reach the jump height")]
	public float timeToMaxJump;
	[Tooltip("Can i change direction in air?")]
	[Range(0, 1)]
	public float airControl;
	bool freeze = false;
	float gravity;
	float jumpForce;
	float maxFallingSpeed;
	int horizontal = 0;
	AnimationsTimes animationsTimes;

	Vector2 velocity = new Vector2();
	MovementController movementController;

	// Start is called before the first frame update
	void Start()
	{
		// Math calculation acceleration
		// s = distance
		// a = acceleration
		// t = time
		// s = 1 / 2 at²
		// a = 2s / t²
		acceleration = (2f * maxSpeed) / Mathf.Pow(timeToMaxSpeed, 2);
		animator = GetComponent<Animator>();
		minSpeedThreshold = acceleration / Application.targetFrameRate * 2f;
		movementController = GetComponent<MovementController>();
		animationsTimes = GetComponent<AnimationsTimes>();
		// Math calculation for gravity and jumpForce
		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToMaxJump, 2);
		jumpForce = Mathf.Abs(gravity) * timeToMaxJump;
		maxFallingSpeed = -jumpForce;
	}

	// Update is called once per frame
	void Update()
	{
		if (movementController.collisions.bottom || movementController.collisions.top)
			velocity.y = 0;

		horizontal = 0;

		if (Input.GetKey(KeyCode.D) && freeze == false)
		{
			horizontal += 1;
		}

		if (Input.GetKey(KeyCode.Q) && freeze == false)
		{
			horizontal -= 1;
		}
		AnimationFrog();
		UpdateJump();

		float controlModifier = 1f;
		if (!movementController.collisions.bottom)
		{
			controlModifier = airControl;
		}

		velocity.x += horizontal * acceleration * controlModifier * Time.deltaTime;

		if (Mathf.Abs(velocity.x) > maxSpeed)
			velocity.x = maxSpeed * horizontal;


		if (horizontal == 0)
		{
			if (velocity.x > minSpeedThreshold)
				velocity.x -= acceleration * Time.deltaTime;
			else if (velocity.x < -minSpeedThreshold)
				velocity.x += acceleration * Time.deltaTime;
			else
				velocity.x = 0;
		}

		velocity.y += gravity * Time.deltaTime;
		if (velocity.y < maxFallingSpeed)
			velocity.y = maxFallingSpeed;

		movementController.Move(velocity * Time.deltaTime);
	}

	void Jump()
	{
		velocity.y += jumpForce;
		jumpCount++;
		if (movementController.collisions.left == true || movementController.collisions.right == true)
		{
			jumpCount = 0;
			velocity.y = jumpForce;
		}

	}

	void UpdateJump()
	{
		if (movementController.collisions.bottom || movementController.collisions.left || movementController.collisions.right)
		{
			jumpCount = 0;
		}

		if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= maxJump)
		{
			Jump();
		}


	}

	void AnimationFrog()
	{
		if (freeze == false)
		{
			if (movementController.collisions.left == true || movementController.collisions.right == true)
			{
				PlayAnimation("FrogWallJump");
			}
			else if (velocity.y <= timeToMaxJump && movementController.collisions.bottom != true)
			{
				PlayAnimation("FrogFall");
			}

			else if (velocity.y != 0 && jumpCount > 1)
			{
				PlayAnimation("FrogDoubleJump");
			}

			else if (velocity.x != 0 && velocity.y != 0)
			{
				PlayAnimation("FrogJump");
			}

			else if (velocity.x != 0)
			{
				PlayAnimation("FrogRun");
			}

			else if (velocity.y != 0)
			{
				animator.Play("FrogJump");
			}

			else
			{
				animator.Play("FrogIdle");
			}
		}
	}

	void PlayAnimation(string AnimationName)
	{
		if (velocity.x > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (velocity.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		animator.Play(AnimationName);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();

		if (enemy != null)
		{
			hitEnemy = StartCoroutine(HitEnemyCoroutine());

		}

	}

	IEnumerator HitEnemyCoroutine()
	{

		freeze = true;
		animator.Play("FrogHit");
		yield return new WaitForSeconds(2); //animationsTimes.GetTime("FrogHit")
		SpawnPlayer spawnPlayer = FindObjectOfType<SpawnPlayer>();
		spawnPlayer.Spawn();
		Destroy(gameObject);
		hitEnemy = null;
		freeze = false;
		yield return null;
	}

	
}
