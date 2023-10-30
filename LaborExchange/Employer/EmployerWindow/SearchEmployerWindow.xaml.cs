using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace Employer.EmployerWindow
{
    /// <summary>
    /// Логика взаимодействия для SearchEmployerWindow.xaml
    /// </summary>
    public partial class SearchEmployerWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public JobVacancy JobVacancy { get; set; }
        public SearchEmployerWindow(LaborExchangeDbContext dbContext, JobVacancy jobVacancy)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;

            JobVacancy = jobVacancy;

            RefreshEmployerGrid();
        }

        private void RefreshEmployerGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            EmployerGrid.ItemsSource = DbContext.Employers
                .Where(x => (x.Address.ToLower().Contains(search) || x.CompanyName.ToLower().Contains(search) || x.Phone.ToLower().Contains(search)))
                .ToList();
            EmployerGrid.Items.Refresh();
        }

        private void SearchEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshEmployerGrid();
        }

        private void ChoiseEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployerGrid.SelectedItems.Count == 1)
            {

                JobVacancy.Employer = (DbContext.Models.Employer)EmployerGrid.SelectedItems[0]!;
            }
            Close();
        }
    }
}
