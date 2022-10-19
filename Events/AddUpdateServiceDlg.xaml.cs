using System;
using System.Windows;


namespace Events
{

    /// <summary>
    /// Interaction logic for AddUpdateServiceDlg.xaml
    /// </summary>
    /// 

    public partial class AddUpdateServiceDlg : Window
    {

        Service currService;
        public AddUpdateServiceDlg(Service currService = null)
        {
            this.currService = currService;
            InitializeComponent();
            if (currService != null)
            {
                TbxName.Text = currService.ServiceName;
                TbxPrice.Text = currService.PricePerHour.ToString(); ;
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

                if (currService != null)
                {//update
                    currService.ServiceName = TbxName.Text;
                    currService.PricePerHour = int.Parse(TbxPrice.Text);

                }
                else
                { //add
                    string serviceName = TbxName.Text;
                    decimal pricePerHour = int.Parse(TbxPrice.Text);
                    Globals.DbContext.Services.Add(new Service() { ServiceName = serviceName, PricePerHour = pricePerHour });

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
          
            TbxPrice.Text = "";

        }
    }
}

