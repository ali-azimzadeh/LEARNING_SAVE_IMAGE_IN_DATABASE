using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEARNING_SAVE_IMAGE_IN_DATABASE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MyDataSetTableAdapters.PersonTableAdapter personAdapter =
                new MyDataSetTableAdapters.PersonTableAdapter();

            byte[] myImage = 
                ImageToByteArray(myPictureBox.Image);

            personAdapter.Insert(Id: 100, Avatar: myImage);
        }

        public byte[] ImageToByteArray(System.Drawing.Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return ms.ToArray();

            //using (var ms = new MemoryStream())
            //{
            //    image.Save(ms, image.RawFormat);
            //    return ms.ToArray();
            //}
        }

        private void BrowsButton_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                myPictureBox.Image = 
                    Image.FromFile(openFileDialog.FileName);
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            MyDataSetTableAdapters.PersonTableAdapter personAdapter =
               new MyDataSetTableAdapters.PersonTableAdapter();
        
            Image image = 
                GetImageFromByteArray(personAdapter.GetData()[0].Avatar);

            myPictureBox.Image = image;
        }

        public static Bitmap GetImageFromByteArray(byte[] byteArray)
        {
            ImageConverter im = new ImageConverter();

            Bitmap bm = (Bitmap)im.ConvertFrom(byteArray);

            return bm;
        }
    }
}
