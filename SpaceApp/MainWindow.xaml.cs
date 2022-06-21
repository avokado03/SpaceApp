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
        private ML.MLFacade _ml;
        public MainWindow()
        {           
            ClearConsoleCmd = new RoutedCommand("ClearConsoleCmd", typeof(Button));          
            ClearStellarFormCmd = new RoutedCommand("ClearStellarFormCmd", typeof(Button));
            LoadModelCmd = new RoutedCommand("LoadModelCmd", typeof(Button));
            EvaluateCmd = new RoutedCommand("EvaluateCmd", typeof(Button));
            PredictCmd = new RoutedCommand("PredictCmd", typeof(Button));
            _stellarDataValidator = new StellarDataValidator();           
            DataContext = _stellarDataValidator;
            _ml = new ML.MLFacade();
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

        #region Загрузить
        public static RoutedCommand LoadModelCmd;
        private void LoadModelCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            RunMLAction(_ml.LoadModelFromFile);
        }
        #endregion

        #region Метрики
        public static RoutedCommand EvaluateCmd;
        private void EvaluateCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var result = _ml.Evaluate();
            string m = string.Format("{0} - {1} - {2} - {3} \n\r", result.MicroAccuracy, result.MacroAccuracy, result.LogLoss, result.LogLossReduction);
        }
        #endregion

        #region Одиночное предсказание
        public static RoutedCommand PredictCmd;
        private void PredictCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var result = _ml.Predict(_stellarDataValidator);
            string m = string.Format("Class - {0} \n\r", result);
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

        private void RunMLAction(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch(Exception ex)
            {
                OutputTxt.Text += ex.Message;
            }
        }
    }
}
