using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GadgetCore.API;
using GadgetCore.Util;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace TiersPlus
{
    internal static class Planets
    {
        internal static void PlanetObjects() {
            ObjectInfo EnergiteOre = new ObjectInfo(ObjectType.ORE, new Item(ItemRegistry.Singleton["Tiers+:energyore"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 1, GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Energite")).Register("TestObject");
            ObjectInfo PlasmaFern = new ObjectInfo(ObjectType.PLANT, new Item(ItemRegistry.Singleton["Tiers+:fern"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 1, GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Fern")).Register("PlasmaFern");
            ObjectInfo LightningBugTree = new ObjectInfo(ObjectType.BUGSPOT, new Item(ItemRegistry.Singleton["Tiers+:lightningbug"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 1, GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/LightningBugNode"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/LightningBugBody"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/LightningBugWing")).Register("LightningBugTree");

        }
        internal static void PlasmaZone() {
            PlanetInfo plasmaZonePlanet = new PlanetInfo(PlanetType.NORMAL,
                    "Plasmatic Rift",
                    new Tuple<int, int>[] { Tuple.Create(-1, 1), Tuple.Create(13, 1) },
                    GadgetCoreAPI.LoadAudioClip("Planets/Plasma Zone/Music")
                );
            plasmaZonePlanet.SetTerrainInfo(
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Entrance"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Zone"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/MidChunkFull"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/MidChunkOpen"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/SideH"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/SideV")
            );
            plasmaZonePlanet.SetBackgroundInfo(
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Parallax"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background3"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background2"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background1"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background0")
            );
            plasmaZonePlanet.SetPortalInfo(
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Sign"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Button"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Icon"));
            plasmaZonePlanet.Register("Plasma Zone");

            plasmaZonePlanet.AddWeightedWorldSpawn(ObjectRegistry.Singleton["Tiers+:TestObject"], 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(ObjectRegistry.Singleton["Tiers+:PlasmaFern"], 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(ObjectRegistry.Singleton["Tiers+:LightningBugTree"], 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(EntityRegistry.Singleton["Tiers+:ParticleWyvern"], 25);
            plasmaZonePlanet.AddWeightedWorldSpawn(EntityRegistry.Singleton["Tiers+:testProjEnemy"], 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(EntityRegistry.Singleton["Tiers+:plasmaPassive"], 15);
            //plasmaZonePlanet.AddWeightedWorldSpawn(plasmaDragonInfo, 10);
            plasmaZonePlanet.AddWeightedWorldSpawn("obj/chest", 20);
            plasmaZonePlanet.AddWeightedWorldSpawn("obj/chestGold", 1);
            plasmaZonePlanet.AddWeightedTownSpawn("obj/chest", 25);
            plasmaZonePlanet.AddWeightedTownSpawn("obj/itemStand", 25);
            plasmaZonePlanet.AddWeightedTownSpawn("obj/chipStand", 25);
            plasmaZonePlanet.AddWeightedWorldSpawn((pos) => (GameObject)null, 12);
            plasmaZonePlanet.AddWeightedTownSpawn((pos) => (GameObject)null, 25);
            plasmaZonePlanet.AddWeightedWorldSpawn("obj/relic", 5);
            //plasmaZonePlanet.AddWeightedWorldSpawn();
            PlanetRegistry.SetVanillaExitPortalWeight(13, plasmaZonePlanet.GetID(), 75);
        }
    }
}
