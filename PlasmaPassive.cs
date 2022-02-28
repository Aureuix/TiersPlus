using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TiersPlus
{
    class PlasmaPassiveScript : EnemyScript
	{
		public void Awake()
		{
			this.networkR2 = (NetworkR2)base.gameObject.GetComponent("NetworkR2");
			this.b.GetComponent<Animation>()["i"].layer = 0;
			this.b.GetComponent<Animation>()["a"].layer = 1;
			this.b.GetComponent<Animation>()["r"].layer = 0;
			this.b.GetComponent<Animation>()["r"].speed = 1f;
			this.b.GetComponent<Animation>()["r"].speed = 1.5f;
			this.b.GetComponent<Animation>()["a"].speed = 0.5f;
			this.drops = new int[]
			{
			22,
			22,
			22
			};
			base.Initialize(5000, 89, 6, this.drops, 3);
			if (Network.isServer)
			{
				base.StartCoroutine(this.Behavior());
			}
		}
		public bool beenHit;
		[RPC]
		public new void TD(float[] msg)
		{
			beenHit = true;
			base.TD(msg);
		}
		// Token: 0x0600021C RID: 540 RVA: 0x0000F129 File Offset: 0x0000D329
		[RPC]
		public void aud1()
		{
			base.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/chamcham"), Menuu.soundLevel / 10f);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000F150 File Offset: 0x0000D350
		public void Animate(int a)
		{
			this.networkR2.mode = a;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000F160 File Offset: 0x0000D360
		public IEnumerator Behavior()
		{
			if (beenHit || GameScript.challengeLevel > 0)
			{
				yield return new WaitForSeconds(0.5f);
				this.r.useGravity = true;
				for (; ; )
				{
					yield return new WaitForSeconds(0.5f);
					if (UnityEngine.Random.Range(0, 5) == 0)
					{
						this.Animate(1);
						yield return new WaitForSeconds(1.5f);
						this.yawning = true;
						yield return new WaitForSeconds(0.3f);
						this.yawning = false;
					}
					yield return new WaitForSeconds((float)UnityEngine.Random.Range(2, 4) * 0.5f);
					if (this.e.transform.rotation.y > 0f)
					{
						this.e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					}
					else
					{
						this.e.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
					}
					if (UnityEngine.Random.Range(0, 2) == 0)
					{
						this.Animate(5);
						this.moving = true;
						yield return new WaitForSeconds(3f);
						this.moving = false;
					}
				}
			}
			else
            {
				yield return new WaitForSeconds(1f);
				this.r.useGravity = true;
				for (; ; )
				{
					yield return new WaitForSeconds(0.5f);
					if (UnityEngine.Random.Range(0, 5) == 0)
					{
						//base.GetComponent<NetworkView>().RPC("aud1", RPCMode.All, new object[0]);
						this.yawning = true;
						yield return new WaitForSeconds(0.5f);
						this.yawning = false;
					}
					yield return new WaitForSeconds((float)UnityEngine.Random.Range(5, 10) * 0.2f);
					if (this.e.transform.rotation.y > 0f)
					{
						this.e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					}
					else
					{
						this.e.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
					}
					if (UnityEngine.Random.Range(0, 2) == 0)
					{
						this.networkR2.mode = 5;
						this.moving = true;
						yield return new WaitForSeconds(3f);
						this.moving = false;
					}
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000F17C File Offset: 0x0000D37C
		public void Update()
		{
			if (Network.isServer)
			{
				if (Physics.Raycast(this.t.position, Vector3.right, out this.hit, 2f, 8390656))
				{
					this.e.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				}
				else if (Physics.Raycast(this.t.position, Vector3.left, out this.hit, 2f, 8390656))
				{
					this.e.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				}
				if (this.r.velocity.y < -10f)
				{
					this.r.velocity = new Vector3(this.r.velocity.x, -10f, 0f);
				}
				if (this.moving && !this.knocking)
				{
					this.Animate(5);
					if (this.e.transform.rotation.y > 0f)
					{
						this.r.velocity = new Vector3(6f, this.r.velocity.y, 0f);
					}
					else
					{
						this.r.velocity = new Vector3(-6f, this.r.velocity.y, 0f);
					}
				}
				else if (this.yawning)
				{
					this.Animate(5);
					if (this.e.transform.rotation.y > 0f)
					{
						this.r.velocity = new Vector3(15f, this.r.velocity.y, 0f);
					}
					else
					{
						this.r.velocity = new Vector3(-15f, this.r.velocity.y, 0f);
					}
				}
				else
				{
					this.Animate(0);
				}
			}
		}

		// Token: 0x04000206 RID: 518
		private bool moving;

		// Token: 0x04000207 RID: 519
		private bool yawning;

		// Token: 0x04000208 RID: 520
		private RaycastHit hit;
	}
}
