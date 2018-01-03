using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlockManager : MonoBehaviour {
	public Dictionary<Marble.Skin, bool> UnlockState;
	public bool forceRefresh;
	void Awake(){
		DontDestroyOnLoad(this.gameObject);

		System.Array templist = System.Enum.GetValues(typeof(Marble.Skin));
		if (ES2.Exists("Unlocks.gsw?tag=UnlockState") && !forceRefresh){
			//If dictionary is same size as marble skin enum, will need to append to ensure updates don't wipe unlocks
			UnlockState = ES2.LoadDictionary<Marble.Skin, bool>("Unlocks.gsw?tag=UnlockState");
		}else{
		UnlockState = new Dictionary<Marble.Skin, bool>();
		InitializeSkins();
		}

	}
	void InitializeSkins () {
		//Set all skins to locked
		System.Array templist = System.Enum.GetValues(typeof(Marble.Skin));
		foreach (Marble.Skin iniSkin in templist){
			UnlockState.Add(iniSkin,false);
		}
		//Ovewrwrite starter skins
		UnlockState[Marble.Skin.DarkCaustic] = true;

		//Save dictionary
		ES2.Save(UnlockState,"Unlocks.gsw?tag=UnlockState");
	}

	public void UnlockSkin(Marble.Skin skin){
		UnlockState[skin] = true;
	}

	public bool CheckSkinUnlock(Marble.Skin skin){
		bool val = UnlockState[skin];
		return val;
	}
}
