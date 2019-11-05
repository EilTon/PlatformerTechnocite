using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{

	public float _speed;
	public float _gravity;

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
        if(Input.GetKey(KeyCode.Q))
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontal += 1;
		}
		_velocity.x = horizontal * _speed;
		if(_moverController._collisions.bottom == true)
		{
			_velocity.y = 0;
		}
		_velocity.y += _gravity * Time.deltaTime * 1f;
		_moverController.Move(_velocity * Time.deltaTime);
	}
}
