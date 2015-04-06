using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class ResolutionSettings : MonoBehaviour {

	private bool _full;
	private bool ResAuto,Res800x600,Res720x480,Res1280x800,Res1280x720,Res1024x768;

	public enum ResolutionDetail
	{
		Auto,
		R800x600,
		R720x480,
		R1280x800,
		R1280x720,
		R1024x768
	}

	public ResolutionDetail resolution = ResolutionDetail.R1024x768;

	 

	public void Start()
	{
		//Screen.SetResolution(PlayerPrefs.GetInt("Resolution Width"), PlayerPrefs.GetInt("Resolution Height"), PlayerPrefs.GetBool("Resolution FullScreen"));
	}


	# region Getter and Setter

	public bool FullScreen
	{
		get{return _full;}
		set
		{
			_full = value;
			SetResolution(_full);
		}
	}

	public bool AutoRes
	{
		get{return ResAuto;}
		set
		{
			ResAuto = value;
			resolution = ResolutionDetail.Auto;
			SetResolution(_full);
		}
	}

	public bool R800x600Screen
	{
		get{return Res800x600;}
		set
		{
			Res800x600 = value;
			resolution = ResolutionDetail.R800x600;
			SetResolution(_full);
		}
	}

	public bool R720x480Screen
	{
		get{return Res720x480;}
		set
		{
			Res720x480 = value;
			resolution = ResolutionDetail.R720x480;
			SetResolution(_full);
		}
	}

	public bool R1280x800Screen
	{
		get{return Res1280x800;}
		set
		{
			Res1280x800 = value;
			resolution = ResolutionDetail.R1280x800;
			SetResolution(_full);
		}
	}

	public bool R1280x720Screen
	{
		get{return Res1280x720;}
		set
		{
			Res1280x720 = value;
			resolution = ResolutionDetail.R1280x720;
			SetResolution(_full);
		}
	}

	public bool R1024x768Screen
	{
		get{return Res1024x768;}
		set
		{
			Res1024x768 = value;
			resolution = ResolutionDetail.R1024x768;
			SetResolution(_full);
		}
	}


	#endregion






	public void SetResolution(bool fullScreen)
	{
		switch(resolution)
		{
		case ResolutionDetail.Auto : break;
		case ResolutionDetail.R800x600 :
			Screen.SetResolution(800, 600, fullScreen);
			break;
		case ResolutionDetail.R720x480 :
			Screen.SetResolution(720, 480, fullScreen);
			break;
		case ResolutionDetail.R1280x800 : 
			Screen.SetResolution(1280, 800, fullScreen);
			break;
		case ResolutionDetail.R1280x720 :
			Screen.SetResolution(1280, 720, fullScreen);
			break;
		case ResolutionDetail.R1024x768 : 
			Screen.SetResolution(1024, 768, fullScreen);
			break;
		}

		PlayerPrefs.SetInt("Resolution Width", Screen.width);
		PlayerPrefs.SetInt("Resolution Height", Screen.height);
		PlayerPrefs.SetBool("Resolution FullScreen", fullScreen);

	}


}
