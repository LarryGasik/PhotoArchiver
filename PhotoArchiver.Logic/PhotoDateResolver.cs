using System;
using System.Collections.Generic;
using System.Linq;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public class PhotoDateResolver : IPhotoDateResolver
    {
        private readonly IReadOnlyCollection<IPhotoDateStrategy> _strategies;

        public PhotoDateResolver(IEnumerable<IPhotoDateStrategy> strategies)
        {
            _strategies = strategies.ToList();
        }

        public PhotoDateResolutionResult GetPhotoDate(FileInformation fileInformation)
        {
            foreach (var strategy in _strategies.Where(strategy => strategy.CanResolve(fileInformation)))
            {
                var resolution = strategy.Resolve(fileInformation);
                if (resolution != null)
                {
                    return resolution;
                }
            }

            throw new InvalidOperationException($"Unable to determine photo date for '{fileInformation.FileName}'.");
        }
    }
}
