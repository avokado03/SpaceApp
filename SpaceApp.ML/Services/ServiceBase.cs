using Microsoft.ML;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Базовый класс для сервисов ML
    /// </summary>
    public abstract class ServiceBase
    {
        public MLContext Context { get; protected set; }

        public ServiceBase(MLContext mLContext)
        {
            Context = mLContext;
        }
    }
}
