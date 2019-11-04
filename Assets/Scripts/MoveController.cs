using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveController : MonoBehaviour
{
	public int _horizontalRayCount;
	float _horizontalRaySpacing;
	public int _verticalRayCount;
	float _verticalRaySpacing;

	BoxCollider2D _boxCollider;
	Vector2 _bottomLeft;
	Vector2 _bottomRight;
	Vector2 _topLeft;
	Vector2 _topRight;
	// Start is called before the first frame update
	void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		CalculteBound();
		_verticalRaySpacing = _boxCollider.bounds.size.y / (_verticalRayCount - 1);
		for (int i = 0; i < _verticalRayCount; i++)
		{
			Debug.DrawRay(_bottomRight + new Vector2(0, _verticalRaySpacing * i), Vector2.right);
		}
	}

	void CalculteBound()
	{
		_bottomLeft = new Vector2(_boxCollider.bounds.min.x, _boxCollider.bounds.min.y);
		_bottomRight = new Vector2(_boxCollider.bounds.max.x, _boxCollider.bounds.min.y);
		_topLeft = new Vector2(_boxCollider.bounds.min.x, _boxCollider.bounds.max.y);
		_topRight = new Vector2(_boxCollider.bounds.max.x, _boxCollider.bounds.max.y);
	}
}
