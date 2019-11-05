using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveController : MonoBehaviour
{
	public int _horizontalRayCount;
	public int _verticalRayCount;
	public LayerMask _layerObstacle;
	public Collisions _collisions;

	BoxCollider2D _boxCollider;
	Vector2 _bottomLeft;
	Vector2 _bottomRight;
	Vector2 _topLeft;
	Vector2 _topRight;
	float _verticalRaySpacing;
	float _horizontalRaySpacing;
	float _skinWidth;

	public struct Collisions
	{
		public bool top, left, bottom, right;

		public void Reset()
		{
			top = bottom = left = right = false;
		}
	}


	// Start is called before the first frame update
	void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_skinWidth = 1 / 16f;
		CalculateSpacing();
	}

	public void Move(Vector2 velocity)
	{
		_collisions.Reset();
		CalculateBound();
		if(velocity.x!=0)
		{
			HorizontalMove(ref velocity);
		}
		if(velocity.y!=0)
		{
			VerticalMove(ref velocity);
		}
		transform.Translate(velocity);
	}

	public void HorizontalMove(ref Vector2 velocity)
	{
		float direction = Mathf.Sign(velocity.x);
		float distance = Mathf.Abs(velocity.x) + _skinWidth;
		for (int i = 0; i < _verticalRayCount; i++)
		{
			Vector2 baseOrigin = direction == 1 ? _bottomRight : _bottomLeft;
			Vector2 origin = baseOrigin + new Vector2(0, _verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(
				origin,
				new Vector2(direction, 0),
				distance,
				_layerObstacle);
			if (hit)
			{
				velocity.x = (hit.distance - _skinWidth) * direction;
				distance = hit.distance - _skinWidth;
				if (direction < 0)
				{
					_collisions.left = true;
				}
				else if (direction > 0)
				{
					_collisions.right = true;
				}
			}
		}
	}

	public void VerticalMove(ref Vector2 velocity)
	{
		float direction = Mathf.Sign(velocity.y);
		float distance = Mathf.Abs(velocity.y) + _skinWidth;
		for (int i = 0; i < _horizontalRayCount; i++)
		{
			Vector2 baseOrigin = direction == 1 ? _topLeft : _bottomLeft;
			Vector2 origin = baseOrigin + new Vector2(_horizontalRaySpacing * i, 0);
			Debug.DrawLine(origin, origin + new Vector2(0, direction * distance));
			RaycastHit2D hit = Physics2D.Raycast(
				origin,
				new Vector2(0, direction),
				distance,
				_layerObstacle);
			if (hit)
			{
				velocity.y = (hit.distance - _skinWidth) * direction;
				distance = hit.distance - _skinWidth;
				if (direction < 0)
				{
					_collisions.bottom = true;
				}
				else if (direction>0)
				{
					_collisions.top = true;
				}
			}
		}
	}

	void CalculateSpacing()
	{
		Bounds bounds = _boxCollider.bounds;
		bounds.Expand(_skinWidth * -2f);
		_verticalRaySpacing = (bounds.size.y) / (_verticalRayCount - 1);
		_horizontalRaySpacing = (bounds.size.x) / (_horizontalRayCount - 1);
	}

	void CalculateBound()
	{
		Bounds bounds = _boxCollider.bounds;
		bounds.Expand(_skinWidth * -2f);
		_bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		_bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		_topLeft = new Vector2(bounds.min.x, bounds.max.y);
		_topRight = new Vector2(bounds.max.x, bounds.max.y);
	}
}
