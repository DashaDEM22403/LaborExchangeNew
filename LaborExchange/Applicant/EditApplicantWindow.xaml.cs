using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Applicant.Utilities;
using DbContext;
using DbContext.Enums;
using Institution;
using Microsoft.EntityFrameworkCore;
using Speciality;

namespace Applicant
{
    /// <summary>
    /// Логика взаимодействия для EditApplicantWindow.xaml
    /// </summary>
    public partial class EditApplicantWindow : Window
    {
        private readonly LaborExchangeDbContext DbContext;
        public DbContext.Models.Applicant Applicant { get; set; }
        public bool IsNewApplicant { get; set; }
        public List<EducationTypes> EducationTypes { get; set; }
        public BitmapImage BitmapPhoto { get; set; }
        public EditApplicantWindow(LaborExchangeDbContext dbContext, DbContext.Models.Applicant applicant, bool isNewApplicant)
        {
            InitializeComponent();
            DbContext = dbContext;
            DataContext = this;
            Applicant = applicant;
            IsNewApplicant = isNewApplicant;
            EducationTypes = Enum.GetValues(typeof(EducationTypes)).Cast<EducationTypes>().ToList();
            ApplicantPhoto.Source = BitmapPhoto;

            Applicant.Photo = Photo.LoadPhoto(Applicant.Photo ?? Photo.ApplicantImage, ApplicantPhoto);
        }

        private void SearchSpecialityButton_Click(object sender, RoutedEventArgs e)
        {
            var searchSpecialityWindow = new SearchSpecialityWindow(DbContext, Applicant);
            searchSpecialityWindow.ShowDialog();

            if (Applicant.Speciality is not null)
            {
                SpecialityTextBox.Text = Applicant.Speciality.Title;
            }
        }

        private void SearchInstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            var searchInstitutionWindow = new SearchInstitutionWindow(DbContext, Applicant);
            searchInstitutionWindow.ShowDialog();
            
            if (Applicant.Institution is not null)
            {
                InstitutionTextBox.Text = Applicant.Institution.Title;
            }
        }

        private void SaveApplicantButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Applicant.FirstName))
            {
                MessageBox.Show("Укажите имя");
                return;
            }
            if (string.IsNullOrEmpty(Applicant.LastName))
            {
                MessageBox.Show("Укажите фамилию");
                return;
            }
            if (string.IsNullOrEmpty(Applicant.Patronymic))
            {
                MessageBox.Show("Укажите отчество");
                return;
            }
            var passportSeries = Applicant.PassportSeries.ToString();
            if (passportSeries == null || passportSeries == "0")
            {
                MessageBox.Show("Укажите серию паспорта");
                return;
            }
            if (passportSeries.Length != 4)
            {
                MessageBox.Show("Серия паспорта должна состоять из 4 чисел");
                return;
            }
            var passportId = Applicant.PassportId.ToString();
            if (passportId == null || passportId == "0")
            {
                MessageBox.Show("Укажите номер паспорта");
                return;
            }
            if (passportId.Length != 6)
            {
                MessageBox.Show("Номер паспорта должна состоять из 6 чисел");
                return;
            }
            if (string.IsNullOrEmpty(Applicant.IssuedBy))
            {
                MessageBox.Show("Укажите кем выдан паспорт");
                return;
            }
            if (Applicant.Speciality == null)
            {
                MessageBox.Show("Выберите специальность");
                return;
            }
            if (Applicant.Institution == null)
            {
                MessageBox.Show("Выберите учебное заведение");
                return;
            }
            if (string.IsNullOrEmpty(Applicant.WorkExperience))
            {
                MessageBox.Show("Укажите опыт работы");
                return;
            }

            try
            {
                if (IsNewApplicant)
                {
                    DbContext.Applicants.Add(Applicant);
                }
                DbContext.SaveChanges();
                Close();
            }
            catch (DbUpdateException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AddApplicantPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            Applicant.Photo = Photo.AddPhoto(ApplicantPhoto);
            Applicant.Photo = Photo.LoadPhoto(Applicant.Photo ?? Photo.ApplicantImage, ApplicantPhoto);
        }
    }
}
