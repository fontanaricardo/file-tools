using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileTools.Helpers;
using FileTools.V1.Models;

namespace FileTools.Services
{
    public interface IFileService
    {
        Task<byte[]> ShrinkAsync(IShrinkable input);
    }
}