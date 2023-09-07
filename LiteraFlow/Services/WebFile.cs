using System.Security.Cryptography;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace LiteraFlow.Web.Services;

public class WebFile
{
    public const string WWW_ROOT = "./wwwroot/";
    public const string IMG_PREFIX = WWW_ROOT + "images/";
    public const string PROFILE_PATH = IMG_PREFIX + "profiles/";
    public const string BOOK_PATH = "books/";
    public const string BLOG_PATH = "blogs/";


    private static void CreateFileFolder(string dir)
    {
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    }


    private static async Task UploadAndResizeImage(Stream fileStream, string filename, int newWidth = 600, int newHeight = 800)
    {
        // если ширина больше чем желаемая, берем желаемую
        // а высоту подгоняем аспектом

        using (Image image = await Image.LoadAsync(fileStream))
        {
            int aspectW = newWidth;
            int aspectH = newHeight;

            if (image.Width / (image.Height / (float)newHeight) > newWidth)
            {
                aspectH = (int)(image.Height / (image.Width / (float)newWidth));
            }
            else
                aspectW = (int)(image.Width / (image.Height / (float)newHeight));

            int width = image.Width / 2;
            int height = image.Height / 2;
            image.Mutate(x => x.Resize(aspectW, aspectH, KnownResamplers.Lanczos3));

            await image.SaveAsJpegAsync(filename, new JpegEncoder() { Quality = 75});
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="userFile">raw IFormFile obj from request</param>
    /// <param name="pathPrefix">path prifix from WebFile static fields</param>
    /// <returns>path of created file</returns>
    public static async Task<string?> SaveAsync(IFormFile userFile, string pathPrefix)
    {
        try
        {
            string imgName = userFile.FileName;

            string folderPath = pathPrefix + GeneratePathForFile(imgName);

            CreateFileFolder(folderPath);
            string totalFilePath = folderPath + Path.GetFileNameWithoutExtension(imgName) + ".jpg";
            await UploadAndResizeImage(userFile.OpenReadStream(), totalFilePath);
            return totalFilePath.Remove(0, WWW_ROOT.Length);
        }
        catch
        {
            return null;
        }
       
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="sizeOfFolder">16 ^ size folders | default 3 (4_096) folders</param>
    /// <param name="sizeOfFile">  16 ^ size files   | default 6 (16_777_216) files total (4096 in single folder)</param>
    /// <returns></returns>
    private static string GeneratePathForFile(string path, int sizeOfFolder = 3, int sizeOfFile = 6)
    {
        MD5 mD5 = MD5.Create();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(path);
        byte[] hashBytes = mD5.ComputeHash(inputBytes);

        string hash = Convert.ToHexString(hashBytes);
        return hash.Substring(0, sizeOfFolder) + "/" + hash.Substring(0, sizeOfFile) + "/";
    }

}
