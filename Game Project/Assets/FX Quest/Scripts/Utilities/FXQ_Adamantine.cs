// FX Quest
// Version: 0.4.1
// Author: Gold Experience Team (http://www.ge-team.com/pages)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion //Namespaces

/***************
* FXQ_Adamantine class
* This class controls Adamantine sprite animaiton.
**************/

public class FXQ_Adamantine : MonoBehaviour
{
	
	#region Variables
	
		Animator anim = null;
	
	#endregion //Variables
		
	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Awake ()
		{
			anim = GetComponent<Animator>();
		}

		// Use this for initialization
		void Start ()
		{
			anim.SetBool("UnderAttack", false);
		}
	
		// Update is called once per frame
		void Update ()
		{
			// Reset "UnderAttack" condition to false
			//if(anim.GetBool("UnderAttack")==true)
			//{ 
			//	AnimatorStateInfo pAnimatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
			//	if(pAnimatorStateInfo.IsName("Hurt"))
			//	{
			//		anim.SetBool("UnderAttack", false);
			//	}
			//}
		}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// Functions
	// ######################################################################

	#region Functions

		public void UnderAttack()
		{
			// Play Hurt animaiton
			if(anim==null)
				return;

			AnimatorStateInfo pAnimatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
			if(pAnimatorStateInfo.IsName("Idle"))
			{
				// Delay play Hurt animation.
				//anim.SetBool("UnderAttack", true);

				// Immediately play Hurt animation
				anim.Play("Hurt");
			}
		
		}

	#endregion //Functions
}
