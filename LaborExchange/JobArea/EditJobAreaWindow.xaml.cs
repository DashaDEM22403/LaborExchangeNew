using System.Windows;
using DbContext;
using Microsoft.EntityFrameworkCore;

namespace JobArea
{
    /// <summary>
    /// Логика взаимодействия для EditJobAreaWindow.xaml
    /// </summary>
    public partial class EditJobAreaWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.JobArea JobArea { get; set; }
        public bool IsNewJobArea { get; set; }
        public EditJobAreaWindow(LaborExchangeDbContext dbContext, DbContext.Models.JobArea jobArea, bool isNewJobArea)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            JobArea = jobArea;
            IsNewJobArea = isNewJobArea;
        }

        private void SaveJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(JobArea.Title))
            {
                MessageBox.Show("Укажите название сферы вакансии");
                return;
            }

            try
            {
                if (IsNewJobArea)
                {
                    DbContext.JobAreas.Add(JobArea);
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
