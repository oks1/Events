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
using System.Xml.Linq;

namespace Events
{
    /// <summary>
    /// Interaction logic for AddUpdateEventDlg.xaml
    /// </summary>
    /// 

    

    public partial class AddUpdateEventDlg : Window
    {
        EventDetail currentEvent;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Customers

                Globals.DbContext = new EventsDbConnection1();

                ComboCustomer.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM Customers
                                                                                  // Console.Write(ComboCustomer.ItemsSource.ToString());
                                                                                  //List<Customer> CustomerList = new List<Customer>();
                                                                                  //int num = Globals.DbContext.Customers.ToList().Count;

                //for (int i = 0; i < num; i++)
                //{
                //    ComboCustomer.Items.Add(ComboCustomer.Items[i]);
                //     //   (Customer.Name[i]["itemname"].tostring());
                //       // items.add(ComboCustomer.itemssource[i]["itemname"].tostring());
                //}
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

        }

        public AddUpdateEventDlg(EventDetail currentEvent=null)
        {
            InitializeComponent();
            this.currentEvent = currentEvent;

            if (currentEvent != null)
            {

                ComboTypes.SelectedValue = currentEvent.TypeOfEvent;
                Customer currSelCust = ComboCustomer.SelectedValue as Customer;
                Console.WriteLine(currSelCust.Name);
                Customer customerID = Globals.DbContext.Customers.Find(currSelCust.Name);

                //ComboCustomer.SelectedValue = customerID.Id;
                currentEvent.CustomerId = customerID.Id;
                //Customer currSelCust = ComboCustomer.SelectedValue as Customer;
                //ComboCustomer.SelectedValue = currentEvent.Name;
                //Customer customerID = Globals.DbContext.Customers.Find(currSelCust.Id);
                //ComboCustomer.SelectedValue = currentEvent.CustomerId.ToString();
                //ComboCustomer.SelectedValue = Globals.DbContext.Customers.Find(currSelCust.Id);
                TbxCustomer.Text = currentEvent.CustomerId.ToString();
                TbxStaff.Text = currentEvent.StaffId.ToString();
                Console.WriteLine(TbxStaff.Text);
                TbxSliderDur.Text = currentEvent.Duration.ToString();
                TbxDate.Text = currentEvent.Date.ToString();
                TbxGuests.Text = currentEvent.Guests.ToString();
                TbxLocation.Text = currentEvent.LocationId.ToString();
                TbxTotal.Text=currentEvent.TotalAmount.ToString();
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

                if (currentEvent != null)
                {//update
                    currentEvent.TypeOfEvent= ComboTypes.SelectedValue?.ToString();

                    //currentEvent.CustomerId = int.Parse(TbxCustomer.Text);
                    //currentEvent.CustomerId = customerID.Id;
                    currentEvent.StaffId = int.Parse(TbxStaff.Text);
                    
                    currentEvent.Duration = (int)SliderDur.Value;
                    
                    DateTime.TryParse(TbxDate.Text, out DateTime eventDate);
                    currentEvent.Date = eventDate;
                    currentEvent.Guests = int.Parse(TbxGuests.Text);

                    currentEvent.LocationId = int.Parse(TbxLocation.Text);
                    currentEvent.TotalAmount = decimal.Parse(TbxTotal.Text);
                    

                }
                else
                { //add

                    string typeOfEvent= ComboTypes.SelectedValue?.ToString();
                    

                    //Customer customID = Globals.DbContext.Customers.Find(customer);
                    // Console.Write(customID);
                    //int customerId = customID.Id;
                    int customerId = int.Parse(ComboCustomer.SelectedValue?.ToString());

                    int staffId = int.Parse(TbxStaff.Text);
                    
                    int duration = (int)SliderDur.Value;
                    
                    DateTime.TryParse(TbxDate.Text, out DateTime eventDate);
                    
                    int guests = int.Parse(TbxGuests.Text);
                    
                    int locationId = int.Parse(TbxLocation.Text);
                    decimal total = decimal.Parse(TbxTotal.Text);
                    string note = TbxNote.Text;
                    //string note = ComboCustomer.SelectedValue?.ToString();
                    Globals.DbContext.EventDetails.Add(new EventDetail() { TypeOfEvent= typeOfEvent, CustomerId = customerId, StaffId = staffId, Duration=duration, Date=eventDate, Guests=guests, LocationId=locationId, TotalAmount=total,
                    Notes=note});

                }

                Globals.DbContext.SaveChanges();
                this.DialogResult = true; // dismiss the dialog

                //    Console.WriteLine($"Customer name: {name}");
              //  ResetFields();


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
            //TbxName.Text = "";
            //TbxAddress.Text = "";
            //TbxEmail.Text = "";
            //TbxPhone.Text = "";


        }

       
    }
}
