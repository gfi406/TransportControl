﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using Spire.Xls;
using System.IO;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using TransportControl.Model.DTO;
using TransportControl.Service.Impl;

namespace TransportControl.Service.Documentation
{
    
    public class TrackListExcelGenerator
    {
        public void FillExcelTemplate(string templatePath, string outputPath, TrackListDto model)
        {
            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                // Заполнение основных данных
                worksheet.Cells["V10"].Value = model.Car?.CarName;
                worksheet.Cells["AI11"].Value = model.Car?.CarNumber;
                worksheet.Cells["BO14"].Value = model.Car?.CarCategory;
                worksheet.Cells["M12"].Value = model.Driver?.DriverName;
                worksheet.Cells["AD36"].Value = model.Driver?.DriverName;
                worksheet.Cells["AD45"].Value = model.Driver?.DriverName;
                worksheet.Cells["BP26"].Value = model.Driver?.DriverName;
                worksheet.Cells["BP12"].Value = model.Car?.PersonnelNumber;
                worksheet.Cells["S14"].Value = model.Driver?.PersonnelNumber;
                worksheet.Cells["AE30"].Value = model.StartTime.ToString("HH:mm");
                worksheet.Cells["AG35"].Value = model.EndTime?.ToString("HH:mm");

                worksheet.Cells["BU19"].Value = model.OdometrStart;
                worksheet.Cells["BT45"].Value = model.OdometrEnd;

                // Заполнение данных о горючем
                worksheet.Cells["BF29"].Value = model.Car?.CarFuelType;
                worksheet.Cells["BT37"].Value = model.RemainingFuelStart;
                worksheet.Cells["BT38"].Value = model.RemainingFuelEnd;
                worksheet.Cells["BT39"].Value = model.Car?.CarFuelUsing;

                worksheet.Cells["Q20"].Value = "Московской";
                worksheet.Cells["A22"].Value = "дистанции СЦБ Окт.Д.И.";
                worksheet.Cells["R8"].Value = "ОАО 'РЖД' г.Москва ул. Новая Басманная, д.2 ";


                var routeWorksheet = package.Workbook.Worksheets[1];
                int row = 5;

                foreach (var point in model.TrackPoints ?? Enumerable.Empty<TrackPointDto>())
                {
                    routeWorksheet.Cells[row, 2].Value = point.NumberPoint;
                    routeWorksheet.Cells[row, 3].Value = point.CustomerCode;
                    routeWorksheet.Cells[row, 5].Value = point.StartPointName;
                    routeWorksheet.Cells[row, 8].Value = point.EndPointName;
                    routeWorksheet.Cells[row, 10].Value = point.StartPointTime?.ToString("HH");
                    routeWorksheet.Cells[row, 12].Value = point.StartPointTime?.ToString("mm");
                    routeWorksheet.Cells[row, 13].Value = point.EndPointTime?.ToString("HH");
                    routeWorksheet.Cells[row, 14].Value = point.EndPointTime?.ToString("mm");
                    routeWorksheet.Cells[row, 15].Value = point.DistanceTraveled;

                    row++;
                    if (row > 25) break;
                }

                package.SaveAs(new FileInfo(outputPath));
            }
        }
    }


    public static class ExcelToPdfConverter
    {
        public static void ConvertExcelToPdf(string excelPath, string pdfPath)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(excelPath);

            foreach (Worksheet sheet in workbook.Worksheets)
            {
                sheet.PageSetup.PaperSize = PaperSizeType.PaperA4;
                sheet.PageSetup.Orientation = PageOrientationType.Landscape;

                sheet.PageSetup.FitToPagesWide = 1;
                sheet.PageSetup.FitToPagesTall = 1;
            }

            workbook.SaveToFile(pdfPath, Spire.Xls.FileFormat.PDF);
        }
    }
   

    public class TrackListDocumentation
        {
            private readonly ITrackListService _trackListService;
            private readonly TrackListExcelGenerator _excelGenerator;

            public TrackListDocumentation(
                ITrackListService trackListService,
                TrackListExcelGenerator excelGenerator)
            {
                _trackListService = trackListService;
                _excelGenerator = excelGenerator;
            }

            public async Task GenerateTrackListDocumentationAsync(
                Guid trackListId,
                string templatePath,
                string outputExcelPath,
                string outputPdfPath)
            {
                var trackListDto = await _trackListService.GetTrackListByIdAsync(trackListId);

                if (trackListDto == null)
                {
                    throw new InvalidOperationException($"Путевой лист с ID {trackListId} не найден.");
                }

                _excelGenerator.FillExcelTemplate(templatePath, outputExcelPath, trackListDto);

                ExcelToPdfConverter.ConvertExcelToPdf(outputExcelPath, outputPdfPath);
            }
        }

        public class TrackListDocumentationGenerator
        {
            private readonly IServiceProvider _serviceProvider;

            public TrackListDocumentationGenerator(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public async Task GenerateDocumentationFromJsonAsync(
                string jsonFilePath,
                string templatePath,
                string outputExcelPath,
                string outputPdfPath)
            {
                var json = await File.ReadAllTextAsync(jsonFilePath, Encoding.UTF8);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var model = JsonSerializer.Deserialize<CreateTrackListDto>(json, options);

                if (model == null)
                {
                    throw new InvalidOperationException("Не удалось десериализовать путевой лист из JSON");
                }

                using var scope = _serviceProvider.CreateScope();
                var trackListService = scope.ServiceProvider.GetRequiredService<TrackListService>();
                var documentationService = scope.ServiceProvider.GetRequiredService<TrackListDocumentation>();

                var createdTrackList = await trackListService.CreateTrackListAsync(model);

                await documentationService.GenerateTrackListDocumentationAsync(
                    createdTrackList.Id,
                    templatePath,
                    outputExcelPath,
                    outputPdfPath
                );
            }

            public async Task GenerateDocumentationForExistingTrackListAsync(
                Guid trackListId,
                string templatePath,
                string outputExcelPath,
                string outputPdfPath)
            {
                using var scope = _serviceProvider.CreateScope();
                var documentationService = scope.ServiceProvider.GetRequiredService<TrackListDocumentation>();

                await documentationService.GenerateTrackListDocumentationAsync(
                    trackListId,
                    templatePath,
                    outputExcelPath,
                    outputPdfPath
                );
            }
        }
    
}
