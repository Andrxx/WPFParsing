using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.IO;
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
using WPFParsing.Methods;
using WPFParsing.Models;

namespace WPFParsing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        //string 
        public MainWindow()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            Loaded += MainWindow_Loaded;
            //var cont = db.Contractors.Count();
            var contractors = (from cont in db.Contractors
                               where  !string.IsNullOrWhiteSpace(cont.GUID)
                               select cont).ToList();
            dataGrid.ItemsSource = contractors;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {            
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("message.json");
            Contractor? contractor = Parser.Parse(json);
            if (contractor is not null)
            {
                try
                {
                    db.Contractors.Add(contractor);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        /// <summary>
        /// метод загружает данные контрагента из файла, меняя значение GUID на случайное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadNewButton_Click(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("message.json");
            Contractor? contractor = Parser.Parse(json);
            if (contractor is not null)
            {
                try
                {
                    contractor.GUID = Guid.NewGuid().ToString();
                    db.Contractors.Add(contractor);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var contractors = (from cont in db.Contractors
                               where !string.IsNullOrWhiteSpace(cont.GUID)
                               select cont).ToList();
            dataGrid.ItemsSource = contractors;
        }
    }
}