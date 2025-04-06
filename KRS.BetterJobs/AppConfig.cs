using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace KRS.BetterJobs
{
	public class PaymentConfig {
		public float TotalMultiplier;
	}

	public class AppConfig
	{
		static AppConfig()
		{
			Load();
			AppDebug.Log($"Config at: {getConfigPath()}");
			AppDebug.Log($"Base Path: {Application.dataPath}");
			AppDebug.Log($"Persistent Path: {Application.persistentDataPath}");
			AppDebug.Log($"Assets Path: {Application.streamingAssetsPath}");
		}

		public static AppConfig Config;

		public PaymentConfig Payments { get; set; }

		private static string getConfigPath()
		{
			return Path.Combine(Application.persistentDataPath, "krs.betterjobs.xml");
		}

		public static void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
			using (StreamWriter writer = new StreamWriter(getConfigPath(), false))
			{
				serializer.Serialize(writer, Config);
			}
		}

		public static void Load()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));

			if (File.Exists(getConfigPath()))
			{
				using (StreamReader _rd = new StreamReader(getConfigPath()))
				{
					Config = (AppConfig)serializer.Deserialize(_rd);
				}	
			}
			else
			{
				Config = new AppConfig();
				Config.Payments = new PaymentConfig();
				Config.Payments.TotalMultiplier = 0.2f;
				Save();
			}
		}
	}
}
