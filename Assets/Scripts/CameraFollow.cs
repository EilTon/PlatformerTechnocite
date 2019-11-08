using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	Player player;
	public GameObject backGround;
	Scene scene;
	Camera cam;
	float height;
	float width;
	float minXlimit;
	float maxXLimit;
	float minYlimit;
	float maxYLimit;
	// Start is called before the first frame update
	void Start()
	{
		cam = FindObjectOfType<Camera>(); ;
		scene = FindObjectOfType<Scene>();
		height = cam.orthographicSize * 2f;
		width = height * cam.aspect;
		minXlimit = scene.LimitLeftBottom.transform.position.x + width / 2f;
		maxXLimit = scene.LimitRighTop.transform.position.x - width / 2f;
		minYlimit = scene.LimitLeftBottom.transform.position.y + height / 2f;
		maxYLimit = scene.LimitRighTop.transform.position.y - height
			/ 2f;
		StartCoroutine(FindPlayerCoroutine());
		StartCoroutine(FollowPlayer());
	}

	IEnumerator FindPlayerCoroutine()
	{
		while (true)
		{
			if (player == null)
			{
				player = FindObjectOfType<Player>();
			}
			yield return true;
		}
	}

	IEnumerator FollowPlayer()
	{
		while (true)
		{
			
			if (player != null)
			{
				Vector3 target = new Vector3(Mathf.Clamp(player.transform.position.x, minXlimit, maxXLimit), Mathf.Clamp(player.transform.position.y, minYlimit, maxYLimit),transform.position.z);
				transform.position = target;
				//transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
				//backGround.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
			}
			yield return true;
		}
	}
}
