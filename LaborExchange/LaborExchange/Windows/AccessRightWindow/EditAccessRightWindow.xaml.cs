using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DbContext;
using DbContext.Enums;
using DbContext.Models;

namespace LaborExchange.Windows.AccessRightWindow
{
    /// <summary>
    /// Логика взаимодействия для EditAccessRightWindow.xaml
    /// </summary>
    public partial class EditAccessRightWindow : Window
    {
        private readonly LaborExchangeDbContext _dbContext;

        private List<AccessRight> AccessRights { get; set; }
        public List<FormTypes> FormTypeColumn { get; set; }
        private User User { get; set; }
        public EditAccessRightWindow(LaborExchangeDbContext dbContext, User user)
        {
            InitializeComponent();
            _dbContext = dbContext;
            DataContext = this;
            User = user;
            UserEmailTextBox.Text = user.Email;
            FormTypeColumn = Enum.GetValues(typeof(FormTypes)).Cast<FormTypes>().ToList();
            AccessRights = User.AccessRights.ToList();
            AccessRightGrid.ItemsSource = AccessRights;
        }
        private void SaveAccessRightButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _dbContext.SaveChanges();
                Close();
            }
            catch (DbUpdateException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
