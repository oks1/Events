using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

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
        private void BtnCloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (currCust != null)
                {//update

                    try
                    {
                        currCust.Name = TbxName.Text;
                        currCust.Address = TbxAddress.Text;
                        currCust.Email = TbxEmail.Text;
                        currCust.Phone = TbxPhone.Text;

                        if (currCust.Name.Length < 3)
                        {
                            MessageBox.Show("Name must be 3 or more characters", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                       if (currCust.Address.Length < 1)
                        {
                            MessageBox.Show("Address must be 1 or more characters", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                       if (!Regex.IsMatch(currCust.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                        {
                            MessageBox.Show("Email format", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                       if (!Regex.IsMatch(currCust.Phone, @"^[0-9]{10}"))
                        {
                            MessageBox.Show("Phone must be in a digital format (10 digits)", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                { //add
                    try
                    {
                        string name = TbxName.Text;
                        string address = TbxAddress.Text;
                        string email = TbxEmail.Text;
                        string phone = TbxPhone.Text;

                        if (name.Length < 3)
                        {
                            MessageBox.Show("Name must be 3 or more characters", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        if (address.Length < 1)
                        {
                            MessageBox.Show("Address must be 1 or more characters", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                        {
                            MessageBox.Show("Email format", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        if (!Regex.IsMatch(phone, @"^[0-9]{10}"))
                        {
                            MessageBox.Show("Phone must be in a digital format (10 digits)", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        Globals.DbContext.Customers.Add(new Customer() { Name = name, Address = address, Email = email, Phone = phone });

                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                 //   Globals.DbContext.Customers.Add(new Customer() { Name = name, Address = address, Email = email, Phone = phone });

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
