using System;

namespace PhotoArchiver.Logic
{
    public static class AlbumNameGenerator
    {
        public static string GenerateAlbumName(string fileName)
        {
            string year = fileName.Substring(0, 4);
            string month = fileName.Substring(4, 2);
            int day = DateTime.DaysInMonth(Convert.ToInt16(year), Convert.ToInt16(month));
            string FolderName = String.Format("{0}-{1}-{2}", year, month, day);
            return FolderName;
        }
    }
}
