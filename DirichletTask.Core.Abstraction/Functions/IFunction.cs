namespace DirichletTask.Core.Abstraction.Functions
{
    public interface IFunction<TInput1, TOutput>
    {
        TOutput Calculate(TInput1 value1);
    }

    public interface IFunction<TInput1, TInput2, TOutput>
    {
        TOutput Calculate(TInput1 value1, TInput2 value2);
    }
}
