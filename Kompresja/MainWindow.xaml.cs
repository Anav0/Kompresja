
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace Kompresja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Probability mProb;
        string mLoadedText;

        ObservableCollection<Letter> mDataGrid2Source { get; set; } = new ObservableCollection<Letter>();

        public MainWindow()
        {
            InitializeComponent();

            mProb = new Probability();
            mDataGrid2Source.Add(new Letter());
            dataGrid2.ItemsSource = mDataGrid2Source;
        }

        private void OpenTextFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                string text = mLoadedText = File.ReadAllText(dialog.FileName, Encoding.GetEncoding("Windows-1250"));
                dataGrid.ItemsSource = mProb.GetLetters(text);
                uiEntropy.Text = mProb.CalculateEntropy(mLoadedText).ToString();
                uiNumberOfCharacters.Text = mLoadedText.Length.ToString();

            }
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            mLoadedText = input.Text;
            dataGrid.ItemsSource = mProb.GetLetters(input.Text);
            uiEntropy.Text = mProb.CalculateEntropy(input.Text).ToString();
            uiNumberOfCharacters.Text = input.Text.Length.ToString();

        }

        private void GenerateRandomString(object sender, RoutedEventArgs e)
        {
            var generator = new RandomTextGenerator();
            var tekst = generator.GenerateRandomString(100000);
            mLoadedText = tekst;
            dataGrid.ItemsSource = mProb.GetLetters(tekst);
            uiEntropy.Text = mProb.CalculateEntropy(tekst).ToString();
            uiNumberOfCharacters.Text = tekst.Length.ToString();
        }

        private void SaveFileTo(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "text";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, mLoadedText);
            }
        }

        private void GenerateByGivenChars(object sender, RoutedEventArgs e)
        {
            var generator = new RandomTextGenerator();

            mLoadedText = generator.GenerateRandomStringWithEqualProb(Convert.ToInt32(uiNumberOfCharToGenerate.Text.Trim()), uiCharToGenerate.Text.Trim());

            dataGrid.ItemsSource = mProb.GetLetters(mLoadedText);
            uiEntropy.Text = mProb.CalculateEntropy(mLoadedText).ToString();
            uiNumberOfCharacters.Text = mLoadedText.Length.ToString();
        }

        private void CalculateEntrropyForDataGrid(object sender, RoutedEventArgs e)
        {
            var entropy = 0d;
            foreach (var letter in mDataGrid2Source)
            {
                entropy += -letter.Probability * Math.Log(letter.Probability, 2);
            }
            iuDataGrid2Entropy.Text = entropy.ToString();
        }
    }
}
