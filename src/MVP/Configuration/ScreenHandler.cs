using MVP.Settings;

namespace MVP.Configuration
{
    public class ScreenHandler
    {
        public string Path { get; set; }
        public IList<ConfigFile> configFiles;
        public ConfigFile entryPoint;

        public ScreenHandler()
        {
            configFiles = new List<ConfigFile>();
        }

        private ScreenHandler SetTitle(string title)
        {
            Console.Title = title;
            return this;
        }

        public ScreenHandler ShowScreen(string screenId)
        {
            var file = configFiles.FirstOrDefault(c => c.Id == screenId);
            if (file is null)
                throw new Exception("File id not registered");

            SetTitle(file.Title);
            Console.WriteLine(file.Title);

            foreach (var field in file.Fields)
            {
                do
                {
                    Console.Write($"{(field.IsRequired ? "*" : string.Empty)}{field}: ");
                    var answer = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(answer) && field.IsRequired)
                    {
                        Console.Clear();
                        Console.WriteLine("Cannot leave required (*) fields empty.");
                        continue;
                    }

                    var x = Console.ReadLine();
                    break;
                }
                while (true);
            }

            return this;
        }
    }
}