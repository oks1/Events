using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Drawing;



//using Image = System.Drawing.Image;

namespace Events
{
    /// <summary>
    /// Interaction logic for AddUpdateStaffDlg.xaml
    /// </summary>
    public partial class AddUpdateStaffDlg : Window
    {
        Staff currentStaff;
        byte[] imageBytes=null ;

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {


            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Images (*.jpg,*.png, *jpeg)|*.jpg;*.png, *jpeg|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                ImagePath.Text = dialog.FileName;
                imagePicture.Source = new BitmapImage(new Uri(ImagePath.Text));

                try
                {
                    using (var fs = new FileStream(ImagePath.Text, FileMode.Open, FileAccess.Read))
                    {
                        imageBytes = new byte[fs.Length];
                        fs.Read(imageBytes, 0, Convert.ToInt32(fs.Length));
                        // fs.Close();
                    } // FIXME: exceptions
                } catch (Exception ex) when (ex is IOException || ex is SystemException)
                {
                    MessageBox.Show(this, ex.Message, "File IO  error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

               // Console.WriteLine("Araaaaay" + imageBytes);

                ////save
                //if (!String.IsNullOrEmpty(ImagePath.Text))
                //{
                //    Globals.DbContext.Staffs.Add(new Staff()
                //    {
                //        Photo = imageBytes,
                //        ///name = ImagePath.Text
                //    }); ;
                //    Globals.DbContext.SaveChanges();

                //}
            }
            
        }


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
                //TbxPhoto.Text = currentStaff.PhotoId.ToString();
                if (currentStaff.Photo!=null) {
                    MemoryStream byteStream = new MemoryStream(currentStaff.Photo);
                    BitmapImage image = new BitmapImage(); image.BeginInit();
                    image.StreamSource = byteStream; image.EndInit();
                    imagePicture.Source = image;
                }
                
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
                    currentStaff.WagePerProject = decimal.Parse(TbxWage.Text);

                    currentStaff.Photo = imageBytes;
               
                }
                else
                { 
                    string name = TbxName.Text;
                    string title = TbxTitle.Text;
                    string email = TbxEmail.Text;
                    string password = TbxPassword.Password;
                    string phone = TbxPhone.Text;
                    string address = TbxAddress.Text;
                    decimal wage = decimal.Parse(TbxWage.Text);

                    byte[] photo = imageBytes;


                    Globals.DbContext.Staffs.Add(new Staff() { Name = name, JobTitle = title, Email = email, Password = password, Phone = phone, Address = address, WagePerProject = wage, Photo = photo });
                }
                //}

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
           // TbxPhoto.Text = "";
            
        }


        //public Image byteArrayToImage(byte[] bytes)
        //{
        //    //MemoryStream ms = new MemoryStream(byteArrayIn);
        //   // Image returnImage = Image.FromStream(ms);
        //   //return returnImage;


        //    MemoryStream byteStream = new MemoryStream(bytes); 
        //    BitmapImage image = new BitmapImage(); image.BeginInit(); 
        //    image.StreamSource = byteStream; image.EndInit(); 
        //    imagePicture.Source = image;

        //}

        //public static byte[] imgToByteConverter(Image inImg) 
        //{ ImageConverter imgCon = new ImageConverter(); 
        //    return (byte[])imgCon.ConvertTo(inImg, typeof(byte[])); }


        //public byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png.Jpg.Jpeg);
        //    return ms.ToArray();
        //}

       
    }
}
