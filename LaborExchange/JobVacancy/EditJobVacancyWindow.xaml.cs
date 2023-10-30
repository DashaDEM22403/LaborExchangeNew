//using DocumentFormat.OpenXml.InkML;
//using DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using DbContext;
using DbContext.Models;
using Employer.EmployerWindow;
using JobArea;
using JobTitle;
using Microsoft.EntityFrameworkCore;

namespace JobVacancy
{
    /// <summary>
    /// Логика взаимодействия для EditJobVacancyWindow.xaml
    /// </summary>
    public partial class EditJobVacancyWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.JobVacancy JobVacancy { get; set; }
        public bool IsNewJobVacancy { get; set; }
        public List<JobType> JobTypes { get; set; }
        public EditJobVacancyWindow(LaborExchangeDbContext dbContext, DbContext.Models.JobVacancy jobVacancy, bool isNewJobVacancy)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            JobVacancy = jobVacancy;
            IsNewJobVacancy = isNewJobVacancy;
            JobTypes = DbContext.JobTypes.ToList();
            RefreshEmployerGrid();
        }

        private void RefreshEmployerGrid()
        {

            List<DbContext.Models.Employer> newEmployer = new List<DbContext.Models.Employer>();
            if (JobVacancy.Employer != null)
            {
                newEmployer.Add(JobVacancy.Employer);
            }

            EmployerGrid.ItemsSource = newEmployer.ToList();
            EmployerGrid.Items.Refresh();
        }

        private void SaveJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobVacancy.JobType == null)
            {
                MessageBox.Show("Выберите тип вакансии");
                return;
            }
            if (string.IsNullOrEmpty(JobVacancy.Patch))
            {
                MessageBox.Show("Укажите зарплату");
                return;
            }
            Regex regexPatch = new Regex(@"^(?:\d+\s(?:руб\.|дол\.|евр\.))$");
            if (!regexPatch.IsMatch(JobVacancy.Patch))
            {
                MessageBox.Show("Укажите зарплату в формате:\r\n - 100000 руб.\r\n- 200 дол.\r\n- 5000000 евр.");
                return;
            }
            //if (!int.TryParse(JobVacancy.Patch, out _))
            //{
            //    MessageBox.Show("Зарплата не является числом");
            //    return;
            //}
            if (JobVacancy.JobArea == null)
            {
                MessageBox.Show("Выберите сферу вакансии");
                return;
            }
            if (JobVacancy.JobTitle == null)
            {
                MessageBox.Show("Выберите должность");
                return;
            }
            if (JobVacancy.Employer == null)
            {
                MessageBox.Show("Выберите работодателя");
                return;
            }
            try
            {
                if (IsNewJobVacancy)
                {
                    DbContext.JobVacancies.Add(JobVacancy);
                }
                DbContext.SaveChanges();
                Close();
            }
            catch (DbUpdateException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SearchEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            var searchEmployerWindow = new SearchEmployerWindow(DbContext, JobVacancy);
            searchEmployerWindow.ShowDialog();
            RefreshEmployerGrid();
        }

        private void SearchJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            var searchJobAreaWindow = new SearchJobAreaWindow(DbContext, JobVacancy);
            searchJobAreaWindow.ShowDialog();
            if (JobVacancy.JobArea is not null)
            {
                JobAreaTextBox.Text = JobVacancy.JobArea.Title;
            }
        }

        private void SearchJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var searchJobTitleWindow = new SearchJobTitleWindow(DbContext, JobVacancy);
            searchJobTitleWindow.ShowDialog();
            if (JobVacancy.JobTitle is not null)
            {
                JobAreaTextBox.Text = JobVacancy.JobArea.Title;
            }
        }
    }
}
