using UnityEngine;
using System.Collections;

public class PegStateMachine : MonoBehaviour {


	public ItemAttachment AttachedItem;
	public GameObject EffectPos;
	public PegTypeMach PegType;
	public PegUpdater pUpdater;

	public PosStatus posStatus;

	public PegAudio pegAudio;

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
			pegAudio.PlayPegSoundFX("Uncap");
			GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0.3f);
			
			break;
		case BlockState.Active :
			//Play sound
			pegAudio.PlayPegSoundFX("Activate");

			GameManger.ACTIVE = true;
			PoolableObjects.ActiveEffectSet(EffectPos.transform);

			GameManger.CURRENT_ACTIVE_BLOCK = this.GetComponent<PegStateMachine>();

			GetComponent<Renderer>().material.color = new Color(0, 1, 1);

			pUpdater.CheckNeighbor();
			
			break;
		case BlockState.Normal : 

			pUpdater.CheckNeighbor();
			PegType.ChangeBlockType();

			// if this peg is no longer a empty peg
			if (PegType.blockType != PegTypeMach.BlockType.Empty)
			{
				moveIn = false;
			}
			break;
			
		case BlockState.Dissolve :

			pegAudio.PlayPegSoundFX("Destroy");

			if(isJumpable == true){
				
				
				//Use for Itemsystems to st peg's point worth
				if(bonusPoints == 0)
				{
					GameManger.TOTAL_POINTS_COUNT += PegType.hexType.points; 

					if(PegType.hexType.points >= 0 && PegType.hexType.points <= 60)
					{
						pegAudio.PlayPegSoundFX("PlusPoints");
					}

					if( PegType.hexType.points > 60)
					{
						pegAudio.PlayPegSoundFX("BigPoints");
					}
					
				}else if(bonusPoints > 0)
				{
					GameManger.TOTAL_POINTS_COUNT += bonusPoints; 

					if(bonusPoints >= 0 && bonusPoints <= 60)
					{
						pegAudio.PlayPegSoundFX("PlusPoints");
					}
					
					if( bonusPoints > 60)
					{
						pegAudio.PlayPegSoundFX("BigPoints");
					}
					
				}else if(bonusPoints < 0)
					
				{
					pegAudio.PlayPegSoundFX("MinusPoints");
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

			pegAudio.PlayPegSoundFX("Move");

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
				GameManger.CURRENT_ACTIVE_BLOCK.ChangeBlockState();
				GameManger.CURRENT_ACTIVE_BLOCK  = null;


				blockState = BlockState.Normal;

				Messenger.Broadcast("Check Empties");
				Debug.Log ("Check for Empties");

				ChangeBlockState();
			}
			break;
			
		}
	}

	public void SelectedSpace()
	{

		if ( moveIn )
		{
		pegAudio.PlayPegSoundFX("Open");
		PegType.SpriteObject.enabled = true;
		GetComponent<Renderer>().material.color = new Color(0, 30, 1);
		}else{
			blockState = BlockState.Normal;
			
			ChangeBlockState();
		}
	}


}
