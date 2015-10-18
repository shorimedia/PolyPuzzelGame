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
* FXQ_RotateShapeParticle class
* This class rotates this object around its own pivot.
* It does rotate the transform rotation value around its own pivot.
**************/
public class FXQ_RotateShapeParticle : MonoBehaviour
{

	// Start Rotation
	public Vector3 m_StartRotation;		// Euler angles when this script starts.
	
	// Start Rotation overtime
	public Vector3 m_RotationOvertime;	// Rotation to rotate this object overtime.
	
		
	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Start ()
		{
			// Set start local rotation
			transform.localEulerAngles = m_StartRotation;
		}
	
		// Update is called once per frame
		void Update ()
		{
			// Rotate around local pivot overtime.
			transform.localEulerAngles = transform.localEulerAngles + (m_RotationOvertime * Time.deltaTime);
		}

	#endregion //MonoBehaviour functions
}
