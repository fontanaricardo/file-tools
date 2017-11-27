using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileTools.Services;
using FileTools.V1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileTools.V1.Controllers
{
    /// <summary>
    /// Endpoints para manipulação de arquivos PDF.
    /// </summary>
    [Route("api/v1/pdf")]
    public class PdfController : Controller
    {
        private readonly IFileService _service;

        /// <summary>
        /// Construtor padrão com o serviço para manipulação de arquivos.
        /// </summary>
        /// <param name="service">Serviço de manipulação de arquivos a ser injetado.</param>
        public PdfController(IFileService service)
        {
            _service = service;
        }

        /// <summary>
        /// Reduz a resolução das imagens para diminuir o tamanho do arquivo PDF.
        /// </summary>
        [Route("shrink")]
        [HttpPost]
        [ProducesResponseType(typeof(byte[]), 200)]
        public async Task<IActionResult> ShrinkAsync(PdfFile input)
        {
            var result = await _service.ShrinkAsync(input);
            return new FileStreamResult(new MemoryStream(result), "application/pdf");
        }
    }
}
