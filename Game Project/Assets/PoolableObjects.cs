using UnityEngine;
using System.Collections;

public class PoolableObjects : MonoBehaviour {

	public Transform targetPos;

	public static GameObject ActiveEffect;

	public static Transform EffectTransform;

	// Use this for initialization
	void Start () 
	{

		EffectTransform = targetPos;
		ActiveEffect = GameObject.Find("ActivePegEffect");

	}






	
	// Update is called once per frame
	 void Update ()
	{
	
		if(GameManger.ACTIVE == false )
		{
			ActiveEffect.transform.position = EffectTransform.position;
		}


	}


	public static void ActiveEffectSet (Transform trans)
	{
		ActiveEffect.transform.position = trans.position;
		
	}

}
