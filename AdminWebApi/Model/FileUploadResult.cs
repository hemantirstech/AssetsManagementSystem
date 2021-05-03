using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminWebApi.Model
{
    public class FileUploadResult
    {
        public long Length { get; set; }

        public string Name { get; set; }
    }
}
