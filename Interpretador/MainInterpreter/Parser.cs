using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Calc{
    class Parser{
        private Lexer lexer;
        private Evaluator evaluator;
        private ErrorHandler error;
        private SymbolTable table;
        static Lexer.Token lookahead;

        public Parser(Lexer lexer, SymbolTable table){
            this.lexer = lexer;
            this.table = table;
            evaluator = new Evaluator();
            error = new ErrorHandler();
            lookahead = lexer.NextToken();
        }

        public void match(Lexer.TokenType token){
            if(lookahead.Type == token){
                lookahead = lexer.NextToken();
            }
            else{
                throw new ArgumentException("Expected" +token + ", but found" + lookahead.Type);
            }
        }

        public String Cmd(){
            if(lookahead.Type == Lexer.TokenType.PRINT){
                match(Lexer.TokenType.PRINT);
                var value = Expr();
                return value.ToString();
            }else if(lookahead.Type == Lexer.TokenType.VAR){
                Atrib();
                return string.Empty;
            }
            else{
                return Expr().ToString();
            }

        }

        public void Atrib(){
            string varName = lookahead.Value.ToString();
            match(Lexer.TokenType.VAR);
            match(Lexer.TokenType.EQ);
            double value = Expr();
            table.AddSymbol(varName, value);
        }
    
        public Double Expr(){
            double value = Term();
            return Rest(value);
        }

        public double Term(){
            double value = 0;
            if(lookahead.Type == Lexer.TokenType.NUM){
                value = lookahead.Value;
                match(Lexer.TokenType.NUM);
            }else if(lookahead.Type == Lexer.TokenType.VAR){
                value = table.GetSymbol(lookahead.Value.ToString());
                match(Lexer.TokenType.VAR);
            }else if(lookahead.Type == Lexer.TokenType.OPEN){
                match(Lexer.TokenType.OPEN);
                value = Expr();
                match(Lexer.TokenType.CLOSE);
            }
            else{
                error.Error("Unexpected token");
            }
            return value;
        }
            /*
                "+" => left + right,
                "-" => left - right,
                "*" => left * right,
                "/" => left / right,

                case '+' : return v1+ v2;
                case '-' : return v1 - v2;
                case '*' : return v1 * v2;
                case '/' :
                    if (v2 != 0){
                        return v1/v2;
                    }
            */
        public double Rest(double v1){
            if(lookahead.Type == Lexer.TokenType.SUM){
                match(Lexer.TokenType.SUM);
                double v2 = Term();
                v1 = evaluator.Calcule(v1, '+', v2);
                return Rest(v1);
            }else if(lookahead.Type == Lexer.TokenType.SUB){
                match(Lexer.TokenType.SUB);
                double v2 = Term();
                v1 = evaluator.Calcule(v1, '-', v2);
                return Rest(v1);
            }else if(lookahead.Type == Lexer.TokenType.MULT){
                match(Lexer.TokenType.MULT);
                double v2 = Term();
                v1 = evaluator.Calcule(v1, '*', v2);
                return Rest(v1);
            }else if(lookahead.Type == Lexer.TokenType.DIV){
                match(Lexer.TokenType.DIV);
                double v2 = Term();
                v1 = evaluator.Calcule(v1, '/', v2);
                return Rest(v1);
            }
            else{
                return v1;
            }
           
        }

    }   

}