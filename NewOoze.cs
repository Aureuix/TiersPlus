using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GadgetCore.API;
using GadgetCore.Util;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using RecipeMenuCore;
using RecipeMenuCore.API;

namespace TiersPlus
{
	public class NewOozeScript : EnemyScript
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x000A2878 File Offset: 0x000A0A78
		public void Awake()
		{
			this.drops = new int[]
			{
			2,
			14,
			14
			};
			if (this.isFell)
			{
				base.Initialize(8500, 88, 103, this.drops, 110);
			}
			else
			{
				base.Initialize(200, 5, 103, this.drops, 110);
			}
			if (Network.isServer)
			{
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000A28E3 File Offset: 0x000A0AE3
		[RPC]
		public void MakeFace()
		{
			base.StartCoroutine(this.MF());
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000A28F4 File Offset: 0x000A0AF4
		public IEnumerator MF()
		{
			this.b.GetComponent<Animation>().Play("a");
			yield return new WaitForSeconds(0.8f);
			base.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/wickedp"), Menuu.soundLevel / 10f);
			yield break;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000A2910 File Offset: 0x000A0B10
		public IEnumerator Bolt()
		{
			for (; ; )
			{
				if (this.target && Vector3.Distance(this.target.transform.position, this.t.position) < 20f)
				{
					this.shooting = true;
					base.GetComponent<NetworkView>().RPC("MakeFace", RPCMode.All, new object[0]);
					yield return new WaitForSeconds(1f);
					GameObject p = (GameObject)Network.Instantiate(Resources.Load("proj/wickedProj"), this.t.position, Quaternion.identity, 0);
					if (p)
					{
						p.SendMessage("EnemySet", this.target.transform.position, SendMessageOptions.DontRequireReceiver);
					}
					yield return new WaitForSeconds(0.5f);
					this.shooting = false;
				}
				yield return new WaitForSeconds(2f);
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000A292C File Offset: 0x000A0B2C
		public void Update()
		{
			if (Network.isServer)
			{
				if (this.target)
				{
					if (Mathf.Abs(this.target.transform.position.x - this.t.position.x) < 80f)
					{
						if (!this.attacking)
						{
							this.attacking = true;
							base.StartCoroutine(this.Attack());
						}
					}
					else
					{
						this.target = null;
						this.r.velocity = new Vector3(0f, 0f, 0f);
					}
				}
				if (this.moving && !this.knocking && !this.shooting)
				{
					this.r.velocity = this.dir * this.spd;
				}
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000A2A1C File Offset: 0x000A0C1C
		public IEnumerator Attack()
		{
			if (this.target.transform.position.x > this.t.position.x)
			{
				this.e.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			}
			else
			{
				this.e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
			if (UnityEngine.Random.Range(0, 4) == 0)
			{
				this.spd = 3f;
				this.dir = this.t.position - this.target.transform.position;
			}
			else
			{
				this.spd = 12f;
				Vector3 a = new Vector3(this.target.transform.position.x, this.target.transform.position.y + (float)UnityEngine.Random.Range(-15, 15), 0f);
				this.dir = a - this.t.position;
			}
			this.dir.Normalize();
			this.moving = true;
			yield return new WaitForSeconds((float)UnityEngine.Random.Range(1, 3) * 0.2f);
			this.moving = false;
			yield return new WaitForSeconds(0.5f);
			this.attacking = false;
			yield break;
		}

		// Token: 0x0400103F RID: 4159
		public bool isFell;

		// Token: 0x04001040 RID: 4160
		private bool attacking;

		// Token: 0x04001041 RID: 4161
		private Vector3 dir;

		// Token: 0x04001042 RID: 4162
		private bool moving;

		// Token: 0x04001043 RID: 4163
		private float spd;

		// Token: 0x04001044 RID: 4164
		private bool shooting;
	}
}
