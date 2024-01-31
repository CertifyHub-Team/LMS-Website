using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Web;

[ApiController]
[Route("api/[controller]")]
public class QrCodeController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ICvService _cvService;

    public QrCodeController(HttpClient httpClient, ICvService cvService)
    {
        _httpClient = httpClient;
        _apiUrl = "https://chart.googleapis.com/chart?";
        _cvService = cvService;
    }


    [HttpPost]
    [Route("GenerateQRCode")]
    public async Task<IActionResult> GenerateQRCode(string name)
    {
        try
        {
            var apiUrl = $"https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl={HttpUtility.UrlEncode($"{name}")}&choe=UTF-8&chco=B0A695&chf=bg,s,FFFFFF&chld=H";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return (IActionResult)response.Content;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    //private string SaveImageAndGetUrl(byte[] imageData)
    //{
    //    var fileName = $"qr-{Guid.NewGuid()}.png";
    //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "qrimages", fileName);

    //    System.IO.File.WriteAllBytes(filePath, imageData);

    //    return $"/qrimages/{fileName}";
    //}



    //private async Task<string> GenerateAndSaveQRCode(string name)
    //{
    //    var apiUrl = $"https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl={HttpUtility.UrlEncode($"{name}")}&choe=UTF-8&chco=B0A695&chf=bg,s,FFFFFF&chld=H";

    //    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        if (response.Content.Headers.ContentType.MediaType.StartsWith("image"))
    //        {
    //            var imageData = await response.Content.ReadAsByteArrayAsync();
    //            return SaveImageAndGetUrl(imageData);
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException("Unexpected content type received in response.");
    //        }
    //    }
    //    else
    //    {
    //        var errorContent = await response.Content.ReadAsStringAsync();
    //        throw new HttpRequestException($"Error generating QR code. Status Code: {response.StatusCode}, Content: {errorContent}");
    //    }
    //}
}
