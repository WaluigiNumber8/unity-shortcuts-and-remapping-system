
namespace RedRats.FileSystem
{
    public class FilePathInfo
    {
        private readonly string id;
        private string title;
        private string path;

        public FilePathInfo(string id, string title, string path)
        {
            this.id = id;
            this.title = title;
            this.path = path;
        }

        public void UpdateTitle(string newTitle) => title = newTitle;
        public void UpdatePath(string newPath) => path = newPath;
        
        public string ID { get => id; }
        public string Title { get => title; }
        public string Path { get => path; }
    }
}