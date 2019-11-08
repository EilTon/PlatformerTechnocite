using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
	public Player player;
    // Start is called before the first frame update
    void Start()
    {
		Spawn();
	}

    public void Spawn()
	{
		Instantiate(player, transform.position, Quaternion.identity);
	}
}
