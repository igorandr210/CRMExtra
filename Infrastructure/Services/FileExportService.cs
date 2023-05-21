using System;
using System.Collections.Generic;
using System.Globalization;
using Application.Documents.DTOs;
using Application.Interfaces;
using ClosedXML.Excel;
using System.IO;
using System.Linq;

namespace Infrastructure.Services
{
    public class FileExportService<T> : IFileExportService<T>
    {
        public DownloadFileDto Export(T entity, string fileName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add();

            var properties = entity.GetType().GetProperties();

            worksheet.Cell(1, 1).InsertData(properties.Select(x => x.Name), false);
            worksheet.Cell(1, 2).InsertData(properties.Select(x => x.GetValue(entity)), false);

            worksheet.Column(1).Style.Font.Bold = true;
            worksheet.Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            worksheet.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Flush();
            stream.Position = 0;

            return new DownloadFileDto { Stream = stream, Name = fileName, ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
        }

        public DownloadFileDto Export(IEnumerable<T> entities, string fileName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add();

            var properties = entities.GetType().GetGenericArguments().FirstOrDefault()?.GetProperties();

            worksheet.Cell(1, 1).InsertData(properties?.Select(x => x.Name), true);

            var currentRow = 2;
            foreach (var entity in entities)
            {
                worksheet.Cell(currentRow, 1).InsertData(properties?.Select(x => x.GetValue(entity)), true);
                currentRow++;
            }

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            worksheet.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Flush();
            stream.Position = 0;

            return new DownloadFileDto { Stream = stream, Name = fileName, ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
        }
    }
}
