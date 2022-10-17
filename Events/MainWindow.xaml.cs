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
                Globals.DbContext = new EventsDbConnection1();
                LvCustomers.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM Customers

                Globals.DbContext = new EventsDbConnection1();
                LvEvents.ItemsSource = Globals.DbContext.EventDetails.ToList(); // equivalent of SELECT * FROM Events
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

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

                LvCustomers.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM people
                                                                               
                Console.WriteLine("Customer was added");

            }


        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddEvent_Click(object sender, RoutedEventArgs e)
        {

            AddUpdateEventDlg dialog = new AddUpdateEventDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {

                LvEvents.ItemsSource = Globals.DbContext.Customers.ToList(); // equivalent of SELECT * FROM people

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

        private void BtnCloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
