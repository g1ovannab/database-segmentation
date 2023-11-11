using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataSegmentation
{
	class Program
	{

		public static List<ImageRepresentation> allImages;
		static void Main(string[] args)
		{
			allImages = new List<ImageRepresentation>();

			string slnPath = GetSlnPath(AppDomain.CurrentDomain.BaseDirectory);
			string zipFile = slnPath + "\\archive.zip";

			string destinationUnzippedDirectory = slnPath + "\\unzipped";
			string csvDirectory = destinationUnzippedDirectory + "\\csv";
			string pjegDirectory = destinationUnzippedDirectory + "\\jpeg";

			string destinationOfSegmentedImages = "C:\\Users\\Giovanna\\Documents\\SegmentedImages";
			string malignantImages = destinationOfSegmentedImages + "\\MALIGNANT";
			string benignImages = destinationOfSegmentedImages + "\\BENIGN";


			UnzipFile(zipFile, destinationUnzippedDirectory);

			GetImagesRepresentation(csvDirectory + "\\mass_case_description_test_set.csv");
			GetImagesRepresentation(csvDirectory + "\\mass_case_description_train_set.csv");

			Regex pathCroppedRegex = new Regex("Mass-(Test|Training)(?<first>[\\w]*)\\/(?<second>[\\d\\.]*)\\/(?<third>[\\d\\.]*)\\/(?<fourth>[\\w\\d]*)\\.dcm");

			foreach (ImageRepresentation image in allImages)
			{
				Match match = pathCroppedRegex.Match(image.path);

				if (match.Success)
				{
					string pathCropped = match.Groups["third"].Value;
					string croppedImageFilePath = pjegDirectory + "\\" + pathCropped;

					if (Directory.Exists(croppedImageFilePath))
					{
						string[] extensoesDeImagem = { ".jpg", ".jpeg" };

						// Obtém todos os arquivos de imagem na pasta com as extensões especificadas
						string[] files = Directory.GetFiles(croppedImageFilePath)
							.Where(file => extensoesDeImagem.Contains(Path.GetExtension(file).ToLower()))
							.ToArray();


						if (files.Length > 0)
						{
							string smallerImage = null;
							long smallestArea = long.MaxValue;

							foreach (string file in files)
							{
								using (var imagem = new Bitmap(file))
								{
									long area = imagem.Width * imagem.Height;

									if (area < smallestArea)
									{
										smallestArea = area;
										smallerImage = file;
									}
								}
							}

							string fileName = Path.GetFileName(smallerImage);
							string completeDestinationDirectory = destinationOfSegmentedImages + "\\" + image.pathology;

                            if(!Directory.Exists(completeDestinationDirectory)) Directory.CreateDirectory(completeDestinationDirectory);


							string completeDestinationFile = completeDestinationDirectory + "\\" + fileName;
							int counter = 1;
							string originalNameFile = fileName;


							while (File.Exists(completeDestinationFile))
							{
								fileName = Path.GetFileNameWithoutExtension(originalNameFile) + " (" + counter + ")" + Path.GetExtension(originalNameFile);
								completeDestinationFile = destinationOfSegmentedImages + "\\" + image.pathology + "\\" + fileName;
								counter++;
							}


							File.Copy(smallerImage, completeDestinationFile);
						}

					}

				}
			}
		}

		static string GetSlnPath(string directory)
		{
			while (directory != null)
			{
				string slnPath = Path.Combine(directory, "*.sln");
				string[] slnFiles = Directory.GetFiles(directory, "*.sln");

				if (slnFiles.Length > 0) return directory;

				directory = Directory.GetParent(directory)?.FullName;
			}

			return null;
		}


		public static void UnzipFile(string zipFile, string destinationPath)
		{
			try
			{
				if (Directory.Exists(destinationPath)) { Directory.Delete(destinationPath); }

				Directory.CreateDirectory(destinationPath);
				ZipFile.ExtractToDirectory(zipFile, destinationPath);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while unzipping file: {ex.Message}");
			}
		}


		public static void GetImagesRepresentation(string path)
		{
			string patient_id = "";
			string pathology = "";
			string cropped_image_file_path = "";

			try
			{
				using (TextFieldParser csvReader = new TextFieldParser(path))
				{
					csvReader.SetDelimiters(new string[] { "," });
					csvReader.HasFieldsEnclosedInQuotes = false;
					string[] colFields = csvReader.ReadFields();

					while (!csvReader.EndOfData)
					{
						string[] fieldData = csvReader.ReadFields();
						for (int i = 0; i < fieldData.Length; i++)
						{
							switch (colFields[i])
							{
								case "patient_id":
									patient_id = fieldData[i];
									break;
								case "pathology":
									pathology = fieldData[i];
									break;
								case "cropped image file path":
									cropped_image_file_path = fieldData[i];
									break;
								default:
									break;
							}

						}
						ImageRepresentation image = new ImageRepresentation(patient_id, pathology, cropped_image_file_path);
						allImages.Add(image);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
