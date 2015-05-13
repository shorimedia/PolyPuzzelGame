using UnityEngine;
using System.Collections;

public class PegStateMachine : MonoBehaviour {


	public ItemAttachment AttachedItem;
	public PegTypeMach PegType;
	public PegUpdater pUpdater;

	public PosStatus posStatus;

	public enum BlockState{
		Uncapable,
		Active,
		Normal,
		Dissolve,
		Hover,
		Selected,
		Move
	}
	
	public BlockState blockState = BlockState.Normal;

	public bool isJumpable = false;
	public bool moveIn  = false;  // if a block is empty and block can move into there set true 

	public int bonusPoints = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	public void ChangeBlockState(){
		
		switch (blockState){
		case BlockState.Uncapable :
			GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0.3f);
			
			break;
		case BlockState.Active :
			GameManger.ACTIVE = true;
			GameManger.CURRENT_ACTIVE_BLOCK = this.GetComponent<PegStateMachine>();
			pUpdater.CheckNeighbor();
			GetComponent<Renderer>().material.color = new Color(0, 1, 1);
			
			break;
		case BlockState.Normal : 
			PegType.ChangeBlockType();
			break;
			
		case BlockState.Dissolve :
			if(isJumpable == true){
				
				
				//Use for Itemsystems to st peg's point worth
				if(bonusPoints == 0)
				{
					GameManger.TOTAL_POINTS_COUNT += PegType.hexType.points; 
					
				}else if(bonusPoints > 0)
				{
					GameManger.TOTAL_POINTS_COUNT += bonusPoints; 
					
				}else if(bonusPoints < 0)
					
				{
					
					//Do Nothing
					
				}
				
				// Check if item  is use on dissolve and attached
				if(AttachedItem.AttachedItem != null && AttachedItem.AttachedItem.onDissolve == true)
				{
					AttachedItem.AttachedItem.OnItemDissolve();
				}
				
				
				bonusPoints = 0;
				isJumpable = false;
			}
			
			// clear the Item thats is attach
			AttachedItem.AttachedItem = null;
			
			PegType.blockType = PegTypeMach.BlockType.Empty; // Call change type on normal state
			
			blockState = BlockState.Normal;
			//Update the number of empty blocks
			pUpdater.EmptyPing();
			pUpdater.UpdateNeighbor();
			ChangeBlockState();
			break;
		case BlockState.Hover	:
			PegType.ChangeBlockType();
			GetComponent<Renderer>().material.color = new Color(0, 30, 1);
			
			break;
		case BlockState.Selected : 
			
			GetComponent<Renderer>().material.color = new Color(0, 30, 1);
			
			break;
		case BlockState.Move :
			if(GameManger.CURRENT_ACTIVE_BLOCK != null){
				PegType.blockType = GameManger.CURRENT_ACTIVE_BLOCK.PegType.blockType; 

				RaycastHit hit;
				Ray ray = new Ray (pUpdater.checkPoints[pUpdater.tokenIndex].position,  pUpdater.checkPoints[pUpdater.tokenIndex].forward);
				
				if (Physics.Raycast(ray , out hit,1)){
					if (hit.collider != null){ 
						pUpdater.neighborHEX[pUpdater.tokenIndex] = hit.collider.gameObject.GetComponent<PegStateMachine>();
						pUpdater.neighborHEX[pUpdater.tokenIndex].blockState = BlockState.Dissolve;
						pUpdater.neighborHEX[pUpdater.tokenIndex].ChangeBlockState();
					}}
				
				GameManger.CURRENT_ACTIVE_BLOCK.blockState = BlockState.Dissolve;
				//GameManger.CURRENT_ACTIVE_BLOCK.blockType  = BlockType.Empty;
				//GameManger.CURRENT_ACTIVE_BLOCK.ChangeBlockType();
				GameManger.CURRENT_ACTIVE_BLOCK.ChangeBlockState();
				GameManger.CURRENT_ACTIVE_BLOCK  = null;
				
				GameManger.TOTAL_PINGS -= pUpdater._ping;
				pUpdater._ping = 0;
				GameManger.TOTAL_NULL_PINGS -= pUpdater._nullPing ;
				pUpdater._nullPing = 0;
				pUpdater.UpdateNeighbor();
				
				Messenger.Broadcast("Check Empties");
				Debug.Log ("Check for Empties");
			}
			break;
			
		}
	}
}
