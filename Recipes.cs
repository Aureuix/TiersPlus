using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GadgetCore.API;
using GadgetCore.Util;
using RecipeMenuCore;
using RecipeMenuCore.API;

namespace TiersPlus
{
    internal class Recipes
    {
        internal static void UniversalCrafter() {

            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { 136, 74, 136 }, new Item(ItemRegistry.Singleton["Tiers+:Plasma Tracer"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), ItemRegistry.Singleton["Tiers+:energyemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:PlasmaCannon"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:PlasmaShield"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:PlasmaLance"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:PlasmaArmor"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID(), ItemRegistry.Singleton["Tiers+:energyemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:PlasmaHelmet"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID(), ItemRegistry.Singleton["Tiers+:energyemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:NebulaCannon"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), ItemRegistry.Singleton["Tiers+:poweremblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:particleAccelerator"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:NexusHelmet"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:NexusArmor"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), ItemRegistry.Singleton["Tiers+:poweremblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:NexusShield"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID() }, new Item(ItemRegistry.Singleton["Tiers+:plantain"].GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));

        }
        internal static void OvergrownCrafter() {
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                    new CraftMenuInfo.AdvancedRecipe(
                        new CraftMenuInfo.AdvancedRecipeComponent(new Item(31, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                        new CraftMenuInfo.AdvancedRecipeComponent(new Item(21, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                        new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                        new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Urugorak Scale"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                        )
              ));
            //Urugorak Scale
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(13, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(31, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Hivemind Eye"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Hive Eye
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(1, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(2, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Scarab Tooth"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Scarab Tooth
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(12, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(22, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(32, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Bully Spore"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Bully Spore
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(3, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(5, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(23, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Ancient Core"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Ancient Core
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(4, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(33, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(14, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Plague Spike"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Plague Spike
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(5, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(25, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(34, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Liquid Fire"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Liquid Fire
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(15, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(24, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(35, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ItemRegistry.Singleton["Tiers+:Briar Leaf"].GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            //Briar Leaf
            //Boss Loot Breakdowns

        }
        internal static void AlchemyStation() {
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(ItemRegistry.Singleton["Tiers+:fern"].GetID(), ItemRegistry.Singleton["Tiers+:powercrystal"].GetID(), ItemRegistry.Singleton["Tiers+:lightningbug"].GetID()), new Item(ItemRegistry.Singleton["Tiers+:Health Pack IV"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 2);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(ItemRegistry.Singleton["Tiers+:fern"].GetID(), ItemRegistry.Singleton["Tiers+:lightningbug"].GetID(), ItemRegistry.Singleton["Tiers+:powercrystal"].GetID()), new Item(ItemRegistry.Singleton["Tiers+:Mana Pack IV"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 2);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(ItemRegistry.Singleton["Tiers+:lightningbug"].GetID(), ItemRegistry.Singleton["Tiers+:fern"].GetID(), ItemRegistry.Singleton["Tiers+:powercrystal"].GetID()), new Item(ItemRegistry.Singleton["Tiers+:Energy Pack IV"].GetID(), 1, 0, 0, 0, new int[3], new int[3]), 2);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(ItemRegistry.Singleton["Tiers+:lightningbug"].GetID(), ItemRegistry.Singleton["Tiers+:powercrystal"].GetID(), ItemRegistry.Singleton["Tiers+:fern"].GetID()), new Item(63, 4, 0, 0, 0, new int[3], new int[3]), 3);
            //T+ Items

            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(21, 31, 11), new Item(64, 1, 0, 0, 0, new int[3], new int[3]), 3); //Anti Poison
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(32, 22, 12), new Item(68, 1, 0, 0, 0, new int[3], new int[3]), 3); //Anti Ice
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(33, 23, 13), new Item(69, 1, 0, 0, 0, new int[3], new int[3]), 3); //Anti Flame
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(34, 24, 14), new Item(63, 1, 0, 0, 0, new int[3], new int[3]), 3); //NullDmg
            //Vanilla Changes
        }
        internal static void AncientFabricator() { 
        
        }
        internal static void CreationMachine() {
            GadgetCoreAPI.AddCreationMachineRecipe(ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), new Item(1030, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddCreationMachineRecipe(ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), new Item(909, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddCreationMachineRecipe(ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), new Item(908, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddCreationMachineRecipe(ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID(), new Item(910, 1, 0, 3, 0, new int[3], new int[3]));

        }
        internal static void emblemForge()
        {
            GadgetCoreAPI.AddEmblemRecipe(ItemRegistry.Singleton["Tiers+:powercrystal"].GetID(), ItemRegistry.Singleton["Tiers+:poweremblem"].GetID(), 10);
            GadgetCoreAPI.AddEmblemRecipe(ItemRegistry.Singleton["Tiers+:lightningBug"].GetID(), ItemRegistry.Singleton["Tiers+:lightningemblem"].GetID(), 10);
            GadgetCoreAPI.AddEmblemRecipe(ItemRegistry.Singleton["Tiers+:fern"].GetID(), ItemRegistry.Singleton["Tiers+:fernemblem"].GetID(), 10);
            GadgetCoreAPI.AddEmblemRecipe(ItemRegistry.Singleton["Tiers+:energyOre"].GetID(), ItemRegistry.Singleton["Tiers+:energyemblem"].GetID(), 10);
        }
        internal static void RecipeChanger() {
            var recAlchemy = new RecipePage(RecipePageType.AlchemyStation, "Stamina/Misc. Recipes", GadgetCoreAPI.LoadTexture2D("recipesAlch.png")).RegisterAsVanilla(1);
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(21, 11, 31, 62, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(22, 12, 32, 62, 4, 3));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(33, 13, 23, 67, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(34, 14, 24, 67, 4, 3));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(35, 15, 25, 72, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(26, 36, 16, 72, 4, 3));

            recAlchemy.AddRecipePageEntry(new RecipePageEntry(21, 31, 11, 64, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(32, 32, 12, 68, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(33, 23, 13, 69, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(34, 24, 14, 63, 1, 2)); // Nuldmg
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(35, 25, 15, 73, 1, 2));
            recAlchemy.AddRecipePageEntry(new RecipePageEntry(36, 16, 26, 74, 1, 2));
        }

    }
}
