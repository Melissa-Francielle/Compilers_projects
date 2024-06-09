<html>
<body>
    <h1> Compilers Project </h1>
    <p> This repository will include compiler codes generated during Compiler classes at the University.</p>
    <p> This work makes reference to the codes used in the Compilers class in the Computer Science course at the University of Northern Paraná</p>
    <p> <h2> Introduction: </h2>
        Interpretador  is a language producer a bit like a Compiler, but without the object program in the generated result. In an Interpretador , the program is executable each time the program is introduced with user input.<p> 
    <p> The Interpreter components: </p>
    <ol>
        <li><strong> Lexer: it is a lexical phase of a compiler and separates lexemes and we have tokens in the result.</li>
          <pre>
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
                case '1': re...
          </pre>
        <li><strong>  Parser: is the syntactic part of the compiler that makes the intermediate program </li>
          <pre>
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
            ...
          </pre>
    <p> Representation of the execution of the program present in this repository: </p>
    <pre>
        >> 2 + 2 
        >> 4
        >> 10 / 5
        >> 2
    </pre>
    <p> This is a small example of how the interpreter works. </p>
    <p> <h3> Link to enter the Interpreter directory: </h3></p>
    <ul>
        <li><a href=" https://github.com/Melissa-Francielle/Compilers_projects/tree/main/Interpretador/MainInterpreter">Interpreter and Classes</a></li>
    </ul>
