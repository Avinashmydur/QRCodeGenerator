using QRCodeProject.Models;
using QRCoder;
using System;
using System.Web.Mvc;
using static QRCoder.PayloadGenerator;

namespace QRCodeProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/CreateQRCode
        // This action renders the view where the user can input the URL.
        public ActionResult CreateQRCode()
        {
            // Return the view with an empty QRCodeModel
            return View(new QRCodeModel());
        }

        // POST: Home/CreateQRCode
        // This action handles the form submission and generates the QR code.
        [HttpPost]
        public ActionResult CreateQRCode(QRCodeModel qrCodeModel)
        {
            // Create a payload using the URL provided by the user.
            Payload payload = new Url(qrCodeModel.QRCodeText);

            // Initialize the QR code generator
            QRCodeGenerator codeGenerator = new QRCodeGenerator();

            // Generate the QR code data from the payload
            QRCodeData qrCodeData = codeGenerator.CreateQrCode(payload);

            // Create a PNG byte array from the QR code data
            PngByteQRCode pngByte = new PngByteQRCode(qrCodeData);
            var QrByte = pngByte.GetGraphic(20);

            // Convert the byte array to a Base64 string
            string base64Url = Convert.ToBase64String(QrByte);

            // Set the Base64 string as the image source for the QR code
            qrCodeModel.QRImageUrl = "data:image/png;base64," + base64Url;

            // Return the view with the generated QR code
            return View("CreateQRCode", qrCodeModel);
        }
    }
}
