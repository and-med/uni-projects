namespace DirichletTask.Core.Abstraction.Parameters
{
    public interface IReadOnlyParameterProvider<TParam>
    {
        TParam GetValue(string parameterName);
    }
}
