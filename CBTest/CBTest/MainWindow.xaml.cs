using CBTest.models;
using CBTest.services;
using CBTest.services.impl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace CBTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fullFileName;
        private List<DataBinding> dataBindings;

        public MainWindow()
        {
            dataBindings = new List<DataBinding>();
            InitializeComponent();
        }

        private void BtnLoadXML_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.ShowDialog();
            fullFileName = openFileDialog.FileName;
            XmlLoadService loader = new XmlLoadServiceImpl();
            List<ParticipantInfo> participants = loader.Load(fullFileName);
            DataBindingService bindingService = new DataBindingServiceImpl();
            dataBindings = bindingService.CreateDataBinding(participants);

            dgClientInfo.ItemsSource = dataBindings;
        }
    }
}
