using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class EndGameCheck : MonoBehaviour {
	
	
	//public List<HexPositionStatus> endPegStat = new List<HexPositionStatus>();

	//public List<List<char>> Group = new List<List<char>>();

	public static Dictionary<byte, List<PosStatus>> groups = new Dictionary<byte,List<PosStatus>>();

	public static byte TotalNum_GroupIndex = 0;  

	void OnEnable () 
	{
		Messenger.AddListener("Check Empties", EmptyCheck);
		Messenger.MarkAsPermanent("Check Empties");
	}

	void Start()
	{
		TotalNum_GroupIndex = 0;
	}
	
	// do somthing
	void CheckEnd() 
	{
	

		//groups.Add(TotalNum_GroupIndex, new List<char>());

		//groups[TotalNum_GroupIndex].Add('C');




	}


// Look for  Empty peg types
	public void EmptyCheck()
	{
		// Clear array and list for new checks
//		endPegStat.Clear();
//		groups.Clear();
//		TotalNum_GroupIndex = 0;


		GameManger.CURRENT_NUM_EMPTY = 0;
		
		for(int i = 0; i < board.TokenData.Count; i++){
			
			if (board.TokenData[i].PegType.blockType == PegTypeMach.BlockType.Empty){
				GameManger.CURRENT_NUM_EMPTY++;
			}
			
			board.TokenData[i].pUpdater.CheckNeighbor();

			// find EndPegs
			if(board.TokenData[i].posStatus.posState == PosStatus.PosState.EndPeg)
			{
//				endPegStat.Add(board.TokenData[i].posStatus);
			}


			// ONly when there are mor then 1 end peg
//			if( endPegStat.Count > 1)
//			{
//				CheckEnd();
//				
//			}


		}
		Debug.Log("Groups Number " + groups.Count);
	} 



	
	void OnDisable()
	{
		
		Messenger.RemoveListener("Check Empties", EmptyCheck);
	}
	


}
