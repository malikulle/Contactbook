using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework.Services.Abstract
{
    public interface IExcelService
    {
        string ExportExcel(Guid reportId);
    }
}
