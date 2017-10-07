using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace TS3Sky
{
    /// <summary>
    /// PackageListItem.xaml 的交互逻辑
    /// </summary>
    public partial class PackageListItem : UserControl
    {
        public PackageListItem()
        {
            InitializeComponent();
        }

        private Package showPackage;
        public Package ShowPackage
        {
            get
            {
                return showPackage;
            }
            set
            {
                showPackage = value;
                PackageName = String.Format(TS3Sky.Language.Package.Name, value.Name);
                PackageCreator = String.Format(TS3Sky.Language.Package.Creator, value.Creator);
                PackageDescription = String.Format(TS3Sky.Language.Package.Description, value.Description);
                PackageImage = value.ImageFile;
            }
        }

        private string name;
        public string PackageName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PackageNameText.Text = value;
            }
        }
        private string creator;
        public string PackageCreator
        {
            get
            {
                return creator;
            }
            set
            {
                creator = value;
                PackageCreatorText.Text = value;
            }
        }
        private string description;
        public string PackageDescription
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                PackageDescriptionText.Text = value;
            }
        }

        private string image;
        public string PackageImage
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                try
                {
                    // 读取图片源文件到byte[]中
                    BinaryReader binReader = new BinaryReader(File.Open(value, FileMode.Open));
                    FileInfo fileInfo = new FileInfo(value);
                    byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                    binReader.Close();
                    // 将图片字节赋值给Bitmap Image
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(bytes);
                    bitmap.EndInit();
                    // 最后给Image控件赋值
                    PreviewImage.Source = bitmap;
                }
                catch { }
            }
        }
    }
}
