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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Events
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Customers
                Globals.DbContext = new EventsDbConnection();
                LvCustomers.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM Customers

                LvEvents.ItemsSource = Globals.DbContext.EventDetails.ToList(); // equivalent of SELECT * FROM Events

                LvStaffs.ItemsSource = Globals.DbContext.Staffs.ToList(); // equivalent of SELECT * FROM Staffs

                LvLocations.ItemsSource = Globals.DbContext.Locations.ToList(); // equivalent of SELECT * FROM Locations

                LvServices.ItemsSource = Globals.DbContext.Services.ToList(); // equivalent of SELECT * FROM Services
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

        }
        private void BtnCloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer currSelCustomer = LvCustomers.SelectedItem as Customer;
            if (currSelCustomer == null) return; // nothing selected
            Console.WriteLine("currSelCustomer: " + currSelCustomer.Name);

           
            AddUpdateCustomerDlg dialog = new AddUpdateCustomerDlg(currSelCustomer);

            dialog.Owner = this;
            // modal = parent is not accessible for input while dialog is shown
            if (dialog.ShowDialog() == true)
            {
                LvCustomers.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM Customers
            }
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            AddUpdateCustomerDlg dialog = new AddUpdateCustomerDlg();
            dialog.Owner = this;
          
            if (dialog.ShowDialog() == true)
            {

                LvCustomers.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM Customers

                Console.WriteLine("Customer was added");

            }


        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer currSelCustomer = LvCustomers.SelectedItem as Customer;
            if (currSelCustomer == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this Customer?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;


            
              Globals.DbContext.Customers.Remove(currSelCustomer);
              Globals.DbContext.SaveChanges();
           


            LvCustomers.ItemsSource = Globals.DbContext.Customers.ToList();
        }

        private void BtnAddEvent_Click(object sender, RoutedEventArgs e)
        {

            AddUpdateEventDlg dialog = new AddUpdateEventDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {

                LvEvents.ItemsSource = Globals.DbContext.EventDetails.ToList(); // equivalent of SELECT * FROM people

                Console.WriteLine("Event was added");

            }

            
        }

        private void BtnUpdateEvent_Click(object sender, RoutedEventArgs e)
        {
            EventDetail currSelEvent = LvEvents.SelectedItem as EventDetail;
            if (currSelEvent == null) return; // nothing selected
            Console.WriteLine("currSelEvent: " + currSelEvent.TypeOfEvent);


            AddUpdateEventDlg dialog = new AddUpdateEventDlg(currSelEvent);

            dialog.Owner = this;
            // modal = parent is not accessible for input while dialog is shown
            if (dialog.ShowDialog() == true)
            {
                LvEvents.ItemsSource = Globals.DbContext.EventDetails.ToList(); // equivalent of SELECT * FROM Events

            }
        }

        private void BtnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateStaffDlg dialog = new AddUpdateStaffDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {

                LvStaffs.ItemsSource = Globals.DbContext.Staffs.ToList(); // equivalent of SELECT * FROM people

                Console.WriteLine("Staff was added");

            }
        }

        private void BtnUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            Staff currSelStaff = LvStaffs.SelectedItem as Staff;
            if (currSelStaff == null) return; // nothing selected
            Console.WriteLine("currSelStaff: " + currSelStaff.Name);


            AddUpdateStaffDlg dialog = new AddUpdateStaffDlg(currSelStaff);

            dialog.Owner = this;
            // modal = parent is not accessible for input while dialog is shown
            if (dialog.ShowDialog() == true)
            {
                LvStaffs.ItemsSource = Globals.DbContext.Staffs.ToList(); // equivalent of SELECT * FROM Customers

            }
        }

        private void BtnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateLocationDlg dialog = new AddUpdateLocationDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {

                LvLocations.ItemsSource = Globals.DbContext.Locations.ToList(); // equivalent of SELECT * FROM people

                Console.WriteLine("Location was added");

            }
        }

        private void BtnUpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            Location currSelLocation = LvLocations.SelectedItem as Location;
            if (currSelLocation == null) return; // nothing selected
            Console.WriteLine("currSelLocation: " + currSelLocation.Name);


            AddUpdateLocationDlg dialog = new AddUpdateLocationDlg(currSelLocation);

            dialog.Owner = this;
            // modal = parent is not accessible for input while dialog is shown
            if (dialog.ShowDialog() == true)
            {
                LvLocations.ItemsSource = Globals.DbContext.Locations.ToList(); // equivalent of SELECT * FROM Customers

            }
        }

        //Service

        private void BtnUpdateService_Click(object sender, RoutedEventArgs e)
        {
            Service currSelService = LvServices.SelectedItem as Service;
            if (currSelService == null) return; // nothing selected
            Console.WriteLine("currSelService: " + currSelService.ServiceName);


            AddUpdateServiceDlg dialog = new AddUpdateServiceDlg(currSelService);

            dialog.Owner = this;
            // modal = parent is not accessible for input while dialog is shown
            if (dialog.ShowDialog() == true)
            {
                LvServices.ItemsSource = Globals.DbContext.Services.ToList(); // equivalent of SELECT * FROM Service
            }
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {

            AddUpdateServiceDlg dialog = new AddUpdateServiceDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {

                LvServices.ItemsSource = Globals.DbContext.Services.ToList();

                Console.WriteLine("Service was added");

            }


        }

        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            Service currSelService = LvServices.SelectedItem as Service;
            if (currSelService == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this Service?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            Globals.DbContext.Services.Remove(currSelService);
            Globals.DbContext.SaveChanges();
            LvServices.ItemsSource = Globals.DbContext.Services.ToList();
        }
        
        private void LvCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Customer currSelTask = LvCustomers.SelectedItem as Customer;     
            if (currSelTask != null) 
            {
                Customer cust = Globals.DbContext.Customers.Find(currSelTask.Id);
                LvEventsOfCustomer.ItemsSource = cust.EventDetails.ToList();

            }

        }

        private void LvEventsOfCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EventDetail currSelEvent = LvEventsOfCustomer.SelectedItem as EventDetail;
            if (currSelEvent == null) return; // nothing selected
            Console.WriteLine("currSelEvent: " + currSelEvent.TypeOfEvent);


            AddUpdateEventDlg dialog = new AddUpdateEventDlg(currSelEvent);

            dialog.Owner = this;
            // modal = parent is not accessible for input while dialog is shown
            if (dialog.ShowDialog() == true)
            {
                LvEventsOfCustomer.ItemsSource = Globals.DbContext.EventDetails.ToList(); // equivalent of SELECT * FROM Events
                LvEvents.ItemsSource = Globals.DbContext.EventDetails.ToList(); // equivalent of SELECT * FROM Events

            }
        }

        private void BtnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            Location currSelLocation = LvLocations.SelectedItem as Location;
            if (currSelLocation == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this Location?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            Globals.DbContext.Locations.Remove(currSelLocation);
            Globals.DbContext.SaveChanges();

            LvLocations.ItemsSource = Globals.DbContext.Locations.ToList();
        }

        private void BtnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            Staff currSelStaff = LvStaffs.SelectedItem as Staff;
            if (currSelStaff == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this row?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            Globals.DbContext.Staffs.Remove(currSelStaff);
            Globals.DbContext.SaveChanges();

            LvStaffs.ItemsSource = Globals.DbContext.Staffs.ToList();
        }
    }
}
