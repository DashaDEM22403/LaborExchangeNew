using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace Applicant
{
    /// <summary>
    /// Логика взаимодействия для SearchApplicantWindow.xaml
    /// </summary>
    public partial class SearchApplicantWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public Deal Deal { get; set; }
        public SearchApplicantWindow(LaborExchangeDbContext dbContext, Deal deal)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Deal = deal;

            RefreshApplicantGrid();
        }

        private void RefreshApplicantGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            ApplicantGrid.ItemsSource = DbContext.Applicants
                .Where(x => (x.FirstName.ToLower().Contains(search) || x.LastName.ToLower().Contains(search) || x.Patronymic.ToLower().Contains(search) || x.Institution.Title.ToLower().Contains(search) || x.Speciality.Title.ToLower().Contains(search)))
                .ToList();
            ApplicantGrid.Items.Refresh();
        }


        private void ChoiseApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicantGrid.SelectedItems.Count == 1)
            {

                Deal.Applicant = (DbContext.Models.Applicant)ApplicantGrid.SelectedItems[0]!;
            }
            Close();
        }

        private void SearchApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshApplicantGrid();
        }
    }
}
