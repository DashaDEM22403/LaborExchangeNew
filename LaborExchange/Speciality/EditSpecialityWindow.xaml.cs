using System.Windows;
using DbContext;
using Microsoft.EntityFrameworkCore;

namespace Speciality
{
    /// <summary>
    /// Логика взаимодействия для EditSpecialityWindow.xaml
    /// </summary>
    public partial class EditSpecialityWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.Speciality Speciality { get; set; }
        public bool IsNewSpeciality { get; set; }
        public EditSpecialityWindow(LaborExchangeDbContext dbContext, DbContext.Models.Speciality speciality, bool isNewSpeciality)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Speciality = speciality;
            IsNewSpeciality = isNewSpeciality;
        }

        private void SaveSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Speciality.Title))
            {
                MessageBox.Show("Укажите название спеиальности");
                return;
            }
            if (string.IsNullOrEmpty(Speciality.Code))
            {
                MessageBox.Show("Укажите код спеиальности");
                return;
            }

            try
            {
                if (IsNewSpeciality)
                {
                    DbContext.Specialities.Add(Speciality);
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
