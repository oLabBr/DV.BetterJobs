using DV.Logic.Job;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KRS.BetterJobs.Patches.JobPayment
{
	[HarmonyPatch(typeof(Job), "GetBasePaymentForTheJob")]
	static class Patch_JobBasePayment
	{
		static void Postfix(ref float __result)
		{
			AppDebug.Log("Patching job value: " + __result);
			float __additionalValue = (__result * AppConfig.Config.Payments.TotalMultiplier);
			__result = __additionalValue + __result;
			AppDebug.Log("New job value: " + __result);
		}
	}
}
