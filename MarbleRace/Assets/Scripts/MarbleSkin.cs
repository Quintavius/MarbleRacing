using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSkin : MonoBehaviour {
    [HideInInspector]
    public int currentSkin;
    [HideInInspector]
    public int newSkin;
    public bool randomizeOnStart;
    MarbleSkin[] allSkins;
    Material mat;
    Mesh mod;
    Mesh sphere;
    Mesh marMirror;
    Mesh marUnique;
    MarbleSkinDefinitions def;
    SkinUnlockManager unlock; 

	// Use this for initialization
	void Awake () {
        def = FindObjectOfType<MarbleSkinDefinitions>();
        allSkins = FindObjectsOfType<MarbleSkin>();
        mat = GetComponent<Renderer>().material;
        mod = GetComponent<MeshFilter>().mesh;
        unlock = FindObjectOfType<SkinUnlockManager>();

        sphere = PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Sphere);
        marMirror = (Mesh)Resources.Load("Marbles/MirroredMarble", typeof(Mesh));
        marUnique = (Mesh)Resources.Load("Marbles/UniqueMarble", typeof(Mesh));

        if (randomizeOnStart){
            RandomizeSkin();
        }

    }
	
	// Update is called once per frame
	public void SetSkin (Marble.Skin skin) {
		currentSkin = (int)skin;
        UpdateSkin();
	}

    void UpdateSkin()
    {
        Marble.SkinClass sdef = def.definition[(Marble.Skin)currentSkin];
        GetComponent<Renderer>().material = sdef.skinMat;
        GetComponent<MeshFilter>().mesh = sdef.skinMesh;
        gameObject.name = mat.name;
    }
    public void GenerateLootSkin(){
        var enumLength = System.Enum.GetValues(typeof (Marble.Skin)).Length;

        var tierChance = Random.Range(0, 100);
        if (tierChance > 95){
            //Generate a legendary
            LegendaryStart:
                newSkin = Random.Range(0, enumLength); //generate skin
                if (def.definition[(Marble.Skin)newSkin].skinRarity == Marble.Rarity.Legendary){
                    goto Outer;
                }else{
                    goto LegendaryStart;
                }
        }else if (tierChance > 60){
            //Generate a rare
            RareStart:
                newSkin = Random.Range(0, enumLength); //generate skin
                if (def.definition[(Marble.Skin)newSkin].skinRarity == Marble.Rarity.Rare){
                    goto Outer;
                }else{
                    goto RareStart;
                }
        }else{
            //Generate a filthy common
            CommonStart:
                newSkin = Random.Range(0, enumLength); //generate skin
                if (def.definition[(Marble.Skin)newSkin].skinRarity == Marble.Rarity.Common){
                    goto Outer;
                }else{
                    goto CommonStart;
                }
        }
        //Finish up
        Outer:
        currentSkin = newSkin;
        unlock.UnlockSkin((Marble.Skin)currentSkin);
        UpdateSkin();
       
    }
    public void RandomizeSkin()
    {
        //Going to add tiers to this later, probably through multiple enums
        var enumLength = System.Enum.GetValues(typeof (Marble.Skin)).Length; 
        //This means i'm an NPC, so we gotta make sure skins don't show twice
        //WARNING!!! WITH THIS METHOD IF THERE ARE MORE MARBLES THAN SKINS EVERYTHING EXPLODES
        if (randomizeOnStart){
            Start:
                newSkin = Random.Range(0, enumLength); //Generate value, as normal
                foreach (MarbleSkin ms in allSkins){
                    if (ms.currentSkin == newSkin){ //Skin is already taken, go back
                        goto Start;
                    }
                }goto Outer;
            Outer:
                currentSkin = newSkin; //Apply skin, as normal
                UpdateSkin();
        }else{
            newSkin = Random.Range(0, enumLength);
            currentSkin = newSkin;
            UpdateSkin();
        }
    }
}
