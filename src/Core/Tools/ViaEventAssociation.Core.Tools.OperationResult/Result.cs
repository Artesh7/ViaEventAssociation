using System.Collections.Generic;
using System.Linq;

namespace ViaEventAssociation.Core.Tools.OperationResult
{
    public class Result<T>
    {
        // 0 = success, any nonzero = error
        public int resultCode { get; } 
        public string errorMessage { get; }
        public T payLoad { get; }
        public List<string> errors { get; }

        // Single-error constructor
        public Result(int resultCode, string errorMessage)
        {
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
            this.errors = new List<string> { errorMessage };
        }

        // Success constructor
        public Result(T payload)
        {
            this.resultCode = 0;
            this.payLoad = payload;
            this.errors = new List<string>();
            this.errorMessage = string.Empty;
        }

        // Multiple-errors constructor
        public Result(List<string> errors)
        {
            this.resultCode = 1;
            this.errors = errors;
            this.errorMessage = string.Join("; ", errors);
        }

        // Helper property
        public bool IsError => resultCode != 0;
    }
}