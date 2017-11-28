using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileTools.Helpers;
using FileTools.V1.Models;
using Microsoft.Extensions.Logging;

namespace FileTools.Services
{
    public class PdfService : IFileService
    {
        private readonly ILogger _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> ShrinkAsync(IShrinkable input)
        {
            var startTime = DateTime.Now;

            var originalFilePath = Path.GetTempFileName();
            using(var stream = new FileStream(originalFilePath, FileMode.Create))
            {
                await input.Content.CopyToAsync(stream);
            }

            var processedFilePath = Path.GetTempFileName();

            string cmd = $"gs -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dQUIET -q -dNOPAUSE -dBATCH -dSAFER -dPDFSETTINGS=/screen -dEmbedAllFonts=true -dSubsetFonts=true -dAutoRotatePages=/None -dColorImageDownsampleType=/Bicubic -dColorImageResolution={input.Resolution} -dGrayImageDownsampleType=/Bicubic -dGrayImageResolution={input.Resolution} -dMonoImageDownsampleType=/Bicubic -dMonoImageResolution={input.Resolution} -sOutputFile={processedFilePath} {originalFilePath}";

            var output = ShellHelper.Bash(cmd);

            var processedFileBytes = File.ReadAllBytes(processedFilePath);

            File.Delete(originalFilePath);
            File.Delete(processedFilePath);

            var elapsedTime = DateTime.Now.Subtract(startTime);

            _logger.LogInformation($"File ${input.Content.FileName} shrinked in {elapsedTime.Seconds} seconds.");

            return processedFileBytes;
        }
    }
}