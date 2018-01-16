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
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                string path = @"C:\Users\ЗейтынБ\Documents\Visual Studio 2017\Projects\users.json";
                if (textbox1.Text == "")
                {
                    MessageBox.Show("Введите логин");
                    return;
                }
                else if (password1.Password== "")
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }
                else
                {
                    string json="";     
                    try
                    {
                        json= File.ReadAllText(path);
                    }
                    catch(Exception z)
                    {
                        MessageBox.Show("Зарегистрируйтесь пожалуйста!");
                        textbox1.Text = "";
                        password1.Password = "";
                        return;
                    }

                    JArray jarray = JArray.Parse(json);

                    for(int i=0; i<jarray.Count; i++)
                    {
                        if (jarray[i]["Name"].ToString() == textbox1.Text && jarray[i]["Password"].ToString() == password1.Password)
                        {
                            MessageBox.Show("Добро пожаловать");
                            Window2 window = new Window2();
                            window.label1.Content += " " + textbox1.Text;
                            window.Show();
                            this.Close();
                            return;
                        }          
                    }
                    MessageBox.Show("Неправильное имя или пароль");
                    textbox1.Text = "";
                    password1.Password = "";
                    return;
                }
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }
    }
}
