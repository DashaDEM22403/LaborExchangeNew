using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Models;

namespace Speciality
{
    /// <summary>
    /// Логика взаимодействия для SearchSpecialityWindow.xaml
    /// </summary>
    public partial class SearchSpecialityWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public Applicant Applicant { get; set; }
        public SearchSpecialityWindow(LaborExchangeDbContext dbContext, Applicant applicant)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Applicant = applicant;

            RefreshSpecialityGrid();
        }

        private void RefreshSpecialityGrid()
        {
            var search = SearchTextBox.Text.ToLower();
            SpecialityGrid.ItemsSource = DbContext.Specialities
                .Where(x => (x.Title.ToLower().Contains(search) || x.Code.ToLower().Contains(search)))
                .ToList();
            SpecialityGrid.Items.Refresh();
        }

        private void ChoiseSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialityGrid.SelectedItems.Count == 1)
            {

                Applicant.Speciality = (DbContext.Models.Speciality)SpecialityGrid.SelectedItems[0]!;
            }
            Close();
        }

        private void SearchSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshSpecialityGrid();
        }
    }
}
