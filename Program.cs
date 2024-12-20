using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EMU
{
    class Program
    {
        static void Main(string[] args)
        {
            var emulator = new Emulator(256);

            var parser = new Parser();
            string script = @"
                        LOAD ""xyz"" 10
                        ADD ""xyz"" 5
                        INPUT ""abc""
                        PRINT
                        HALT
                        ";

            int[] program = parser.Parse(script);
            emulator.LoadProgram(program);
            emulator.Run();


            Console.ReadLine();
        }
    }
}
