

namespace Maham.Helpers
{
   public interface IFileHelper
    {
        void FilePath( string filepath);
        string file(string name);
        void GetStoragePermission();
        //bool StoragePermissionGranted();
       // void CreateAppSpecificDirectory();
    }
}
