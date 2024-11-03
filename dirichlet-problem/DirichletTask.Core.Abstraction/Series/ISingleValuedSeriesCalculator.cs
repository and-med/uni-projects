namespace DirichletTask.Core.Abstraction.Series
{
    public interface ISingleValuedSeriesCalculator<TIndex, TVar, TReturn>
    {
        TReturn Calculate(TIndex n, TVar x);
    }
}