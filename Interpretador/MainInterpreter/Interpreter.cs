namespace Calc{
    public class Interpreter{
        private SymbolTable table;

        public Interpreter(){
            table = new SymbolTable();
        }

        public string Exec(string command ){
            if(command == null){
                return "";
            }
            var lexer = new Lexer(command, table);
            var parser = new Parser(lexer, table);
            return parser.Cmd();
        }
    }
}