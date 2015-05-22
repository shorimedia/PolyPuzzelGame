using UnityEngine;
using System.Collections;

public class PosStatus : MonoBehaviour {

	public char[] side = new char[6]; 
	
	public byte groupIndex = 0;  
	
	public bool cennectedGroup = false;
	
	public enum PosState
	{
		IsoPeg,
		EndPeg,
		ReadyPeg,
		TurnPeg,
		AlignPeg,
		ClosedPeg
	}
	
	
	
	public  PosState posState =  PosState.IsoPeg;
	
	
	
	private int NumberOfConnectedSides;
	private int NumberOfNullSides;

	public bool Closed = false;
	
	// Use this for initialization
	void Start () {
		
		// set to zero  at the start of a levels
		groupIndex = 0;
		cennectedGroup = false; 
		
	}
	
	
	public void SetPositionState()
	{

		if(Closed == false)
		{
				NumberOfConnectedSides = 0;
				NumberOfNullSides = 0;
				
				// Set the N and C; Use the number of "C" Connect side  and "N" Null sides to determine the Peg State
				for(int s = 0; s < 6; s++)
				{
					if(side[s] == 'C')
					{
						NumberOfConnectedSides++;
					}
					
					
					if(side[s] == 'N')
					{
						NumberOfNullSides++;
					}
					
				}
		}else{
			NumberOfConnectedSides = 7;
		}


		// Us
		switch(NumberOfConnectedSides)
		{
			
		case 0: 
			posState =  PosState.IsoPeg;
			break;
		case 1:
			
			if(side[0] == 'N' && side[3] == 'C')
			{
				posState =  PosState.EndPeg;
				
			}else
				if(side[1] == 'N' && side[4] == 'C')
			{
				posState =  PosState.EndPeg;
				
			}else
				if(side[2] == 'N' && side[5] == 'C')
			{
				posState =  PosState.EndPeg;
			}else
				if(side[0] == 'C' && side[3] == 'N')
			{
				posState =  PosState.EndPeg;
				
			}else
				if(side[1] == 'C' && side[4] == 'N')
			{
				posState =  PosState.EndPeg;
				
			}else
				if(side[2] == 'C' && side[5] == 'N')
			{
				posState =  PosState.EndPeg;
			}else
			{
				posState =  PosState.ReadyPeg;
			}

			break;
		case 2:
			
			if(NumberOfNullSides < 3)
			{
				if(side[0] == 'C' && side[3] == 'C')
				{
					posState =  PosState.AlignPeg;
					
				}else
					if(side[1] == 'C' && side[4] == 'C')
				{
					posState =  PosState.AlignPeg;
					
				}else
					if(side[2] == 'C' && side[5] == 'C')
				{
					posState =  PosState.AlignPeg;
				}else
				{
					posState =  PosState.ReadyPeg;
				}
			}else
				
			{
				if((side[0] == 'C' && side[2] == 'C') || (side[0] == 'C' && side[4] == 'C') )
				{
					posState =  PosState.TurnPeg;
					
				}else
					if((side[1] == 'C' && side[3] == 'C') || (side[1] == 'C' && side[5] == 'C'))
				{
					posState =  PosState.TurnPeg;
					
				}else
					if((side[5] == 'C' && side[3] == 'C') || (side[4] == 'C' && side[2] == 'C'))
				{
					posState =  PosState.TurnPeg;
					
				}else
				{
					posState =  PosState.ReadyPeg;

				
				}
				
				
			}
			break;

		case 7 :

			posState =  PosState.ClosedPeg;

			break;

		default: 
			posState =  PosState.ReadyPeg;
			break;
			
			
		}
		
		
		
		
	}


}
