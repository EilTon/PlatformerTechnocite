﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{

	public float _speed;
	public float _gravity;
	public float _jumpForce;

	Vector2 _velocity;
	MoveController _moverController;

	// Start is called before the first frame update
	void Start()
	{
		_moverController = GetComponent<MoveController>();
	}

	// Update is called once per frame
	void Update()
	{
		int horizontal = 0;
		int vertical = 0;

		if (_moverController._collisions.bottom == true || _moverController._collisions.top == true)
		{
			_velocity.y = 0;
		}

		if (Input.GetKey(KeyCode.Q))
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontal += 1;
		}
		if (Input.GetKeyDown(KeyCode.Space) && _moverController._collisions.bottom == true)
		{
			_velocity.y = _jumpForce;
		}
		_velocity.x = horizontal * _speed;
		_velocity.y += _gravity * Time.deltaTime * -1f;
		_moverController.Move(_velocity * Time.deltaTime);
	}
}
