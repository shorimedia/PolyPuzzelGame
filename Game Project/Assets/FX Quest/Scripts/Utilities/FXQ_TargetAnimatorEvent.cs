// FX Quest
// Version: 0.4.1
// Author: Gold Experience Team (http://www.ge-team.com/pages)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion //Namespaces

public enum ParticleEvent
{ 
	None,
	Attack,
	UI
}

/***************
* FXQ_TargetAnimatorEvent class
* This class stores information of behaviors when particle played
* It is used with ParticleSystem in 2D Demo scene.
* 
* FXQ_2D_Demo class use m_TargetAnimator and m_ParticleEvent variables to 
* make a decision in FXQ_2D_Demo.UpdateTargetAnimator() function.
**************/
public class FXQ_TargetAnimatorEvent : MonoBehaviour
{
	
	#region Variables

		public Animator			m_TargetAnimator		= null;
		public ParticleEvent	m_ParticleEvent			= ParticleEvent.None;
	
	#endregion //Variables

}
