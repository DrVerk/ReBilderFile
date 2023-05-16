using Aspose.Cells;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.IO;

namespace ReBilderFile
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void WayFileButton(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Multiselect = false;

			if (openFile.ShowDialog() == true)
				way_file.Text = openFile.FileName;
		}

		private void reBuildButton(object sender, RoutedEventArgs e)
		{

			if (way_file.Text == "")
				return;
			try
			{
				Workbook workbook = new Workbook(way_file.Text);
				Worksheet worksheet = workbook.Worksheets[0];
				worksheet.Cells.DeleteColumns(4, 4, false);

				workbook.Save(way_file.Text.Split('.').First() + ".csv", SaveFormat.Csv);

				reBuild_file.Text = "Фаил .csv создан начаты преобразования";
				string str;
				using (StreamReader sr = new StreamReader(way_file.Text.Split('.').First() + ".csv"))
				{
					str = sr.ReadToEnd().Replace("mm", "").Replace("Evaluation Only. Created with Aspose.Cells for .NET.Copyright 2003 - 2023 Aspose Pty Ltd.", "");
					sr.Close();
				}
				using (StreamWriter sw = new StreamWriter(way_file.Text.Split('.').First() + ".csv", false))
				{

					sw.Write(str);
					sw.Close();
				}
				reBuild_file.Text = "Фаил для станка завершён";
			}
			catch (Exception a)
			{
				reBuild_file.Text = "Что-то пошло не так!";
				Close();
				throw new WindowExeption(a.Message);
			}
		}
	}
	class WindowExeption : Exception
	{
		public WindowExeption(string messeng) : base() => MessageBox.Show(messeng, "Ошиба", MessageBoxButton.OK, MessageBoxImage.Error);
	}
}
