using System;

namespace PdfUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose your action...");
            Console.WriteLine("1. Merge (max 10) PDF files into one - Press 1");
            Console.WriteLine("2. Split one PDF file into multiple pages - Press 2");
            Console.WriteLine("3. Convert a TIFF image to PDF file - Press 3");
            Console.WriteLine("4. Set password for a PDF file - Press 4");
            Console.WriteLine("5. Remove password for a PDF file - Press 5");
            Console.WriteLine("6. Change password for a PDF file - Press 6");
            Console.WriteLine("7. Convert an image to a PDF file - Press 7");

            try
            {
                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int input))
                {
                    if (input >= 1 && input <= 7)
                    {
                        switch (input)
                        {
                            case 1:
                                Console.WriteLine("Enter directory path of files to merge");
                                string mergePath = Console.ReadLine();
                                PdfOperations.MergeDocuments(mergePath);
                                break;
                            case 2:
                                Console.WriteLine("Enter directory path of file to split");
                                string splitPath = Console.ReadLine();
                                PdfOperations.SplitDocuments(splitPath);
                                break;
                            case 3:
                                Console.WriteLine("Enter directory path of TIFF file to be converted to PDF");
                                string tiffPath = Console.ReadLine();
                                PdfOperations.ConvertTiffToPdf(tiffPath);
                                break;
                            case 4:
                                Console.WriteLine("Enter directory path of file to set password for");
                                string unprotectedFilePath = Console.ReadLine();
                                Console.WriteLine("Enter password to set");
                                string password = Console.ReadLine();
                                PdfOperations.SetDocumentPassword(unprotectedFilePath, password);
                                break;
                            case 5:
                                Console.WriteLine("Enter directory path of file to remove password of");
                                string protectedFilePath = Console.ReadLine();
                                Console.WriteLine("Enter password for document");
                                string existingPassword = Console.ReadLine();
                                PdfOperations.RemoveDocumentPassword(protectedFilePath, existingPassword);
                                break;
                            case 6:
                                Console.WriteLine("Enter directory path of file to change password of");
                                string filePath = Console.ReadLine();
                                Console.WriteLine("Enter current password for document");
                                string currentPassword = Console.ReadLine();
                                Console.WriteLine("Enter new password for document");
                                string newPassword = Console.ReadLine();
                                PdfOperations.ChangeDocumentPassword(filePath, currentPassword, newPassword);
                                break;
                            case 7:
                                Console.WriteLine("Enter directory path of image file to convert to PDF");
                                string imageFilePath = Console.ReadLine();
                                PdfOperations.ConvertImageToPdf(imageFilePath);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");                        
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");                    
                }
                Console.ReadLine();
            }
            catch (Syncfusion.Pdf.PdfException ex)
            {
                Console.WriteLine("Exception encountered: " + ex.Message);
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception encountered: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
