using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiersPlus
{
	using System;
	using System.Collections;
	using UnityEngine;

	// Token: 0x0200027C RID: 636
	public class MykWormEgg : MonoBehaviour
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x000A2F3C File Offset: 0x000A113C
		public void Start()
		{
			this.t = this.eye.transform;
			base.StartCoroutine(this.Blink());
			if (GameScript.challengeLevel > 0)
			{
				this.headObj.GetComponent<Renderer>().material = this.head1;
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000A2F88 File Offset: 0x000A1188
		[RPC]
		public void Explode()
		{
			this.exploding = true;
			base.StartCoroutine(this.Exploding());
			if (Network.isServer)
			{
				GameScript.wormBossCounter++;
				Camera.main.gameObject.SendMessage("WormBoss");
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000A2FC8 File Offset: 0x000A11C8
		public IEnumerator Exploding()
		{
			base.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/wormegg"), Menuu.soundLevel / 10f);
			this.headObj.GetComponent<Animation>().Play();
			this.eye.SetActive(false);
			this.blinkObj.SetActive(true);
			this.blinkObj.GetComponent<Renderer>().material = this.redeye;
			this.blinkObj.GetComponent<Animation>().Play();
			yield return new WaitForSeconds(1f);
			if (GameScript.challengeLevel > 0)
			{
				this.headObj.GetComponent<Renderer>().material = this.headExplode1;
			}
			else
			{
				this.headObj.GetComponent<Renderer>().material = this.headExplode;
			}
			UnityEngine.Object.Instantiate<GameObject>(this.particleGoo, this.headObj.transform.position, Quaternion.identity);
			this.blinkObj.SetActive(false);
			yield return new WaitForSeconds(0.1f);
			this.headObj.GetComponent<Animation>().Stop();
			if (Network.isServer)
			{
				//TiersPlus.MykWormEntity.Spawn(), this.headObj.transform.position, Quaternion.identity, 0);
				if (GameScript.challengeLevel > 1)
				{
					Network.Instantiate(Resources.Load("e/worm"), this.headObj.transform.position, Quaternion.identity, 0);
				}
			}
			yield break;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000A2FE4 File Offset: 0x000A11E4
		[RPC]
		public void App()
		{
			this.blinkObj.SetActive(false);
			if (GameScript.challengeLevel > 0)
			{
				this.headObj.GetComponent<Renderer>().material = this.headExplode1;
			}
			else
			{
				this.headObj.GetComponent<Renderer>().material = this.headExplode;
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000A303C File Offset: 0x000A123C
		public IEnumerator Blink()
		{
			while (!this.exploding)
			{
				this.blinkObj.SetActive(true);
				yield return new WaitForSeconds(0.2f);
				if (this.exploding)
				{
					yield break;
				}
				this.blinkObj.SetActive(false);
				yield return new WaitForSeconds((float)UnityEngine.Random.Range(1, 10) * 0.5f);
			}
			yield break;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000A3058 File Offset: 0x000A1258
		public void Update()
		{
			if (this.eyeTarget)
			{
				this.mouse_pos = this.eye.transform.position;
				this.objectPos = this.eyeTarget.transform.position;
				this.mouse_pos.z = -20f;
				this.mouse_pos.x = this.mouse_pos.x - this.objectPos.x;
				this.mouse_pos.y = this.mouse_pos.y - this.objectPos.y;
				this.angle = Mathf.Atan2(this.mouse_pos.y, this.mouse_pos.x) * 57.29578f;
				this.eye.transform.localRotation = Quaternion.Euler(0f, 0f, this.angle);
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000A3141 File Offset: 0x000A1341
		public void OnTriggerEnter(Collider c)
		{
			if (c.gameObject.layer == 8)
			{
				this.eyeTarget = c.gameObject;
			}
		}

		// Token: 0x04001056 RID: 4182
		public Material head1;

		// Token: 0x04001057 RID: 4183
		public Material headExplode1;

		// Token: 0x04001058 RID: 4184
		public Material redeye;

		// Token: 0x04001059 RID: 4185
		public GameObject particleGoo;

		// Token: 0x0400105A RID: 4186
		public Material headExplode;

		// Token: 0x0400105B RID: 4187
		public GameObject headObj;

		// Token: 0x0400105C RID: 4188
		public GameObject blinkObj;

		// Token: 0x0400105D RID: 4189
		private GameObject eyeTarget;

		// Token: 0x0400105E RID: 4190
		public GameObject eye;

		// Token: 0x0400105F RID: 4191
		private Transform t;

		// Token: 0x04001060 RID: 4192
		private Vector3 objectPos;

		// Token: 0x04001061 RID: 4193
		private float angle;

		// Token: 0x04001062 RID: 4194
		private Vector3 mouse_pos;

		// Token: 0x04001063 RID: 4195
		private bool initialized;

		// Token: 0x04001064 RID: 4196
		public GameObject[] trig = new GameObject[2];

		// Token: 0x04001065 RID: 4197
		private bool exploding;

		// Token: 0x04001066 RID: 4198
		private GameObject networkStuff;
	}

}
