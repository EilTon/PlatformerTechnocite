using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveController : MonoBehaviour
{
	//public int _horizontalRayCount;
	public int _verticalRayCount;
	public LayerMask _layerObstacle;

	BoxCollider2D _boxCollider;
	Vector2 _bottomLeft;
	Vector2 _bottomRight;
	Vector2 _topLeft;
	Vector2 _topRight;
	float _verticalRaySpacing;
	float _horizontalRaySpacing;

	// Start is called before the first frame update
	void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_verticalRaySpacing = _boxCollider.bounds.size.y / (_verticalRayCount - 1);
	}

	// Update is called once per frame
	void Update()
	{
		
		
	}

	public void Move(Vector2 velocity)
	{
		CalculateBound();
		HorizontalMove(ref velocity);
		transform.Translate(velocity);
	}

	public void HorizontalMove(ref Vector2 velocity)
	{
		float direction = Mathf.Sign(velocity.x);
		float distance = Mathf.Abs(velocity.x);
		for (int i = 0; i < _verticalRayCount; i++)
		{
			Vector2 baseOrigin = direction == 1 ? _bottomRight : _bottomLeft;
			Vector2 origin = baseOrigin + new Vector2(0, _verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(
				origin,
				new Vector2(direction,0),
				velocity.x,
				_layerObstacle);
			if(hit)
			{
				velocity.x = hit.distance * direction;
			}
			//Debug.DrawRay(_bottomRight + new Vector2(0, _verticalRaySpacing * i), Vector2.right);
		}
	}

	void CalculateBound()
	{
		_bottomLeft = new Vector2(_boxCollider.bounds.min.x, _boxCollider.bounds.min.y);
		_bottomRight = new Vector2(_boxCollider.bounds.max.x, _boxCollider.bounds.min.y);
		_topLeft = new Vector2(_boxCollider.bounds.min.x, _boxCollider.bounds.max.y);
		_topRight = new Vector2(_boxCollider.bounds.max.x, _boxCollider.bounds.max.y);
	}
}
