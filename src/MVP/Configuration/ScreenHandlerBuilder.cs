using MVP.Settings;
using Newtonsoft.Json;

namespace MVP.Configuration
{
    public class ScreenHandlerBuilder // : IScreenHandlerBuilder
    {
        private ScreenHandler _screenHandler;
        private string _entryPoint;

        public ScreenHandlerBuilder()
        {
            _screenHandler = new();
            _screenHandler.configFiles = new List<ConfigFile>();
        }

        public ScreenHandler Build() => _screenHandler;

        public ScreenHandlerBuilder AddFile(string path)
        {
            using (var file = new StreamReader(path))
            {
                var newFile = JsonConvert.DeserializeObject<ConfigFile>(file.ReadToEnd());
                if (newFile is null)
                    throw new Exception($"Could not find any configuration file on path ({path}))");

                _screenHandler.configFiles.Add(newFile);
                return this;
            }
        }

        public ScreenHandlerBuilder SetEntryPoint(string fileId)
        {
            var entryPoint = JsonConvert.DeserializeObject<ConfigFile>(fileId)!;
            _screenHandler.entryPoint = entryPoint;
            return this;
        }

        // public ScreenHandlerBuilder SetFields(IEnumerable<Field> fields)
        // {
        //     var name = new List<string>(fields);
        // }
        // public ScreenHandlerBuilder SetActions(IEnumerable<MVP.Settings.Action> actions)
        // {
        // }
    }
}
