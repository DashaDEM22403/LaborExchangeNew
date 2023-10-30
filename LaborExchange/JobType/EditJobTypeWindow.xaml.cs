using System.Windows;
using DbContext;
using Microsoft.EntityFrameworkCore;

namespace JobType
{
    /// <summary>
    /// Логика взаимодействия для EditJobTypeWindow.xaml
    /// </summary>
    public partial class EditJobTypeWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.JobType JobType { get; set; }
        public bool IsNewJobType { get; set; }
        public EditJobTypeWindow(LaborExchangeDbContext dbContext, DbContext.Models.JobType jobType, bool isNewJobType)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            JobType = jobType;
            IsNewJobType = isNewJobType;
        }

        private void SaveJobTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(JobType.Type))
            {
                MessageBox.Show("Укажите название типа вакансии");
                return;
            }

            try
            {
                if (IsNewJobType)
                {
                    DbContext.JobTypes.Add(JobType);
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
