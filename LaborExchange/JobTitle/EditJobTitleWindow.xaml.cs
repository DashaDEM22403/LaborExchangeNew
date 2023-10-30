using System.Windows;
using DbContext;
using Microsoft.EntityFrameworkCore;

namespace JobTitle
{
    /// <summary>
    /// Логика взаимодействия для EditJobTitleWindow.xaml
    /// </summary>
    public partial class EditJobTitleWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.JobTitle JobTitle { get; set; }
        public bool IsNewJobTitle { get; set; }
        public EditJobTitleWindow(LaborExchangeDbContext dbContext, DbContext.Models.JobTitle jobTitle, bool isNewJobTitle)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            JobTitle = jobTitle;
            IsNewJobTitle = isNewJobTitle;
        }

        private void SaveJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(JobTitle.Title))
            {
                MessageBox.Show("Укажите название должности");
                return;
            }
         
            try
            {
                if (IsNewJobTitle)
                {
                    DbContext.JobTitles.Add(JobTitle);
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
