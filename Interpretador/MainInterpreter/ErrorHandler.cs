using System;

namespace Calc{
    class ErrorHandler{
        public void Error(string message){
            throw new Exception(message);
        }
        
    }
}