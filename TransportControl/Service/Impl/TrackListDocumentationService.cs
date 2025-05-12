using System;
using System.IO;
using System.Threading.Tasks;
using TransportControl.Service;
using TransportControl.Service.Documentation;

namespace TransportControl.Service.Impl
{
    public class TrackListDocumentationService : ITrackListDocumentationService
    {
        private readonly Documentation.TrackListDocumentation _documentationService;

        public TrackListDocumentationService(Documentation.TrackListDocumentation documentationService)
        {
            _documentationService = documentationService;
        }

        public async Task<string> GenerateTrackListDocumentationAsync(
            Guid trackListId,
            string templatePath,
            string outputExcelPath,
            string outputPdfPath)
        {
            if (!File.Exists(templatePath))
                throw new FileNotFoundException("Шаблон Excel не найден", templatePath);

            Directory.CreateDirectory(Path.GetDirectoryName(outputExcelPath));
            Directory.CreateDirectory(Path.GetDirectoryName(outputPdfPath));

            await _documentationService.GenerateTrackListDocumentationAsync(
                trackListId,
                templatePath,
                outputExcelPath,
                outputPdfPath
            );

            return outputPdfPath;
        }
    }
}
