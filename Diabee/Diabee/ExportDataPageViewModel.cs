using iTextSharp.text;
using iTextSharp.text.pdf;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Color = iTextSharp.text.Color;
using Element = iTextSharp.text.Element;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;

namespace Diabee
{
    public class ExportDataPageViewModel : INotifyPropertyChanged
    {
        BaseFont bfComic = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.EMBEDDED);
        public event PropertyChangedEventHandler PropertyChanged;

        public ExportDataPageViewModel()
        {
            ExportFileCommand = new Command(
            execute: () =>
            {
                ExportFile();
            });
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string filePath = DependencyService.Get<IOSDependency>().GetPlatformSpecificFileSaveDescription();
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged(nameof(filePath));
            }
        }

        public ICommand ExportFileCommand { private set; get; }

        public async void ExportFile()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    await Application.Current.MainPage.DisplayAlert("Need storage", "Request storage permission", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Storage))
                    status = results[Permission.Storage];
            }

            if (status == PermissionStatus.Granted)
            {
                try
                {
                    string documentsPath = DependencyService.Get<IOSDependency>().GetPath();
                    string localFilename = string.Format("{0}.pdf", DateTime.Now.ToLongDateString());
                    string localPath = Path.Combine(documentsPath, localFilename);
                    //File.WriteAllBytes(localPath, bytes);
                    FileStream fs = new FileStream(localPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                    // Create an instance of the document class which represents the PDF document itself.
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                    // Create an instance to the PDF file by creating an instance of the PDF Writer class, using the document and the filestrem in the constructor.
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Open the document to enable you to write to the document
                    document.Open();
                    PdfContentByte cb = writer.DirectContent;

                    // Set text font and size
                    cb.BeginText();
                    cb.SetFontAndSize(bfComic, 9);

                    // Create header of doc
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    Font times = new Font(bfTimes, 38, Font.BOLDITALIC, Color.MAGENTA);
                    Paragraph paragraph = new Paragraph("Diabee", times);
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.SpacingAfter = 10f;

                    // Add header to doc
                    document.Add(paragraph);

                    paragraph = new Paragraph("The simplest glucose and weight monitoring app",
                        new Font(Font.HELVETICA, 24f, Font.BOLD));
                    paragraph.IndentationLeft = 20f;
                    paragraph.IndentationRight = 20f;
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.SpacingAfter = 18f;
                    document.Add(paragraph);

                    // Add the app image on the top left
                    Stream s = null;
                    s = DependencyService.Get<IOSDependency>().GetStream();

                    Image img = Image.GetInstance(s);
                    img.SetAbsolutePosition(20, 700);
                    img.ScalePercent(75);
                    img.Alignment = Image.TEXTWRAP | Image.ALIGN_RIGHT;
                    cb.AddImage(img);

                    // Retrieve glucose data first
                    List<GlucoseHistoryDataModel> glucoseData = Constants.ReadGlucoseData(Constants.GlucoseFileName).ToList();
                    PdfPTable glucoseTable = new PdfPTable(3);
                    PdfPCell glucoseHeaderCell = new PdfPCell(new Phrase("Glucose Readings",
                        new Font(Font.HELVETICA, 10f, Font.BOLD)));
                    glucoseHeaderCell.Colspan = 3;
                    glucoseHeaderCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    glucoseHeaderCell.Padding = 5f;
                    glucoseTable.AddCell(glucoseHeaderCell);

                    // Setup proper header labels
                    glucoseTable.AddCell("Date");
                    glucoseTable.AddCell("Time");
                    glucoseTable.AddCell("Glucose Value (mmol/l)");

                    foreach (var glucoseEntry in glucoseData)
                    {
                        glucoseTable.AddCell(glucoseEntry.HistoryDate);
                        glucoseTable.AddCell(glucoseEntry.HistoryTime.ToString());
                        glucoseTable.AddCell(glucoseEntry.GlucoseValue);
                    }

                    glucoseTable.SpacingBefore = 20f;
                    glucoseTable.SpacingAfter = 30f;

                    // Add glucose table to document
                    document.Add(glucoseTable);

                    // Then retrieve weight data
                    List<WeightHistoryDataModel> weightData = Constants.ReadWeightData(Constants.WeightFileName).ToList();
                    PdfPTable weightTable = new PdfPTable(3);
                    PdfPCell weightHeaderCell = new PdfPCell(new Phrase("Weight Readings",
                        new Font(Font.HELVETICA, 10f, Font.BOLD)));
                    weightHeaderCell.Colspan = 3;
                    weightHeaderCell.Padding = 5f;
                    weightHeaderCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    weightTable.AddCell(weightHeaderCell);

                    // Setup proper header labels
                    weightTable.AddCell("Date");
                    weightTable.AddCell("Time");
                    weightTable.AddCell("Weight Value (Kg)");

                    foreach (var weightEntry in weightData)
                    {
                        weightTable.AddCell(weightEntry.HistoryDate);
                        weightTable.AddCell(weightEntry.HistoryTime.ToString());
                        weightTable.AddCell(weightEntry.WeightValue);
                    }

                    weightTable.SpacingBefore = 20f;
                    weightTable.SpacingAfter = 30f;

                    // End text
                    cb.EndText();

                    // Add weight table to document
                    document.Add(weightTable);

                    // Close the document
                    s.Close();
                    document.Close();

                    // Close the writer instance
                    writer.Close();
                    // Always close open file handles explicitly
                    fs.Close();
                    DependencyService.Get<IMessage>().ShortAlert("Exported!");
                }
                catch
                {

                }
            }
        }
    }
}
