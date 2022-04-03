using System;
using GadgetCore.API;
using GadgetCore.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossPlus
{
    [Gadget("BossesDropItems", RequiredOnClients: true, LoadPriority: 500)]

    public class BossesDropItems : Gadget<BossesDropItems>
    {
        protected override void Initialize()
        {
            Logger.Log("BossesDropItems" + Info.Mod.Version);
            ItemInfo UrugorakScale = new ItemInfo(ItemType.GENERIC, "Urugorak Scale", "A scale from Urugorak. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("Scale.png")).Register();
            UrugorakScale.AddToLootTable("entity:millipede", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(31, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(21, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(UrugorakScale.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo HiveEye = new ItemInfo(ItemType.GENERIC, "Hivemind Eye", "An eye from The Hivemind. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("HiveEye.png")).Register();
            HiveEye.AddToLootTable("entity:hivemind", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(13, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(31, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(HiveEye.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo ScarabTooth = new ItemInfo(ItemType.GENERIC, "Scarab Tooth", "A tooth from a rock scarab. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("ScarabTeeth.png")).Register();
            ScarabTooth.AddToLootTable("entity:scarab", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(1, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(2, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ScarabTooth.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo BullySpore = new ItemInfo(ItemType.GENERIC, "Bully Spore", "A spore from a Shroom Bully. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("BullySpore.png")).Register();
            BullySpore.AddToLootTable("entity:bully", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(12, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(22, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(32, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(BullySpore.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo AncientCore = new ItemInfo(ItemType.GENERIC, "Ancient Core", "A core from a Golem. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("AncientCore.png")).Register();
            AncientCore.AddToLootTable("entity:golem", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(3, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(5, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(23, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(AncientCore.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo PlagueSpike = new ItemInfo(ItemType.GENERIC, "Plague Spike", "A spike from a plaguebeast. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("PlagueSpike.png")).Register();
            PlagueSpike.AddToLootTable("entity:plaguebeast", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(4, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(33, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(14, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(PlagueSpike.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo LiquidFire = new ItemInfo(ItemType.GENERIC, "Liquid Fire", "Fire from a Lava Dragon \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("LiquidFire.png")).Register();
            LiquidFire.AddToLootTable("entity:lavadragon", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(5, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(25, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(34, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(LiquidFire.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo BriarLeaf = new ItemInfo(ItemType.GENERIC, "Briar Leaf", "A leaf from Moloch \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("BriarLeaf.png")).Register();
            BriarLeaf.AddToLootTable("entity:moloch", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(15, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(24, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(35, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(BriarLeaf.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
        }
    }
}
