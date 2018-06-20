namespace JobControl.Bll
{
    public class ConcurrencyResult<T>
    {
        public ConcurrencyResult(bool concurrencyError, T data)
        {
            ConcurrencyError = concurrencyError;
            Data = data;
        }

        public bool ConcurrencyError { get; }

        public T Data { get; }
    }
}