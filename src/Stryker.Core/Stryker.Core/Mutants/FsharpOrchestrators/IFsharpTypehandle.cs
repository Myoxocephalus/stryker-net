namespace Stryker.Configuration.Mutants.FsharpOrchestrators
{
    public interface IFsharpTypeHandler<T>
    {
        public T Mutate(T input, FsharpMutantOrchestrator iterator);
    }
}
