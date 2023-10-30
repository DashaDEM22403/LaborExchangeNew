using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Applicant;
using DbContext;
using DbContext.Enums;
using DbContext.Models;
using Deal;
using Document.Windows;
using Employer.EmployerWindow;
using Institution;
using JobArea;
using JobTitle;
using JobType;
using JobVacancy;
using LaborExchange.Utilities;
using LaborExchange.Windows.AccessRightWindow;
using Microsoft.EntityFrameworkCore;
using Speciality;

using MenuItem = System.Windows.Controls.MenuItem;
using Window = System.Windows.Window;

namespace LaborExchange.Windows
{
    public partial class MainWindow : Window
    {
        private readonly LaborExchangeDbContext _dbContext;
        public List<DbContext.Models.Employer> Employers { get; set; }
        public List<DbContext.Models.JobTitle> JobTitles { get; set; }
        public List<DbContext.Models.JobArea> JobAreas { get; set; }
        public List<DbContext.Models.Speciality> Specialities { get; set; }
        public List<DbContext.Models.Institution> Institutions { get; set; }
        public List<DbContext.Models.JobType> JobTypes { get; set; }
        public List<DbContext.Models.JobVacancy> JobVacancies { get; set; }
        public List<DbContext.Models.Applicant> Applicants { get; set; }
        public List<DbContext.Models.Deal> Deals { get; set; }
        public User User { get; set; }

        public MainWindow(LaborExchangeDbContext dbContext, User user)
        {
            InitializeComponent();
            _dbContext = dbContext;
            DataContext = this;
            User = user;

            InitMenuItems();
            InitAccessRights();

            if (User.UserType != UserTypes.admin)
            {
                var adminMenuItem = GetMenuItemByName("AdminMenuItem");
                adminMenuItem.Visibility = Visibility.Hidden;
            }


            RefreshEmployerGrid();
            RefreshJobTitleGrid();
            RefreshJobAreaGrid();
            RefreshSpecialityGrid();
            RefreshInstitutionGrid();
            RefreshJobTypeGrid();
            RefreshJobVacancyGrid();
            RefreshApplicantGrid();
            RefreshDealGrid();
            RefreshUserGrid();

            Closing += MainWindow_Closing;
        }

        private void InitAccessRights()
        {
            var employerFAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.EmployerForm);
            AccessRightForm(employerFAccessRight, GetMenuItemByName("EmployerMenuItem"), EmployerGridItem,
                AddEmployerButton, EditEmployerButton, DeleteEmployerButton);

            var institutionFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.InstitutionForm);
            AccessRightForm(institutionFormAccessRight, GetMenuItemByName("InstitutionMenuItem"), InstitutionGridItem,
                AddInstitutionButton, EditInstitutionButton, DeleteInstitutionButton);

            var applicantFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.ApplicantForm);
            AccessRightForm(applicantFormAccessRight, GetMenuItemByName("ApplicantMenuItem"), ApplicantGridItem,
                AddApplicantButton, EditApplicantButton, DeleteApplicantButton);

            var specialityFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.SpecialityForm);
            AccessRightForm(specialityFormAccessRight, GetMenuItemByName("SpecialityMenuItem"), SpecialityGridItem,
                AddSpecialityButton, EditSpecialityButton, DeleteSpecialityButton);

            var dealFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.DealForm);
            AccessRightForm(dealFormAccessRight, GetMenuItemByName("DealMenuItem"), DealGridItem, AddDealButton,
                EditDealButton, DeleteDealButton);

            var jobVacancyFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.JobVacancyForm);
            AccessRightForm(jobVacancyFormAccessRight, GetMenuItemByName("JobVacancyMenuItem"), JobVacancyGridItem,
                AddJobVacancyButton, EditJobVacancyButton, DeleteJobVacancyButton);

            var jobTypeFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.JobTypeForm);
            AccessRightForm(jobTypeFormAccessRight, GetMenuItemByName("JobTypeMenuItem"), JobTypeGridItem,
                AddJobTypeButton, EditJobTypeButton, DeleteJobTypeButton);

            var jobTitleFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.JobTitleForm);
            AccessRightForm(jobTitleFormAccessRight, GetMenuItemByName("JobTitleMenuItem"), JobTitleGridItem,
                AddJobTitleButton, EditJobTitleButton, DeleteJobTitleButton);

            var jobAreaFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.JobAreaForm);
            AccessRightForm(jobAreaFormAccessRight, GetMenuItemByName("JobAreaMenuItem"), JobAreaGridItem,
                AddJobAreaButton, EditJobAreaButton, DeleteJobAreaButton);

            var firstDocumentFormAccessRight = User.AccessRights.Single(x => x.Form == FormTypes.FirstDocumentForm);

            AccessRightForm(firstDocumentFormAccessRight, GetMenuItemByName("FirstDocumentMenuItem"), null,
                null, null,
                null);
        }

        public MenuItem GetMenuItemByName(string menuItemName)
        {
            Menu menu = Menu;

            if (menu != null)
            {
                foreach (var item in menu.Items.OfType<MenuItem>())
                {
                    if (item.Name == menuItemName)
                        return item;

                    MenuItem subMenuItem = FindMenuItemByName(item, menuItemName);
                    if (subMenuItem != null)
                        return subMenuItem;
                }
            }

            return null;
        }

        private MenuItem FindMenuItemByName(MenuItem menuItem, string menuItemName)
        {
            foreach (var item in menuItem.Items.OfType<MenuItem>())
            {
                if (item.Name == menuItemName)
                    return item;

                MenuItem subMenuItem = FindMenuItemByName(item, menuItemName);
                if (subMenuItem != null)
                    return subMenuItem;
            }

            return null;
        }

        private void InitMenuItems()
        {
            var menuItems = (_dbContext.MenuItems).ToList();

            var parents = menuItems.Where(x => x.ParentId == null).OrderBy(x => x.Priority);

            foreach (var parent in parents)
            {
                var parentItem = AddParentToMenuItem(parent.ItemName, parent.DllName, parent.FunctionName);

                var children = menuItems.Where(x => x.ParentId.HasValue && x.ParentId == parent.Id)
                    .OrderBy(x => x.Priority);

                if (children.Any())
                {
                    foreach (var child in children)
                    {
                        AddChildToMenuItem(parentItem, child.ItemName, child.DllName, child.FunctionName);
                    }
                }
            }
        }

        private void AddChildToMenuItem(MenuItem parent, string header, string name,
            string function)
        {
            MenuItem childItem = new MenuItem
            {
                Name = name,
                Header = header,
                Cursor = Cursors.Hand
            };

            if (function is not null)
            {
                RoutedEventHandler eventHandler = (sender, e) =>
                {
                    Type type = GetType();

                    MethodInfo method = type.GetMethod(function);

                    if (method != null)
                    {
                        object[] methodParams = { sender, e };
                        method.Invoke(this, methodParams);
                    }
                    else
                    {
                        Console.WriteLine("Метод не найден: " + function);
                    }
                };

                childItem.Click += eventHandler;
            }

            parent.Items.Add(childItem);
        }

        private System.Windows.Controls.MenuItem AddParentToMenuItem(string header, string name,
            string? function)
        {
            System.Windows.Controls.MenuItem parentItem = new System.Windows.Controls.MenuItem
            {
                Name = name,
                Header = header,
                Cursor = Cursors.Hand
            };

            if (function is not null)
            {
                RoutedEventHandler eventHandler = (sender, e) =>
                {
                    Type type = GetType();

                    MethodInfo method = type.GetMethod(function);

                    if (method != null)
                    {
                        object[] methodParams = { sender, e };
                        method.Invoke(this, methodParams);
                    }
                    else
                    {
                        Console.WriteLine("Метод не найден: " + function);
                    }
                };

                parentItem.Click += eventHandler;
            }

            Menu.Items.Add(parentItem);

            return parentItem;
        }

        private void AccessRightForm(AccessRight accessRight, MenuItem menuItem, Grid? grid, Button? addButton,
            Button? editButton, Button? deleteButton)
        {
            if (!accessRight.Read)
            {
                if (!accessRight.Read)
                {
                    menuItem.Visibility = Visibility.Hidden;
                    if (grid is not null)
                    {
                        grid.Visibility = Visibility.Hidden;
                    }
                }

                if (addButton is not null)
                {
                    addButton.IsEnabled = accessRight.Write;
                }

                if (editButton is not null)
                {
                    editButton.IsEnabled = accessRight.Edit;
                }

                if (deleteButton is not null)
                {
                    deleteButton.IsEnabled = accessRight.Delete;
                }
            }
        }

        private void AccessRightForm(AccessRight accessRight, TabItem tabItem, Grid grid, Button addButton,
            Button editButton, Button deleteButton)
        {
            if (!accessRight.Read)
            {
                tabItem.Visibility = Visibility.Hidden;
                grid.Visibility = Visibility.Hidden;
            }

            addButton.IsEnabled = accessRight.Write;
            editButton.IsEnabled = accessRight.Edit;
            deleteButton.IsEnabled = accessRight.Delete;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var authWindow = new AuthWindow();
            authWindow.Show();
        }

        private void RefreshEmployerGrid()
        {
            Employers = _dbContext.Employers.ToList();
            EmployerGrid.ItemsSource = Employers;
            EmployerGrid.Items.Refresh();
        }

        private void RefreshJobTitleGrid()
        {
            JobTitles = _dbContext.JobTitles.ToList();
            JobTitleGrid.ItemsSource = JobTitles;
            JobTitleGrid.Items.Refresh();
        }

        private void RefreshJobAreaGrid()
        {
            JobAreas = _dbContext.JobAreas.ToList();
            JobAreaGrid.ItemsSource = JobAreas;
            JobAreaGrid.Items.Refresh();
        }

        private void RefreshSpecialityGrid()
        {
            Specialities = _dbContext.Specialities.ToList();
            SpecialityGrid.ItemsSource = Specialities;
            SpecialityGrid.Items.Refresh();
        }

        private void RefreshInstitutionGrid()
        {
            Institutions = _dbContext.Institutions.ToList();
            InstitutionGrid.ItemsSource = Institutions;
            InstitutionGrid.Items.Refresh();
        }

        private void RefreshJobTypeGrid()
        {
            JobTypes = _dbContext.JobTypes.ToList();
            JobTypeGrid.ItemsSource = JobTypes;
            JobTypeGrid.Items.Refresh();
        }

        private void RefreshJobVacancyGrid()
        {
            JobVacancies = _dbContext.JobVacancies
                .AsSplitQuery()
                .Include(x => x.JobTitle)
                .Include(x => x.JobType)
                .Include(x => x.Employer)
                .ToList();
            JobVacancyGrid.ItemsSource = JobVacancies;
            JobVacancyGrid.Items.Refresh();
        }

        private void RefreshApplicantGrid()
        {
            Applicants = _dbContext.Applicants
                .AsSplitQuery()
                .Include(x => x.Institution)
                .Include(x => x.Speciality)
                .ToList();
            ApplicantGrid.ItemsSource = Applicants;
            ApplicantGrid.Items.Refresh();
        }

        private void RefreshDealGrid()
        {
            Deals = _dbContext.Deals
                .AsSplitQuery()
                .Include(x => x.Applicant)
                .Include(x => x.JobVacancy)
                .ToList();
            DealGrid.ItemsSource = Deals;
            DealGrid.Items.Refresh();
        }

        private void RefreshUserGrid()
        {
            var userTypes = Enum.GetValues(typeof(UserTypes)).Cast<UserTypes>().ToList();
            UserTypeColumn.ItemsSource = userTypes;
            UserGrid.ItemsSource = _dbContext.Users.Include(x => x.AccessRights).ToList();
            UserGrid.Items.Refresh();
        }

        private void RefreshFirstDocumentGrid()
        {
            var search = SearchJobVacancyTextBox.Text.ToLower();
            FirstDocumentGrid.ItemsSource = _dbContext.JobVacancies
                .Where(x => (x.JobArea.Title.ToLower().Contains(search) || x.JobType.Type.ToLower().Contains(search) ||
                             x.JobTitle.Title.ToLower().Contains(search) ||
                             x.Employer.CompanyName.ToLower().Contains(search) || x.Patch.ToLower().Contains(search)))
                .ToList();
            FirstDocumentGrid.Items.Refresh();
        }

        private void SearchJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshFirstDocumentGrid();
        }

        private void AddEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.Employer newEmployer = new DbContext.Models.Employer();
            var editEmployerWindow = new EditEmployerWindow(_dbContext, newEmployer, true);
            editEmployerWindow.ShowDialog();
            RefreshEmployerGrid();
        }

        private void EditEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployerGrid.SelectedItems.Count > 0)
            {
                var editEmployerWindow = new EditEmployerWindow(_dbContext,
                    (DbContext.Models.Employer)EmployerGrid.SelectedItems[0]!, false);
                editEmployerWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.Employer)EmployerGrid.SelectedItems[0]!).Reload();
                RefreshEmployerGrid();
            }
            else
            {
                MessageBox.Show("Выберете работодателя для редактирования");
                return;
            }
        }

        private void DeleteEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployerGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < EmployerGrid.SelectedItems.Count; i++)
                {
                    if (EmployerGrid.SelectedItems[i] is DbContext.Models.Employer employer)
                    {
                        _dbContext.Employers.Remove(employer);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете работодателя для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshEmployerGrid();
        }

        private void AddJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.JobTitle newJobTitle = new DbContext.Models.JobTitle();
            var editJobTitleWindow = new EditJobTitleWindow(_dbContext, newJobTitle, true);
            editJobTitleWindow.ShowDialog();
            RefreshJobTitleGrid();
        }

        private void EditJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobTitleGrid.SelectedItems.Count > 0)
            {
                var editJobTitleWindow = new EditJobTitleWindow(_dbContext,
                    (DbContext.Models.JobTitle)JobTitleGrid.SelectedItems[0]!, false);
                editJobTitleWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.JobTitle)JobTitleGrid.SelectedItems[0]!).Reload();
                RefreshJobTitleGrid();
            }
            else
            {
                MessageBox.Show("Выберете должность для редактирования");
                return;
            }
        }

        private void DeleteJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobTitleGrid.SelectedItems.Count > 0)
            {
                foreach (var t in JobTitleGrid.SelectedItems)
                {
                    if (t is DbContext.Models.JobTitle jobTitle)
                    {
                        _dbContext.JobTitles.Remove(jobTitle);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете должность для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshJobTitleGrid();
        }

        private void DeleteJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobAreaGrid.SelectedItems.Count > 0)
            {
                foreach (var t in JobAreaGrid.SelectedItems)
                {
                    if (t is DbContext.Models.JobArea jobArea)
                    {
                        _dbContext.JobAreas.Remove(jobArea);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете сферу вакансии  для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshJobAreaGrid();
        }

        private void EditJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobAreaGrid.SelectedItems.Count > 0)
            {
                var editJobAreaWindow = new EditJobAreaWindow(_dbContext,
                    (DbContext.Models.JobArea)JobAreaGrid.SelectedItems[0]!, false);
                editJobAreaWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.JobArea)JobAreaGrid.SelectedItems[0]!).Reload();
                RefreshJobAreaGrid();
            }
            else
            {
                MessageBox.Show("Выберете должность для редактирования");
                return;
            }
        }

        private void AddJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.JobArea newJobArea = new DbContext.Models.JobArea();
            var editJobAreaWindow = new EditJobAreaWindow(_dbContext, newJobArea, true);
            editJobAreaWindow.ShowDialog();
            RefreshJobAreaGrid();
        }

        private void DeleteSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialityGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < SpecialityGrid.SelectedItems.Count; i++)
                {
                    if (SpecialityGrid.SelectedItems[i] is DbContext.Models.Speciality speciality)
                    {
                        _dbContext.Specialities.Remove(speciality);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете специальность для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshSpecialityGrid();
        }

        private void EditSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialityGrid.SelectedItems.Count > 0)
            {
                var editSpecialityWindow = new EditSpecialityWindow(_dbContext,
                    (DbContext.Models.Speciality)SpecialityGrid.SelectedItems[0]!, false);
                editSpecialityWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.Speciality)SpecialityGrid.SelectedItems[0]!).Reload();
                RefreshSpecialityGrid();
            }
            else
            {
                MessageBox.Show("Выберете специальность для редактирования");
                return;
            }
        }

        private void AddSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.Speciality newSpeciality = new DbContext.Models.Speciality();
            var editSpecialityWindow = new EditSpecialityWindow(_dbContext, newSpeciality, true);
            editSpecialityWindow.ShowDialog();
            RefreshSpecialityGrid();
        }

        private void AddInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.Institution newInstitution = new DbContext.Models.Institution();
            var editInstitutionWindow = new EditInstitutionWindow(_dbContext, newInstitution, true);
            editInstitutionWindow.ShowDialog();
            RefreshInstitutionGrid();
        }

        private void EditInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            if (InstitutionGrid.SelectedItems.Count > 0)
            {
                var editInstitutionWindow = new EditInstitutionWindow(_dbContext,
                    (DbContext.Models.Institution)InstitutionGrid.SelectedItems[0]!, false);
                editInstitutionWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.Institution)InstitutionGrid.SelectedItems[0]!).Reload();
                RefreshInstitutionGrid();
            }
            else
            {
                MessageBox.Show("Выберете учебное заведение для редактирования");
                return;
            }
        }

        private void DeleteInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            if (InstitutionGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < InstitutionGrid.SelectedItems.Count; i++)
                {
                    if (InstitutionGrid.SelectedItems[i] is DbContext.Models.Institution institution)
                    {
                        _dbContext.Institutions.Remove(institution);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете учебное заведение для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshInstitutionGrid();
        }

        private void DeleteJobTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobTypeGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < JobTypeGrid.SelectedItems.Count; i++)
                {
                    if (JobTypeGrid.SelectedItems[i] is DbContext.Models.JobType jobType)
                    {
                        _dbContext.JobTypes.Remove(jobType);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете тип вакансии для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshJobTypeGrid();
        }

        private void EditJobTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobTypeGrid.SelectedItems.Count > 0)
            {
                var editJobTypeWindow = new EditJobTypeWindow(_dbContext,
                    (DbContext.Models.JobType)JobTypeGrid.SelectedItems[0]!, false);
                editJobTypeWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.JobType)JobTypeGrid.SelectedItems[0]!).Reload();
                RefreshJobTypeGrid();
            }
            else
            {
                MessageBox.Show("Выберете тип вакансии для редактирования");
                return;
            }
        }

        private void AddJobTypeButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.JobType newJobType = new DbContext.Models.JobType();
            var editJobTypeWindow = new EditJobTypeWindow(_dbContext, newJobType, true);
            editJobTypeWindow.ShowDialog();
            RefreshJobTypeGrid();
        }

        private void AddJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.JobVacancy newJobVacancy = new DbContext.Models.JobVacancy();
            var editJobVacancyWindow = new EditJobVacancyWindow(_dbContext, newJobVacancy, true);
            editJobVacancyWindow.ShowDialog();
            RefreshJobVacancyGrid();
        }

        private void EditJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobVacancyGrid.SelectedItems.Count > 0)
            {
                var editJobVacancyWindow = new EditJobVacancyWindow(_dbContext,
                    (DbContext.Models.JobVacancy)JobVacancyGrid.SelectedItems[0]!, false);
                editJobVacancyWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.JobVacancy)JobVacancyGrid.SelectedItems[0]!).Reload();
                RefreshJobVacancyGrid();
            }
            else
            {
                MessageBox.Show("Выберете вакансию для редактирования");
                return;
            }
        }

        private void DeleteJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobVacancyGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < JobVacancyGrid.SelectedItems.Count; i++)
                {
                    if (JobVacancyGrid.SelectedItems[i] is DbContext.Models.JobVacancy jobVacancy)
                    {
                        _dbContext.JobVacancies.Remove(jobVacancy);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете вакансию для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshJobVacancyGrid();
        }

        private void AddApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.Applicant newApplicant = new DbContext.Models.Applicant();
            var editApplicantWindow = new EditApplicantWindow(_dbContext, newApplicant, true);
            editApplicantWindow.ShowDialog();
            RefreshApplicantGrid();
        }

        private void EditApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicantGrid.SelectedItems.Count > 0)
            {
                var editApplicantWindow = new EditApplicantWindow(_dbContext,
                    (DbContext.Models.Applicant)ApplicantGrid.SelectedItems[0]!, false);
                editApplicantWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.Applicant)ApplicantGrid.SelectedItems[0]!).Reload();
                RefreshApplicantGrid();
            }
            else
            {
                MessageBox.Show("Выберете соискателя для редактирования");
                return;
            }
        }

        private void DeleteApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicantGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ApplicantGrid.SelectedItems.Count; i++)
                {
                    if (ApplicantGrid.SelectedItems[i] is DbContext.Models.Applicant applicant)
                    {
                        _dbContext.Applicants.Remove(applicant);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете соискателя для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshApplicantGrid();
        }

        private void EditDealButton_Click(object sender, RoutedEventArgs e)
        {
            if (DealGrid.SelectedItems.Count > 0)
            {
                var editDealWindow =
                    new EditDealWindow(_dbContext, (DbContext.Models.Deal)DealGrid.SelectedItems[0]!, false);
                editDealWindow.ShowDialog();
                _dbContext.Entry((DbContext.Models.Deal)DealGrid.SelectedItems[0]!).Reload();
                RefreshDealGrid();
            }
            else
            {
                MessageBox.Show("Выберете сделку для редактирования");
                return;
            }
        }

        private void AddDealButton_Click(object sender, RoutedEventArgs e)
        {
            DbContext.Models.Deal newDeal = new DbContext.Models.Deal();
            var editDealWindow = new EditDealWindow(_dbContext, newDeal, true);
            editDealWindow.ShowDialog();
            RefreshDealGrid();
        }

        private void DeleteDealButton_Click(object sender, RoutedEventArgs e)
        {
            if (DealGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < DealGrid.SelectedItems.Count; i++)
                {
                    if (DealGrid.SelectedItems[i] is DbContext.Models.Deal deal)
                    {
                        _dbContext.Deals.Remove(deal);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете сделку для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshDealGrid();
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EditAccessRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItems.Count > 0)
            {
                var editAccessRightWindow = new EditAccessRightWindow(_dbContext, (User)UserGrid.SelectedItems[0]!);
                editAccessRightWindow.ShowDialog();
                _dbContext.Entry((User)UserGrid.SelectedItems[0]!).Reload();
                RefreshUserGrid();
            }
            else
            {
                MessageBox.Show("Выберете пользователя для редактирования прав");
                return;
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UserGrid.SelectedItems.Count; i++)
                {
                    if (UserGrid.SelectedItems[i] is User user)
                    {
                        if (user.UserType != UserTypes.admin)
                        {
                            _dbContext.Users.Remove(user);
                        }
                        else
                        {
                            MessageBox.Show("Вы не можете удалить администратора");
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберете пользователя для удаления");
                return;
            }

            _dbContext.SaveChanges();
            RefreshUserGrid();
        }

        private void RecoverPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var oldPassword = OldPasswordTexBox.Password;
            var newPassword = NewPasswordTexBox.Password;
            var confirmPassword = ConfirmPasswordTexBox.Password;

            if (oldPassword == String.Empty)
            {
                MessageBox.Show("Старый может быть пустым");
                return;
            }

            if (newPassword == String.Empty)
            {
                MessageBox.Show("Новый пароль не может быть пустым");
                return;
            }

            if (confirmPassword == String.Empty)
            {
                MessageBox.Show("Подтвержение пароля не может быть пустым");
                return;
            }

            if (newPassword.Length < 5)
            {
                MessageBox.Show("Новый пароль не может быть меньше 5 символов");
                return;
            }

            if (confirmPassword.Length < 5)
            {
                MessageBox.Show("Подтвержение пароля не может быть меньше 5 символов");
                return;
            }

            if (User.Password == PasswordEncrypter.GetHash(oldPassword))
            {
                if (newPassword == confirmPassword)
                {
                    User.Password = PasswordEncrypter.GetHash(newPassword);
                    _dbContext.SaveChanges();
                    OldPasswordTexBox.Password = string.Empty;
                    NewPasswordTexBox.Password = string.Empty;
                    ConfirmPasswordTexBox.Password = string.Empty;
                    MessageBox.Show("Пароль успешно изменён!");
                }
                else
                {
                    MessageBox.Show("Новые пароли не совпадают");
                }
            }
            else
            {
                MessageBox.Show("Старый пароль неправильный");
            }
        }

        public void OpenJobTitleButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = JobTitleTabItem;
            TitleTextBox.Text = JobTitleTabItem.Header.ToString();
        }

        public void OpenEmployerButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = EmployerTabItem;
            TitleTextBox.Text = EmployerTabItem.Header.ToString();
        }

        public void OpenJobAreaButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = JobAreaTabItem;
            TitleTextBox.Text = JobAreaTabItem.Header.ToString();
        }

        public void OpenSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = SpecialityTabItem;
            TitleTextBox.Text = SpecialityTabItem.Header.ToString();
        }

        public void OpenInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = InstitutionTabItem;
            TitleTextBox.Text = InstitutionTabItem.Header.ToString();
        }


        public void OpenJobTypeButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = JobTypeTabItem;
            TitleTextBox.Text = JobTypeTabItem.Header.ToString();
        }

        public void OpenJobVacancyButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = JobVacancyTabItem;
            TitleTextBox.Text = JobVacancyTabItem.Header.ToString();
        }


        public void OpenApplicantButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = ApplicantTabItem;
            TitleTextBox.Text = ApplicantTabItem.Header.ToString();
        }

        public void OpenDealButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = DealTabItem;
            TitleTextBox.Text = DealTabItem.Header.ToString();
        }

        public void OpenHelpButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = HelpTabItem;
            TitleTextBox.Text = HelpTabItem.Header.ToString();
        }

        public void OpenFirstDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = FirstDocumentTabItem;
            TitleTextBox.Text = FirstDocumentTabItem.Header.ToString();
            RefreshFirstDocumentGrid();
        }

        public void OpenRecoverPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = RecoverPasswordTabItem;
            TitleTextBox.Text = RecoverPasswordTabItem.Header.ToString();
        }

        public void OpenAdminButton_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = AdminTabItem;
            TitleTextBox.Text = AdminTabItem.Header.ToString();
        }

        public void ExportJobVacancyAsButton_Click(object sender, RoutedEventArgs e)
        {
            var firstDocumentWindow = new ExportFirstDocumentWindow(_dbContext, FirstDocumentGrid);
            firstDocumentWindow.ShowDialog();
        }

        public void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}