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
        private static readonly FieldInfo canAttack = typeof(PlayerScript).GetField("canAttack", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo attacking = typeof(PlayerScript).GetField("attacking", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo hyper = typeof(PlayerScript).GetField("hyper", BindingFlags.NonPublic | BindingFlags.Instance);
        public static ItemInfo NebulaCannon;
        TileInfo loreKeeperTile;
        ItemInfo PlasmaCannon;
        ItemInfo MykonogreToken;
        ItemInfo FellbugToken;
        ItemInfo GladriaToken;
        PlasmaLanceItemInfo PlasmaLance;
        ItemInfo PlasmaArmor;
        ItemInfo PlasmaHelmet;
        ItemInfo PlasmaShield;
        public static EntityInfo MykWormEntity;
        public const string MOD_VERSION = "2.0";
        public const string CONFIG_VERSION = "2.0";

        protected override void Initialize()
        {
            Logger.Log("Tiers+ v" + Info.Mod.Version);
            Logger.Log("Plasmathrower v" + Info.Mod.Version);
            PlasmaCannon = new ItemInfo(ItemType.WEAPON, "Plasmathrower", "", GadgetCoreAPI.LoadTexture2D("items/PlasmaCannonItem"), Stats: new EquipStats(5, 0, 10, 10, 0, 0), HeldTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaCannonHeld"));
            PlasmaCannon.SetWeaponInfo(new float[] { 0, 0, 0.5f, 0.25f, 0, 0 }, GadgetCoreAPI.GetAttackSound(497));
            PlasmaCannon.Register("PlasmaCannon");
            GameObject PlasmaCannonProj = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetWeaponProjectileResource(471));
            PlasmaCannonProj.GetComponentInChildren<ParticleSystemRenderer>().material = new Material(Shader.Find("Particles/Additive"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("CustomParticleTextureFileName"),
            };
            PlasmaCannonProj.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("items/PlasmaCannonProj"),
            };
            Vector3 particleSystemPos = PlasmaCannonProj.transform.Find("Particle System").localPosition;
            UnityEngine.Object.DestroyImmediate(PlasmaCannonProj.transform.Find("Particle System").gameObject);
            GameObject customParticleSystem = UnityEngine.Object.Instantiate(PlasmaCannonProj.GetComponent<Projectile>().particleAtalanta);
            customParticleSystem.name = "Particle System";
            customParticleSystem.transform.SetParent(PlasmaCannonProj.transform);
            customParticleSystem.transform.localPosition = particleSystemPos;
            customParticleSystem.SetActive(true);
            customParticleSystem.GetComponent<ParticleSystemRenderer>().material = new Material(Shader.Find("Particles/Additive"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("items/particleplasma"),
            };
            GadgetCoreAPI.AddCustomResource("proj/shot" + PlasmaCannon.GetID(), PlasmaCannonProj);


            PlasmaCannon.OnAttack += TripleShot;
            //plasmacannon
            ItemInfo
            particleAccelerator = new ItemInfo(ItemType.WEAPON, "Particle Accelerator", "", GadgetCoreAPI.LoadTexture2D("items/ParticleAccItem"), Stats: new EquipStats(3, 0, 7, 0, 7, 3), HeldTex: GadgetCoreAPI.LoadTexture2D("items/ParticleAccHeld"));
            particleAccelerator.SetWeaponInfo(new float[] { 0, 0, 1.5f, 0, 1, 0 }, GadgetCoreAPI.LoadAudioClip("Sounds/particleacc.wav"));
            particleAccelerator.Register("ParticleAcc");
            GameObject particleAccProj = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetWeaponProjectileResource(470));
            GadgetCoreAPI.AddCustomResource("proj/shot" + particleAccelerator.GetID(), particleAccProj);
            particleAccProj.GetComponent<Projectile>().speed = 55;
            particleAccelerator.OnAttack += particleAccelerator.ShootGun;

            NebulaCannon = new ItemInfo(ItemType.WEAPON, "Nebular Grenadier", "", GadgetCoreAPI.LoadTexture2D("items/NebulaCannonItem"), Stats: new EquipStats(0, 0, 10, 10, 0, 0), HeldTex: GadgetCoreAPI.LoadTexture2D("items/NebulaCannonHeld"));
            NebulaCannon.SetWeaponInfo(new float[] { 0, 0, 0.5f, 2, 0, 0 }, GadgetCoreAPI.GetAttackSound(473));
            NebulaCannon.Register("NebulaCannon");
            NebulaCannon.OnAttack += CustomPlasma;

            ItemInfo plantain = new ItemInfo(ItemType.WEAPON, "Plantain", "", GadgetCoreAPI.LoadTexture2D("items/PlantainItem"), Stats: new EquipStats(0, 0, 0, 0, 10, 7), HeldTex: GadgetCoreAPI.LoadTexture2D("items/PlantainHeld"));
            plantain.SetWeaponInfo(new float[] { 0, 0, 0, 0, 1.5f, 0.5f }, GadgetCoreAPI.GetAttackSound(529));
            plantain.Register("Plantain");
            GameObject plantainProj = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetWeaponProjectileResource(596));
            GadgetCoreAPI.AddCustomResource("proj/shot" + plantain.GetID(), plantainProj);
            plantainProj.GetComponent<Projectile>().speed = 45;
            plantainProj.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("items/PlantainItem"),
            };
            plantain.OnAttack += plantain.CastStaff;

            ItemInfo closeCombatant = new ItemInfo(ItemType.WEAPON, "Close Combatant", "", GadgetCoreAPI.LoadTexture2D("items/ParticleGauntlet"), Stats: new EquipStats(0, 8, 0, 0, 7, 0), HeldTex: GadgetCoreAPI.LoadTexture2D("items/Blank"));
            closeCombatant.SetWeaponInfo(new float[] { 0, 0.5f, 0, 0, 1f, 0 }, GadgetCoreAPI.GetAttackSound(523));
            closeCombatant.Register("CloseCombat");
            GameObject closeCombatProj = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetWeaponProjectileResource(523));
            GadgetCoreAPI.AddCustomResource("proj/shot" + closeCombatant.GetID(), closeCombatProj);
            closeCombatProj.GetComponent<Projectile>().speed = 0;
            closeCombatant.OnAttack += closeCombatant.CastGauntlet;
            closeCombatant.OnAttack += closeCombatant.CastGauntlet;
            closeCombatant.OnAttack += closeCombatant.CastGauntlet;
            closeCombatant.OnAttack += closeCombatant.CastGauntlet;

            GameObject proj = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load("proj/wyvern"));
            proj.GetComponent<HazardScript>().damage = 40;
            GadgetCoreAPI.AddCustomResource("proj/wyvernCustom", proj);

            GameObject proj2 = (GameObject)UnityEngine.Object.Instantiate((GameObject)Resources.Load("proj/spongeProj"));
            proj2.GetComponent<HazardScript>().damage = 45;
            GadgetCoreAPI.AddCustomResource("proj/spongeProjCustom", proj2);

            //projectiles
            PlasmaLance = new PlasmaLanceItemInfo(ItemType.WEAPON, "Plasma Lance", "", GadgetCoreAPI.LoadTexture2D("items/Plasmaspearitem"), Stats: new EquipStats(5, 20, 0, 0, 0, 0), HeldTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaSpear"));
            PlasmaLance.SetWeaponInfo(new float[] { 2f, 4f, 0, 0, 0, 0 }, GadgetCoreAPI.GetAttackSound(367));
            PlasmaLance.Register("PlasmaLance");
            PlasmaLance.OnAttack += PlasmaLance.ThrustLance;
            //plasmalance
            PlasmaArmor = new ItemInfo(ItemType.ARMOR, "Plasmatic Armor", "", GadgetCoreAPI.LoadTexture2D("items/PlasmaArmorinv"), Stats: new EquipStats(8, 4, 8, 4, 8, 4), BodyTex: GadgetCoreAPI.LoadTexture2D("Items/PlasmaArmor"), ArmTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaHand"));
            PlasmaArmor.Register("PlasmaArmor");
            PlasmaHelmet = new ItemInfo(ItemType.HELMET, "Plasmatic Helmet", "", GadgetCoreAPI.LoadTexture2D("items/PlasmaHelm"), Stats: new EquipStats(8, 4, 4, 8, 4, 8), HeadTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaHelmEquip"));
            PlasmaHelmet.Register("PlasmaHelmet");
            ItemInfo NexusArmor = new ItemInfo(ItemType.ARMOR, "Nexus Armor", "", GadgetCoreAPI.LoadTexture2D("items/NexusArmor"), Stats: new EquipStats(6, 5, 5, 5, 8, 8), BodyTex: GadgetCoreAPI.LoadTexture2D("Items/NexusArmor"), ArmTex: GadgetCoreAPI.LoadTexture2D("items/NexusHand"));
            NexusArmor.Register("NexusArmor");
            ItemInfo NexusHelmet = new ItemInfo(ItemType.HELMET, "Nexus Helmet", "", GadgetCoreAPI.LoadTexture2D("items/NexusHelmItem"), Stats: new EquipStats(4, 4, 4, 4, 10, 10), HeadTex: GadgetCoreAPI.LoadTexture2D("items/NexusHelm"));
            NexusHelmet.Register("NexusHelmet");
            PlasmaShield = new ItemInfo(ItemType.OFFHAND, "Nexus Shield", "", GadgetCoreAPI.LoadTexture2D("items/NexusShield"), Stats: new EquipStats(5, 3, 3, 3, 3, 8), HeldTex: GadgetCoreAPI.LoadTexture2D("items/NexusShield"));
            PlasmaShield.Register("NexusShield");
            ItemInfo CarbonHelm = new ItemInfo(ItemType.HELMET, "Carbon Fibre Visor", "Nanomachines son...", GadgetCoreAPI.LoadTexture2D("items/CarbonHead"), Stats: new EquipStats(500, 500, 500, 500, 500, 500), HeadTex: GadgetCoreAPI.LoadTexture2D("Items/CarbonHead"));
            CarbonHelm.Register("CarbonHelm");
            ItemInfo NexusShield = new ItemInfo(ItemType.OFFHAND, "Plasmatic Shield", "", GadgetCoreAPI.LoadTexture2D("items/PlasmaShield"), Stats: new EquipStats(7, 5, 5, 5, 5, 5), HeldTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaShield"));
            NexusShield.Register("Plasmahield");
            //equipment
            ItemInfo SliverCard = new ItemInfo(ItemType.GENERIC, "Sliver Card", "A rare card \nCan be placed \nIn your ship", GadgetCoreAPI.LoadTexture2D("Cards/SliverCard")).Register("Slivercard");
            GameObject SliverCardPlaced = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetPropResource(2518));
            TileInfo SliverCardTile = new TileInfo(TileType.NONSOLID, GadgetCoreAPI.LoadTexture2D("Cards/SliverCardPlaced"), SliverCardPlaced, SliverCard).Register("SliverCardPlaced");
            SliverCardPlaced.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Cards/SliverCardPlaced"),
            };
            ItemInfo GhostCard = new ItemInfo(ItemType.GENERIC, "Ghostshroom Card", "A rare card \nCan be placed \nIn your ship", GadgetCoreAPI.LoadTexture2D("Cards/GhostShroomCard")).Register("Ghostcard");
            GameObject GhostCardPlaced = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetPropResource(2518));
            TileInfo GhostCardTile = new TileInfo(TileType.NONSOLID, GadgetCoreAPI.LoadTexture2D("Cards/GhostShroomCardPlaced"), GhostCardPlaced, GhostCard).Register("GhostCardPlaced");
            GhostCardPlaced.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Cards/GhostShroomCardPlaced"),
            };
            ItemInfo GuardianCard = new ItemInfo(ItemType.GENERIC, "Guardian Card", "A rare card \nCan be placed \nIn your ship", GadgetCoreAPI.LoadTexture2D("Cards/GuardianCard")).Register("Guardcard");
            GameObject GuardianCardPlaced = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetPropResource(2518));
            TileInfo GuardianCardTile = new TileInfo(TileType.NONSOLID, GadgetCoreAPI.LoadTexture2D("Cards/GuardianCardPlaced"), GuardianCardPlaced, GuardianCard).Register("GuardianCardPlaced");
            GuardianCardPlaced.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Cards/GuardianCardPlaced"),
            };
            ItemInfo BlankCard = new ItemInfo(ItemType.GENERIC, "Blank Card", "A rare card \nCard Mimic.", GadgetCoreAPI.LoadTexture2D("Cards/BlankCard")).Register("Blankcard");
            GameObject BlankCardPlaced = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetPropResource(2518));
            TileInfo BlankCardTile = new TileInfo(TileType.NONSOLID, GadgetCoreAPI.LoadTexture2D("Cards/BlankCardPlaced"), BlankCardPlaced, BlankCard).Register("BlankCardPlaced");
            BlankCardPlaced.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Cards/BlankCardPlaced"),
            };
            //cards
            MykonogreToken = new ItemInfo(ItemType.EMBLEM, "Mykonogre Token", "A token dropped from \n Mykonogre \n used to craft items at the universal crafter.", GadgetCoreAPI.LoadTexture2D("MykonogreToken"));
            MykonogreToken.Register("MykonogreToken");

            FellbugToken = new ItemInfo(ItemType.EMBLEM, "Fellbug Token", "A token dropped from \n Fellbug. \n Used to craft items at the universal crafter.", GadgetCoreAPI.LoadTexture2D("FellbugToken"));
            FellbugToken.Register("FellbugToken");

            GladriaToken = new ItemInfo(ItemType.EMBLEM, "Gladria Token", "A token dropped from \n Glaedria. \n used to craft items at the universal crafter.", GadgetCoreAPI.LoadTexture2D("GladriaToken"));
            GladriaToken.Register("GladriaToken");
            //tokens
            //FellbugToken.AddToLootTable("entity:fellbug", 1.0f, 1);
            //GladriaToken.AddToLootTable("entity:glaedria", 1.0f, 1);
            //loot pools
            //plasmathrower
            ItemInfo energiteItem = new ItemInfo(ItemType.LOOT | ItemType.ORE | ItemType.TIER7, "Energite", "LOOT - ORE\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/Energite")).Register("energyore");
            ItemInfo plasmaFernItem = new ItemInfo(ItemType.LOOT | ItemType.PLANT | ItemType.TIER7, "Cactus Spine", "LOOT - PLANT\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/PlasmaFern")).Register("fern");
            ItemInfo powerCrystalItem = new ItemInfo(ItemType.LOOT | ItemType.MONSTER | ItemType.TIER7, "Plasma Claw", "LOOT - MONSTER PART\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/PowerCrystal")).Register("powercrystal");
            ItemInfo lightingBugItem = new ItemInfo(ItemType.LOOT | ItemType.BUG | ItemType.TIER7, "Lightning Bug", "LOOT - BUG\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/LightningBug")).Register("lightningbug");

            //Resin
            //Needle
            //Worm Tooth
            //Bookworm Larva
            ItemInfo resinItem = new ItemInfo(ItemType.LOOT | ItemType.ORE | ItemType.TIER7, "Resin", "LOOT - ORE\nTIER: 8",
                 GadgetCoreAPI.LoadTexture2D("Items/Energite")).Register("resin");
            ItemInfo pineNeedleItem = new ItemInfo(ItemType.LOOT | ItemType.PLANT | ItemType.TIER7, "Pine Needle", "LOOT - PLANT\nTIER: 8",
                GadgetCoreAPI.LoadTexture2D("Items/PlasmaFern")).Register("pineneedle");
            ItemInfo WormToothItem = new ItemInfo(ItemType.LOOT | ItemType.MONSTER | ItemType.TIER7, "Worm Tooth", "LOOT - MONSTER PART\nTIER: 8",
                GadgetCoreAPI.LoadTexture2D("Items/PowerCrystal")).Register("wormtooth");
            ItemInfo bookwormLarvaItem = new ItemInfo(ItemType.LOOT | ItemType.BUG | ItemType.TIER7, "Bookworm Larva", "LOOT - BUG\nTIER: 8",
                GadgetCoreAPI.LoadTexture2D("Items/LightningBug")).Register("bookwormlarva");


            ItemInfo energiteEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.ORE | ItemType.TIER7, "Energy Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/EnergiteEmblem")).Register("energyemblem");
            ItemInfo fernEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.PLANT | ItemType.TIER7, "Cactus Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/FernEmblem")).Register("fernemblem");
            ItemInfo powerEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.MONSTER | ItemType.TIER7, "Plasma Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/PowerEmblem")).Register("poweremblem");
            ItemInfo lightingEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.BUG | ItemType.TIER7, "Lightning Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/LightningEmblem")).Register("lightningemblem");

            ItemInfo UrugorakScale = new ItemInfo(ItemType.GENERIC, "Urugorak Scale", "A scale from Urugorak. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("Scale.png")).Register();
            UrugorakScale.AddToLootTable("entity:millipede", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(31, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(21, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(UrugorakScale.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));

            ItemInfo HiveEye = new ItemInfo(ItemType.GENERIC, "Hivemind Eye", "An eye from The Hivemind. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("HiveEye.png")).Register();
            HiveEye.AddToLootTable("entity:hivemind", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(13, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(31, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(HiveEye.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo ScarabTooth = new ItemInfo(ItemType.GENERIC, "Scarab Tooth", "A tooth from a rock scarab. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("ScarabTeeth.png")).Register();
            ScarabTooth.AddToLootTable("entity:scarab", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(1, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(2, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(11, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(ScarabTooth.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo BullySpore = new ItemInfo(ItemType.GENERIC, "Bully Spore", "A spore from a Shroom Bully. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("BullySpore.png")).Register();
            BullySpore.AddToLootTable("entity:bully", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(12, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(22, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(32, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(BullySpore.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo AncientCore = new ItemInfo(ItemType.GENERIC, "Ancient Core", "A core from a Golem. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("AncientCore.png")).Register();
            AncientCore.AddToLootTable("entity:golem", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(3, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(5, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(23, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(AncientCore.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo PlagueSpike = new ItemInfo(ItemType.GENERIC, "Plague Spike", "A spike from a plaguebeast. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("PlagueSpike.png")).Register();
            PlagueSpike.AddToLootTable("entity:plaguebeast", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(4, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(33, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(14, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(PlagueSpike.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo LiquidFire = new ItemInfo(ItemType.GENERIC, "Liquid Fire", "Fire from a Lava Dragon \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("LiquidFire.png")).Register();
            LiquidFire.AddToLootTable("entity:lavadragon", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(5, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(25, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(34, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(LiquidFire.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo BriarLeaf = new ItemInfo(ItemType.GENERIC, "Briar Leaf", "A leaf from Moloch \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("BriarLeaf.png")).Register();
            BriarLeaf.AddToLootTable("entity:moloch", 1.0f, 1, 7);
            ((CraftMenuInfo)MenuRegistry.Singleton["Tiers+:OFMenu"]).AddCraftPerformer(CraftMenuInfo.CreateAdvancedCraftPerformer(
                new CraftMenuInfo.AdvancedRecipe(
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(15, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(24, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(35, 5, 0, 0, 0, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.OUTPUT, 10),
                    new CraftMenuInfo.AdvancedRecipeComponent(new Item(BriarLeaf.GetID(), 1, 1, 1, 1, new int[3], new int[3]), CraftMenuInfo.AdvancedRecipeComponentType.CORE_INPUT)
                    )
          ));
            ItemInfo plasmaTracerItem = new ItemInfo(ItemType.CONSUMABLE, "Plasma Tracer", "Grants 3 portal uses to\nThe Plasma Zone.",
                GadgetCoreAPI.LoadTexture2D("Items/PlasmaTracer"), 32).Register();

            ItemInfo healthPack4Item = new ItemInfo(ItemType.CONSUMABLE, "Health Pack IV", "Restores 18 Health.",
                GadgetCoreAPI.LoadTexture2D("Items/HealthPack4")).Register();

            ItemInfo TydusRing = new ItemInfo(ItemType.RING, "Tydus Ring", "", GadgetCoreAPI.GetItemMaterial(909), Stats: new EquipStats(0, 0, 3, 3, 0, 0)).Register(909);
            ItemInfo OwainPearl = new ItemInfo(ItemType.RING, "Owain's Pearl", "", GadgetCoreAPI.LoadTexture2D("Items/OwainPearl"), Stats: new EquipStats(2, 1, 1, 1, 1, 1)).Register("OwainRing", 908);
            ItemInfo VaatiBadge = new ItemInfo(ItemType.RING, "Vaati's Badge", "", GadgetCoreAPI.GetItemMaterial(910), Stats: new EquipStats(2, 0, 2, 0, 2, 1)).Register("VaatiRing", 910);

            healthPack4Item.OnUse += (slot) =>
            {
                InstanceTracker.GameScript.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/drink"), Menuu.soundLevel / 10f);
                InstanceTracker.GameScript.RecoverHP(18);
                return true;
            };
            ItemInfo manaPack4Item = new ItemInfo(ItemType.CONSUMABLE, "Mana Pack IV", "Restores 50 Mana.",
                GadgetCoreAPI.LoadTexture2D("Items/ManaPack4")).Register();
            manaPack4Item.OnUse += (slot) =>
            {
                InstanceTracker.GameScript.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/drink"), Menuu.soundLevel / 10f);
                InstanceTracker.GameScript.RecoverMana(50);
                return true;
            };
            ItemInfo energyPack4Item = new ItemInfo(ItemType.CONSUMABLE, "Energy Pack IV", "Restores 100 Stamina.\nGrants a temporary\nspeed boost.",
                GadgetCoreAPI.LoadTexture2D("Items/EnergyPack4")).Register();
            energyPack4Item.OnUse += (slot) =>
            {
                InstanceTracker.GameScript.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/drink"), Menuu.soundLevel / 10f);
                InstanceTracker.GameScript.RecoverStamina(100);
                InstanceTracker.GameScript.StartCoroutine(EnergyPackSpeedBoost(15, 10));
                return true;
            };

            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(plasmaFernItem.GetID(), powerCrystalItem.GetID(), lightingBugItem.GetID()), new Item(healthPack4Item.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 2);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(plasmaFernItem.GetID(), lightingBugItem.GetID(), powerCrystalItem.GetID()), new Item(manaPack4Item.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 2);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(lightingBugItem.GetID(), plasmaFernItem.GetID(), powerCrystalItem.GetID()), new Item(energyPack4Item.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 2);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(lightingBugItem.GetID(), powerCrystalItem.GetID(), plasmaFernItem.GetID()), new Item(63, 4, 0, 0, 0, new int[3], new int[3]), 3);

            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(
                Tuple.Create(new int[] { 136, 74, 136 }, new Item(plasmaTracerItem.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { fernEmblemItem.GetID(), powerEmblemItem.GetID(), energiteEmblemItem.GetID() }, new Item(PlasmaCannon.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { energiteEmblemItem.GetID(), fernEmblemItem.GetID(), lightingEmblemItem.GetID() }, new Item(PlasmaShield.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { energiteEmblemItem.GetID(), powerEmblemItem.GetID(), fernEmblemItem.GetID() }, new Item(PlasmaLance.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { energiteEmblemItem.GetID(), lightingEmblemItem.GetID(), fernEmblemItem.GetID() }, new Item(PlasmaArmor.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { fernEmblemItem.GetID(), lightingEmblemItem.GetID(), energiteEmblemItem.GetID() }, new Item(PlasmaHelmet.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { powerEmblemItem.GetID(), lightingEmblemItem.GetID(), energiteEmblemItem.GetID() }, new Item(NebulaCannon.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { fernEmblemItem.GetID(), energiteEmblemItem.GetID(), powerEmblemItem.GetID() }, new Item(particleAccelerator.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { fernEmblemItem.GetID(), powerEmblemItem.GetID(), lightingEmblemItem.GetID() }, new Item(NexusHelmet.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { powerEmblemItem.GetID(), fernEmblemItem.GetID(), lightingEmblemItem.GetID() }, new Item(NexusArmor.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { lightingEmblemItem.GetID(), fernEmblemItem.GetID(), powerEmblemItem.GetID() }, new Item(NexusShield.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            ((CraftMenuInfo)MenuRegistry.Singleton["Gadget Core:Crafter Menu"]).AddCraftPerformer(CraftMenuInfo.CreateSimpleCraftPerformer(Tuple.Create(new int[] { powerEmblemItem.GetID(), energiteEmblemItem.GetID(), fernEmblemItem.GetID() }, new Item(plantain.GetID(), 1, 0, 3, 0, new int[3], new int[3]), 0)));
            GadgetCoreAPI.AddCreationMachineRecipe(powerEmblemItem.GetID(), new Item(1030, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddCreationMachineRecipe(energiteEmblemItem.GetID(), new Item(909, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddCreationMachineRecipe(fernEmblemItem.GetID(), new Item(908, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddCreationMachineRecipe(lightingEmblemItem.GetID(), new Item(910, 1, 0, 3, 0, new int[3], new int[3]));
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(21, 31, 11), new Item(64, 1, 0, 0, 0, new int[3], new int[3]), 3);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(32, 22, 12), new Item(68, 1, 0, 0, 0, new int[3], new int[3]), 3);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(33, 23, 13), new Item(69, 1, 0, 0, 0, new int[3], new int[3]), 3);
            GadgetCoreAPI.AddAlchemyStationRecipe(Tuple.Create(34, 24, 14), new Item(63, 1, 0, 0, 0, new int[3], new int[3]), 3);
            GadgetCoreAPI.AddEmblemRecipe(powerCrystalItem.GetID(), powerEmblemItem.GetID(), 10);
            GadgetCoreAPI.AddEmblemRecipe(lightingBugItem.GetID(), lightingEmblemItem.GetID(), 10);
            GadgetCoreAPI.AddEmblemRecipe(plasmaFernItem.GetID(), fernEmblemItem.GetID(), 10);
            GadgetCoreAPI.AddEmblemRecipe(energiteItem.GetID(), energiteEmblemItem.GetID(), 10);
            //crafting recipes

            ObjectInfo EnergiteOre = new ObjectInfo(ObjectType.ORE, new Item(energiteItem.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 1, GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Energite")).Register("TestObject");
            ObjectInfo PlasmaFern = new ObjectInfo(ObjectType.PLANT, new Item(plasmaFernItem.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 1, GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Fern")).Register("PlasmaFern");
            ObjectInfo LightningBugTree = new ObjectInfo(ObjectType.BUGSPOT, new Item(lightingBugItem.GetID(), 1, 0, 0, 0, new int[3], new int[3]), 1, GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/LightningBugNode"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/LightningBugBody"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/LightningBugWing")).Register("LightningBugTree");

            CharacterRaceInfo Iraian = new CharacterRaceInfo("Lesser Iraian", "While smaller and physically weaker than their \"Greater\" counterpart, \nLesser Iraians are masters of medicine and tech, and put their foxy wit to use \nas combat medics and mechanics. Hailing from the inhospitible ice planet Ira they \nare quite rare, with a population estimated at under 1000.", "The percent chance to unlock is equal to your Mech City portals, capped at 99.", new EquipStats(1, 0, 0, 2, 0, 2), GadgetCoreAPI.LoadTexture2D("Races/foxpreview.png"), GadgetCoreAPI.LoadTexture2D("races/foxalt1.png"), GadgetCoreAPI.LoadTexture2D("races/foxalt2"), GadgetCoreAPI.LoadTexture2D("races/foxalt3.png")).Register("IraFox");
            Iraian.SetUnlockChecker(() =>
            {
                int hold = InstanceTracker.GameScript.GetFieldValue<int[]>("portalUses")[8];
                Logger.Log(InstanceTracker.GameScript.GetFieldValue<int[]>("portalUses")[8]);
                if (hold >= 100) {
                    hold = 99;
                }
                return UnityEngine.Random.Range(0, 100 - hold) == 0;


            });
            ItemInfo overgrownFabricatorItem = new ItemInfo(ItemType.GENERIC, "Overgrown Fabricator", "Deconstructs boss loot \n into its parts.", GadgetCoreAPI.LoadTexture2D("tiles/aficon.png")).Register("overgrownFabricator");
            GameObject overgrownFabricatorObject = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetPlaceableNPCResource(2104));
            overgrownFabricatorObject.name = "overgrownFabricator";
            TileInfo overgrownFabricator = new TileInfo(TileType.INTERACTIVE, GadgetCoreAPI.LoadTexture2D("tiles/ancientfabricator.png"), overgrownFabricatorObject, overgrownFabricatorItem).Register("overgrownFabricatorPlaced");

            overgrownFabricatorObject.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("tiles/ancientfabricator"),
            };
            CraftMenuInfo overgrownFabricatorMenu = new CraftMenuInfo("Overgrown Fabricator", "Breaks down Boss Loot into smaller parts",
                GadgetCoreAPI.LoadTexture2D("Tiles/OFMenu/menu_tex"), GadgetCoreAPI.LoadTexture2D("Tiles/OFMenu/bar_tex"),
                GadgetCoreAPI.LoadTexture2D("Tiles/OFMenu/button0_tex"), GadgetCoreAPI.LoadTexture2D("Tiles/OFMenu/button1_tex"), GadgetCoreAPI.LoadTexture2D("Tiles/OFMenu/button2_tex"),
                GadgetCoreAPI.LoadAudioClip("Tiles/OFMenu/craft_au"), null, overgrownFabricator);
            overgrownFabricatorMenu.Register("OFMenu");
            Material lkPortrait = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/portraitLorekeeper.png"),
                mainTextureScale = new Vector2(0.5f, 1)


            };



            int loreKQuest = PreviewLabs.PlayerPrefs.GetInt("loreKQuest", 0);
            PreviewLabs.PlayerPrefs.SetInt("loreKQuest", loreKQuest);
            GameObject loreKeeper = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetNPCResource("ringabolt"));
            loreKeeper.name = "Lorekeeper";
            loreKeeperTile = new TileInfo(TileType.INTERACTIVE, GadgetCoreAPI.LoadTexture2D("npcs/lkbody"), loreKeeper).Register("Lorekeeper");
            loreKeeperTile.OnInteract += loreKeeperTile.InitiateDialogRoutine;
            //lorekeeper nothing found dialogue
            DialogChains.RegisterDialogChain(loreKeeperTile.GetID(), "The Lorekeeper", lkPortrait, null, "There is nothing to see here, Cadet.", "Perhaps you messed with the save file?", "(if you see this message something went wrong! contact Aure!)");
            //lorekeeper quest part 1
            DialogChains.RegisterDialogChain(loreKeeperTile.GetID(), "The Lorekeeper", lkPortrait, (b) => {
                return PlayerPrefs.GetInt("LoreKQuest") == 0;
            }, "You. Cadet. Could you do me a favor quickly?", "Can you see how my armor's long since rusted up?", "I need you to go to the Desolate Canyon and slay Urugorak. The acid under his scales does wonders for an aching joint...", "Upon your return i can use the remaining acid and the rest of the scale to power a machine that can turn loot such as that scale back into their constituent parts.", new DialogMessage("It will surely prove useful to you...", () => PlayerPrefs.SetInt("LoreKQuest", 1)));
            DialogChains.RegisterDialogChain(loreKeeperTile.GetID(), "The Lorekeeper", lkPortrait, DialogConditions.HasItem(UrugorakScale.GetID()),
                    new DialogMessage(DialogActions.BranchDialog(new DialogChain(loreKeeperTile.GetID(), "The Lorekeeper", lkPortrait, (b) => { return PlayerPrefs.GetInt("LoreKQuest") == 1; },
                            "Wow, your flag is 1! Epic!"
                    ))),
                    new DialogMessage(DialogActions.BranchDialog(new DialogChain(loreKeeperTile.GetID(), "The Lorekeeper", lkPortrait, (b) => {return PlayerPrefs.GetInt("LoreKQuest") == 1; },
                            "Wow, your flag is 2! Awesome!"
                    ))),
                    "Aww, your flag is unrecognized :("
            );


            DialogChains.RegisterDialogChain(loreKeeperTile.GetID(), "The Lorekeeper", lkPortrait, (b) => { return PlayerPrefs.GetInt("LoreKQuest") == 1; }, "Take your time Cadet, I can wait...");

            DialogConditions.HasItem(UrugorakScale.GetID());
            loreKeeper.transform.Find("e").Find("ringabolt").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/lkhead.png"),
                
            };
            loreKeeper.transform.Find("e").Find("ringabolt").Find("Plane_001").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Npcs/lkbody.png"),   
                
            };
            
            


            SceneManager.sceneLoaded += OnSceneLoaded;
            GameObject ParticleDragon = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("wicked"));
            ParticleDragon.name = "particleWyvern";
            UnityEngine.Object.Destroy(ParticleDragon.GetComponent<Wicked>());
            ParticleDragon.AddComponent<ParticleWyvernScript>();
            ParticleDragon.transform.Find("e").Find("wicked").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCultist/culthead"),
            };
            ParticleDragon.transform.Find("e").Find("wicked").Find("Plane_001").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCultist/cultbody"),
            };
            ParticleDragon.transform.Find("e").Find("wicked").Find("Plane_002").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCultist/cultwing"),
            };
            EntityInfo ParticleWyvern = new EntityInfo(EntityType.COMMON, ParticleDragon).Register("ParticleWyvern");
            powerCrystalItem.AddToLootTable("entity:particleWyvern", 1.0f, 0, 4);

            GameObject MykBug = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("spider"));
            MykBug.name = "Mykdunebug";
            MykBug.SetActive(false);
            MykBug.ReplaceComponent<SpiderScript, MykBugScript>();
            MykBug.GetComponent<Rigidbody>().useGravity = true;
            //SpiderScript spiderScript = MykBug.GetComponent<SpiderScript>();
            //GameObject mykBugHead = spiderScript.head;
            //GameObject mykBugBody = spiderScript.body;
            //GameObject mykBugLeg = spiderScript.leg;
            //GameObject mykBugB = spiderScript.b;
            //UnityEngine.Object.Destroy(spiderScript);
            //MykBugScript mykBugScript = MykBug.AddComponent<MykBugScript>();
            //mykBugScript.head = mykBugHead;
            //mykBugScript.body = mykBugBody;
            //mykBugScript.leg = mykBugLeg;
            //mykBugScript.b = mykBugB;
            MykBug.transform.Find("e").Find("dunebug").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/MykDunebug/Head"),
            };
            MykBug.transform.Find("e").Find("dunebug").Find("Plane_001").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/MykDunebug/Body"),
            };
            MykBug.transform.Find("e").Find("dunebug").Find("Plane_002").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/MykDunebug/Back"),
            };
            MykBug.transform.Find("e").Find("dunebug").Find("Plane_003").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/MykDunebug/Legs"),
            };
            MykBug.transform.Find("e").Find("dunebug").Find("Plane_004").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/MykDunebug/Wings"),
            };
            MykBug.transform.Find("e").Find("dunebug").Find("Plane_005").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/MykDunebug/Wings"),
            };
            EntityInfo MykDunebug = new EntityInfo(EntityType.COMMON, MykBug).Register("Mykdunebug");

            GameObject MykWasp = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("wasp"));
            MykWasp.name = "MykWasp";
            MykWasp.SetActive(false);
            MykWasp.ReplaceComponent<WaspScript, MykWaspScript>();
            EntityInfo mykWasp = new EntityInfo(EntityType.COMMON, MykWasp).Register("MykWasp");

            //GameObject MykWorm = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("worm"));
            //MykWorm.name = "MykWorm";
            //MykWorm.SetActive(false);
            //MykWorm.ReplaceComponent<WormScript, MykWormScript>();
            //MykWormEntity = new EntityInfo(EntityType.COMMON, MykWorm).Register("MykWorm");

            GameObject NewOoze = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("ooze"));
            NewOoze.name = "NewOoze";
            NewOoze.SetActive(false);
            NewOoze.ReplaceComponent<OozeScript, NewOozeScript>();
            EntityInfo newOoze = new EntityInfo(EntityType.COMMON, NewOoze).Register("NewOoze");




            GameObject testProjEnemy = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("sponge"));
            testProjEnemy.name = "testProjEnemy";
            testProjEnemy.SetActive(false);
            testProjEnemy.ReplaceComponent<SpongeScript, TestProjScript>();
            EntityInfo TestProjEnemy = new EntityInfo(EntityType.COMMON, testProjEnemy).Register("testProjEnemy");
            manaPack4Item.AddToLootTable("entity:testProjEnemy", 0.5f, 0, 2);
            powerCrystalItem.AddToLootTable("entity:testProjEnemy", 0.5f, 0, 4);
            //reminder to destroy the goblin thing's 5head hitbox
            var box = testProjEnemy.GetComponent<BoxCollider>();
            box.size = new Vector3(2, 4.3f, 1);
            box.center = new Vector3(0, -1.6f, 0);

            testProjEnemy.transform.Find("e").Find("b").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCaster/spongeHead"),
            };
            testProjEnemy.transform.Find("e").Find("b").Find("Plane_001").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCaster/spongeBody"),
            };
            testProjEnemy.transform.Find("e").Find("b").Find("Plane_002").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCaster/spongeArm"),
            };
            testProjEnemy.transform.Find("e").Find("b").Find("Plane_003").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/PlasmaCaster/spongeArm"),
            };

            GameObject plasmaPassive = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("blastBug"));
            plasmaPassive.name = "plasmaPassive";
            plasmaPassive.SetActive(false);
            plasmaPassive.ReplaceComponent<BlastbugScript, PlasmaPassiveScript>();
            EntityInfo plasmaBug = new EntityInfo(EntityType.COMMON, plasmaPassive).Register("plasmaPassive");
            powerCrystalItem.AddToLootTable("entity:plasmaBug", 0.5f, 0, 4);
            plasmaPassive.transform.Find("e").Find("blastbug").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/BlastBug/caterspikebody"),
            };
            plasmaPassive.transform.Find("e").Find("blastbug").Find("Plane_001").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/BlastBug/caterspikeleg"),
            };
            plasmaPassive.transform.Find("e").Find("blastbug").Find("Plane_002").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/BlastBug/caterspikeleg"),
            };
            plasmaPassive.transform.Find("e").Find("blastbug").Find("Plane_003").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("enemies/BlastBug/caterspikeleg"),
            };


            GameObject plasmaDragonBall = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetProjectileResource("blaze"));
            plasmaDragonBall.name = "PlasmaDragonBall";
            plasmaDragonBall.SetActive(false);
            GravityBallScript plasmaDragonGravityBall = plasmaDragonBall.ReplaceComponent<Projectile, GravityBallScript>();
            plasmaDragonGravityBall.Set(60); //dmg
            plasmaDragonGravityBall.Speed = 15;
            plasmaDragonGravityBall.DecelerationRate = 5;
            plasmaDragonGravityBall.isGood = false; //this is false, all plasma dragons are good girls
            plasmaDragonGravityBall.isBurn = 50;
            GadgetCoreAPI.AddCustomResource("proj/PlasmaDragonBall", plasmaDragonBall);
            //reminder to destroy the plasma dragon wings

            GameObject lavadragonPrefab = GadgetCoreAPI.GetEntityResource("lavadragon");
            lavadragonPrefab.SetActive(false);
            GameObject plasmaDragon = UnityEngine.Object.Instantiate(lavadragonPrefab);
            plasmaDragon.name = "PlasmaDragon";
            foreach (Millipede m in plasmaDragon.GetComponentsInChildren<Millipede>())
            {
                m.gameObject.ReplaceComponent<Millipede, PlasmaDragonScript>();
            }
            UnityEngine.Object.Destroy(plasmaDragon.transform.Find("2").Find("-").Find("XP_8BTFireA_1A_0000 (2)").gameObject);
            UnityEngine.Object.Destroy(plasmaDragon.transform.Find("2").Find("- (1)").Find("XP_8BTFireA_1A_0000 (1)").gameObject);
            EntityInfo plasmaDragonInfo = new EntityInfo(EntityType.BOSS, plasmaDragon).Register("PlasmaDragon");

            plasmaDragon.transform.Find("0").Find("GameObject").Find("wormHead").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Enemies/ParticleDragon/lavadragonhead.png"),
            };
            for (int e = 1; e <= 5; e++) {
                plasmaDragon.transform.Find(e + "").Find("milBody").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
                 {
                    mainTexture = GadgetCoreAPI.LoadTexture2D("Enemies/ParticleDragon/lavadragonbody.png"),
                 }; 
            }
            plasmaDragon.transform.Find("2").Find("- (1)").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Enemies/ParticleDragon/lavadragonwing2.png"),
            };
            
            plasmaDragon.transform.Find("2").Find("-").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Enemies/ParticleDragon/lavadragonwing.png"),
            };
            plasmaDragon.transform.Find("6").Find("milBody").Find("Plane").GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("Enemies/ParticleDragon/lavadragontail.png"),
            };

                PlanetInfo plasmaZonePlanet = new PlanetInfo(PlanetType.NORMAL, "Plasmatic Rift", new Tuple<int, int>[] { Tuple.Create(-1, 1), Tuple.Create(13, 1) }, GadgetCoreAPI.LoadAudioClip("Planets/Plasma Zone/Music"));
            plasmaZonePlanet.SetTerrainInfo(GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Entrance"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Zone"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/MidChunkFull"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/MidChunkOpen"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/SideH"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/SideV"));
            plasmaZonePlanet.SetBackgroundInfo(GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Parallax"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background3"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background2"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background1"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Background0"));
            plasmaZonePlanet.SetPortalInfo(GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Sign"), GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Button"),
                GadgetCoreAPI.LoadTexture2D("Planets/Plasma Zone/Icon"));
            plasmaZonePlanet.Register("Plasma Zone");


            plasmaZonePlanet.AddWeightedWorldSpawn(EnergiteOre, 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(ParticleWyvern, 25);
            plasmaZonePlanet.AddWeightedWorldSpawn(LightningBugTree, 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(PlasmaFern, 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(TestProjEnemy, 15);
            plasmaZonePlanet.AddWeightedWorldSpawn(plasmaBug, 15);
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

            /*PlanetInfo MykPlanet = new PlanetInfo(PlanetType.NORMAL, "Mykonogre's Zone", new Tuple<int, int>[] { Tuple.Create(-1, 1) }, GadgetCoreAPI.LoadAudioClip("Planets/Plasma Zone/Music"));
            MykPlanet.SetTerrainInfo(GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/Entrance"), GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/Zone"),
                GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/MidChunkFull"), GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/MidChunkOpen"),
                GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/SideH"), GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/SideV"));
            MykPlanet.SetBackgroundInfo(GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/Parallax"),
                GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/bg0"), GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/bg1"),
                GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/bg2"), GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/bg3"));
            MykPlanet.SetPortalInfo(GadgetCoreAPI.LoadTexture2D("Planets/Mykworld/MykSign"), GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/Button"),
                GadgetCoreAPI.LoadTexture2D("Planets/MykWorld/Planet"));
            MykPlanet.Register("Mykonogre Zone");
            MykPlanet.AddWeightedWorldSpawn(MykDunebug, 1);
            MykPlanet.AddWeightedWorldSpawn(mykWasp, 1);
            MykPlanet.OnGenerateWorld += (spawner, s) => spawner.StartCoroutine(SpawnMykonogre2());*/

            /*PlanetInfo AbandonedColony = new PlanetInfo(PlanetType.NORMAL, "Abandoned Colony", new Tuple<int, int>[] { Tuple.Create(-1, 1), Tuple.Create(2, 2), Tuple.Create(3, 1), Tuple.Create(4, 2) }, GadgetCoreAPI.LoadAudioClip("Planets/Abandoned/Music"));
            AbandonedColony.SetTerrainInfo(GadgetCoreAPI.LoadTexture2D("Planets/abandoned/entrance"), GadgetCoreAPI.LoadTexture2D("Planets/abandoned/zone"),
                GadgetCoreAPI.LoadTexture2D("Planets/abandoned/mid0Chunk"), GadgetCoreAPI.LoadTexture2D("Planets/abandoned/mid1Chunk"),
                GadgetCoreAPI.LoadTexture2D("Planets/abandoned/Side1b"), GadgetCoreAPI.LoadTexture2D("Planets/abandoned/Sidesmall"));
            AbandonedColony.SetBackgroundInfo(GadgetCoreAPI.LoadTexture2D("Planets/abandoned/Parallax"),
                GadgetCoreAPI.LoadTexture2D("Planets/Abandoned/bg0"), GadgetCoreAPI.LoadTexture2D("Planets/Abandoned/bg1"),
                GadgetCoreAPI.LoadTexture2D("Planets/Abandoned/bg2"), GadgetCoreAPI.LoadTexture2D("Planets/Abandoned/bg3"));
            AbandonedColony.SetPortalInfo(GadgetCoreAPI.LoadTexture2D("Planets/abandoned/Sign"), GadgetCoreAPI.LoadTexture2D("Planets/abandoned/Button"),
                GadgetCoreAPI.LoadTexture2D("Planets/abandoned/Icon"));
            AbandonedColony.Register("Abandoned Colony");
            PlanetRegistry.SetVanillaExitPortalWeight(2, AbandonedColony.GetID(), 25);
            PlanetRegistry.SetVanillaExitPortalWeight(3, AbandonedColony.GetID(), 25);
            PlanetRegistry.SetVanillaExitPortalWeight(4, AbandonedColony.GetID(), 25);
            AbandonedColony.AddWeightedWorldSpawn(newOoze, 1);
            //AbandonedColony.AddWeightedWorldSpawn("e/wormold", 1);
            //AbandonedColony.AddWeightedWorldSpawn("e/worm2", 1);
            AbandonedColony.AddWeightedWorldSpawn((pos) => (GameObject)null, 3);*/

            plasmaTracerItem.OnUse += (slot) =>
            {
                plasmaZonePlanet.PortalUses += 3;
                InstanceTracker.GameScript.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/glitter"), Menuu.soundLevel / 10f);
                return true;
            };
            BlankCard.AddToLootTable("entity:all", 0.00013f, 1);
            SliverCard.AddToLootTable("entity:sliver", 0.013f, 1);
            GuardianCard.AddToLootTable("entity:guardian", 0.013f, 1);
            GhostCard.AddToLootTable("entity:shroomy1", 0.013f, 1);
            //MykonogreToken.AddToLootTable("entity:mykonogre", 1.0f, 1, CustomDropBehavior: (item, pos) => {
            //MykPlanet.PortalUses += 3;
            //return true;
            //});
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

        public static IEnumerator CustomPlasma(PlayerScript script)
        {
            canAttack.SetValue(script, false);
            attacking.SetValue(script, true);
            script.StartCoroutine(script.ATKSOUND());
            script.StartCoroutine(script.GunEffects(NebulaCannon.GetID()));
            script.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/shoot"), Menuu.soundLevel / 10f);
            script.Animate(4);
            yield return new WaitForSeconds(0.3f);
            int dmg = NebulaCannon.GetDamage(script);
            if ((bool)hyper.GetValue(script))
            {
                hyper.SetValue(script, false);
                script.HyperBeam();
            }
            if (NebulaCannon.TryCrit(script))
            {
                dmg = NebulaCannon.MultiplyCrit(script, dmg);
                script.GetComponent<AudioSource>().PlayOneShot(script.critSound, Menuu.soundLevel / 10f);
                UnityEngine.Object.Instantiate(script.crit, script.transform.position, Quaternion.identity);
            }

            script.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/plasma1"), Menuu.soundLevel / 10f);
            int num = InstanceTracker.GameScript.GetFinalStat(3) * 2 + InstanceTracker.GameScript.GetFinalStat(2) / 2;
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PackPlasma value = new PackPlasma(num, vector);
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("proj/plasma"), script.shot.transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<MeshRenderer>().material = new Material(Shader.Find("Unlit/Transparent Cutout"))
            {
                mainTexture = GadgetCoreAPI.LoadTexture2D("items/NebulaBomb"),
                mainTextureScale = new Vector2(0.5f, 1)
            };
            gameObject.SendMessage("Plasma2", value);
            script.GetComponent<NetworkView>().RPC("ShootSpecial", RPCMode.Others, new object[]
            {
        1001,   
        vector,
        num
            });

            yield return new WaitForSeconds(0.2f);
            attacking.SetValue(script, false);
            yield return new WaitForSeconds(0.1f);
            canAttack.SetValue(script, true);
            yield break;

        }

        private IEnumerator EnergyPackSpeedBoost(int power, float duration)
        {
            GameScript.MODS[16] += power;
            GameScript.MODS[17] += power;
            GameScript.MODS[18] += power;
            int boostedMoveModCount = GameScript.MODS[16];
            int boostedDashModCount = GameScript.MODS[17];
            int boostedJumpModCount = GameScript.MODS[18];
            yield return new WaitForSeconds(duration);
            if (GameScript.MODS[16] == boostedMoveModCount) GameScript.MODS[16] -= power;
            if (GameScript.MODS[17] == boostedDashModCount) GameScript.MODS[17] -= power;
            if (GameScript.MODS[18] == boostedJumpModCount) GameScript.MODS[18] -= power;
            yield break;
        }
        public IEnumerator SpawnMykonogre2()
        {
            yield return new WaitForSeconds(15f);
            {
                if (Network.isServer) { 
                Network.Instantiate(Resources.Load("e/mykonogre"), new Vector3(MenuScript.player.transform.position.x, MenuScript.player.transform.position.y + 75f, 0f), Quaternion.identity, 0);
                InstanceTracker.PlayerScript.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/plague"), Menuu.soundLevel / 10f);
                }
            }
            yield break;
        }
        IEnumerator logNPCDialog(String NPC) {
            Logger.Log("NPC " + NPC + "'s Dialogue is working fine!");
            yield break;
        }
        IEnumerator TripleShot(PlayerScript script)
        {
            script.StartCoroutine(PlasmaCannon.ShootGun(script));
            script.StartCoroutine(PlasmaCannon.ShootGun(script));
            yield return new WaitForSeconds(0.2f);
            script.StartCoroutine(PlasmaCannon.ShootGun(script));
            script.StartCoroutine(PlasmaCannon.ShootGun(script));
            yield return new WaitForSeconds(0.2f);
            script.StartCoroutine(PlasmaCannon.ShootGun(script));
            script.StartCoroutine(PlasmaCannon.ShootGun(script));
            yield return new WaitForSeconds(0.2f);
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