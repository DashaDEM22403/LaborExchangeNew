using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClosedXML.Excel;
using DbContext;
using DbContext.Models;
using Novacode;
using Border = Novacode.Border;


namespace Document.Windows;

public partial class ExportFirstDocumentWindow : Window
{
    public DataGrid DocumentGrid { get; set; }

    public ExportFirstDocumentWindow(LaborExchangeDbContext dbContext, DataGrid documentGrid)
    {
        InitializeComponent();
        DocumentGrid = documentGrid;
    }

    private bool ValidateNameFile()
    {
        if (string.IsNullOrEmpty(NameFileTextBox.Text.Trim()))
        {
            return true;
        }

        return false;
    }

    private void ExportPassengerToExcelButton_Click(object sender, RoutedEventArgs e)
    {
        if (ValidateNameFile())
        {
            MessageBox.Show("Введите название файла");
            return;
        }

        var myExportList = GetDataToExport();

        if (!myExportList.Any())
        {
            MessageBox.Show("Нет данных для экспорта");
            return;
        }

        string xlsxFilePath = $"../../../../../{NameFileTextBox.Text}.xlsx";
        string absolutePath = Path.GetFullPath(xlsxFilePath);
        ExportToExcel(myExportList, xlsxFilePath);
        OpenDir(xlsxFilePath);
        MessageBox.Show(
            $"Файл Excel сохранен по пути:\n{absolutePath}");
    }

    private void ExportToExcel(List<ExportJobVacancy> myExportList, string xlsxFilePath)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Data");

            var headers = new[]
                { "ID", "Тип вакансии", "Должность", "Сфера", "Компания", "Зарплата" };
            for (int col = 0; col < headers.Length; col++)
            {
                ws.Cell(1, col + 1).Value = headers[col];
            }

            for (int row = 0; row < myExportList.Count; row++)
            {
                var passenger = myExportList[row];

                ws.Cell(row + 2, 1).Value = passenger.Id;
                ws.Cell(row + 2, 2).Value = passenger.JobType;
                ws.Cell(row + 2, 3).Value = passenger.JobTitle;
                ws.Cell(row + 2, 4).Value = passenger.JobArea;
                ws.Cell(row + 2, 5).Value = passenger.Employer;
                ws.Cell(row + 2, 6).Value = passenger.Patch;
            }

            wb.SaveAs(xlsxFilePath);
        }
    }

    private List<ExportJobVacancy> GetDataToExport()
    {
        var l = new List<JobVacancy>(
            (IEnumerable<JobVacancy>)DocumentGrid.ItemsSource);

        var myExportList = new List<ExportJobVacancy>();

        foreach (var item in l)
        {
            myExportList.Add(new ExportJobVacancy()
            {
                Id = item.Id.ToString(),
                JobType = item.JobType.Type,
                JobTitle = item.JobTitle.Title,
                JobArea = item.JobArea.Title,
                Employer = item.Employer.CompanyName,
                Patch = item.Patch,
            });
        }

        return myExportList;
    }

    private void ExportPassengerToWordButton_Click(object sender, RoutedEventArgs e)
    {
        if (ValidateNameFile())
        {
            MessageBox.Show("Введите название файла");
            return;
        }

        var myExportList = GetDataToExport();

        if (!myExportList.Any())
        {
            MessageBox.Show("Нет данных для экспорта");
            return;
        }

        string docxFilePath = $"../../../../../{NameFileTextBox.Text}.docx";
        string absolutePath = Path.GetFullPath(docxFilePath);
        ExportToWord(myExportList, docxFilePath);
        OpenDir(docxFilePath);
        MessageBox.Show(
            $"Успешно. Файл Word сохранен по пути:\n{absolutePath}\n");
    }

    private void ExportToWord(List<ExportJobVacancy> myExportList, string docxFilePath)
    {
        using (DocX doc = DocX.Create(docxFilePath))
        {
            int rowCount = myExportList.Count + 1;
            int columnCount = 6;

            Table table = doc.AddTable(rowCount, columnCount);
            table.Design = TableDesign.None;

            foreach (var row in table.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.SetBorder((TableCellBorderType)TableBorderType.Bottom,
                        new Border(BorderStyle.Tcbs_single, BorderSize.one, 1, Color.Black));
                    cell.SetBorder((TableCellBorderType)TableBorderType.Top,
                        new Border(BorderStyle.Tcbs_single, BorderSize.one, 1, Color.Black));
                    cell.SetBorder((TableCellBorderType)TableBorderType.Left,
                        new Border(BorderStyle.Tcbs_single, BorderSize.one, 1, Color.Black));
                    cell.SetBorder((TableCellBorderType)TableBorderType.Right,
                        new Border(BorderStyle.Tcbs_single, BorderSize.one, 1, Color.Black));
                }
            }

            table.Rows[0].Cells[0].Paragraphs[0].Append("ID");
            table.Rows[0].Cells[1].Paragraphs[0].Append("Тип вакансии");
            table.Rows[0].Cells[2].Paragraphs[0].Append("Должность");
            table.Rows[0].Cells[3].Paragraphs[0].Append("Сфера");
            table.Rows[0].Cells[4].Paragraphs[0].Append("Компания");
            table.Rows[0].Cells[5].Paragraphs[0].Append("Зарплата");

            for (int row = 1; row < rowCount; row++)
            {
                ExportJobVacancy passenger = myExportList[row - 1];

                table.Rows[row].Cells[0].Paragraphs.First().Append(passenger.Id);
                table.Rows[row].Cells[1].Paragraphs.First().Append(passenger.JobType);
                table.Rows[row].Cells[2].Paragraphs.First().Append(passenger.JobTitle);
                table.Rows[row].Cells[3].Paragraphs.First().Append(passenger.JobArea);
                table.Rows[row].Cells[4].Paragraphs.First().Append(passenger.Employer);
                table.Rows[row].Cells[5].Paragraphs.First().Append(passenger.Patch);
            }

            doc.InsertTable(table);
            doc.Save();
        }
    }

    public class ExportJobVacancy
    {
        public string Id { get; set; }
        public string JobType { get; set; }
        public string JobTitle { get; set; }
        public string JobArea { get; set; }
        public string Employer { get; set; }
        public string Patch { get; set; }
    }

    private void OpenDir(string absolutePath)
    {
        string directoryPath = Path.GetDirectoryName(absolutePath);

        try
        {
            Process.Start("explorer.exe", directoryPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при открытии проводника: " + ex.Message);
        }

        NameFileTextBox.Clear();
    }
}