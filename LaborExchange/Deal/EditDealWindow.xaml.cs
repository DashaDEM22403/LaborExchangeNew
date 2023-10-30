//using DocumentFormat.OpenXml.InkML;

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Applicant;
using DbContext;
using JobVacancy;
using Microsoft.EntityFrameworkCore;

namespace Deal
{
    /// <summary>
    /// Логика взаимодействия для EditDealWindow.xaml
    /// </summary>
    public partial class EditDealWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.Deal Deal { get; set; }
        public bool IsNewDeal { get; set; }
        public EditDealWindow(LaborExchangeDbContext dbContext, DbContext.Models.Deal deal, bool isNewDeal)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Deal = deal;
            IsNewDeal = isNewDeal;

            RefreshApplicantGrid();
            RefreshJobVacancyGrid();
        }

        private void RefreshJobVacancyGrid()
        {
            List<DbContext.Models.JobVacancy> newJobVacancy = new List<DbContext.Models.JobVacancy>();
            if (Deal.JobVacancy != null)
            {
                newJobVacancy.Add(Deal.JobVacancy);
            }

            JobVacancyGrid.ItemsSource = newJobVacancy.ToList();
            JobVacancyGrid.Items.Refresh();
        }
        private void RefreshApplicantGrid()
        {
            List<DbContext.Models.Applicant> newApplicant = new List<DbContext.Models.Applicant>();
            if (Deal.Applicant != null)
            {
                newApplicant.Add(Deal.Applicant);
            }

            ApplicantGrid.ItemsSource = newApplicant.ToList();
            ApplicantGrid.Items.Refresh();
        }

        private void SearchJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            var searchJobVacancyWindow = new SearchJobVacancyWindow(DbContext, Deal);
            searchJobVacancyWindow.ShowDialog();
            RefreshJobVacancyGrid();
        }

        private void SearchApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            var searchApplicantWindow = new SearchApplicantWindow(DbContext, Deal);
            searchApplicantWindow.ShowDialog();
            RefreshApplicantGrid();
        }

        private void SaveDealButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Deal.Сontractor))
            {
                MessageBox.Show("Укажите подрядчика");
                return;
            }
            if (Deal.JobVacancy == null)
            {
                MessageBox.Show("Выберите вакансию");
                return;
            }
            if (Deal.Applicant == null)
            {
                MessageBox.Show("Выберите соискателя");
                return;
            }
            try
            {
                if (IsNewDeal)
                {
                    DbContext.Deals.Add(Deal);
                }
                DbContext.SaveChanges();
                Close();
            }
            catch (DbUpdateException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
