using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookClient
{
    // Need to set build action to EmbeddedResource for settings.json
    public static class SettingsLoader
	{
        const string Filename = "settings.json";
        const string ResourceName = "BookClient.settings.json";

        public static async Task<Settings> LoadAsync()
        {
			using (var reader = new StreamReader(OpenData()))
            {
                var settings = JsonConvert.DeserializeObject<Settings>(await reader.ReadToEndAsync());
                return settings;
            }
        }

        public static IStreamLoader Loader { get; set; }

		private static Stream OpenData()
        {
            if (Loader == null)
                throw new ApplicationException("Must set platform Loader before calling Load.");

            return Loader.GetStreamForFilename(Filename);
        }

		public static async Task<Settings> ImprovedLoadAsync()
		{
		    var assembly = typeof(SettingsLoader).GetTypeInfo().Assembly;
		    using (var stream = assembly.GetManifestResourceStream(ResourceName))
		    using (var reader = new StreamReader(stream))
		    {
                var settings = JsonConvert.DeserializeObject<Settings>(await reader.ReadToEndAsync());
                return settings;
		    }
		}

        public static Settings ImprovedLoad()
        {
            var assembly = typeof(SettingsLoader).GetTypeInfo().Assembly;
            var names = assembly.GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream(ResourceName))
            using (var reader = new StreamReader(stream))
            {
                var settings = JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd());
                return settings;
            }
        }
    }
}
