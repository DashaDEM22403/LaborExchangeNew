using LaborExchange.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DbContext;
using DbContext.Enums;
using DbContext.Models;

namespace LaborExchange.Windows
{
    public partial class AuthWindow : Window
    {
        private readonly LaborExchangeDbContext _dbContext;
        public List<UserTypes> UserTypes { get; set; }
        public AccessRight AccessRight { get; set; }
        public List<AccessRight> AccessRights { get; set; } = new List<AccessRight>();

        public AuthWindow()
        {
            InitializeComponent();
            _dbContext = new LaborExchangeDbContext();
            DataContext = this;
        }

        private AccessRight AddAccessRight(bool v1, bool v2, bool v3, bool v4, FormTypes formTypes)
        {
            AccessRight = new AccessRight();
            AccessRight.Read = v1;
            AccessRight.Write = v2;
            AccessRight.Edit = v3;
            AccessRight.Delete = v4;

            AccessRight.Form = formTypes;
            return AccessRight;
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                var email = RegisterEmailTextBox.Text;
                if (String.IsNullOrEmpty(RegisterEmailTextBox.Text))
                {
                    MessageBox.Show("Емаил не может быть пустым");
                    return;
                }

                if (String.IsNullOrEmpty(RegisterPasswordTextBox.Password))
                {
                    MessageBox.Show("Пароль не может быть пустым");
                    return;
                }

                if (String.IsNullOrEmpty(RegisterPasswordConfirmTextBox.Password))
                {
                    MessageBox.Show("Подтвержение пароля не может быть пустым");
                    return;
                }

                if (RegisterEmailTextBox.Text.Length < 5)
                {
                    MessageBox.Show("Емаил не может быть меньше 5 символов");
                    return;
                }

                if (RegisterPasswordTextBox.Password.Length < 5)
                {
                    MessageBox.Show("Пароль не может быть меньше 5 символов");
                    return;
                }

                if (RegisterPasswordConfirmTextBox.Password.Length < 5)
                {
                    MessageBox.Show("Подтвержение пароля не может быть меньше 5 символов");
                    return;
                }

                if (!_dbContext.Users.Any(x => x.Email == email))
                {
                    if (RegisterPasswordConfirmTextBox.Password == RegisterPasswordTextBox.Password)
                    {
                        var passwordHash = PasswordEncrypter.GetHash(RegisterPasswordTextBox.Password);
                        User user = new User();
                        user.Email = email;
                        user.Password = passwordHash;
                        user.UserType = DbContext.Enums.UserTypes.user;

                        foreach (var form in Enum.GetValues(typeof(FormTypes)).Cast<FormTypes>())
                        {
                            AccessRights.Add(AddAccessRight(false, false, false, false, form));
                        }

                        user.AccessRights = AccessRights;
                        _dbContext.Add(user);
                        try
                        {
                            _dbContext.SaveChanges();
                        }
                        catch (DbUpdateException exception)
                        {
                            MessageBox.Show(exception.Message);
                        }

                        var mainWindow = new MainWindow(_dbContext, user);
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Такой емаил уже есть");
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                if (String.IsNullOrEmpty(LoginEmailTextBox.Text))
                {
                    MessageBox.Show("Емаил не может быть пустым");
                    return;
                }

                if (String.IsNullOrEmpty(LoginPasswordTextBox.Password))
                {
                    MessageBox.Show("Пароль не может быть пустым");
                    return;
                }

                var email = LoginEmailTextBox.Text;
                var passwordHash = PasswordEncrypter.GetHash(LoginPasswordTextBox.Password);
                User? user = _dbContext.Users
                    .Include(x => x.AccessRights)
                    .FirstOrDefault(u => u.Email == email && u.Password == passwordHash);
                if (user != null)
                {
                    var mainWindow = new MainWindow(_dbContext, user);
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}