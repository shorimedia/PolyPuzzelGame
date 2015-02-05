using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class DetailsSettings : MonoBehaviour {


	public Toggle LowResTog, MedResTog, HighResTog, AutoResTog;


	private bool LowRes, MedRes, HighRes, AutoRes;


	public enum DetailSettings
	{
		low,
		med,
		high,
		auto
	}
	
	public DetailSettings details = DetailSettings.auto;


	// Use this for initialization
	public void  Start() {
		LoadDetail(PlayerPrefs.GetString("Options Detail"));
	}



	#region Getting and setter
	public bool LowResolution
	{
		get {return LowRes;}
		set 
		{
			LowRes = value;
			SetDetails(DetailSettings.low);
		}
	}
	public bool MedResolution
	{
		get {return MedRes;}
		set 
		{
			MedRes = value;
			SetDetails(DetailSettings.med);
		}
	}
	
	public bool HighResolution
	{
		get {return HighRes;}
		set 
		{
			HighRes = value;
			SetDetails(DetailSettings.high);
		}
	}
	
	public bool AutoResolution
	{
		get {return AutoRes;}
		set 
		{
			AutoRes = value;
			SetDetails(DetailSettings.auto);
		}
	}
	
	#endregion
	
	public void SetDetails(DetailSettings set)
	{
		
		details = set;
		switch(details)
		{
		case DetailSettings.auto:
			PlayerPrefs.SetString("Options Detail", "Auto");
			//QualitySettings.SetQualityLevel(QualityName[4], true);
			break;
		case DetailSettings.low:
			PlayerPrefs.SetString("Options Detail", "Low");
			QualitySettings.SetQualityLevel(1, true);
			break;
		case DetailSettings.med:
			PlayerPrefs.SetString("Options Detail", "Med");
			QualitySettings.SetQualityLevel(3, true);
			break;
		case DetailSettings.high:
			PlayerPrefs.SetString("Options Detail", "High");
			QualitySettings.SetQualityLevel(5, true);
			break;
			
		}
	}


	public void LoadDetail(string setting)
	{

		switch(setting)
		{
		case "Auto":
			AutoRes = true;
			SetDetails(DetailSettings.auto);
			//AutoResTog.isOn = true;
			break;
		case "Low":
			LowRes = true;
			SetDetails(DetailSettings.low);
			LowResTog.isOn = true;
			break;
		case "Med":
			MedRes = true;
			SetDetails(DetailSettings.med);
		//	MedResTog.isOn = true;
			break;
		case "High":
			HighRes = true;
			SetDetails(DetailSettings.high);
		//	QualitySettings.SetQualityLevel(QualityName[], true);
			break;
			
		}
	}



	

}
