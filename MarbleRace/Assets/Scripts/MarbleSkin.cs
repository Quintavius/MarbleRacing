using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSkin : MonoBehaviour {
    public int currentSkin;
    public bool randomizeOnStart;

    Material mat;
    Mesh mod;

    Mesh sphere;
    Mesh marMirror;
    Mesh marUnique;

	// Use this for initialization
	void Start () {
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
            default: break;
        }

        GetComponent<Renderer>().material = mat;
        GetComponent<MeshFilter>().mesh = mod;
    }

    public void RandomizeSkin()
    {
        //Going to add tiers to this later, probably through multiple enums
        var enumLength = System.Enum.GetValues(typeof (Marble.Skin)).Length;
        var newSkin = Random.Range(0, enumLength);
        currentSkin = newSkin;
        UpdateSkin();
    }

}
