namespace AdminPanel.Helper
{
    public class PictureSettings
    {
        public static string UploadFile(IFormFile File , string folderName)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

            var fileName = $"{Guid.NewGuid()}{File.FileName}"; 

            var filepath = Path.Combine(folderpath, fileName);  

           using var fs = new FileStream(filepath,FileMode.Create);

             File.CopyTo(fs);

            return Path.Combine("images/products/",fileName);
        }

        public static void DeleteFile(string FolderName , string FileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FolderName, FileName);

            if (File.Exists(filepath))
                File.Delete(filepath);
        }
    }
}
