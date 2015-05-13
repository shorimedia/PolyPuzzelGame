using UnityEngine;
using System.Collections;

public class ItemEgg : Item {

	public int NumberOfSpawns = 1;

	private ItemSpawner spawner;

	public override void Use()
	{
		GameManger.ACTIVE = false;
		PegObject.isJumpable  = true;

		spawner = GameObject.Find("Item_Bar").GetComponent<ItemSpawner>();
		PegObject.pUpdater.UpdateNeighbor();
	}


	public override void OnItemDissolve()
	{
		switch(NumberOfSpawns)
		{
		case 1 :
			spawner.SpawnItem();
			break;
		case 2 :
			spawner.SpawnItem();
			spawner.SpawnItem();
			break;
		case 3 :
			spawner.SpawnItem();
			spawner.SpawnItem();
			spawner.SpawnItem();
			break;

		default :         

			// Do nothing
			break;
		}


		
	}


}
