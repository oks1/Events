using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Events
{
    /// <summary>
    /// Interaction logic for AddUpdateStaffDlg.xaml
    /// </summary>
    public partial class AddUpdateStaffDlg : Window
    {
        Staff currentStaff;
        public AddUpdateStaffDlg(Staff currentStaff = null)
        {
            InitializeComponent();
            this.currentStaff = currentStaff;
            if (currentStaff != null)
            {
                TbxName.Text = currentStaff.Name;
                TbxTitle.Text = currentStaff.JobTitle;
                TbxEmail.Text = currentStaff.Email;
                TbxPassword.Password = currentStaff.Password;
                TbxPhone.Text = currentStaff.Phone;
                TbxAddress.Text = currentStaff.Address;
                TbxWage.Text = currentStaff.WagePerProject.ToString();
                TbxPhoto.Text = currentStaff.PhotoId.ToString();
                btSave.Content = "Update";

            }
            else
            {
                btSave.Content = "Add";
            }

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (currentStaff != null)
                {//update
                    currentStaff.Name = TbxName.Text;
                    currentStaff.JobTitle = TbxTitle.Text;
                    currentStaff.Email = TbxEmail.Text;
                    currentStaff.Password = TbxPassword.Password;
                    currentStaff.Phone = TbxPhone.Text;
                    currentStaff.Address = TbxAddress.Text;
                    currentStaff.WagePerProject = int.Parse(TbxWage.Text);
                    currentStaff.PhotoId = int.Parse(TbxPhoto.Text);
                
                }
                else
                { //add
                    string name = TbxName.Text;
                    string title = TbxTitle.Text;
                    string email = TbxEmail.Text;
                    string password = TbxPassword.Password;
                    string phone = TbxPhone.Text;
                    string address = TbxAddress.Text;
                    int wage = int.Parse(TbxWage.Text);
                    int photoId = int.Parse(TbxPhoto.Text);
                    
                    Globals.DbContext.Staffs.Add(new Staff() { Name = name, JobTitle=title, Email = email, Password=password, Phone = phone, Address = address, WagePerProject = wage, PhotoId=photoId  });

                }

                Globals.DbContext.SaveChanges();
                this.DialogResult = true; // dismiss the dialog

                //    Console.WriteLine($"Customer name: {name}");
                ResetFields();


            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetFields()
        {
            TbxName.Text = "";
            TbxTitle.Text = "";
           TbxEmail.Text = "";
            TbxPassword.Password = "";
            TbxPhone.Text = ""; 
            TbxAddress.Text = "";
            TbxWage.Text = "";
            TbxPhoto.Text = "";
            


        }
    }
}
