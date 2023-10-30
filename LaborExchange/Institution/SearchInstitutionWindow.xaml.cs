using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace Institution
{
    /// <summary>
    /// Логика взаимодействия для SearchInstitutionWindow.xaml
    /// </summary>
    public partial class SearchInstitutionWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public Applicant Applicant { get; set; }
        public SearchInstitutionWindow(LaborExchangeDbContext dbContext, Applicant applicant)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;

            Applicant = applicant;

            RefreshInstitutionGrid();
        }

        private void RefreshInstitutionGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            InstitutionGrid.ItemsSource = DbContext.Institutions
                .Where(x => (x.Title.ToLower().Contains(search)))
                .ToList();
            InstitutionGrid.Items.Refresh();
        }


        private void ChoiseInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            if (InstitutionGrid.SelectedItems.Count == 1)
            {

                Applicant.Institution = (DbContext.Models.Institution)InstitutionGrid.SelectedItems[0]!;
            }
            Close();
        }

        private void SearchInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshInstitutionGrid();
        }
    }
}
