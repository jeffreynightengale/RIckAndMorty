using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace RickAMorty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            cboCharacters.Items.Clear();

            using (var client = new HttpClient())
            {
                string urlBeg = "https://rickandmortyapi.com/api/character?page=";
                double page = 1;

                while (page < 50)
                {
                    string url = string.Concat(urlBeg, page.ToString());
                    string jsonData = client.GetStringAsync(url).Result;
                    RickAndMortyAPI api = JsonConvert.DeserializeObject<RickAndMortyAPI>(jsonData);

                    foreach (Character item in api.results)
                    {
                        cboCharacters.Items.Add(item);
                    }
                    page++;
                }
            }
        }

        private void cboCharacters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Character selected = (Character)cboCharacters.SelectedItem;

            imgCharacter.Source = new BitmapImage(new Uri(selected.image));
            lblName.Content = selected.name;

        }
    }
}
