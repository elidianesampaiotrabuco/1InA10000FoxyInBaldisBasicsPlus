using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MTM101BaldAPI.Components;
using MTM101BaldAPI.Components.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace FoxyIn10000Chances
{
	// Token: 0x02000005 RID: 5
	[HarmonyPatch(typeof(PlayerManager), "Start")]
	internal class JumpscareInit
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020D1 File Offset: 0x000002D1
		private static void DoJumpscare(GameObject foxy)
		{
			foxy.GetComponent<CustomImageAnimator>().Play("boo", 1f);
			Singleton<CoreGameManager>.Instance.audMan.PlaySingle(FoxyPlugin.jumpscareStuff.Item2);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		private static IEnumerator JumpscareChance(GameObject foxy)
		{
			float timer = 1f;
			for (;;)
			{
				if (timer <= 0f)
				{
					if (UnityEngine.Random.Range(1, FoxyPlugin.JumpscareChance.Value) == 1)
					{
						JumpscareInit.DoJumpscare(foxy);
					}
					timer = 1f;
					yield return null;
				}
				else
				{
					timer -= 1f * Time.deltaTime;
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		private static void Postfix(PlayerManager __instance)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(FoxyPlugin.jumpscareStuff.Item3, null);
			CustomImageAnimator customImageAnimator = gameObject.gameObject.AddComponent<CustomImageAnimator>();
			customImageAnimator.image = gameObject.transform.Find("Image").GetComponent<Image>();
			customImageAnimator.AddAnimation("boo", new SpriteAnimation(FoxyPlugin.jumpscareStuff.Item1, 0.45f));
			Dictionary<string, SpriteAnimation> thing = new Dictionary<string, SpriteAnimation>(); // placeholder snippet cause i can't figure that out
            string key = "doNothing";
			Sprite[] array = new Sprite[1];
			array[0] = Resources.FindObjectsOfTypeAll<Sprite>().Last((Sprite x) => x.name == "Transparent");
            customImageAnimator.AddAnimation(key, new SpriteAnimation(array, 1f));
			customImageAnimator.SetDefaultAnimation("doNothing", 1f);
			__instance.StartCoroutine(JumpscareInit.JumpscareChance(gameObject));
		}
	}
}
