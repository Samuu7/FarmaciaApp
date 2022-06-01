using System;
using System.Collections.Generic;
using System.Windows;
using FarmaciaApp.Entity;
using FarmaciaApp.API;
using System.Linq;
using MongoDB.Bson;
using MongoExample.Entity;

namespace FarmaciaApp
{
    public partial class FinestraProducte : Window
    {
        Producte ProducteInicial = new Producte();
        public FinestraProducte()
        {
            InitializeComponent();
        }
        public FinestraProducte(String id)
        {
            ApiProducte TAPI = new ApiProducte();
            GetProducte(id);
        }

        //Agafem els productes
        private async void GetProducte(string id)
        {
            ApiProducte TAPI = new ApiProducte();
            ProducteInicial = await TAPI.GetProducteAsync(id);
            this.DataContext = ProducteInicial;
            InitializeComponent();
        }

        //Afegir producte
        private async void Button_Click(object sender, RoutedEventArgs e)
        {   
            if(tbNom.Text != "" && tbDescripcio.Text != "" && tbDCreacio.SelectedDate != null  && lbRepresentant.SelectedItem != null)
            {
                Producte productes = new Producte();
                productes.Name = tbNom.Text;
                productes.Descripcio = tbDescripcio.Text;
                productes.Data = (DateTime)tbDCreacio.SelectedDate;
                productes.Stock = Int32.Parse(tbStock.Text);
                
                Farmacia t1 = (Farmacia)lbRepresentant.SelectedItem;
                productes.Representant = t1.Nom;

                ApiProducte TAPI = new ApiProducte();
/*
              if (tbNom.Text == "")
                {

                  
                    await TAPI.AddAsync(productes);
                    Close();
                }
                else
                {
                    
                    productes.id = tbNom.Text;
                    await TAPI.UpdateAsync(productes);
                    Close();

               }
                */
                     productes.Name = tbNom.Text;
                    await TAPI.AddAsync(productes);
                    await TAPI.UpdateAsync(productes);
                    Close();
                

            }
            else
            {
                MessageBox.Show("S'han d'emplenar tots els camps.","Informació",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            
        }


        //Modificar Producte
        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            ApiFarmacia TAPI = new ApiFarmacia();
            lbRepresentant.ItemsSource = await TAPI.GetFarmaciaAsync();
            if (this.DataContext != null)
            {
                tbNom.Text = ((Producte)this.DataContext).Name;
                int contador = 0;
                bool trobat = false;
                int i = 0;
                List<Farmacia> farmacies = await TAPI.GetFarmaciaAsync();
                while (contador < farmacies.Count() && !trobat)
                {
                    if (farmacies[i].Nom == ((Producte)this.DataContext).Representant)
                    {
                        lbRepresentant.SelectedIndex = contador;
                        trobat = true;
                    }
                    else
                    {
                        contador++;
                    }
                    i++;
                }
                if(trobat == false)
                {
                    MessageBox.Show("Selecciona una nova farmàcia.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            else
            {
                tbNom.Text = "";

            }
        }
    }
}
