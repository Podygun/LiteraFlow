using System.Security.Cryptography;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace LiteraFlow.Web.Services;

public class WebFile
{
    public const string IMG_PREFIX = "./wwwroot";

    public static string CreateFilePath(string fileName)
    {
        MD5 mD5 = MD5.Create();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(fileName);
        byte[] hashBytes = mD5.ComputeHash(inputBytes);

        string hash = Convert.ToHexString(hashBytes);
        return "/images/" + hash.Substring(0, 3) + "/" + hash.Substring(0, 6);
    }

    public static void CreateFileFolder(string dir)
    {
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    }

    public static string CreateWebFile(string filename)
    {
        string dir = CreateFilePath(filename);
        CreateFileFolder(IMG_PREFIX + dir);
        return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpg";
    }

    public static async Task UploadAndResizeImage(Stream fileStream, string filename, int newWidth, int newHeight)
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

            await image.SaveAsJpegAsync(IMG_PREFIX + filename, new JpegEncoder() { Quality = 75});
        }
    }
}
