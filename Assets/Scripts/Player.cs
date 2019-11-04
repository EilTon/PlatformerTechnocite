using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{

	public float _speed;
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
		if (Input.GetKey(KeyCode.Z))
		{
			vertical += 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			vertical -= 1;
		}
		_velocity = new Vector2(horizontal, vertical);
		_moverController.Move(_velocity * _speed * Time.deltaTime);
	}
}
