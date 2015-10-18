using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;
//
// Script Name: Clear Data
//Script by: Victor L Josey
// Description: re set play's data
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class ClearData : MonoBehaviour {

    public GameDataManager dataManager;

    public void Clear()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetBool("New Game", true);
        PlayerPrefs.Flush();

        dataManager.CheckNewGame();
    }
}
