using DirichletTask.Core.Abstraction.Series;

namespace DirichletTask.Core.Abstraction.Cache
{
    public interface ICachedSingleSeriesCalculator<TIndex, TVar, TReturn> :
        ISingleValuedSeriesCalculator<TIndex, TVar, TReturn>
    {
        void EmptyCache();
        void SetCacheSize(TIndex size);
    }
}
