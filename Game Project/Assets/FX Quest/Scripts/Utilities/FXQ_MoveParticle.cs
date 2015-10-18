// FX Quest
// Version: 0.4.1
// Author: Gold Experience Team (http://www.ge-team.com/pages)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion //Namespaces

public enum eMoveMethod
{
	LeftRight,
	UpDown,
	CircularXY_Clockwise,
	CircularXY_CounterClockwise,
	CircularXZ_Clockwise,
	CircularXZ_CounterClockwise,
	CircularYZ_Clockwise,
	CircularYZ_CounterClockwise,
}

/***************
* FXQ_2D_Demo class
* This class handles user key inputs, play and stop all particle effects for 2D demo scene.
* 
* Up/Down Buttons to switch Particle type
* Left/Right Buttons to switch Particle.
**************/

public class FXQ_MoveParticle : MonoBehaviour
{
	
	#region Variables

		public eMoveMethod m_MoveMethod = eMoveMethod.LeftRight;
		eMoveMethod m_MoveMethodOld = eMoveMethod.LeftRight;

		public float m_Range = 5.0f;

		float m_RangeOld = 5.0f;

		public float m_Speed = 2.0f;

		bool m_ToggleDirectionFlag = false;
	
	#endregion
		
	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Start () {
	
		}
	
		// Update is called once per frame
		void Update () {

			if(m_MoveMethod!=m_MoveMethodOld || m_Range!=m_RangeOld)
			{
				m_MoveMethodOld = m_MoveMethod;
				ResetPosition();
			}

			// Check what method to control moving of this Particle object
			switch(m_MoveMethod)
			{
			case eMoveMethod.LeftRight:
				UpdateLeftRight();
				break;
			case eMoveMethod.UpDown:
				UpdateUpDown();
				break;
			case eMoveMethod.CircularXY_Clockwise:
				UpdateCircularXY_Clockwise();
				break;
			case eMoveMethod.CircularXY_CounterClockwise:
				UpdateCircularXY_CounterClockwise();
				break;
			case eMoveMethod.CircularXZ_Clockwise:
				UpdateCircularXZ_Clockwise();
				break;
			case eMoveMethod.CircularXZ_CounterClockwise:
				UpdateCircularXZ_CounterClockwise();
				break;
			case eMoveMethod.CircularYZ_Clockwise:
				UpdateCircularYZ_Clockwise();
				break;
			case eMoveMethod.CircularYZ_CounterClockwise:
				UpdateCircularYZ_CounterClockwise();
				break;
			}
	
		}

	#endregion //MonoBehaviour functions
	
	// ######################################################################
	// Update Position Functions
	// ######################################################################
	
	#region Update Positions

	// Reset the positon of this Particle object
	void ResetPosition()
	{
		switch(m_MoveMethod)
		{
		case eMoveMethod.LeftRight:
		case eMoveMethod.UpDown:
			this.transform.localPosition = new Vector3(0,0,0);
			break;
		case eMoveMethod.CircularXY_Clockwise:
		case eMoveMethod.CircularXY_CounterClockwise:
		case eMoveMethod.CircularXZ_Clockwise:
		case eMoveMethod.CircularXZ_CounterClockwise:
			this.transform.localPosition = new Vector3(m_Range,0,0);
			break;
		case eMoveMethod.CircularYZ_Clockwise:
		case eMoveMethod.CircularYZ_CounterClockwise:
			this.transform.localPosition = new Vector3(0,0,m_Range);
			break;
		}
		m_RangeOld = m_Range;
	}

	void UpdateLeftRight()
	{
		// moving to left
		if(m_ToggleDirectionFlag==false)
		{
			this.transform.localPosition = new Vector3(this.transform.localPosition.x-(m_Speed*Time.deltaTime),0,0);
			if(this.transform.localPosition.x<=-m_Range)
			{
				m_ToggleDirectionFlag=true;
			}
		}
		// moving to right
		else
		{
			this.transform.localPosition = new Vector3(this.transform.localPosition.x+(m_Speed*Time.deltaTime),0,0);
			if(this.transform.localPosition.x>=m_Range)
			{
				m_ToggleDirectionFlag=false;
			}
		}
	}

	void UpdateUpDown()
	{
		// moving up
		if(m_ToggleDirectionFlag==false)
		{
			this.transform.localPosition = new Vector3(0,this.transform.localPosition.y+(m_Speed*Time.deltaTime),0);
			if(this.transform.localPosition.y>=m_Range)
			{
				m_ToggleDirectionFlag=true;
			}
		}
		// moving down
		else
		{
			this.transform.localPosition = new Vector3(0,this.transform.localPosition.y-(m_Speed*Time.deltaTime),0);
			if(this.transform.localPosition.y<=-m_Range)
			{
				m_ToggleDirectionFlag=false;
			}
		}
	}

	void UpdateCircularXY_Clockwise()
	{
		float centerX = 0;
		float centerY = 0;
		float point2x = this.transform.localPosition.x;
		float point2y = this.transform.localPosition.y;
		float newX = centerX + ((point2x-centerX)*Mathf.Cos(m_Speed/360.0f) - (point2y-centerY)*Mathf.Sin(m_Speed/360.0f));
		float newY = centerY + ((point2x-centerX)*Mathf.Sin(m_Speed/360.0f) + (point2y-centerY)*Mathf.Cos(m_Speed/360.0f));

		this.transform.localPosition = new Vector3(newX,newY,0);
	}

	void UpdateCircularXY_CounterClockwise()
	{
		float centerX = 0;
		float centerY = 0;
		float point2x = this.transform.localPosition.x;
		float point2y = this.transform.localPosition.y;
		float newX = centerX + ((point2x-centerX)*Mathf.Cos(-m_Speed/360.0f) - (point2y-centerY)*Mathf.Sin(-m_Speed/360.0f));
		float newY = centerY + ((point2x-centerX)*Mathf.Sin(-m_Speed/360.0f) + (point2y-centerY)*Mathf.Cos(-m_Speed/360.0f));
		
		this.transform.localPosition = new Vector3(newX,newY,0);
	}
	
	void UpdateCircularXZ_Clockwise()
	{
		float centerX = 0;
		float centerZ = 0;
		float point2x = this.transform.localPosition.x;
		float point2z = this.transform.localPosition.z;
		float newX = centerX + ((point2x-centerX)*Mathf.Cos(m_Speed/360.0f) - (point2z-centerZ)*Mathf.Sin(m_Speed/360.0f));
		float newZ = centerZ + ((point2x-centerX)*Mathf.Sin(m_Speed/360.0f) + (point2z-centerZ)*Mathf.Cos(m_Speed/360.0f));
		
		this.transform.localPosition = new Vector3(newX,0,newZ);
	}
	
	void UpdateCircularXZ_CounterClockwise()
	{
		float centerX = 0;
		float centerZ = 0;
		float point2x = this.transform.localPosition.x;
		float point2z = this.transform.localPosition.z;
		float newX = centerX + ((point2x-centerX)*Mathf.Cos(-m_Speed/360.0f) - (point2z-centerZ)*Mathf.Sin(-m_Speed/360.0f));
		float newZ = centerZ + ((point2x-centerX)*Mathf.Sin(-m_Speed/360.0f) + (point2z-centerZ)*Mathf.Cos(-m_Speed/360.0f));
		
		this.transform.localPosition = new Vector3(newX,0,newZ);
	}
	
	void UpdateCircularYZ_Clockwise()
	{
		float centerY = 0;
		float centerZ = 0;
		float point2y = this.transform.localPosition.y;
		float point2z = this.transform.localPosition.z;
		float newY = centerY + ((point2y-centerY)*Mathf.Cos(m_Speed/360.0f) - (point2z-centerZ)*Mathf.Sin(m_Speed/360.0f));
		float newZ = centerZ + ((point2y-centerY)*Mathf.Sin(m_Speed/360.0f) + (point2z-centerZ)*Mathf.Cos(m_Speed/360.0f));
		
		this.transform.localPosition = new Vector3(0,newY,newZ);
	}
	
	void UpdateCircularYZ_CounterClockwise()
	{
		float centerY = 0;
		float centerZ = 0;
		float point2y = this.transform.localPosition.y;
		float point2z = this.transform.localPosition.z;
		float newY = centerY + ((point2y-centerY)*Mathf.Cos(-m_Speed/360.0f) - (point2z-centerZ)*Mathf.Sin(-m_Speed/360.0f));
		float newZ = centerZ + ((point2y-centerY)*Mathf.Sin(-m_Speed/360.0f) + (point2z-centerZ)*Mathf.Cos(-m_Speed/360.0f));
		
		this.transform.localPosition = new Vector3(0,newY,newZ);
	}
	
	#endregion //Update Positions
}
