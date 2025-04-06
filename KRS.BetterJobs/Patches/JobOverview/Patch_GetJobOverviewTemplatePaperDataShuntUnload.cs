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

	[HarmonyPatch(typeof(BookletCreator_JobOverview), "GetJobOverviewTemplatePaperData", new Type[] { typeof(ShuntingUnloadJobData) })]
	static class Patch_GetJobOverviewTemplatePaperDataShuntUnload
	{
		static void Postfix(object[] __args, ref List<TemplatePaperData> __result)
		{
			ShuntingUnloadJobData job = (ShuntingUnloadJobData)__args[0];
			List<TemplatePaperData> r = new List<TemplatePaperData>();
			foreach (TemplatePaperData tpd in __result)
			{
				FrontPageTemplatePaperData x = (FrontPageTemplatePaperData)tpd;
				List<string> tracks = new List<string>();
				foreach (CarDataPerTrackID d in job.destinationTracksData)
				{
					tracks.Add(d.track.TrackPartOnly);
				}
				string track = tracks.Join<string>();

				x.jobDescription = x.jobDescription + "\n Pick-up: " + job.startingTrack.TrackPartOnly
						+ " \r\n Unload: " + job.unloadMachineTrack.TrackPartOnly + " at [" + track + "]";
				r.Add(x);
			}

			__result = r;
		}
	}

}
