using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GadgetCore.API;
using System.Collections;


namespace TiersPlus
{
	using System;
	using System.Collections;
	using UnityEngine;

	// Token: 0x02000240 RID: 576
	public class BerrieScript : CustomEntityScript<BerrieScript>
	{
		protected void awake() {
			Initialize(40000, 0, 50000 + GameScript.challengeLevel * 500, true, true, true);
			Update();
		}
		protected void Update()
		{
			if (IsDead) return;
			UpdateAI();
		}

		protected void FixedUpdate()
		{
			if (IsDead) return;
		}
		protected void UpdateAI() {
			if (this.HP < MaxHP / 2)
			{


			}
			else { }
		
		}

		public enum BerrieAttackPhases
		{
			IDLE,
			SLASH, //teleport to player, do a slash that fires projectiles
			DASH, //teleport to player, do a dash at them that shoots projectiles in a <::: pattern
			SLAM, //teleport above the player, hover for a second before slamming and releasing projectiles in an arc
			PHASETRANS1,
			SLASH2, //teleport to player, do a slash that fires projectiles, repeat 3 times in a L R L pattern
			DASH2, //Teleport to player, do 3 predictive dashes in a row, same projectiles as before
			SLAM2, //slam down on the player 5 times in a row, shooting arcing projectiles each time
			BEAM, //teleport to the center of the arena and sweep a beam across the whole thing, making players circle her
			THROW, //Wind up for a bit and then throw her lance at a player, causing a wave of projectiles to come out of the wall
			FINAL
		}
	}
}