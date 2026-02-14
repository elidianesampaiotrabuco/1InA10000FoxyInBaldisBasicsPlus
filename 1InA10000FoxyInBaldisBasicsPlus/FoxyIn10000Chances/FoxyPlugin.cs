using System;
using System.Collections;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.Registers;
using UnityEngine;
using UnityEngine.UI;

namespace FoxyIn10000Chances
{
	// Token: 0x02000004 RID: 4
	[BepInPlugin("alexbw145.baldiplus.foxyjumpscare", "The 1 in 10000 Foxy Jumpscare Mod", "1.0.1")]
	[BepInDependency("mtm101.rulerp.bbplus.baldidevapi", "10.0.0.0")]
	[BepInProcess("BALDI.exe")]
	//[BepInProcess("Baldi's Basics Plus Prerelease.exe")] - mod might not work correctly in 0.14 pre-release
	public class FoxyPlugin : BaseUnityPlugin
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		private void Awake()
		{
			FoxyPlugin.JumpscareChance = base.Config.Bind<int>("Settings", "JumpscareChance", 10000, "Sets the jumpscare chance for Withered Foxy");
			new Harmony("alexbw145.baldiplus.foxyjumpscare").PatchAllConditionals();
Debug.Log("The 1 in 10000 Chance for Foxy Jumpscare Mod - Decompiled and ported by MSVE640 HD");
Debug.Log("Enjoy!");
			LoadingEvents.RegisterOnAssetsLoaded(base.Info, this.StartLoad(), 0);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020BA File Offset: 0x000002BA
		private IEnumerator StartLoad()
		{
			yield return 1;
			yield return string.Format("There's a 1 in a {0} chance that Withered Foxy will kill you.", FoxyPlugin.JumpscareChance.Value);
			Sprite[] item = AssetLoader.SpritesFromSpritesheet(14, 1, 1f, Vector2.one / 2f, AssetLoader.TextureFromMod(this, new string[]
			{
				"foxy.png"
			}));
			Canvas component = UnityEngine.Object.Instantiate<GameObject>(Resources.FindObjectsOfTypeAll<Gum>().ToList<Gum>().Last<Gum>().transform.Find("GumOverlay").gameObject, MTM101BaldiDevAPI.prefabTransform).GetComponent<Canvas>();
			component.transform.Find("Image").GetComponent<Image>().sprite = null;
			component.gameObject.name = "foxy png";
			component.gameObject.SetActive(true);
			FoxyPlugin.jumpscareStuff = new Tuple<Sprite[], SoundObject, GameObject>(item, ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, new string[]
			{
				"Xscream3.ogg"
			}), "fnaf2jumpscare", SoundType.Voice, Color.red, 0f), component.gameObject);
			yield break;
		}

		// Token: 0x04000002 RID: 2
		private const string PLUGIN_GUID = "alexbw145.baldiplus.foxyjumpscare";

		// Token: 0x04000003 RID: 3
		private const string PLUGIN_NAME = "The 1 in 10000 Foxy Jumpscare Mod";

		// Token: 0x04000004 RID: 4
		private const string PLUGIN_VERSION = "1.0.1";

		// Token: 0x04000005 RID: 5
		internal static ConfigEntry<int> JumpscareChance;

		// Token: 0x04000006 RID: 6
		internal static Tuple<Sprite[], SoundObject, GameObject> jumpscareStuff;
	}
}
