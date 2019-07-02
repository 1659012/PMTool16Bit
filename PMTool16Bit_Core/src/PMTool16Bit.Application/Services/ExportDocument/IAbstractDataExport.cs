using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace PMTool16Bit.Services
{
    public interface IAbstractDataExport
    {
        IWorkbook Export<T>
            (List<T> exportData, string sheetName);
    }
}