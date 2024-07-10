namespace AppTemplate.Dto.Helpers
{
    public static class CommonMethods
    {
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Characters to choose from
            var random = new Random();
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()); // Generate the random string

            return randomString;
        }
        public static DateTime GetBDCurrentTime()
        {
            //Previously Used
            //var Bangladesh_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            //return TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow, Bangladesh_Standard_Time);

            //Added to support PostGreSQL
            int interval = 6; // BD Time = Utc + 6 hours 

            return DateTime.UtcNow.AddHours(interval);
        }
        public static string GetFormattedSlug(string slug)
        {
            //return slug.Replace(" ", "_") + DateTime.Now.Ticks.ToString();
            return slug.Replace(" ", "").Replace("&", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        //public static string GetMonthWiseYearQyarter(int monthId)
        //{
        //    if (monthId >= 1 && monthId <= 3)
        //    {
        //        return Enum.GetName(typeof(YearQuarters), 1);
        //        //return (int)YearQuarters.Q1;
        //    }
        //    else if (monthId >= 4 && monthId <= 6)
        //    {
        //        return Enum.GetName(typeof(YearQuarters), 2);
        //        //return (int)YearQuarters.Q2;
        //    }
        //    else if (monthId >= 7 && monthId <= 9)
        //    {
        //        return Enum.GetName(typeof(YearQuarters), 3);
        //        //return (int)YearQuarters.Q3;
        //    }
        //    else
        //    {
        //        return Enum.GetName(typeof(YearQuarters), 4);
        //        //return (int)YearQuarters.Q4;
        //    }
        //}
        //public static string GetFilePaths(string relativeFilePath, IFormFile uploadedFiles)
        //{
        //    //List<string> fileList = new List<string>();
        //    if (uploadedFiles != null)
        //    {
        //        if (!Directory.Exists(relativeFilePath))
        //        {
        //            try
        //            {
        //                // Create the directory if it doesn't exist
        //                Directory.CreateDirectory(relativeFilePath);
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //        //foreach (var file in uploadedFiles)
        //        //{
        //        var fileInfo = new FileInfo(uploadedFiles.FileName);
        //        var fileExtension = fileInfo.Extension;
        //        var fileName = $"{"File_"}{fileExtension}";
        //        var uniqueFileName = GetUniqueFileName(fileName);
        //        var filePath = Path.Combine(relativeFilePath, relativeFilePath, uniqueFileName);
        //        uploadedFiles.CopyToAsync(new FileStream(filePath, FileMode.Create));
        //        //fileList.Add(uniqueFileName);
        //        //}
        //        return uniqueFileName;
        //    }
        //    return string.Empty;

        //}
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            var uniqueName = Path.GetFileNameWithoutExtension(fileName) + "_";
            return string.Concat(uniqueName
                                , Guid.NewGuid().ToString().AsSpan(0, 4)
                                , Path.GetExtension(fileName));
        }
    }
}
