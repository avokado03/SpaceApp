using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            OutputTxt.Text += "Загрузка модели завершена успешно \n\r";
        }
        #endregion

        #region Метрики
        public static RoutedCommand EvaluateCmd;
        private void EvaluateCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            RunMLAction(Evaluate);
        }

        private void Evaluate()
        {
            var result = _ml.Evaluate();
            StringBuilder builder = new StringBuilder();
            builder.Append("Метрики для текущей модели: \n");
            builder.Append(string.Format("Микроточность : {0} \n", result.MicroAccuracy));
            builder.Append(string.Format("Мaкроточность : {0} \n", result.MacroAccuracy));
            builder.Append(string.Format("Логарифмические потери: {0} \n", result.LogLoss));
            builder.Append(string.Format("Минимизация логарифмических потерь : {0} \n", result.LogLossReduction));
            string output = builder.ToString();
            OutputTxt.Text += output;
        }
        #endregion

        #region Одиночное предсказание
        public static RoutedCommand PredictCmd;
        private void PredictCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            RunMLAction(Predict);
        }

        private void Predict()
        {
            if (_stellarDataValidator.ZeroOrEmpty())
                throw new Exception("Введите валидные данные \n");
            var result = _ml.Predict(_stellarDataValidator);
            string output = string.Format("Класс небесного тела - {0} \n", result);
            OutputTxt.Text += output;
        }
        #endregion

        private void ButtonCanExecute (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Source is Button;
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            var errorEventMsg = e.Error.ErrorContent.ToString() + "\n";
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
