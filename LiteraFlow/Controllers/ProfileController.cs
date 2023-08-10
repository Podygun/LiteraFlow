
using System.Security.Cryptography;

namespace LiteraFlow.Web.Controllers;

public class ProfileController : Controller
{
    [HttpGet]
    [Route("/profile")]
    public IActionResult Index()
    {
        return View(new ProfileViewModel());
    }

    [HttpPost]
    [Route("/profile")]
    public async Task<IActionResult> IndexSave(ProfileViewModel model)
    {
        //TODO refactor code to another layer

        //if(ModelState.IsValid) { }
        string filename = string.Empty;
        var imgData = Request.Form.Files[0];

        if(imgData is null) return View(model);

        MD5 mD5 = MD5.Create();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imgData.FileName);
        byte[] hashBytes = mD5.ComputeHash(inputBytes);

        string hash = Convert.ToHexString(hashBytes);
        var dir = "./wwwroot/images/" + hash.Substring(0,3)+ "/" + hash.Substring(0,6);

        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        filename = dir + "/" + imgData.FileName;

        using (var stream = System.IO.File.Create(filename))
        {
            await imgData.CopyToAsync(stream);
        }
        

        return View("Index", new ProfileViewModel());
    }
}
