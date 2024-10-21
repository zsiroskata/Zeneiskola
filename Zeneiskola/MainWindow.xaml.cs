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
using Zeneiskola.src;

namespace Zeneiskola
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Hangverseny> hangversenyek = new();
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new StreamReader(
                    path: @"..\..\..\src\zene.txt",
                    encoding: Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                hangversenyek.Add(new Hangverseny(sr.ReadLine()));
            }

            sr.Close();

            //1.feladat
            for (int i = 0; i < hangversenyek.Count; i++)
            {
                if (!cim.Items.Contains(hangversenyek[i].Cim))
                {
                   cim.Items.Add(hangversenyek[i].Cim);
                }   
            }
            //2.feladat
            List<string> abcSzerzo = new();
            for (int i = 0; i < hangversenyek.Count; i++)
            {
                if (!abcSzerzo.Contains(hangversenyek[i].Szerzo))
                {
                    abcSzerzo.Add(hangversenyek[i].Szerzo);
                }
            }
   
            abcSzerzo.Sort();

            foreach (var item in abcSzerzo)
            {
                szerzo.Items.Add(item);
            }

        }

        //3.feladat
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var min = hangversenyek.Min(x => x.Ev);
            var max = hangversenyek.Max(x => x.Ev);
            harmadikF.Text = $"A komolyzenei darabok keletkezései éve {min} és {max} között terjed";
        }

        private void negyedikB_Click(object sender, RoutedEventArgs e)
        {
            //4.feladat
            int nehezsegSzam = 0;
            for (int i = 0; i < hangversenyek.Count; i++)
            {
                if (hangversenyek[i].Nehezseg == 10)
                {
                    nehezsegSzam++;
                }
            }
            negyedikF.Text = $"Az adatfáljban {nehezsegSzam}db 10-es nézségű komoly zenei darab van.";
        }

        //5.feladat
        private void otodikB_Click(object sender, RoutedEventArgs e)
        {
            int szam = 0;
            for (int i = 0; i < hangversenyek.Count; i++)
            {
                if (hangversenyek[i].Szerzo.Contains("Debussy"))
                {
                    szam++; 
                }
            }
            otodikF.Text = $"Az adatfáljba {szam} Debussy szerzőjű komolyzenei darab van.";


        }
        //6.feladat
        private void General_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dc = new();
            List<string> list = new();
            List<int> atlag = new();
            Random rnd = new Random();
            int szam = 0;
            using (StreamWriter sw = new StreamWriter(path: @"..\..\..\src\zenemuvek.txt"))
            {
                for (int i = 0; i < hangversenyek.Count; i++)
                {
                    szam = rnd.Next(0, hangversenyek.Count);
                    if (list.Count < 15 && !list.Contains(hangversenyek[szam].Cim))
                    {
                        dc.Add(hangversenyek[szam].Cim, hangversenyek[szam].Szerzo);
                        list.Add(hangversenyek[szam].Cim);
                        atlag.Add(hangversenyek[szam].Nehezseg);
                    }
                }
                var av = atlag.Average();
                sw.WriteLine($"Átlagos nehézség: {Math.Round(av)}");
                foreach (var item in dc)
                {
                    sw.Write($"{item.Value} - {item.Key} |");
                }
                sw.Close();
            }
        }

        //7.feladat
        private void keres_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new();
            var index = 0;

            if (string.IsNullOrEmpty(hetedikTxtbox.Text))
            {
                MessageBox.Show("Nem írtál be semmit");
                return;
            }

          
            hetedikListBox.Items.Clear();

            for (int i = 0; i < hangversenyek.Count; i++)
            {
                if (hangversenyek[i].Cim.Contains(hetedikTxtbox.Text, StringComparison.OrdinalIgnoreCase))
                {
                    hetedikListBox.Items.Add(hangversenyek[i].Cim);
                    list.Add(hangversenyek[i].Cim);
                }
            }
     
            if (hetedikListBox.Items.Count == 0)
            {
                MessageBox.Show("Nem találtunk a keresésnek megfelelő komolyzenei darabot.");
                return;
            }

           
            Random rnd = new Random();
            if (hetedikListBox.Items.Count > 0)
            {
                int intRnd = rnd.Next(0, hetedikListBox.Items.Count);
                hetedikLabel.Content = hetedikListBox.Items[intRnd];
            }
        }

        private void valasz_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(evTxtbox.Text))
            {
                MessageBox.Show("Add meg a mű keletkezési évét!");
                return;
            }


            if (!int.TryParse(evTxtbox.Text, out int bekertEv))
            {
                MessageBox.Show("Az évszám nem megfelelő formátumú.");
                return;
            }


            var kivalasztottMu = hangversenyek.FirstOrDefault(mu => mu.Cim == hetedikLabel.Content.ToString());

            if (kivalasztottMu != null)
            {

                if (bekertEv == kivalasztottMu.Ev)
                {
                    MessageBox.Show("Helyes válasz!");

                    nehezsegTxtBlock.Text = $"A mű nehézségi foka: {kivalasztottMu.Nehezseg}";
                }
                else
                {
                    MessageBox.Show($"Helytelen válasz. A helyes év: {kivalasztottMu.Ev}");
                }
            }
        }
    }
}