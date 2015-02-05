using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class LoadStageData : MonoBehaviour {


	public bool IsLocked = true; 
	public int StageNum;
	public Image lockImage;
	private Button btn;



	void Awake () {
		btn = this.GetComponent<Button>();
	}


	// Use this for initialization
	void Start () {

		IsLocked = PlayerPrefs.GetBool("Stage Number " + StageNum + " LockStatus" );

		if(IsLocked)
		{


			btn.interactable = false;
			btn.transition = Selectable.Transition.ColorTint;
			lockImage.enabled = true;
		}else{
			btn.interactable = true;
			btn.transition = Selectable.Transition.Animation;
			lockImage.enabled = false;
		
		}


	}

	public void SetStageNum(){
		GameManger.STAGE_NUM =  StageNum;
	}
	

}
