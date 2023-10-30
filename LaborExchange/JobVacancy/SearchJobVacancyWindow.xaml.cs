using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace JobVacancy
{
    /// <summary>
    /// Логика взаимодействия для SearchJobVacancyWindow.xaml
    /// </summary>
    public partial class SearchJobVacancyWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public Deal Deal { get; set; }
        public SearchJobVacancyWindow(LaborExchangeDbContext dbContext, Deal deal)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Deal = deal;

            RefreshJobVacancyGrid();
        }

        private void RefreshJobVacancyGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            JobVacancyGrid.ItemsSource = DbContext.JobVacancies
                .Where(x => (x.JobArea.Title.ToLower().Contains(search) || x.JobType.Type.ToLower().Contains(search) || x.JobTitle.Title.ToLower().Contains(search) || x.Employer.CompanyName.ToLower().Contains(search) || x.Patch.ToLower().Contains(search)))
                .ToList();
            JobVacancyGrid.Items.Refresh();
        }

        private void SearchJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {

            RefreshJobVacancyGrid();
        }

        private void ChoiseJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobVacancyGrid.SelectedItems.Count == 1)
            {

                Deal.JobVacancy = (DbContext.Models.JobVacancy)JobVacancyGrid.SelectedItems[0]!;
            }
            Close();
        }
    }
}
