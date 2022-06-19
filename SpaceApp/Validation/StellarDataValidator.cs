using SpaceApp.ML.ViewModels;
using System;
using System.ComponentModel;

namespace SpaceApp.Validation
{
    /// <summary>
    /// Валидатор для ViewModel <see cref="StellarDataViewModel"/>
    /// </summary>
    public class StellarDataValidator : StellarDataViewModel, IDataErrorInfo
    {
        public bool FirstLoad { get; set; } = true;
        /// <inheritdoc />
        public string this[string columnName] 
        { 
            get 
            {
                if (FirstLoad)
                {
                    return Error;
                }
                //Интервалы взяты из: https://www.kaggle.com/code/psycon/stars-galaxies-eda-and-classification/data
                switch (columnName)
                {
                    case nameof(alpha):
                        Error = SetErrorMessage(alpha, 0.01f, 360f);
                        break;
                    case nameof(delta):
                        Error = SetErrorMessage(delta, -18.8f, 83f);
                        break;
                    case nameof(u):
                        Error = SetErrorMessage(u, -10000f, 32.8f);
                        break;
                    case nameof(g):
                        Error = SetErrorMessage(g, -10000f, 31.6f);
                        break;
                    case nameof(r):
                        Error = SetErrorMessage(r, 9.82f, 29.6f);
                        break;
                    case nameof(i):
                        Error = SetErrorMessage(i, 9.47f, 32.1f);
                        break;
                    case nameof(z):
                        Error = SetErrorMessage(z, -10000f, 29.4f);
                        break;
                    case nameof(cam_col):
                        Error = SetErrorMessage(cam_col, 1f, 6f);
                        break;
                    case nameof(redshift):
                        Error = SetErrorMessage(redshift, -0.01f, 7.01f);
                        break;
                    case nameof(plate):
                        Error = SetErrorMessage(plate, 266f, 12500f);
                        break;
                    case nameof(MJD):
                        Error = SetErrorMessage(MJD, 51600f, 58900f);
                        break;
                    case nameof(fiber_ID):
                        Error = SetErrorMessage(fiber_ID, 1f, 1000f);
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        /// <inheritdoc />
        public string Error { get; set; }

        private Func<float, float, float, string> SetErrorMessage = (float prop, float from, float to) => 
        {
            return (prop < from || prop > to)
                ? string.Format("Значение должно быть в диапазоне от {0} до {1} \n\r", from.ToString(), to.ToString())
                : string.Empty;
        };

    }
}
