using Amazon.S3;
using Amazon.S3.Model;
using Infra.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Vaggi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IAmazonS3 _s3client;
    private readonly S3Setup _s3Setup;

    public FilesController(IAmazonS3 s3client, IOptions<S3Setup> s3Setup)
    {
        _s3client = s3client;
        _s3Setup = s3Setup.Value;
    }

    [HttpPost("images")]
    public async Task<IActionResult> UploadImageAsync(IFormFile image, CancellationToken cancellationToken)
    {
        if (image.Length == 0)
        {
            return BadRequest(
                new
                {
                    Message = "Arquivo de upload não informado."
                });
        }

        string[] extensionsAllowed = ["jpg", "png", "jpeg", "svg"];
        string fileExtension = image.FileName.Split(".").AsEnumerable().Last();
        bool acceptedExtendionFile = extensionsAllowed.Contains(fileExtension);

        if (!acceptedExtendionFile)
        {
            return BadRequest(new
            {
                Message = "Formato inválido."
            });
        }

        using Stream? fileStream = image.OpenReadStream();
        string objectKey = $"profile/images/{image.FileName}";

        var imageUploadRequest = new PutObjectRequest
        {
            InputStream = fileStream,
            BucketName = _s3Setup.BucketName,
            Key = objectKey,
            ContentType = image.ContentType,
            AutoCloseStream = true,
            Metadata =
                {
                    ["file_name"] = image.FileName,
                }
        };

        PutObjectResponse? imageUploadResponse = await _s3client.PutObjectAsync(imageUploadRequest, cancellationToken);

        await _s3client.MakeObjectPublicAsync(_s3Setup.BucketName, objectKey, true);

        return Ok(new
        {
            Code = imageUploadResponse.HttpStatusCode
        });
    }

    [HttpPost("cv")]
    public async Task<IActionResult> UploadFileAsync(IFormFile cv, CancellationToken cancellationToken)
    {
        if (cv.Length == 0)
        {
            return BadRequest(
                new
                {
                    Message = "Arquivo de upload não informado."
                });
        }

        string[] extensionsAllowed = ["pdf"];
        string fileExtension = cv.FileName.Split(".").AsEnumerable().Last();
        bool acceptedExtendionFile = extensionsAllowed.Contains(fileExtension);

        if (!acceptedExtendionFile)
        {
            return BadRequest(new
            {
                Message = "Formato inválido."
            });
        }

        using Stream? fileStream = cv.OpenReadStream();
        string objectKey = $"vacancy/{cv.FileName}";

        var fileUploadRequest = new PutObjectRequest
        {
            InputStream = fileStream,
            BucketName = _s3Setup.BucketName,
            Key = objectKey,
            ContentType = cv.ContentType,
            AutoCloseStream = true,
            Metadata =
                {
                    ["file_name"] = cv.FileName,
                }
        };

        PutObjectResponse? fileUploadResponse = await _s3client.PutObjectAsync(fileUploadRequest, cancellationToken);

        await _s3client.MakeObjectPublicAsync(_s3Setup.BucketName, objectKey, true);

        return Ok(new
        {
            Code = fileUploadResponse.HttpStatusCode
        });
    }
}
