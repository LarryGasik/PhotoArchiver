using System;

namespace PhotoArchiver.Logic
{
    public class PhotoDateResolutionResult
    {
        public PhotoDateResolutionResult(DateTime photoDate, bool dateFromFileName)
        {
            PhotoDate = photoDate;
            DateFromFileName = dateFromFileName;
        }

        public DateTime PhotoDate { get; }

        public bool DateFromFileName { get; }
    }
}
