using FarmaciaApp.API;
using FarmaciaApp.Entity;
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
    /// Lógica de interacció per Farmacies.xaml
    /// </summary>
    public partial class Farmacies : Window
    {
        public Farmacies()
        {
            InitializeComponent();
        }

        //Mètode per eliminar una farmacia
        private async void BotoEliminar_Click(object sender, RoutedEventArgs e)
        {
            ApiFarmacia TAPI = new ApiFarmacia();

            Farmacia tfarmacia = (Farmacia)LlistaDeFarmacies.SelectedItem;
            if (tfarmacia == null)
            {
                MessageBox.Show("No s'ha seleccionat una farmacia.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show("Vols eliminar la farmacia?", "Advertència", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Farmacia farmacia = (Farmacia)LlistaDeFarmacies.SelectedItem;
                try
                {
                    await TAPI.DeleteAsync(farmacia.Nom);
                    MessageBox.Show("S'ha eliminat la farmacia correctament.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar farmacia: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                LlistaDeFarmacies.ItemsSource = await TAPI.GetFarmaciaAsync();
            }
        }
         
        private void LlistaDeFarmacies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Farmacia farmacia = (Farmacia)LlistaDeFarmacies.SelectedItem;
            if (farmacia != null)
            {
                tbNom.Text = farmacia.Nom;
                tbCarrer.Text = farmacia.Carrer;
                tbTel.Text = farmacia.Telefon;
                
            }
        }

        //Agafem la llista de les farmàcies
        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            ApiFarmacia TAPI = new ApiFarmacia();
            LlistaDeFarmacies.ItemsSource = await TAPI.GetFarmaciaAsync();
        }

        //Mètode per modificar una farmàcia
        private async void BotoModificar_Click(object sender, RoutedEventArgs e)
        {
            Farmacia TNIF = (Farmacia)LlistaDeFarmacies.SelectedItem;
            if (TNIF == null)
            {
                MessageBox.Show("No s'ha seleccionat una farmacia.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (tbNom.Text != "" && tbTel.Text != "" && tbCarrer.Text !="")
                {
                    Farmacia MongoCodi = (Farmacia)LlistaDeFarmacies.SelectedItem;
                    Farmacia farmacia = new Farmacia
                    {
                        id = MongoCodi.id,
                        Nom = tbNom.Text,
                        Carrer = tbCarrer.Text,
                        Telefon = tbTel.Text
                        
                    };

                    ApiFarmacia TAPI = new ApiFarmacia();
                    String NIF = TNIF.Nom;
                    Farmacia tempt = await TAPI.GetFarmaciaAsync(farmacia.Nom);
                    if (tempt != null && tempt.id != farmacia.id)
                    {
                        MessageBox.Show("Ja existeix una farmacia amb aquest nom.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        await TAPI.UpdateAsync(farmacia, TNIF.Nom);
                        LlistaDeFarmacies.ItemsSource = await TAPI.GetFarmaciaAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Tots els camps son obligatoris", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        //Afegim farmacia
        private async void BotoAfegir_Click(object sender, RoutedEventArgs e)
        {
            ApiFarmacia TAPI = new ApiFarmacia();
            if (tbNom.Text != "" && tbTel.Text != "" && tbCarrer.Text != "")
            {
                Farmacia farmacies = new Farmacia
                {
                    Nom = tbNom.Text,
                    Carrer = tbCarrer.Text,
                    Telefon = tbTel.Text
                    
                };

                if (await TAPI.GetFarmaciaAsync(farmacies.Nom) != null)
                {
                    MessageBox.Show("Ja existeix una farmacia amb aquest nom.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    await TAPI.AddAsync(farmacies);
                    LlistaDeFarmacies.ItemsSource = await TAPI.GetFarmaciaAsync();
                }
            }
            else
            {
                MessageBox.Show("Tots els camps son obligatoris", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}

   