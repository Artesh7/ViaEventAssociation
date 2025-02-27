namespace OperationResult
{
    public class Result<T>
    {
        // Felter
        private int resultCode;        // 0 = Success, >0 = Error
        private string errorMessage;
        private T payLoad;

        // Konstruktør der tager en fejlkode og fejlbesked
        public Result(int resultCode, string errorMessage)
        {
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
        }

        // Konstruktør der tager et payload (success-tilfælde)
        public Result(T payload)
        {
            this.resultCode = 0;       // Success
            this.payLoad = payload;
        }

        // Metode til at generere en ny fejl (bemærk returtype)
        public Result<T> AddError(int errorCode, string errorMessage)
        {
            return new Result<T>(errorCode, errorMessage);
        }
    }
}