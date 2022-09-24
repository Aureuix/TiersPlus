using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GadgetCore.API.Dialog;
using GadgetCore.API;
using GadgetCore.Util;
using UnityEngine;

namespace TiersPlus
{
    internal class Lorekeeper
    {
        internal static void LorekeeperObject() {
            Material lkPortrait = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/portraitLorekeeper.png"),
                mainTextureScale = new Vector2(0.5f, 1)

            };

            int loreKQuest = PreviewLabs.PlayerPrefs.GetInt("loreKQuest", 0);

            PreviewLabs.PlayerPrefs.SetInt("loreKQuest", loreKQuest);

            GameObject loreKeeper = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetNPCResource("ringabolt"));

            loreKeeper.name = "Lorekeeper";

            TileInfo loreKeeperTile = new TileInfo(TileType.INTERACTIVE, GadgetCoreAPI.LoadTexture2D("npcs/lkbody"), loreKeeper).Register("lorekeeper");

            loreKeeperTile.OnInteract += loreKeeperTile.InitiateDialogRoutine;
            loreKeeper.transform.Find("e").Find("ringabolt").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/lkhead.png"),

            };
            loreKeeper.transform.Find("e").Find("ringabolt").Find("Plane_001").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/lkbody.png"),

            };


        }
        internal static void LorekeeperDialogue1() {
            Material lkPortrait = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/portraitLorekeeper.png"),
                mainTextureScale = new Vector2(0.5f, 1)

            };
            DialogChains.RegisterDialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, (b) => {
                return PlayerPrefs.GetInt("LoreKQuest") == 0;
            },
                    "You. Cadet. Could you do me a favor quickly?",
                    "Can you see how my armor's long since rusted up?",
                    "I need you to go to the Desolate Canyon and slay Urugorak. The acid under his scales does wonders for an aching joint...",
                    "Upon your return i can use the remaining acid and the rest of the scale to power a machine that can turn loot such as that scale back into their constituent parts.",
                    new DialogMessage("It will surely prove useful to you...", () => PlayerPrefs.SetInt("LoreKQuest", 1))
                );
        }
        internal static void LorekeeperDialogue2() {
            Material lkPortrait = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/portraitLorekeeper.png"),
                mainTextureScale = new Vector2(0.5f, 1)

            };
            DialogChains.RegisterDialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, (b) => { return PlayerPrefs.GetInt("LoreKQuest") == 1; },
                "",
                new DialogMessage(DialogActions.BranchDialog(
                    new DialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, DialogConditions.HasItem(ItemRegistry.Singleton["Urugorak Scale"].GetID()),
                            "Thank you Cadet, this'll surely help ease my pain...",
                            "In return, here is the machine I promised you.",
                            new DialogMessage(DialogActions.GrantItem(new Item(ItemRegistry.Singleton["overgrownFabricator"].GetID(), 1, 0, 0, 0, new int[3], new int[3]))), 
                            new DialogMessage("hmm. I have something else you could help with later. it is a much tougher task however, please feel free to say no if you cannot complete it.", 
                                () => PlayerPrefs.SetInt("LoreKQuest", 2)
                            )
                        )
                    )
                ),
                "I can wait, cadet..."
            );
            DialogChains.RegisterDialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, (b) => { return PlayerPrefs.GetInt("LoreKQuest") == 2; },
                    "Wow, your flag is 2! Awesome!"
            );
            DialogChains.RegisterDialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, null,
                    "Aww, your flag is unrecognized :("
            );


            DialogChains.RegisterDialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, (b) => { return PlayerPrefs.GetInt("LoreKQuest") == 1; }, "Take your time Cadet, I can wait...");

            DialogConditions.HasItem(ItemRegistry.Singleton["Urugorak Scale"].GetID());

        }
        internal static void LorekeeperDialogue3() { }
        internal static void LorekeeperDialogueNotFound() {
            Material lkPortrait = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/portraitLorekeeper.png"),
                mainTextureScale = new Vector2(0.5f, 1)

            };
            DialogChains.RegisterDialogChain(TileRegistry.Singleton["lorekeeper"].GetID(), "The Lorekeeper", lkPortrait, null, "There is nothing to see here, Cadet.", "Perhaps you messed with the save file?", "(if you see this message something went wrong! contact Aure!)");

        }

    }
}
