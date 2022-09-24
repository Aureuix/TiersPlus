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
    internal static class Items
    {
        static ItemInfo PlasmaCannon;
        public static ItemInfo NebulaCannon;
        private static readonly FieldInfo canAttack = typeof(PlayerScript).GetField("canAttack", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo attacking = typeof(PlayerScript).GetField("attacking", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo hyper = typeof(PlayerScript).GetField("hyper", BindingFlags.NonPublic | BindingFlags.Instance);
        static PlasmaLanceItemInfo PlasmaLance;
        static ItemInfo PlasmaArmor;
        static ItemInfo PlasmaHelmet;
        static ItemInfo PlasmaShield;

        internal static void Weapons() {
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
            //PlasmaGun
            NebulaCannon = new ItemInfo(ItemType.WEAPON, "Nebular Grenadier", "", GadgetCoreAPI.LoadTexture2D("items/NebulaCannonItem"), Stats: new EquipStats(0, 0, 10, 10, 0, 0), HeldTex: GadgetCoreAPI.LoadTexture2D("items/NebulaCannonHeld"));
            NebulaCannon.SetWeaponInfo(new float[] { 0, 0, 0.5f, 2, 0, 0 }, GadgetCoreAPI.GetAttackSound(473));
            NebulaCannon.Register("NebulaCannon");
            NebulaCannon.OnAttack += CustomPlasma;
            //NebulaCannon
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
            //PlasmaStaff
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
            //UnusedGauntlet
            PlasmaLance = new PlasmaLanceItemInfo(ItemType.WEAPON, "Plasma Lance", "", GadgetCoreAPI.LoadTexture2D("items/Plasmaspearitem"), Stats: new EquipStats(5, 20, 0, 0, 0, 0), HeldTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaSpear"));
            PlasmaLance.SetWeaponInfo(new float[] { 2f, 4f, 0, 0, 0, 0 }, GadgetCoreAPI.GetAttackSound(367));
            PlasmaLance.Register("PlasmaLance");
            PlasmaLance.OnAttack += PlasmaLance.ThrustLance;
            //plasmalance
        }
        internal static void Equipment() {
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
            ItemInfo CarbonHelm = new ItemInfo(ItemType.HELMET, "Carbon Fiber Visor", "Nanomachines son...", GadgetCoreAPI.LoadTexture2D("items/CarbonHead"), Stats: new EquipStats(500, 500, 500, 500, 500, 500), HeadTex: GadgetCoreAPI.LoadTexture2D("Items/CarbonHead"));
            CarbonHelm.Register("CarbonHelm");
            ItemInfo NexusShield = new ItemInfo(ItemType.OFFHAND, "Plasmatic Shield", "", GadgetCoreAPI.LoadTexture2D("items/PlasmaShield"), Stats: new EquipStats(7, 5, 5, 5, 5, 5), HeldTex: GadgetCoreAPI.LoadTexture2D("items/PlasmaShield"));
            NexusShield.Register("Plasmahield");
            ItemInfo TydusRing = new ItemInfo(ItemType.RING, "Tydus Ring", "", GadgetCoreAPI.GetItemMaterial(909), Stats: new EquipStats(0, 0, 3, 3, 0, 0)).Register(909);
            ItemInfo OwainPearl = new ItemInfo(ItemType.RING, "Owain's Pearl", "", GadgetCoreAPI.LoadTexture2D("Items/OwainPearl"), Stats: new EquipStats(2, 1, 1, 1, 1, 1)).Register("OwainRing", 908);
            ItemInfo VaatiBadge = new ItemInfo(ItemType.RING, "Vaati's Badge", "", GadgetCoreAPI.GetItemMaterial(910), Stats: new EquipStats(2, 0, 2, 0, 2, 1)).Register("VaatiRing", 910);

            //equipment
        }
        internal static void Placeables() {
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

        }
        internal static void Misc() {
            ItemInfo energiteItem = new ItemInfo(ItemType.LOOT | ItemType.ORE | ItemType.TIER7, "Energite", "LOOT - ORE\nTIER: 7",
                    GadgetCoreAPI.LoadTexture2D("Items/Energite")).Register("energyore");
            ItemInfo plasmaFernItem = new ItemInfo(ItemType.LOOT | ItemType.PLANT | ItemType.TIER7, "Cactus Spine", "LOOT - PLANT\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/PlasmaFern")).Register("fern");
            ItemInfo powerCrystalItem = new ItemInfo(ItemType.LOOT | ItemType.MONSTER | ItemType.TIER7, "Plasma Claw", "LOOT - MONSTER PART\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/PowerCrystal")).Register("powercrystal");
            ItemInfo lightingBugItem = new ItemInfo(ItemType.LOOT | ItemType.BUG | ItemType.TIER7, "Lightning Bug", "LOOT - BUG\nTIER: 7",
                GadgetCoreAPI.LoadTexture2D("Items/LightningBug")).Register("lightningbug");
            //T7
            ItemInfo resinItem = new ItemInfo(ItemType.LOOT | ItemType.ORE | ItemType.TIER7, "Resin", "LOOT - ORE\nTIER: 8",
                 GadgetCoreAPI.LoadTexture2D("Items/Energite")).Register("resin");
            ItemInfo pineNeedleItem = new ItemInfo(ItemType.LOOT | ItemType.PLANT | ItemType.TIER7, "Pine Needle", "LOOT - PLANT\nTIER: 8",
                GadgetCoreAPI.LoadTexture2D("Items/PlasmaFern")).Register("pineneedle");
            ItemInfo WormToothItem = new ItemInfo(ItemType.LOOT | ItemType.MONSTER | ItemType.TIER7, "Worm Tooth", "LOOT - MONSTER PART\nTIER: 8",
                GadgetCoreAPI.LoadTexture2D("Items/PowerCrystal")).Register("wormtooth");
            ItemInfo bookwormLarvaItem = new ItemInfo(ItemType.LOOT | ItemType.BUG | ItemType.TIER7, "Bookworm Larva", "LOOT - BUG\nTIER: 8",
                GadgetCoreAPI.LoadTexture2D("Items/LightningBug")).Register("bookwormlarva");
            ItemInfo energiteEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.ORE | ItemType.TIER7, "Energy Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
           //T8
           //Loot
                GadgetCoreAPI.LoadTexture2D("Items/EnergiteEmblem")).Register("energyemblem");
            ItemInfo fernEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.PLANT | ItemType.TIER7, "Cactus Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/FernEmblem")).Register("fernemblem");
            ItemInfo powerEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.MONSTER | ItemType.TIER7, "Plasma Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/PowerEmblem")).Register("poweremblem");
            ItemInfo lightingEmblemItem = new ItemInfo(ItemType.EMBLEM | ItemType.BUG | ItemType.TIER7, "Lightning Emblem", "Tier 7.\nA shiny Token. Used\nto forge items.",
                GadgetCoreAPI.LoadTexture2D("Items/LightningEmblem")).Register("lightningemblem");
            //Emblems
            ItemInfo UrugorakScale = new ItemInfo(ItemType.GENERIC, "Urugorak Scale", "A scale from Urugorak. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("Scale.png")).Register();
            ItemInfo HiveEye = new ItemInfo(ItemType.GENERIC, "Hivemind Eye", "An eye from The Hivemind. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("HiveEye.png")).Register();
            ItemInfo ScarabTooth = new ItemInfo(ItemType.GENERIC, "Scarab Tooth", "A tooth from a rock scarab. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("ScarabTeeth.png")).Register();
            ItemInfo BullySpore = new ItemInfo(ItemType.GENERIC, "Bully Spore", "A spore from a Shroom Bully. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("BullySpore.png")).Register();
            ItemInfo AncientCore = new ItemInfo(ItemType.GENERIC, "Ancient Core", "A core from a Golem. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("AncientCore.png")).Register();
            ItemInfo PlagueSpike = new ItemInfo(ItemType.GENERIC, "Plague Spike", "A spike from a plaguebeast. \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("PlagueSpike.png")).Register();
            ItemInfo LiquidFire = new ItemInfo(ItemType.GENERIC, "Liquid Fire", "Fire from a Lava Dragon \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("LiquidFire.png")).Register();
            ItemInfo BriarLeaf = new ItemInfo(ItemType.GENERIC, "Briar Leaf", "A leaf from Moloch \n Used to craft materials", GadgetCoreAPI.LoadTexture2D("BriarLeaf.png")).Register();
            //Boss Loot
            ItemInfo plasmaTracerItem = new ItemInfo(ItemType.CONSUMABLE, "Plasma Tracer", "Grants 3 portal uses to\nThe Plasma Zone.",
    GadgetCoreAPI.LoadTexture2D("Items/PlasmaTracer"), 32).Register();

            ItemInfo healthPack4Item = new ItemInfo(ItemType.CONSUMABLE, "Health Pack IV", "Restores 18 Health.",
                GadgetCoreAPI.LoadTexture2D("Items/HealthPack4")).Register();


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
            plasmaTracerItem.OnUse += (slot) =>
            {
               PlanetRegistry.Singleton["Tiers+:PlasmaZonePlanet"].PortalUses += 3;
                InstanceTracker.GameScript.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/glitter"), Menuu.soundLevel / 10f);
                return true;
            };  
            //potions
        }
        internal static void DropTable() {
            //if i get another NRE from here im killing myself
            ItemRegistry.Singleton["Tiers+:Blankcard"].AddToLootTable("entity:all", 0.00013f, 1);

            ItemRegistry.Singleton["Tiers+:Slivercard"].AddToLootTable("entity:sliver", 0.013f, 1);

            ItemRegistry.Singleton["Tiers+:Guardcard"].AddToLootTable("entity:guardian", 0.013f, 1);

            ItemRegistry.Singleton["Tiers+:Ghostcard"].AddToLootTable("entity:shroomy1", 0.013f, 1);

            ItemRegistry.Singleton["Tiers+:powercrystal"].AddToLootTable("entity:plasmaBug", 0.5f, 0, 4);

            ItemRegistry.Singleton["Tiers+:Mana Pack IV"].AddToLootTable("entity:testProjEnemy", 0.5f, 0, 2);

            ItemRegistry.Singleton["Tiers+:powercrystal"].AddToLootTable("entity:testProjEnemy", 0.5f, 0, 4);

            ItemRegistry.Singleton["Tiers+:powercrystal"].AddToLootTable("entity:particleWyvern", 1.0f, 0, 4);

            ItemRegistry.Singleton["Tiers+:Urugorak Scale"].AddToLootTable("entity:millipede", 1.0f, 1, 7);

            ItemRegistry.Singleton["Tiers+:Hivemind Eye"].AddToLootTable("entity:hivemind", 1.0f, 1, 7); //this registry entry is giving me gray hairs at 18

            ItemRegistry.Singleton["Tiers+:Scarab Tooth"].AddToLootTable("entity:scarab", 1.0f, 1, 7);

            ItemRegistry.Singleton["Tiers+:Bully Spore"].AddToLootTable("entity:bully", 1.0f, 1, 7);

            ItemRegistry.Singleton["Tiers+:Ancient Core"].AddToLootTable("entity:golem", 1.0f, 1, 7);

            ItemRegistry.Singleton["Tiers+:Plague Spike"].AddToLootTable("entity:plaguebeast", 1.0f, 1, 7);

            ItemRegistry.Singleton["Tiers+:Liquid Fire"].AddToLootTable("entity:lavadragon", 1.0f, 1, 7);

            ItemRegistry.Singleton["Tiers+:Briar Leaf"].AddToLootTable("entity:moloch", 1.0f, 1, 7);
            //Drop Tables
        }
        static IEnumerator TripleShot(PlayerScript script)
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
        private static IEnumerator EnergyPackSpeedBoost(int power, float duration)
        {
            GameScript.MODS[16] += power;
            GameScript.MODS[17] += power;
            GameScript.MODS[18] += power;
            int boostedMoveModCount = GameScript.MODS[16];
            int boostedDashModCount = GameScript.MODS[17];
            int boostedJumpModCount = GameScript.MODS[18];
            yield return new WaitForSeconds(duration);
            if (GameScript.MODS[16] <= boostedMoveModCount) GameScript.MODS[16] -= power;
            if (GameScript.MODS[17] <= boostedDashModCount) GameScript.MODS[17] -= power;
            if (GameScript.MODS[18] <= boostedJumpModCount) GameScript.MODS[18] -= power;
            yield break;
        }
    }
}
