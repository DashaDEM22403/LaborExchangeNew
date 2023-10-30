using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace JobTitle
{
    /// <summary>
    /// Логика взаимодействия для SearchJobTitleWindow.xaml
    /// </summary>
    public partial class SearchJobTitleWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public JobVacancy JobVacancy { get; set; }
        public SearchJobTitleWindow(LaborExchangeDbContext dbContext, JobVacancy jobVacancy)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;

            JobVacancy = jobVacancy;

            RefreshJobTitleGrid();
        }
        private void RefreshJobTitleGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            JobTitleGrid.ItemsSource = DbContext.JobTitles
                .Where(x => (x.Title.ToLower().Contains(search)))
                .ToList();
            JobTitleGrid.Items.Refresh();
        }

        private void SearchJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshJobTitleGrid();
        }

        private void ChoiseJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobTitleGrid.SelectedItems.Count == 1)
            {

                JobVacancy.JobTitle = (DbContext.Models.JobTitle)JobTitleGrid.SelectedItems[0]!;
            }
            Close();
        }
    }
}
