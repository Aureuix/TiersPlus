using GadgetCore.API;
using GadgetCore.Util;
using GadgetCore.Loader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using RecipeMenuCore;
using RecipeMenuCore.API;
using GadgetCore.API.Dialog;

namespace TiersPlus
{
    [Gadget("Tiers+", RequiredOnClients: true, LoadPriority: 500)]
    public class TiersPlus : Gadget<TiersPlus>
    {
        
        TileInfo loreKeeperTile;
        
        //ItemInfo MykonogreToken;
        //ItemInfo FellbugToken;
        //ItemInfo GladriaToken;
        
        
        public static EntityInfo MykWormEntity;
        public const string MOD_VERSION = "2.0";
        public const string CONFIG_VERSION = "2.0";

        protected override void Initialize()
        {
            Logger.Log("Tiers+ v" + Info.Mod.Version);
            //Logger.Log("Plasmathrower v" + Info.Mod.Version);
            Items.Weapons();
            Items.Misc();
            Items.Placeables();
            Items.Equipment();
            Items.DropTable();

            Recipes.CreationMachine();
            Recipes.OvergrownCrafter();
            Recipes.CreationMachine();
            Recipes.AncientFabricator();
            Recipes.AlchemyStation();
            Recipes.RecipeChanger();


            Enemies.PlasmaCultist();
            Enemies.PlasmaDragon();
            Enemies.BlastBug();
            Enemies.PlasmaCaster();

            Planets.PlanetObjects();
            Planets.PlasmaZone();

            //Lorekeeper.LorekeeperObject();
            //Lorekeeper.LorekeeperDialogueNotFound();
            //Lorekeeper.LorekeeperDialogue1();
            //Lorekeeper.LorekeeperDialogue2();
            //Lorekeeper.LorekeeperDialogue3();
            

            
            GameObject proj = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load("proj/wyvern"));
            proj.GetComponent<HazardScript>().damage = 40;
            GadgetCoreAPI.AddCustomResource("proj/wyvernCustom", proj);

            GameObject proj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load("proj/spongeProj"));
            proj2.GetComponent<HazardScript>().damage = 45;
            GadgetCoreAPI.AddCustomResource("proj/spongeProjCustom", proj2);

            //projectiles

            
            CharacterRaceInfo Iraian = new CharacterRaceInfo("Iraian", "Iraians are masters of medicine and tech, and put their foxy wit to use \nas combat medics and mechanics. Hailing from the inhospitible ice planet Ira they \nare quite rare, with a population estimated at under 1000.", "The percent chance to unlock is equal to your Mech City portals, capped at 99.", new EquipStats(1, 0, 0, 2, 0, 2), GadgetCoreAPI.LoadTexture2D("Races/foxpreview.png"), GadgetCoreAPI.LoadTexture2D("races/foxalt1.png"), GadgetCoreAPI.LoadTexture2D("races/foxalt2"), GadgetCoreAPI.LoadTexture2D("races/foxalt3.png")).Register("IraFox");
            Iraian.SetUnlockChecker(() =>
            {
                int hold = InstanceTracker.GameScript.GetFieldValue<int[]>("portalUses")[8];
                Logger.Log(InstanceTracker.GameScript.GetFieldValue<int[]>("portalUses")[8]);
                if (hold >= 100) {
                    hold = 99;
                }
                return UnityEngine.Random.Range(0, 100 - hold) == 0;


            });
            

            
            
            //lorekeeper nothing found dialogue
            
            //lorekeeper quest part 1
            
        
            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        

        
        IEnumerator logNPCDialog(String NPC) {
            Logger.Log("NPC " + NPC + "'s Dialogue is working fine!");
            yield break;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 1)
            {

                UnityEngine.Object.Instantiate(loreKeeperTile.Prop, new Vector2(-220f, 0.49f), Quaternion.identity);

            }
        }
    }
}