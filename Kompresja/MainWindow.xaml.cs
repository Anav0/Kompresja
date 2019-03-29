using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

        private void LoadFileToTable(object sender, RoutedEventArgs e)
        {
            var content = OpenTxtFile();
            if (content == null) return;

            mLoadedText = content;
            dataGrid.ItemsSource = mProb.GetLetters(content);
            uiEntropy.Text = mProb.CalculateEntropy(mLoadedText).ToString();
            uiNumberOfCharacters.Text = mLoadedText.Length.ToString();
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
            SaveFile(mLoadedText);
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
            double sumOfProbability = 0;
            foreach (var letter in mDataGrid2Source)
            {
                sumOfProbability += letter.Probability;

                if (sumOfProbability > 1)
                {
                    MessageBox.Show("Suma prawdopodobieństw wystąpienia danych znaków nie może być większa od 1", "Błąd", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

                entropy += -letter.Probability * Math.Log(letter.Probability, 2);
            }

            iuDataGrid2Entropy.Text = entropy.ToString();
        }

        private void GenerateWords(object sender, RoutedEventArgs e)
        {
            var numberOfWords = 0;
            var numberOfLetters = 0;
            try
            {
                numberOfLetters = Convert.ToInt32(uiNumberOfLettersInWords.Text);
                numberOfWords = Convert.ToInt32(uiNumberOfWordsToGenerate.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Zły format", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SaveFile(new RandomTextGenerator().GenerateWords(numberOfWords, numberOfLetters));
        }

        private void SaveFile(string textToSave)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "text";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, textToSave);
            }
        }
        private void SaveFile(IEnumerable<string> textToSave)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "text";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllLines(dialog.FileName, textToSave);
            }
        }
        private string OpenTxtFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                return File.ReadAllText(dialog.FileName, Encoding.GetEncoding("Windows-1250"));
            }

            return null;
        }

        private void GenerateProbModelBasedOnFile(object sender, RoutedEventArgs e)
        {
            var words = OpenTxtFile().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
        }

        private void GenerateHuffman(object sender, RoutedEventArgs e)
        {
            new Probability().Huffman();
        }
    }
}
