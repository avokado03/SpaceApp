using System;
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
using System.Reflection;
using SpaceApp.ML.ViewModels;
using SpaceApp.Validation;

namespace SpaceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StellarDataValidator _stellarDataValidator;
        public MainWindow()
        {           
            ClearConsoleCmd = new RoutedCommand("ClearConsoleCmd", typeof(Button));          
            ClearStellarFormCmd = new RoutedCommand("ClearStellarFormCmd", typeof(Button));
            _stellarDataValidator = new StellarDataValidator();
            
            DataContext = _stellarDataValidator;
            
            InitializeComponent();
            _stellarDataValidator.FirstLoad = false;
        }

        #region Oчистка бокса с результатами
        public static RoutedCommand ClearConsoleCmd; 
        private void ClearConsoleCmdExecuted (object sender, ExecutedRoutedEventArgs e)
        {
            OutputTxt.Text = string.Empty;
        }
        #endregion

        #region Oчистка формы для предсказания
        public static RoutedCommand ClearStellarFormCmd;
        private void ClearStellarFormCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FormGrid.Children.OfType<TextBox>().ToList().ForEach(x => { x.Text = string.Empty; });
        }
        #endregion

        private void ButtonCanExecute (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Source is Button;
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            var errorEventMsg = e.Error.ErrorContent.ToString() + "\n\r";
            OutputTxt.Text += errorEventMsg;
        }
    }
}
