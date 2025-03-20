using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Core.Tools.OperationResult
{
    public class Result<T>
    {
        public int resultCode { get; set; } // Success = 0, error != 0
        public string errorMessage { get; private set; }
        public T payLoad { get; init; }
        public List<string> errors { get; init; }

        // Constructor for a single error => put it in errors-list
        public Result(int resultCode, string errorMessage)
        {
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
            this.errors = new List<string> { errorMessage };
        }

        // Constructor for success
        public Result(T payload)
        {
            this.resultCode = 0;
            this.payLoad = payload;
            this.errors = new List<string>();
        }

        // Constructor for multiple errors
        public Result(List<string> errors)
        {
            this.resultCode = 1;  
            this.errors = errors;
            this.errorMessage = string.Join("; ", errors);
        }

        // Append more errors
        public void AddError(int resultCode, string errorMessage)
        {
            this.resultCode = resultCode; 
            this.errors.Add(errorMessage);
            this.errorMessage = string.Join("; ", this.errors);
        }
    }
}