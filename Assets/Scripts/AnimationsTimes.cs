using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTimes : MonoBehaviour
{
	Animator anim;
	Dictionary<string, float> dictionnary = new Dictionary<string, float>();

	void Start()
	{
		anim = GetComponent<Animator>();
		CalculateAnimationsTime();
	}

	void CalculateAnimationsTime()
	{
		AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
		foreach (AnimationClip clip in clips)
		{
			dictionnary.Add(clip.name, clip.length);
		}

		//foreach (KeyValuePair<string, float> word in dictionnary)
		//{
		//	Debug.Log(word.Key + " " + word.Value);
		//}
	}

	public float GetTime(string clipName)
	{
		//Debug.Log(dictionnary[clipName]);
		return dictionnary[clipName];
	}
}
