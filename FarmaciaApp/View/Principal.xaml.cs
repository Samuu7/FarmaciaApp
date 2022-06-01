using FarmaciaApp.API;
using MongoExample.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FarmaciaApp.View
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
        }

        //Botó per obrir la finestra dels productes
        private void BotoAfegirTasca_Click(object sender, RoutedEventArgs e)
        {
            FinestraProducte wnd = new FinestraProducte();
            wnd.ShowDialog();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            ActualitzarTaula();
        }


        //Funció per actualitzar la taula
        private async void ActualitzarTaula()
        {
            ApiProducte TAPI = new ApiProducte();
            List<Producte> productes = await TAPI.GetProducteAsync();
            //LlistatProductes.Items.Clear();
            LlistatProductes.ItemsSource = productes;

        }
           
        //Mètode per modificar
        private void BotoModificar_Click(object sender, RoutedEventArgs e)
        {
            ApiProducte TAPI = new ApiProducte();
            FinestraProducte fproducte = new FinestraProducte((sender as Button).Tag.ToString());
            fproducte.ShowDialog();
            ActualitzarTaula();
        }

        private async void BotoEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Vols eliminar el producte?", "Advertència", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ApiProducte TAPI = new ApiProducte();
                await TAPI.DeleteAsync((sender as Button).Tag.ToString());
                ActualitzarTaula();
            }

        }

        private void Llista_GotFocus(object sender, RoutedEventArgs e)
        {
            var selected = ((ListBox)sender).SelectedItem;
            LlistatProductes.UnselectAll();
            ((ListBox)sender).SelectedItem = selected;
        }
       
        private void BotoActualitzar_Click(object sender, RoutedEventArgs e)
        {
            ActualitzarTaula();
        }

        //Botó per obrir la finestra de les farmàcies
        private void BotoFarmacies_Click(object sender, RoutedEventArgs e)
        {
            Farmacies farmacies = new Farmacies();
            farmacies.ShowDialog();
        }

        private void Llista_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
    