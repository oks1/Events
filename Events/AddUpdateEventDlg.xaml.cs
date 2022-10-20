using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Events
{
     public partial class AddUpdateEventDlg : Window
    {
        EventDetail currentEvent;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try

            {
               //Globals.DbContext = new EventsDbConnection();
                ComboCustomer.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM Customers
                ComboLocation.ItemsSource = Globals.DbContext.Locations.ToList();
                ComboStaff.ItemsSource = Globals.DbContext.Staffs.ToList();
                //List<Service> services = new List<Service>();
                //DgService.ItemsSource = Globals.DbContext.Services.ToList();
                

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
                ComboCustomer.Text = currentEvent.Customer.Name.ToString();
                ComboStaff.Text = currentEvent.Staff.Name.ToString();
                TbxSliderDur.Text = currentEvent.Duration.ToString();
                TbxDate.Text = currentEvent.Date.ToString();
                TbxGuests.Text = currentEvent.Guests.ToString();
                ComboLocation.Text = currentEvent.Location.Name.ToString();
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
                    currentEvent.Duration = (int)SliderDur.Value;
                    
                    DateTime.TryParse(TbxDate.Text, out DateTime eventDate);
                    currentEvent.Date = eventDate;
                   // currentEvent.Guests = int.Parse(TbxGuests.Text);

                    //currentEvent.TotalAmount = decimal.Parse(TbxTotal.Text);
                    
                }
                else
                { //add
                    string typeOfEvent= ComboTypes.SelectedValue?.ToString();

                    int customerId = int.Parse(ComboCustomer.SelectedIndex.ToString());
                
                    int staffId = int.Parse(ComboStaff.SelectedValue?.ToString());

                    int duration = (int)SliderDur.Value;
                    
                    DateTime.TryParse(TbxDate.Text, out DateTime eventDate);
                    
                    int guests = int.Parse(TbxGuests.Text);
                    
                    int locationId = int.Parse(ComboLocation.SelectedValue?.ToString());
                    decimal total = decimal.Parse(TbxTotal.Text);
                    //decimal total = decimal.Parse(guests.ToString());
                    //currentEvent.TotalAmount = currentEvent.Staff.WagePerProject +
                    //    (currentEvent.Guests * currentEvent.Location.PricePerPerson);
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

        private void ComboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentEvent != null)
            {
                currentEvent.CustomerId = int.Parse(ComboCustomer.SelectedValue?.ToString());
            //    currentEvent.TotalAmount = currentEvent.Staff.WagePerProject +
            //                (currentEvent.Guests * currentEvent.Location.PricePerPerson);
            //    TbxTotal.Text = currentEvent.TotalAmount.ToString();
            //
            }

        }

        private void ComboLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentEvent != null)
            {
                currentEvent.LocationId = int.Parse(ComboLocation.SelectedValue?.ToString());
               // currentEvent.TotalAmount = currentEvent.Staff.WagePerProject +
              //              (currentEvent.Guests * currentEvent.Location.PricePerPerson);
               // TbxTotal.Text = currentEvent.TotalAmount.ToString();
            }
        }

        private void ComboStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentEvent != null)
            {
                currentEvent.StaffId = int.Parse(ComboStaff.SelectedValue?.ToString());
                //currentEvent.TotalAmount = currentEvent.Staff.WagePerProject +
                //            (currentEvent.Guests * currentEvent.Location.PricePerPerson);
                //TbxTotal.Text = currentEvent.TotalAmount.ToString();
            }
        }

        //private void TbxGuests_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (currentEvent != null)
        //    {
        //        currentEvent.Guests = int.Parse(TbxGuests.Text);
        //        currentEvent.TotalAmount = currentEvent.Staff.WagePerProject +
        //                    (currentEvent.Guests * currentEvent.Location.PricePerPerson);
        //        TbxTotal.Text = currentEvent.TotalAmount.ToString();
        //    }
        //}
    }
}
