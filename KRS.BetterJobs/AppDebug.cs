using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KRS.BetterJobs
{
	public static class AppDebug
	{
		public static void Log(string message) {
			Debug.Log($"[KRS.BetterJobs] {message}");
		}
	}
}
