using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class FileExport
    {
        /// <summary>
        /// Stream của file
        /// </summary>
        /// CreatedDate: 05/10/2021
        /// CreatedBy: TDDUNG
        public FileExport(MemoryStream fileStream, string fileContent, string fileName)
        {
            FileStream = fileStream;
            FileContent = fileContent;
            FileName = fileName;
        }

        /// <summary>
        /// Stream của file
        /// </summary>
        /// CreatedDate: 05/10/2021
        /// CreatedBy: TDDUNG
        public MemoryStream FileStream { get; set; }

        /// <summary>
        /// Content file
        /// </summary>
        /// CreatedDate: 05/10/2021
        /// CreatedBy: TDDUNG
        public string FileContent { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        /// CreatedDate: 05/10/2021
        /// CreatedBy: TDDUNG
        public string FileName { get; set; }
    }
}
