using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Flag : MonoBehaviour
{
	Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
		
		animator = GetComponent<Animator>();
		animator.Play("FlagIdle");
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Player player;
		player = collision.gameObject.GetComponent<Player>();
		if(player!=null)
		{
			SceneManager.LoadScene("SandBox");
		}
	}
}
