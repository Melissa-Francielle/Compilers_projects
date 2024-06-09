using System;

namespace Calc{
    class Lexer{
        public string Input {get;set;}
        private const char EOF = '\0';
        public int count;
        public SymbolTable Table{get; private set;}

        public Lexer(string input, SymbolTable table){
            Input = input;
            this.count = 0;
            Table = table;
        }

        public char Scan(){
            if (count == Input.Length){
                return EOF;
            }
            return Input[count++];
        }

        private int ParseInt(char c){
            switch(c){
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                default:
                    return -1;
            };
        }

        public Token NextToken(){
            char c = Scan();
            if(c == '\0'){
                return new Token{Type = TokenType.EOF};
            }
            while(c == ' ' || c == '\t'){
                c = Scan();
            }
            switch(c){
                case '+' : return new Token{Type = TokenType.SUM};
                case '-' : return new Token{Type = TokenType.SUB};
                case '*' : return new Token{Type = TokenType.MULT};
                case '/' : return new Token{Type = TokenType.DIV};
                case '(' : return new Token{Type = TokenType.OPEN};
                case ')' : return new Token{Type = TokenType.CLOSE};
                case '=' : return new Token{Type = TokenType.EQ};

            }
            if (Char.IsDigit(c)){
                var x = ParseInt(c);
                c = Scan();
                while(Char.IsDigit(c)){
                    x = x*10+ParseInt(c);
                    c = Scan();
                }
                count--;
                return new Token{Type = TokenType.NUM, Value = x};
            }
            if(Char.IsUpper(c)){
                if(c == 'P'){
                    return new Token{Type = TokenType.PRINT};

                }
                return new Token{Type = TokenType.UNK};
            }
            if (Char.IsLower(c)){
                var v = c.ToString();
                c = Scan();
                while(Char.IsLower(c)){
                    v = v + c.ToString();
                    c = Scan();
                }
                count --;
                return new Token{Type = TokenType.VAR, Value = Table.AddSymbol(v) };
            }
            return new Token{Type = TokenType.UNK};
        }
        public struct Token{
            public TokenType Type;
            public double Value;
        }
        public enum TokenType{
            VAR, NUM, EQ, SUB, SUM, MULT, DIV, OPEN, CLOSE, PRINT, EOF, UNK
        }
    }
}