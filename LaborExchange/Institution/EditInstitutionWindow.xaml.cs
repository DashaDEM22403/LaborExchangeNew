using System.Windows;
using DbContext;
using Microsoft.EntityFrameworkCore;

namespace Institution
{
    /// <summary>
    /// Логика взаимодействия для EditInstitutionWindow.xaml
    /// </summary>
    public partial class EditInstitutionWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.Institution Institution { get; set; }
        public bool IsNewInstitution { get; set; }
        public EditInstitutionWindow(LaborExchangeDbContext dbContext, DbContext.Models.Institution institution, bool isNewInstitution)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Institution = institution;
            IsNewInstitution = isNewInstitution;
        }

        private void SaveInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Institution.Title))
            {
                MessageBox.Show("Укажите название учебного заведения");
                return;
            }

            try
            {
                if (IsNewInstitution)
                {
                    DbContext.Institutions.Add(Institution);
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
