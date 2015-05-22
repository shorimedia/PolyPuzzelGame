using UnityEngine;
using System.Collections;

public class PegController : MonoBehaviour {

	
	public PegStateMachine pegState;

	public PegTypeMach PegType;

	// Use this for initialization
	void Start () 
	{
		//pegState = this.gameObject.GetComponent<PegStateMachine>();

		if(GameManger.ACTIVE == false ){
			pegState.moveIn = false;
		}
	}

	void OnMouseOver() 
	{

		if(GameManger.ACTIVE == false )
		{
			pegState.moveIn = false;
		}

		// Only set the mouse over render if Peg not a Active state, Empty type  and uncapable
		if(pegState.blockState != PegStateMachine.BlockState.Active && PegType.blockType != PegTypeMach.BlockType.Empty && pegState.blockState != PegStateMachine.BlockState.Uncapable ){
			pegState.blockState = PegStateMachine.BlockState.Hover;
			pegState.ChangeBlockState();
		}else

			//if move in is true and peg is a empty type set hover state
		if (pegState.moveIn == true && PegType.blockType == PegTypeMach.BlockType.Empty){
			pegState.blockState = PegStateMachine.BlockState.Hover;
			pegState.ChangeBlockState();
		}
		
	}
	
	void OnMouseExit() 
	{
		if(pegState.blockState != PegStateMachine.BlockState.Active && pegState.blockState != PegStateMachine.BlockState.Uncapable )
		{
			pegState.blockState = PegStateMachine.BlockState.Normal;
			pegState.ChangeBlockState();
		}
		
		//if active set empty blocks back false
		if(GameManger.ACTIVE == false){
			pegState.moveIn = false;
		}
		
	}
	
	void OnMouseUp() 
	{
		//Debug.Log("Drag ended!");
		if(pegState.blockState != PegStateMachine.BlockState.Uncapable)
		{

		}


	}
	
	
	void OnMouseDown() 
	{
		//if a block is full, when click on check if there are any other active blocks. if not set to active
		if(pegState.blockState != PegStateMachine.BlockState.Active && GameManger.ACTIVE == false && PegType.blockType != PegTypeMach.BlockType.Empty)
		{
			pegState.blockState = PegStateMachine.BlockState.Active;
			//GameManger.CURRENT_OPEN_BLOCK.moveIn = false;
			
		}else if(pegState.blockState == PegStateMachine.BlockState.Active)
		{
			// if this block is active when clicked on de-activeate it
			GameManger.ACTIVE = false;
			GameManger.CURRENT_OPEN_BLOCK.moveIn = false;
			pegState.blockState = PegStateMachine.BlockState.Normal;

			//if this block a empty block and another  block is  active, check  if this block can be moved into
		}else if( GameManger.ACTIVE == true && PegType.blockType == PegTypeMach.BlockType.Empty && pegState.moveIn == true)
		{
			pegState.blockState = PegStateMachine.BlockState.Move;
			GameManger.ACTIVE = false;

		}
		
		pegState.ChangeBlockState();
		//Debug.Log("Click!");
	}

}
