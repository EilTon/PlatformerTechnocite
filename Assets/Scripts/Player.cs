using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{

	public float _acceleration;
	public float _maxSpeed;
	public float _timeToMaxJump;
	public float _jumpHeight;
	public bool _airControl;
	public float _speedAirControl;
	float _maxFallingSpeed; 
	float _thresHold = 0.02f;
	float _speed;
	float _gravity;
	float _jumpForce;
	Vector2 _velocity;
	MoveController _moverController;

	// Start is called before the first frame update
	void Start()
	{
		_thresHold = _acceleration * Application.targetFrameRate * 2f;
		_moverController = GetComponent<MoveController>();
		_gravity = -(2 * _jumpHeight) / Mathf.Pow(_timeToMaxJump, 2);
		_jumpForce = Mathf.Abs(_gravity) * _timeToMaxJump;
		_maxFallingSpeed = -_jumpForce;
	}

	// Update is called once per frame
	void Update()
	{
		int horizontal = 0;
		int vertical = 0;
		float airControl = 1;
		if (_moverController._collisions.bottom == true || _moverController._collisions.top == true)
		{
			_velocity.y = 0;
		}

		//if (_moverController._collisions.bottom || _airControl)
		//{
			horizontal = 0;
			if (Input.GetKey(KeyCode.Q))
			{
				horizontal -= 1;
			}
			if (Input.GetKey(KeyCode.D))
			{
				horizontal += 1;
			}
		//}
		/*if (Input.GetKey(KeyCode.Q))
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontal += 1;
		}*/

		if (Input.GetKeyDown(KeyCode.Space) && _moverController._collisions.bottom == true)
		{
			_velocity.y = _jumpForce;
		}

		_velocity.x += horizontal * _acceleration * Time.deltaTime;
		if (_velocity.x > _maxSpeed)
		{
			_velocity.x = _maxSpeed;
		}

		if (_velocity.x < -_maxSpeed)
		{
			_velocity.x = -_maxSpeed;
		}

		if(!_moverController._collisions.bottom)
		{
			airControl = _speedAirControl;
		}

		if (horizontal == 0)
		{
			if (_velocity.x > _thresHold)
			{
				_velocity.x -= _acceleration * Time.deltaTime ;
			}
			else if (_velocity.x < -_thresHold)
			{
				_velocity.x += _acceleration * Time.deltaTime ;
			}
			else
			{
				_velocity.x = 0;
			}
		}



		_velocity.y += _gravity * Time.deltaTime;
		if(_velocity.y < _maxFallingSpeed)
		{
			_velocity.y = _maxFallingSpeed;
		}
		_moverController.Move(_velocity * Time.deltaTime);
	}
}
