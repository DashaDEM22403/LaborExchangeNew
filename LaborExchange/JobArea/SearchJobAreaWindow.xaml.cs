using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace JobArea
{
    /// <summary>
    /// Логика взаимодействия для SearchJobAreaWindow.xaml
    /// </summary>
    public partial class SearchJobAreaWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public JobVacancy JobVacancy { get; set; }
        public SearchJobAreaWindow(LaborExchangeDbContext dbContext, JobVacancy jobVacancy)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;

            JobVacancy = jobVacancy;

            RefreshJobAreaGrid();
        }

        private void RefreshJobAreaGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            JobAreaGrid.ItemsSource = DbContext.JobAreas
                .Where(x => (x.Title.ToLower().Contains(search)))
                .ToList();
            JobAreaGrid.Items.Refresh();
        }
        private void ChoiseJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobAreaGrid.SelectedItems.Count == 1)
            {

                JobVacancy.JobArea = (DbContext.Models.JobArea)JobAreaGrid.SelectedItems[0]!;
            }
            Close();
        }

        private void SearchJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshJobAreaGrid();
        }
    }
}
