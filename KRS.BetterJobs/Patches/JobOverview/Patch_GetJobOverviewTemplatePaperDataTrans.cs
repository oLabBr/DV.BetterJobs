using DV.Booklets;
using DV.Logic.Job;
using DV.RenderTextureSystem.BookletRender;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRS.BetterJobs.Patches.JobOverview
{
	[HarmonyPatch(typeof(BookletCreator_JobOverview), "GetJobOverviewTemplatePaperData", new Type[] { typeof(TransportJobData) })]
	static class Patch_GetJobOverviewTemplatePaperDataTrans
	{
		static void Postfix(object[] __args, ref List<TemplatePaperData> __result)
		{
			TransportJobData job = (TransportJobData)__args[0];

			List<TemplatePaperData> r = new List<TemplatePaperData>();
			foreach (TemplatePaperData tpd in __result)
			{
				FrontPageTemplatePaperData x = (FrontPageTemplatePaperData)tpd;
				x.startStationName = x.startStationName + " / " + job.startingTrack.TrackPartOnly;
				x.endStationName = x.endStationName + " / " + job.destinationTrack.TrackPartOnly;
				r.Add(x);
			}

			__result = r;
		}
	}
}
