using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public interface IPhotoDateResolver
    {
        PhotoDateResolutionResult GetPhotoDate(FileInformation fileInformation);
    }
}
