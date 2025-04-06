using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace KRS.BetterJobs
{
	[EnableReloading]
	public static class Main
	{
		// Unity Mod Manage Wiki: https://wiki.nexusmods.com/index.php/Category:Unity_Mod_Manager
		private static bool Load(UnityModManager.ModEntry modEntry)
		{
			Harmony? harmony = null;

			try
			{
				harmony = new Harmony(modEntry.Info.Id);
				harmony.PatchAll(Assembly.GetExecutingAssembly());

				// Other plugin startup logic
			}
			catch (Exception ex)
			{
				modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
				harmony?.UnpatchAll(modEntry.Info.Id);
				return false;
			}

			return true;
		}

		static bool Unload(UnityModManager.ModEntry modEntry)
		{
			modEntry.Logger.Log("Map restart still required to regenerate overview book textures.");
			Harmony harmony = new Harmony(modEntry.Info.Id);
			harmony.UnpatchAll();

			return true;
		}
	}

}
