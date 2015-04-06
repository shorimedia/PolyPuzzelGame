using UnityEngine;
using System.Collections;


public class FadeScreen : MonoBehaviour {
	
	private Animator _animator;
	private int inputIndex = 0;


	public void Awake()
	{
		_animator = GetComponent<Animator>();
		inputIndex = 0;
	}


	public bool IsOpen
	{
		get{return _animator.GetBool("IsOpen");}
		set{_animator.SetBool("IsOpen", value);}
	}

	public void ToggleOn(int inOut)
	{
		if (inOut == inputIndex)
		{
			IsOpen = true;
			inputIndex = 0;
		} else if (inOut != inputIndex)
		{
			IsOpen = false;
			inputIndex = 1;
		}
	}
	

}
