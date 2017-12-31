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

	// Use this for initialization
	void Start () {
        allSkins = FindObjectsOfType<MarbleSkin>();
        mat = GetComponent<Renderer>().material;
        mod = GetComponent<MeshFilter>().mesh;

        sphere = PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Sphere);
        marMirror = (Mesh)Resources.Load("Marbles/MirroredMarble", typeof(Mesh));
        marUnique = (Mesh)Resources.Load("Marbles/UniqueMarble", typeof(Mesh));

        if (randomizeOnStart){
            RandomizeSkin();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateSkin()
    {
        switch (currentSkin)
        {
            case (int)Marble.Skin.DarkCaustic: mat = (Material)Resources.Load("Skins/DarkCaustic", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.FlowerPower: mat = (Material)Resources.Load("Skins/FlowerPower", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.OceanGreen: mat = (Material)Resources.Load("Skins/OceanGreen", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.RedNumber8: mat = (Material)Resources.Load("Skins/RedNumber8", typeof(Material)); mod = marMirror; break;
            case (int)Marble.Skin.Swampy: mat = (Material)Resources.Load("Skins/Swampy", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.WhiteTiger: mat = (Material)Resources.Load("Skins/WhiteTiger", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.BlackMarble: mat = (Material)Resources.Load("Skins/BlackMarble", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.WhiteInnocence: mat = (Material)Resources.Load("Skins/WhiteInnocence", typeof(Material)); mod = sphere; break;
            case (int)Marble.Skin.FoolsGold: mat = (Material)Resources.Load("Skins/FoolsGold", typeof(Material)); mod = sphere; break;

            default: break;
        }

        GetComponent<Renderer>().material = mat;
        GetComponent<MeshFilter>().mesh = mod;
        gameObject.name = mat.name;
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
