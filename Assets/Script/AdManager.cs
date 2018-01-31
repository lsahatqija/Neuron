using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    public static AdManager AdInstance { set; get; }

	// Use this for initialization
	void Start () {
        AdInstance = this;
        Advertisement.Initialize("1687329");
	}

    public void ShowAd()
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = HandleShowResult;
        Advertisement.Show("video", so);
    }

    private void HandleShowResult(ShowResult obj)
    {
        //throw new NotImplementedException();
    }
}
