using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSkinDefinitions : MonoBehaviour {
    Mesh sphere;
    Mesh marMirror;
    Mesh marUnique;
	SkinUnlockManager unlocks;
	public Dictionary<Marble.Skin, Marble.SkinClass> definition;
	static bool created = false;
    void Start()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
		
		unlocks = FindObjectOfType<SkinUnlockManager>();
		sphere = PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Sphere);
        marMirror = (Mesh)Resources.Load("Marbles/MirroredMarble", typeof(Mesh));
        marUnique = (Mesh)Resources.Load("Marbles/UniqueMarble", typeof(Mesh));

		CreateMarbleSkinDefinitions();
	}
	
	//Create a class for each skin. This will get long
	void CreateMarbleSkinDefinitions () {
		definition = new Dictionary<Marble.Skin,Marble.SkinClass>();

		Marble.SkinClass DarkCaustic = new Marble.SkinClass();
		DarkCaustic.skinIndex = Marble.Skin.DarkCaustic;
		DarkCaustic.skinRarity = Marble.Rarity.Common;
		DarkCaustic.skinMat = (Material)Resources.Load("Skins/Dark Caustic", typeof(Material));
		DarkCaustic.skinMesh = sphere;
		DarkCaustic.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.DarkCaustic);
		definition.Add(Marble.Skin.DarkCaustic, DarkCaustic);

		Marble.SkinClass BlackMarble = new Marble.SkinClass();
		BlackMarble.skinIndex = Marble.Skin.BlackMarble;
		BlackMarble.skinRarity = Marble.Rarity.Common;
		BlackMarble.skinMat = (Material)Resources.Load("Skins/Black Marble", typeof(Material));
		BlackMarble.skinMesh = sphere;
		BlackMarble.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.BlackMarble);
		definition.Add(Marble.Skin.BlackMarble, BlackMarble);

		Marble.SkinClass FlowerPower = new Marble.SkinClass();
		FlowerPower.skinIndex = Marble.Skin.FlowerPower;
		FlowerPower.skinRarity = Marble.Rarity.Common;
		FlowerPower.skinMat = (Material)Resources.Load("Skins/Flower Power", typeof(Material));
		FlowerPower.skinMesh = sphere;
		FlowerPower.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.FlowerPower);
		definition.Add(Marble.Skin.FlowerPower, FlowerPower);

		Marble.SkinClass FoolishGold = new Marble.SkinClass();
		FoolishGold.skinIndex = Marble.Skin.FoolishGold;
		FoolishGold.skinRarity = Marble.Rarity.Rare;
		FoolishGold.skinMat = (Material)Resources.Load("Skins/Foolish Gold", typeof(Material));
		FoolishGold.skinMesh = sphere;
		FoolishGold.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.FoolishGold);
		definition.Add(Marble.Skin.FoolishGold, FoolishGold);

		Marble.SkinClass OceanGreen = new Marble.SkinClass();
		OceanGreen.skinIndex = Marble.Skin.OceanGreen;
		OceanGreen.skinRarity = Marble.Rarity.Legendary;
		OceanGreen.skinMat = (Material)Resources.Load("Skins/Ocean Green", typeof(Material));
		OceanGreen.skinMesh = sphere;
		OceanGreen.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.OceanGreen);
		definition.Add(Marble.Skin.OceanGreen, OceanGreen);

		Marble.SkinClass RedNumber8 = new Marble.SkinClass();
		RedNumber8.skinIndex = Marble.Skin.RedNumber8;
		RedNumber8.skinRarity = Marble.Rarity.Common;
		RedNumber8.skinMat = (Material)Resources.Load("Skins/Red Number 8", typeof(Material));
		RedNumber8.skinMesh = marMirror;
		RedNumber8.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.RedNumber8);
		definition.Add(Marble.Skin.RedNumber8, RedNumber8);

		Marble.SkinClass Swampy = new Marble.SkinClass();
		Swampy.skinIndex = Marble.Skin.Swampy;
		Swampy.skinRarity = Marble.Rarity.Common;
		Swampy.skinMat = (Material)Resources.Load("Skins/Swampy", typeof(Material));
		Swampy.skinMesh = sphere;
		Swampy.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.Swampy);
		definition.Add(Marble.Skin.Swampy, Swampy);

		Marble.SkinClass WhiteInnocence = new Marble.SkinClass();
		WhiteInnocence.skinIndex = Marble.Skin.WhiteInnocence;
		WhiteInnocence.skinRarity = Marble.Rarity.Common;
		WhiteInnocence.skinMat = (Material)Resources.Load("Skins/White Innocence", typeof(Material));
		WhiteInnocence.skinMesh = sphere;
		WhiteInnocence.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.WhiteInnocence);
		definition.Add(Marble.Skin.WhiteInnocence, WhiteInnocence);

		Marble.SkinClass WhiteTiger = new Marble.SkinClass();
		WhiteTiger.skinIndex = Marble.Skin.WhiteTiger;
		WhiteTiger.skinRarity = Marble.Rarity.Common;
		WhiteTiger.skinMat = (Material)Resources.Load("Skins/White Tiger", typeof(Material));
		WhiteTiger.skinMesh = sphere;
		WhiteTiger.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.WhiteTiger);
		definition.Add(Marble.Skin.WhiteTiger, WhiteTiger);

		Marble.SkinClass Amnesia = new Marble.SkinClass();
		Amnesia.skinIndex = Marble.Skin.Amnesia;
		Amnesia.skinRarity = Marble.Rarity.Legendary;
		Amnesia.skinMat = (Material)Resources.Load("Skins/Amnesia", typeof(Material));
		Amnesia.skinMesh = sphere;
		Amnesia.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.Amnesia);
		definition.Add(Marble.Skin.Amnesia, Amnesia);

		Marble.SkinClass Flux = new Marble.SkinClass();
		Flux.skinIndex = Marble.Skin.Flux;
		Flux.skinRarity = Marble.Rarity.Rare;
		Flux.skinMat = (Material)Resources.Load("Skins/Flux", typeof(Material));
		Flux.skinMesh = sphere;
		Flux.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.Flux);
		definition.Add(Marble.Skin.Flux, Flux);

		Marble.SkinClass Spirit = new Marble.SkinClass();
		Spirit.skinIndex = Marble.Skin.Spirit;
		Spirit.skinRarity = Marble.Rarity.Legendary;
		Spirit.skinMat = (Material)Resources.Load("Skins/Spirit", typeof(Material));
		Spirit.skinMesh = sphere;
		Spirit.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.Spirit);
		definition.Add(Marble.Skin.Spirit, Spirit);

		Marble.SkinClass VanillaGrape = new Marble.SkinClass();
		VanillaGrape.skinIndex = Marble.Skin.VanillaGrape;
		VanillaGrape.skinRarity = Marble.Rarity.Common;
		VanillaGrape.skinMat = (Material)Resources.Load("Skins/Vanilla Grape", typeof(Material));
		VanillaGrape.skinMesh = sphere;
		VanillaGrape.unlocked = unlocks.CheckSkinUnlock(Marble.Skin.VanillaGrape);
		definition.Add(Marble.Skin.VanillaGrape, VanillaGrape);
	}
}
