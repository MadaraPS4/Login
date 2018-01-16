using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            while (true)
            {
                if (textbox3.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                    break;
                }
                else if (password1.Password == "")
                {
                    MessageBox.Show("Заполните все поля");
                    break;
                }
                else if (password2.Password == "")
                {
                    MessageBox.Show("Заполните все поля");
                    break;
                }
                else if (password1.Password != password2.Password)
                {
                    MessageBox.Show("Пароли не совпадают");
                    break;
                }
                else if(textbox6.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                    break;
                }
                else if (image.Source == null)
                {
                    MessageBox.Show("Выберите фото, пожалуиста");
                    break;
                }
                else if (textBox.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                    break;
                }
                else
                {
                    string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

                    while (true)
                    {
                        string email = textbox6.Text;
                        if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                        {
                            string path = @"C:\Users\ЗейтынБ\Documents\Visual Studio 2017\Projects\users.json";
                            if (!File.Exists(path))
                            {
                                List<User> user = new List<User>();
                                user.Add(new User { Name = textbox3.Text, Password = password1.Password, Email = textbox6.Text });
                                File.WriteAllText(path, JsonConvert.SerializeObject(user));
                                MessageBox.Show("Поздравляю ваша региcтрация прошла успешно");
                                textbox3.Text = "";
                                password1.Password = "";
                                password2.Password = "";
                                textbox6.Text = "";
                                textBox.Text = "";
                                image.Source = null;
                                return;
                            }
                            else
                            {
                                string json = File.ReadAllText(path);
                                var deserialize = JsonConvert.DeserializeObject<List<User>>(json);
                                foreach (var q in deserialize)
                                {
                                    if (q.Email == textbox6.Text)
                                    {
                                        MessageBox.Show("Пользователь уже существует");
                                        textbox3.Text = "";
                                        password1.Password = "";
                                        password2.Password = "";
                                        textbox6.Text = "";
                                        textBox.Text = "";
                                        image.Source = null;
                                        return;
                                    }
                                }
                                deserialize.Add(new User { Name = textbox3.Text, Password = password1.Password, Email = textbox6.Text });
                                var jsonToOutput = JsonConvert.SerializeObject(deserialize, Formatting.Indented);
                                File.WriteAllText(path, jsonToOutput);
                                MessageBox.Show("Поздравляю ваша региcтрация прошла успешно");
                                textbox3.Text = "";
                                password1.Password = "";
                                password2.Password = "";
                                textbox6.Text = "";
                                textBox.Text = "";
                                image.Source = null;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Некорректный email");
                            textbox6.Text = "";
                            return;
                        }
                    }
                }              
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();

            of.Filter = "png files (*.png)|*.png";

            if (of.ShowDialog() == true)
            {
                BitmapImage bimage = new BitmapImage();
                bimage.BeginInit();
                bimage.UriSource = new Uri(of.FileName, UriKind.Absolute);
                bimage.EndInit();
                image.Source = bimage;
            }

        }
    }

}