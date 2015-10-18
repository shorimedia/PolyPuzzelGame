using UnityEngine;

public class PegStateMachine : MonoBehaviour {
    //
    // Script Name: Peg State Mechine
    //Script by: Victor L Josey
    // Description: Peg state machine that changes peg founcton during the game.
    // (c) 2015 Shoori Studios LLC  All rights reserved.

	public ItemAttachment AttachedItem;
	public GameObject EffectPos;
	public PegTypeMach PegType;
	public PegUpdater pagState_pUpdater;

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


    public Color uncapColor;
    public Color activeColor;
    public Color selectedColor;
    public Color hoverColor;


	public void ChangeBlockState(){
		
		switch (blockState){
		case BlockState.Uncapable :
			pegAudio.PlayPegSoundFX("Uncap");
			GetComponent<Renderer>().material.color = uncapColor;
			
			break;
		case BlockState.Active :
			//Play sound
			pegAudio.PlayPegSoundFX("Activate");

			GameManger.ACTIVE = true;
			PoolableObjects.ActiveEffectSet(EffectPos.transform);

			GameManger.CURRENT_ACTIVE_BLOCK = this.GetComponent<PegStateMachine>();

			GetComponent<Renderer>().material.color = activeColor;

			pagState_pUpdater.CheckNeighbor();
			
			break;

		case BlockState.Normal :

           //  GetComponent<Renderer>().material.color = new Color(0,0,0);
             pagState_pUpdater.CheckNeighbor();
			PegType.ChangeBlockType();

			// if this peg is no longer a empty peg
			if (PegType.blockType != PegTypeMach.BlockType.Empty)
			{
				moveIn = false;
			}

            
			break;
			
		case BlockState.Dissolve :

			//pegAudio.PlayPegSoundFX("Destroy");

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

                    if (PegType.hexType.points >= 200)
                    {
                        Achievements.MaxPoints();
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

                    if (PegType.hexType.points >= 200)
                    {
                        Achievements.MaxPoints();
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

                    if (AttachedItem.AttachedItem.name == "Bomb")
                        {
                            PlayParicleFX("Bomb FX");

                        }
				}


                if (PegType.blockType == PegTypeMach.BlockType.Darkness)
                {
                    PlayParicleFX("Dark FX");
                   
                }


                if (PegType.blockType != PegTypeMach.BlockType.Darkness )
                {

                    PlayParicleFX("Dissolve FX");
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
			GetComponent<Renderer>().material.color = hoverColor;
			
			break;
		case BlockState.Selected :

            if (moveIn)
            {
                pegAudio.PlayPegSoundFX("Open");
                PegType.SpriteObject.enabled = true;
                GetComponent<Renderer>().material.color = selectedColor;
      
            }
            else
            {
                blockState = BlockState.Normal;

                ChangeBlockState();
            }
			
			break;
		case BlockState.Move :

			pegAudio.PlayPegSoundFX("Move");

			if(GameManger.CURRENT_ACTIVE_BLOCK != null){
				PegType.blockType = GameManger.CURRENT_ACTIVE_BLOCK.PegType.blockType; 

				RaycastHit hit;
				Ray ray = new Ray (pagState_pUpdater.checkPoints[pagState_pUpdater.tokenIndex].position,  pagState_pUpdater.checkPoints[pagState_pUpdater.tokenIndex].forward);
				
                //Dissovle peg to jump over
				if (Physics.Raycast(ray , out hit,1)){
					if (hit.collider != null){ 
						pagState_pUpdater.neighborHEX[pagState_pUpdater.tokenIndex] = hit.collider.gameObject.GetComponent<PegStateMachine>();
						pagState_pUpdater.neighborHEX[pagState_pUpdater.tokenIndex].blockState = BlockState.Dissolve;
						pagState_pUpdater.neighborHEX[pagState_pUpdater.tokenIndex].ChangeBlockState();
					}}
				
				GameManger.CURRENT_ACTIVE_BLOCK.blockState = BlockState.Dissolve;
				GameManger.CURRENT_ACTIVE_BLOCK.ChangeBlockState();
				GameManger.CURRENT_ACTIVE_BLOCK  = null;


				blockState = BlockState.Normal;
                ChangeBlockState();

				Messenger.Broadcast("Check Empties");
#if DEBUG
				Debug.Log ("Check for Empties");

#endif		
			}
			break;
			
		}
	}

    void PlayParicleFX(string name)
    {
        GameObject obj = PoolerScript.current.GetPooledObject(name);

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    
    }

}
