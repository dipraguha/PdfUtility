using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System;
using System.IO;
using System.Linq;

namespace PdfUtility
{
    public class PdfOperations
    {
        //Merge multiple (max 10) PDF files into a single PDF file
        public static void MergeDocuments(string path)
        {
            try
            {
                var filesToMerge = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));

                if (filesToMerge.Count() > 10)
                {
                    throw new PdfException("Too many files to merge. Limit to 10 at a time.");                    
                }

                Console.WriteLine("Merging files...");

                var mergedFile = new PdfDocument();
                PdfDocument.Merge(mergedFile, filesToMerge.ToArray());

                var mergedFilePath = path + @"\MergedFile.pdf";
                mergedFile.Save(mergedFilePath);
                mergedFile.Close(true);

                Console.WriteLine("File merge completed successfully");
                return;
            }
            catch
            {
                throw;
            }            
        }

        //Split a PDF file into multiple files by page
        public static void SplitDocuments(string inputFilePath)
        {
            try
            {
                Console.WriteLine("Splitting files...");
                var fileToSplit = new PdfLoadedDocument(inputFilePath);
                const string targetFileNamePattern = "SplitFile" + "{0}.pdf";

                fileToSplit.Split(targetFileNamePattern, 1);
                fileToSplit.Close(true);
                Console.WriteLine("Files split successfully");
            }
            catch
            {
                throw;
            }
        }

        //Convert a (multi-frame) TIFF image to a PDF file
        public static void ConvertTiffToPdf(string inputFilePath)
        {
            try
            {
                Console.WriteLine("Converting image to PDF file...");

                var pdfDocument = new PdfDocument();
                var section = pdfDocument.Sections.Add();

                PdfPage page;
                PdfGraphics graphics;

                var tiffImage = new PdfBitmap(inputFilePath);
                int frameCount = tiffImage.FrameCount;

                for (int i = 0; i < frameCount; i++)
                {
                    page = section.Pages.Add();
                    section.PageSettings.Margins.All = 0;
                    graphics = page.Graphics;
                    tiffImage.ActiveFrame = 1;
                    graphics.DrawImage(tiffImage, 0, 0, page.GetClientSize().Width, page.GetClientSize().Height);
                }

                var tiffFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                var targetFilePath = Path.GetDirectoryName(inputFilePath) + @"\" + tiffFileName + "-ConvertedFile.pdf";
                pdfDocument.Save(targetFilePath);
                pdfDocument.Close(true);

                Console.WriteLine("Image converted successfully to PDF file");
            }
            catch
            {
                throw;
            }
        }

        //Remove password from password protected PDF file
        public static void RemoveDocumentPassword(string inputFilePath, string password)
        {
            try
            {
                Console.WriteLine("Removing password...");

                var loadedDocument = new PdfLoadedDocument(inputFilePath, password);
                loadedDocument.Security.UserPassword = string.Empty;

                var inputFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                var targetFilePath = Path.GetDirectoryName(inputFilePath) + @"\" + inputFileName + "-WithoutPassword.pdf";

                loadedDocument.Save(targetFilePath);
                loadedDocument.Close(true);

                Console.WriteLine("Password removed and document saved");
            }
            catch (PdfDocumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Set password for a PDF document
        public static void SetDocumentPassword(string inputFilePath, string password)
        {
            try
            {
                Console.WriteLine("Setting password...");

                var loadedDocument = new PdfLoadedDocument(inputFilePath);
                var security = loadedDocument.Security;

                security.KeySize = Syncfusion.Pdf.Security.PdfEncryptionKeySize.Key256Bit;
                security.Algorithm = Syncfusion.Pdf.Security.PdfEncryptionAlgorithm.AES;

                security.UserPassword = password;

                var inputFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                var targetFilePath = Path.GetDirectoryName(inputFilePath) + @"\" + inputFileName + "-WithPassword.pdf";

                loadedDocument.Save(targetFilePath);
                loadedDocument.Close(true);

                Console.WriteLine("Password set and document saved");
            }
            catch
            {
                throw;
            }
        }

        //Change password for a PDF document
        public static void ChangeDocumentPassword(string inputFilePath, string existingPassword, string newPassword)
        {
            try
            {
                Console.WriteLine("Changing document password...");

                var loadedDocument = new PdfLoadedDocument(inputFilePath, existingPassword);

                loadedDocument.Security.UserPassword = newPassword;

                var inputFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                var targetFilePath = Path.GetDirectoryName(inputFilePath) + @"\" + inputFileName + "-WithNewPassword.pdf";

                loadedDocument.Save(targetFilePath);
                loadedDocument.Close(true);

                Console.WriteLine("Password changed and document saved");
            }
            catch (PdfDocumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Convert an image to a PDF file
        public static void ConvertImageToPdf(string inputImageFilePath)
        {
            try
            {
                Console.WriteLine("Converting image to a PDF file...");

                var document = new PdfDocument();
                var page = document.Pages.Add();
                var graphics = page.Graphics;

                var image = new PdfBitmap(inputImageFilePath);
                graphics.DrawImage(image, 0, 0);

                var inputFileName = Path.GetFileNameWithoutExtension(inputImageFilePath);
                var targetFilePath = Path.GetDirectoryName(inputImageFilePath) + @"\" + inputFileName + "-ConvertedImage.pdf";
                document.Save(targetFilePath);
                document.Close(true);

                Console.WriteLine("Image converted to PDF and saved");
            }
            catch
            {
                throw;
            }
        }
    }    
}
