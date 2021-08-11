using BusinessLayer.Utilities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjeKampi.Models
{
    public class ImageViewModel
    {
        [Required]
        [Display(Name = "Upload File")]
        [AllowFileSize(FileSize = 5 * 1024 * 1024, ErrorMessage = "İzin verilen dosya boyutu : 5 MB")] //Resim boyutu en fazla 5MB
        [AllowExtensions(Extensions = "png,jpg,jpeg,gif,PNG,JPG,JPEG,GIF", ErrorMessage = "Desteklenen dosya uzantıları :  .png | .jpg | .jpeg | .gif")]
        public HttpPostedFileBase FileAttach { get; set; }

        public string Message { get; set; }

        public bool IsValid { get; set; }
    }
}