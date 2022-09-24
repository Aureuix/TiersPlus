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
    internal static class Enemies
    {
        internal static void PlasmaCultist() {
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
        }
        internal static void PlasmaCaster() {
            GameObject testProjEnemy = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("sponge"));
            testProjEnemy.name = "testProjEnemy";
            testProjEnemy.SetActive(false);
            testProjEnemy.ReplaceComponent<SpongeScript, TestProjScript>();
            EntityInfo TestProjEnemy = new EntityInfo(EntityType.COMMON, testProjEnemy).Register("testProjEnemy");

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
        }
        internal static void BlastBug() {
            GameObject plasmaPassive = UnityEngine.Object.Instantiate(GadgetCoreAPI.GetEntityResource("blastBug"));
            plasmaPassive.name = "plasmaPassive";
            plasmaPassive.SetActive(false);
            plasmaPassive.ReplaceComponent<BlastbugScript, PlasmaPassiveScript>();
            EntityInfo plasmaBug = new EntityInfo(EntityType.COMMON, plasmaPassive).Register("plasmaPassive");

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
        }
        internal static void PlasmaDragon() {
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
            for (int e = 1; e <= 5; e++)
            {
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
        }
    }
}
