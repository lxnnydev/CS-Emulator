using System;
using System.Collections.Generic;

class Emulator
{
    public enum Instruction
    {
        LOAD = 1,
        ADD = 2,
        SUBTRACT = 3,
        MULTIPLY = 5,
        DIVIDE = 6,
        PUSH = 8,
        POP = 9,
        INPUT = 10,
        OUTPUT = 11,
        PRINT = 4,
        HALT = 99
    }

    private int[] memory; // Simulated memory
    private Dictionary<string, int> registers; // Named registers like Lua variables
    private bool running; // Emulator running state
    private Stack<int> stack; // Stack for advanced operations

    public Emulator(int memorySize)
    {
        memory = new int[memorySize];
        registers = new Dictionary<string, int>();
        running = false;
        stack = new Stack<int>();
    }

    public void LoadProgram(int[] program)
    {
        if (program.Length > memory.Length)
        {
            throw new ArgumentException("Program size exceeds memory size.");
        }

        Array.Copy(program, memory, program.Length);
    }

    public void Run()
    {
        running = true;
        int instructionPointer = 0;

        while (running)
        {
            var instruction = (Instruction)memory[instructionPointer++];

            switch (instruction)
            {
                case Instruction.LOAD: // LOAD value into a named register
                    string registerName = ReadString(ref instructionPointer);
                    int value = memory[instructionPointer++];
                    registers[registerName] = value;
                    break;

                case Instruction.ADD: // ADD value to a named register
                    registerName = ReadString(ref instructionPointer);
                    value = memory[instructionPointer++];
                    registers[registerName] += value;
                    break;

                case Instruction.SUBTRACT: // SUBTRACT value from a named register
                    registerName = ReadString(ref instructionPointer);
                    value = memory[instructionPointer++];
                    registers[registerName] -= value;
                    break;

                case Instruction.MULTIPLY: // MULTIPLY a named register by a value
                    registerName = ReadString(ref instructionPointer);
                    value = memory[instructionPointer++];
                    registers[registerName] *= value;
                    break;

                case Instruction.DIVIDE: // DIVIDE a named register by a value
                    registerName = ReadString(ref instructionPointer);
                    value = memory[instructionPointer++];
                    if (value == 0)
                    {
                        Console.WriteLine("Error: Division by zero.");
                        running = false;
                    }
                    else
                    {
                        registers[registerName] /= value;
                    }
                    break;

                case Instruction.PUSH: // PUSH a named register value to stack
                    registerName = ReadString(ref instructionPointer);
                    stack.Push(registers[registerName]);
                    break;

                case Instruction.POP: // POP value from stack into a named register
                    registerName = ReadString(ref instructionPointer);
                    if (stack.Count > 0)
                    {
                        registers[registerName] = stack.Pop();
                    }
                    else
                    {
                        Console.WriteLine("Error: Stack underflow.");
                        running = false;
                    }
                    break;

                case Instruction.INPUT: // INPUT a value into a named register
                    registerName = ReadString(ref instructionPointer);
                    Console.Write("Enter a value: ");
                    if (int.TryParse(Console.ReadLine(), out value))
                    {
                        registers[registerName] = value;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                        running = false;
                    }
                    break;

                case Instruction.OUTPUT: // OUTPUT a named register value
                    registerName = ReadString(ref instructionPointer);
                    Console.WriteLine("Output (" + registerName + "): " + registers[registerName]);
                    break;

                case Instruction.PRINT: // PRINT all registers
                    foreach (var kvp in registers)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                    break;

                case Instruction.HALT: // HALT
                    running = false;
                    break;

                default:
                    Console.WriteLine($"Unknown instruction: {instruction}");
                    running = false;
                    break;
            }
        }

        Console.WriteLine("Program halted.");
    }

    private string ReadString(ref int instructionPointer)
    {
        int length = memory[instructionPointer++];
        char[] chars = new char[length];

        for (int i = 0; i < length; i++)
        {
            chars[i] = (char)memory[instructionPointer++];
        }

        return new string(chars);
    }
}
