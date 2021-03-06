﻿using System;
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
//Añadido
using System.IO;
using System.Diagnostics; //Este espacio de nombre puede abrir procesos para abrir paginas webs (github)
using Microsoft.Win32;
using System.Windows.Forms;
using System.Threading;

namespace ExtractorFicherosProgramaciónDAM
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

        /// <summary>
        /// Método principal del programa. Realiza la extracción de los ficheros utilizando la clase Extractor.
        /// </summary>
        public void Ejecucion()
        {
            try
            {
                Extractor.BusquedaPrimera(Path_Origen.Text, Path_Destino.Text, (bool)autocompresion.IsChecked,nombre_compresion.Text); // Realizar la extracción.

                // Si todo ha ido bien, el porgrama continua por aquí, de lo contrario, de lo contrario, salta el catch.
                TextBox_TodoBien.Text = "Extracción de ficheros completado... Todo correcto";
                TextBox_TodoBien.Foreground = Brushes.Green;
                TextBox_TodoBien.Opacity = 100;
                System.Windows.MessageBox.Show("Por favor, no se le olvide comprobar que todos los archivos se hayan copiado correctamente.\n\nMuchas gracias por usar el programa.\n\nSi ocurre algún error, se agradeceria que informaseis a los respectivos developers ubicado en la ventana 'Acerca de' en el menú de ayuda.", "Extracción de ficheros completado correctamente", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                TextBox_TodoBien.Text = e.Message;
                TextBox_TodoBien.Foreground = Brushes.Red;
                TextBox_TodoBien.Opacity = 100;
            }
        }
        #region Eventos
        private void MiMenu_Instrucciones_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/ElAdrixHD/ExtractorFicheros/blob/master/README.md"); // Ir al enlace con las instrucciones del programa.
        }

        private void BotonIniciar_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje para que el usuario se asegure de haber introducido correctamente la ruta.
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Estás seguro de que has introducido bien las rutas de los archivos?\n\nIMPORTANTE... ANTES DE ACEPTAR DEBES SABER QUE ESTE PROGRAMA ESTÁ AÚN EN DESARROLLO Y SE PUEDEN PRODUCIR ERRORES.\nLOS DESARROLLADORES NO NOS HACEMOS CARGO DEL MAL USO DE ESTE PROGRAMA, NI DE LAS POSIBLES PÉRDIDAS QUE SE PUEDAN PRODUCIR USANDO ESTA APLICACIÓN.", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Warning,MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Ejecucion();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void BotonExaminar_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialogo = new FolderBrowserDialog();
            
            if (e.OriginalSource == BotonExaminar_Origen) // Si el botón pulsado es el de la ruta origen.
            {
                dialogo.ShowDialog();
                Path_Origen.Text = dialogo.SelectedPath;
                if (Path_Origen.Text == "")
                {
                    Path_Origen.Text = "Path"; // Texto predeterminado para rutas vacías.
                }
            }
            else if (e.OriginalSource == BotonExaminar_Destino) //Si el botón pulsado es el de la ruta destino.
            {
                dialogo.ShowDialog();
                Path_Destino.Text = dialogo.SelectedPath;
                if (Path_Destino.Text == "")
                {
                    Path_Destino.Text = "Path"; // Texto predeterminado para rutas vacías.
                }
            }
            //Si se le indicaron todas las rutas, se activa el botón de iniciar extracción.
            if (Path_Destino.Text != "Path" && Path_Origen.Text != "Path")
            {
                BotonIniciar.IsEnabled = true;
            }
            else
            {
                BotonIniciar.IsEnabled = false;
            }
            dialogo.Dispose();
        }

        private void MiMenu_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MiMenu_Acerca_Click(object sender, RoutedEventArgs e)
        {
            AcercaDe ventana = new AcercaDe();
            ventana.ShowDialog();
        }

        private void MiMenu_Git_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == MiMenu_Git_Pablo)
                Process.Start("https://github.com/rasky0607/");
            else if (e.OriginalSource == MiMenu_Git_Adrian)
                Process.Start("https://github.com/ElAdrixHD/");
        }

        private void autocompresion_Checked(object sender, RoutedEventArgs e)
        {
            // Activar el campo de texto para el nombre del comprimido.
            nombre_compresion.IsEnabled = true;
            nombre_compresion.Focus();
            nombre_compresion.Text = string.Empty;
        }

        private void autocompresion_Unchecked(object sender, RoutedEventArgs e)
        {
            // Desactivar el campo de texto para el nombre del comprimido.
            nombre_compresion.IsEnabled = false;
            nombre_compresion.Text = "Introduce el nombre del comprimido";
        }
        #endregion
    }
}
