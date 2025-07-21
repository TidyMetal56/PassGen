// Proyect: PassGen
// Author: Carlos G Vera
// Description: Solo un proyecto de repaso y un poco experimental, una herramienta para la generacion de contraseñas seguras dando opciones de los elementos o longitud de la contraseña
// Creation Date: 06/12/2024
// Versión: 1.0.0

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PassGen
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Metodo donde se genera la contraseña
        private void Gen()
        {   
            //variables usadas para la generacion
            String let = "ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnopqrstwxyz";
            String num = "0123456789";
            String esp = "!#$%&/()=?¡¿";

            //conversion del tipo de variable
            try
            {
                warning.Visibility = Visibility.Hidden;

                String numchar = Nbox.Text;

                int nc = int.Parse(numchar);

                bool inclunum = chkNum.IsChecked == true;

                bool incluesp = chkEsp.IsChecked == true;

                String charAva = let;

                if (inclunum)
                {
                    charAva += num; 
                }
                if (incluesp)
                {
                    charAva += esp;
                }

                Random random = new Random();

                StringBuilder password = new StringBuilder();

                //Creacion de la contraseña
                for (int i = 0; i < nc; i++)
                {
                    int index = random.Next(charAva.Length);

                    password.Append(charAva[index]);
                }

                string genpassword = password.ToString();

                Pass.Text = genpassword;
            }
            catch
            (Exception)
            {
                warning.Visibility = Visibility.Visible;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Gen();
        }

        private void Nbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Metodo creado para bloquear el ingreso de strings en Nbox
        private void NumeroTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) 
            {
                e.Handled = true; 
            }
        }

        private DispatcherTimer timer;

        //Este metodo es para configurar el temporizador
        public void Timer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
        }

        //Este metodo que será llamado cuando el temporizador termine
        private void Timer_Tick(object sender, EventArgs e)
        {
            Copy.Visibility = Visibility.Collapsed;
            Copynt.Visibility = Visibility.Collapsed;

            timer.Stop();
        }

        //Este metodo es para los mensajes cuando se tiene un texto para copiar o no
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if(!String.IsNullOrEmpty(Pass.Text))
            {
                Clipboard.SetText(Pass.Text);
                Copy.Visibility = Visibility.Visible;

                Timer();
                timer.Start();
            }
            else
            {
                Copynt.Visibility = Visibility.Visible;

                Timer();
                timer.Start();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            this.DragMove();
        }

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
