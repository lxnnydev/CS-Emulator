using System;
using System.Collections.Generic;
using System.Linq;

class Parser
{
    private Dictionary<string, Emulator.Instruction> instructionMap;

    public Parser()
    {
        instructionMap = new Dictionary<string, Emulator.Instruction>(StringComparer.OrdinalIgnoreCase)
        {
            { "LOAD", Emulator.Instruction.LOAD },
            { "ADD", Emulator.Instruction.ADD },
            { "SUBTRACT", Emulator.Instruction.SUBTRACT },
            { "MULTIPLY", Emulator.Instruction.MULTIPLY },
            { "DIVIDE", Emulator.Instruction.DIVIDE },
            { "PUSH", Emulator.Instruction.PUSH },
            { "POP", Emulator.Instruction.POP },
            { "INPUT", Emulator.Instruction.INPUT },
            { "OUTPUT", Emulator.Instruction.OUTPUT },
            { "PRINT", Emulator.Instruction.PRINT },
            { "HALT", Emulator.Instruction.HALT }
        };
    }

    public int[] Parse(string script)
    {
        var lines = script.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var program = new List<int>();

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0 || parts[0].StartsWith("#")) // Ignore comments and empty lines
                continue;

            if (!instructionMap.TryGetValue(parts[0], out var instruction))
            {
                throw new InvalidOperationException($"Unknown instruction: {parts[0]}");
            }

            program.Add((int)instruction);

            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i].StartsWith("\"")) // Handle string arguments
                {
                    string str = parts[i].Trim('"');
                    program.Add(str.Length);
                    program.AddRange(str.Select(c => (int)c));
                }
                else if (int.TryParse(parts[i], out int value)) // Handle numeric arguments
                {
                    program.Add(value);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid argument: {parts[i]}");
                }
            }
        }

        return program.ToArray();
    }
}
