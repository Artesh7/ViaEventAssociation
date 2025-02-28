using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Tools.OperationResult
{
    public class Result<T>
    {
        int resultCode; // Success is 0, anything else is error.
        string errorMessage;
        T payLoad;

        // Constructor if an error occoured.
        public Result(int resultCode, string errorMessage){
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
        }

        // Constructor if the call finished successfully.
        public Result(T payload){
            this.resultCode = 0;
            this.payLoad = payLoad;
        }

        // More than one error occoured. The following error messages are appendid to the 1st one.
        public void AddError(int resultCode, string errorMessage){
            this.errorMessage += "," + resultCode + "-" + errorMessage;
        }
    }
}
