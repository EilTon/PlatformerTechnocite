using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	Player player;
	public GameObject backGround;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(FindPlayerCoroutine());
		StartCoroutine(FollowPlayer());
    }

	IEnumerator FindPlayerCoroutine()
	{
		while(true)
		{
			if(player == null)
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
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
				backGround.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
			}
			yield return true;
		}
	}
}
