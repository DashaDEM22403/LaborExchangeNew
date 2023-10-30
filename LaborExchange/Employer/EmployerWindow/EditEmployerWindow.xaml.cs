using System.Text.RegularExpressions;
using System.Windows;
using DbContext;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Employer.EmployerWindow
{
    /// <summary>
    /// Логика взаимодействия для EditEmployerWindow.xaml
    /// </summary>
    public partial class EditEmployerWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.Employer Employer { get; set; }
        public bool IsNewEmployer { get; set; }
        public EditEmployerWindow(LaborExchangeDbContext dbContext, DbContext.Models.Employer employer, bool isNewEmployer)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Employer = employer;
            IsNewEmployer = isNewEmployer;
        }

        private void SaveEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Employer.CompanyName))
            {
                MessageBox.Show("Укажите название компании");
                return;
            }
            if (string.IsNullOrEmpty(Employer.Phone))
            {
                MessageBox.Show("Укажите номер телефона");
                return;
            }
            Regex regexPhone = new Regex(@"^(?:8\s?)?(?:\+7\s?)?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{2}[-.\s]?\d{2}$");
            Regex regexСorrectPhone = new Regex(@"\D");
            string target = "";
            if (!regexPhone.IsMatch(Employer.Phone))
            {
                MessageBox.Show("Укажите номер телефона в формате:\n 89513949722\r\n9513949722\r\n+79513949722\r\n+7 (951) 394-97-22\r\n+7 (951) 394 97 22");
                return;
            }
            if (string.IsNullOrEmpty(Employer.Address))
            {
                MessageBox.Show("Укажите адрес");
                return;
            }
            Employer.Phone = regexСorrectPhone.Replace(Employer.Phone, target);
            try
            {
                if (IsNewEmployer)
                {
                    DbContext.Employers.Add(Employer);
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
