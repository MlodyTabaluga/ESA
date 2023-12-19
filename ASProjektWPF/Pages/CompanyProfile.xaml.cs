using TIProjekt.Classes;
using TIProjekt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TIProjekt.Pages
{
    /// <summary>
    /// Interaction logic for CompanyProfile.xaml
    /// </summary>
    public partial class CompanyProfile : Page
    {
        Frame? CurrentPage;
        Company? company;
        public CompanyProfile(Frame currentPage, Company company, bool editFlag)
        {
            InitializeComponent();
            CurrentPage = currentPage;
            this.company = company;
           
         
         
            Lbl_Jobs.Content = $"{App.DataAccess.GetAnnouncmentList().Where(item => item.CompanyID == company.CompanyID).ToList().Count} ogłoszeń o pracę";
            Lbl_Company.Content = company.Name;
            Lbl_Adress.Content = company.Adress;
            Lbl_Email.Content = company.Email;
            int count = App.DataAccess.GetAnnouncmentList(company).Count;
            if (count == 1)
            {
                Lbl_Jobs.Content = count + " Job offer";
            }else if (count <4 && count >1)
            {
                Lbl_Jobs.Content = count + " Job Offers";
            }
            else
            {
                Lbl_Jobs.Content = count + " Job Offers";
            }
            
        }


        //Idz do ogloszenia
        private void Btn_GoToAnnouncment_Click(object sender, RoutedEventArgs e)
        {

            AnnouncmentItem? tmpItem = ((Button)sender).CommandParameter as AnnouncmentItem;
            if (tmpItem != null)
            {
                Announcment? item = tmpItem.Announcment;
                if (item != null  && CurrentPage != null)
                {
                    CurrentPage.Navigate(new AnnouncmentPage(CurrentPage, item));
                }
                else if (item != null && CurrentPage != null)
                {
                    CurrentPage.Navigate(new AnnouncmentPage(CurrentPage, item));
                }
            }
            
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentPage != null)
            {
                CurrentPage.GoBack();
            }
        }


        //edycja firmy - stan na 27.11 - nie dziala
        private void Btn_EditCompany_Click(object sender, RoutedEventArgs e)
        {
            string? content = Btn_EditCompany.Content.ToString();
            if(content == "Anuluj")
            {
                Lbl_Company.Visibility = Visibility.Visible;
                TxtB_CompanyEdit.Visibility = Visibility.Collapsed;
                Lbl_Adress.Visibility = Visibility.Visible;
                TxtB_Adress_Edit.Visibility = Visibility.Collapsed;
                Lbl_Email.Visibility = Visibility.Visible;
                TxtB_Email_Edit.Visibility = Visibility.Collapsed;
                Btn_EditCompany.Content = "Edytuj";
            }
            else
            {
                Lbl_Company.Visibility = Visibility.Collapsed;
                TxtB_CompanyEdit.Visibility = Visibility.Visible;
                Lbl_Adress.Visibility = Visibility.Collapsed;
                TxtB_Adress_Edit.Visibility = Visibility.Visible;
                Lbl_Email.Visibility = Visibility.Collapsed;
                TxtB_Email_Edit.Visibility = Visibility.Visible;
                Btn_EditCompany.Content = "Anuluj";
            }
        }


        //Zaisaywanie stanu do bazy
        private void Btn_SaveCompany_Click(object sender, RoutedEventArgs e)
        {
            if(company != null) 
            {
                company.Name = TxtB_CompanyEdit.Text;
                company.Adress = TxtB_Adress_Edit.Text;
                company.Email = TxtB_Email_Edit.Text;
                App.DataAccess.Update_Company(company);
                company = App.DataAccess.GetCompanyFromID(company.CompanyID);
                Lbl_Company.Content = company.Name;
                Lbl_Adress.Content = company.Adress;
                Lbl_Email.Content = company.Email;
                Lbl_Company.Visibility = Visibility.Visible;
                TxtB_CompanyEdit.Visibility = Visibility.Collapsed;
                Lbl_Adress.Visibility = Visibility.Visible;
                TxtB_Adress_Edit.Visibility = Visibility.Collapsed;
                Lbl_Email.Visibility = Visibility.Visible;
                TxtB_Email_Edit.Visibility = Visibility.Collapsed;
                Btn_EditCompany.Content = "Edytuj";
            }
        }

      
    }
}
