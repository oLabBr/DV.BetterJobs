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

	[HarmonyPatch(typeof(BookletCreator_JobOverview), "GetJobOverviewTemplatePaperData", new Type[] { typeof(ShuntingLoadJobData) })]
	static class Patch_GetJobOverviewTemplatePaperDataShuntLoad
	{
		static void Postfix(object[] __args, ref List<TemplatePaperData> __result)
		{
			ShuntingLoadJobData job = (ShuntingLoadJobData)__args[0];
			List<TemplatePaperData> r = new List<TemplatePaperData>();
			foreach (TemplatePaperData tpd in __result)
			{
				FrontPageTemplatePaperData x = (FrontPageTemplatePaperData)tpd;
				List<string> tracks = new List<string>();
				foreach (CarDataPerTrackID d in job.startingTracksData)
				{
					tracks.Add(d.track.TrackPartOnly);
				}
				string track = tracks.Join<string>();

				x.jobDescription = x.jobDescription + "\n Pick-up: " + job.loadMachineTrack.TrackPartOnly
					+ $" at  \"{track}\" " +
					" \r\n Unload: " + job.destinationTrack.TrackPartOnly;
				r.Add(x);
			}

			__result = r;
		}
	}
}
