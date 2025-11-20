using System;

namespace PhotoArchiver.Logic
{
    public static class AlbumNameGenerator
    {
        public static string GenerateAlbumName(DateTime photoDate)
        {
            int day = DateTime.DaysInMonth(photoDate.Year, photoDate.Month);
            return $"{photoDate:yyyy-MM}-{day:00}";
        }
    }
}
