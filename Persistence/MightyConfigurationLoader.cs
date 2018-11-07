using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace MightyHomeAutomation.Persistence
{
    public class MightyConfigurationLoader
    {
        private const string ConfigKeyMightyConfigFile = "MightyConfigFile";

        public ILoggerFactory LoggerFactory { get; }
        public string ConfigFile { get; }

        public MightyConfigurationLoader(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            ConfigFile = configuration[ConfigKeyMightyConfigFile];
        }

        public Configuration Load()
        {
            var logger = LoggerFactory.CreateLogger<MightyConfigurationLoader>();

            if (!File.Exists(ConfigFile))
            {
                logger.LogCritical("Did not find specified configuration file '{ConfigFile}'", ConfigFile);
                throw new FileNotFoundException("Could not load application configuration.");
            }

            try
            {
                var jsonSerializer = new JsonSerializer();
                using (var reader = new JsonTextReader(new StreamReader(ConfigFile, Encoding.UTF8, true)))
                {
                    return jsonSerializer.Deserialize<Configuration>(reader);
                }
            }
            catch (IOException e)
            {
                logger.LogCritical("Could not deserialize specified configuration file '{ConfigFile}'", ConfigFile);
                throw e;
            }
        }
    }
}
