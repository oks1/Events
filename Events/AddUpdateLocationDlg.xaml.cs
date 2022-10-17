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
    /// Interaction logic for AddUpdateLocationDlg.xaml
    /// </summary>
    public partial class AddUpdateLocationDlg : Window
    {
        Location currLocation;
        public AddUpdateLocationDlg(Location currLocation=null)
        {
            InitializeComponent();
            this.currLocation = currLocation;

            if (currLocation != null)
            {
                TbxName.Text = currLocation.Name;
                TbxContact.Text = currLocation.ContactName;
                TbxEmail.Text = currLocation.Email;
                TbxPhone.Text = currLocation.Phone;
                TbxAddress.Text = currLocation.Address;
                TbxPrice.Text = currLocation.PricePerPerson.ToString();
                
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

                if (currLocation != null)
                {//update
                    currLocation.Name = TbxName.Text;
                    currLocation.ContactName = TbxContact.Text;
                    currLocation.Email = TbxEmail.Text;
                   
                    currLocation.Phone = TbxPhone.Text;
                    currLocation.Address = TbxAddress.Text;
                    currLocation.PricePerPerson = decimal.Parse(TbxPrice.Text);
              
                }
                else
                { //add
                    string name = TbxName.Text;
                    string contact = TbxContact.Text;
                    string email = TbxEmail.Text;
                    
                    string phone = TbxPhone.Text;
                    string address = TbxAddress.Text;
                    decimal price = decimal.Parse(TbxPrice.Text);
                    

                    Globals.DbContext.Locations.Add(new Location() { Name = name, ContactName = contact, Email = email, Phone = phone, Address = address, PricePerPerson = price});

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
            TbxContact.Text = "";
            TbxEmail.Text = "";
            
            TbxPhone.Text = "";
            TbxAddress.Text = "";
            TbxPrice.Text = "";
            



        }
    }
}
