using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FileTools.V1.Models
{
    /// <summary>
    /// Conteúdo do arquivo PDF e opções para processamento.
    /// </summary>
    public class PdfFile : IShrinkable
    {
        /// <summary>
        /// Arquivo à ser processado.
        /// </summary>
        [Required]
        public IFormFile Content { get; set; }

        /// <summary>
        /// Resolução das imagens para efetuar o shrink. Valor padrão 72.
        /// </summary>
        public int? Resolution { get; set; } = 72;
    }
}
