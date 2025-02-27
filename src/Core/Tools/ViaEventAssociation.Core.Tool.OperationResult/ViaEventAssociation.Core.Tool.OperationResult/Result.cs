using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Tool.OperationResult
{
    public class Result<T>
    {
        int resultCode; // Success is 0, anything else is error.
        string errorMessage;
        T payLoad;

        public Result(int resultCode, string errorMessage){
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
        }
        public Result(T payload){
            this.resultCode = 0;
            this.payLoad = payLoad;
        }

        public AddError(int resultCode, string errorMessage){
            this.errorMessage += "," + resultCode + "-" + errorMessage;
        }
    }
}
