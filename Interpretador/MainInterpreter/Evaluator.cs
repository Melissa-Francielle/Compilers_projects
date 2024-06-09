using System;

namespace Calc{
    class Evaluator{
        public double Calcule(double v1, char op, double v2){
            switch(op){
                case '+' : return v1 + v2;
                case '-' : return v1 - v2;
                case '*' : return v1 * v2;
                case '/' :
                    if (v2 != 0){
                        return v1/v2;
                    }
                    else{
                        throw new DivideByZeroException("ERROR: Divide by Zero.");
                    }
                default:
                    throw new ArgumentException("Invalid Op: " + op);
            }
        }
    }
}