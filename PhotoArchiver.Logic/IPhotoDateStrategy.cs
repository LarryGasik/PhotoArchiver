using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public interface IPhotoDateStrategy
    {
        bool CanResolve(FileInformation fileInformation);

        PhotoDateResolutionResult? Resolve(FileInformation fileInformation);
    }
}
