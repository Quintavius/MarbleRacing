using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Marble {

    public enum Skin
    {
        DarkCaustic,
        FlowerPower,
        RedNumber8,
        OceanGreen,
        Swampy,
        WhiteTiger,
        BlackMarble,
        WhiteInnocence,
        FoolishGold,
        Amnesia,
        Flux,
        Spirit,
        VanillaGrape
    }
    public enum Rarity{
        Common,
        Rare,
        Legendary
    }

    public class SkinClass{
        public Skin skinIndex;
        public Rarity skinRarity;
        public Material skinMat;
        public Mesh skinMesh;
        public bool unlocked;
    }
}
