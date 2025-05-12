namespace TransportControl.Service
{
    public interface ITrackListDocumentationService
    {
        Task<string> GenerateTrackListDocumentationAsync(
            Guid trackListId,
            string templatePath,
            string outputExcelPath,
            string outputPdfPath);
    }
}
