using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;

namespace SpaceApp.ML
{
    public class ML
    {
        private MLContext _mlContext;
        private PredictionEngine<> _predEngine;
        private ITransformer _trainedModel;
        private IDataView _trainingDataView;
    }
}
