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
    /// Interaction logic for AddUpdateCustomerDlg.xaml
    /// </summary>
    public partial class AddUpdateCustomerDlg : Window
    {
        Customer currCust;
        public AddUpdateCustomerDlg(Customer currCust = null)
        {
            this.currCust = currCust;
            InitializeComponent();
            if (currCust != null)
            {
                TbxName.Text = currCust.Name;
                TbxAddress.Text = currCust.Address;
                TbxEmail.Text = currCust.Email;
                TbxPhone.Text = currCust.Phone;
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

                if (currCust != null)
                {//update
                    currCust.Name = TbxName.Text;
                    currCust.Address = TbxAddress.Text;
                    currCust.Email = TbxEmail.Text;
                    currCust.Phone = TbxPhone.Text;

                }
                else
                { //add
                    string name = TbxName.Text;
                    string address = TbxAddress.Text;
                    string email = TbxEmail.Text;
                    string phone = TbxPhone.Text;

                    Globals.DbContext.Customers.Add(new Customer() { Name = name, Address = address, Email = email, Phone = phone });

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
            TbxAddress.Text = "";
            TbxEmail.Text = "";
            TbxPhone.Text = "";


        }
    }
}
