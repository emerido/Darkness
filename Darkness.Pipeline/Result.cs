namespace Darkness.Pipeline
{
    
    public class Result<TValue>
    {

        public TValue Value { get; }
        public object Error { get; }

        public Result(TValue value)
        {
            Value = value;
        }

        private Result(object error)
        {
            Error = error;
        }

        public bool IsSuccess => Value != null;
        public bool IsFailure => Error != null;

        public static Result<TValue> Failure(object error)
        {
            return new Result<TValue>(error);
        }
        
    }
}