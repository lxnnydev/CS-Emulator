# **C# Emulator**

### **Insanely Basic Emulator Written in C#**
**Learn how to simulate a simple virtual machine with minimal setup and functionality.**

---

## **Overview**
This simple emulator demonstrates how to implement a vm that can execute a small set of predefined instructions. The emulator uses a memory array and an accumulator register to perform simple arithmetic operations and control flow. It is very beginner friendly.

---

## **Features**
- **Instruction Set:** Supports a small, easy-to-understand instruction set:
  - `1`: Load a value into the accumulator.
  - `2`: Add a value to the accumulator.
  - `3`: Subtract a value from the accumulator.
  - `4`: Print the accumulator.
  - `99`: Halt the emulator.
- **Simple Memory Model:** A fixed-size memory array to store the program and data.
- **Accumulator Register:** Performs arithmetic operations and holds results.
- **Program Loader:** Load programs into memory before execution.
- **Basic Control Flow:** Executes instructions sequentially with a program counter.

---

## **How It Works**
The emulator follows an easy workflow:
1. The user writes a program using the supported instruction set.
2. The program is loaded into the emulator's memory.
3. The emulator executes the program step-by-step until a `HALT` instruction is read.

### **Core Workflow**
- Fetch an instruction from memory.
- Decode the instruction and execute it.
- Repeat until the program halts.

---

## **Use Cases**
1. **Educational Tool:** Learn about virtual machines and how basic emulators work.
2. **Programming Practice:** Implement additional features or expand the instruction set.

---

## **Supported Instructions**
| Instruction | Description                                     |
|-------------|-------------------------------------------------|
| `1`         | Load a value into the accumulator.             |
| `2`         | Add a value to the accumulator.                |
| `3`         | Subtract a value from the accumulator.         |
| `4`         | Print the current value of the accumulator.    |
| `99`        | Halt the execution of the program.             |

---

## **Example Program**
### **Program Description**
This program:
1. Loads the value `10` into the accumulator.
2. Adds `20` to the accumulator.
3. Subtracts `5` from the accumulator.
4. Prints the accumulator value.
5. Halts the program.

### **Code**
```csharp
int[] program = new int[]
{
    1, 10,  // LOAD 10
    2, 20,  // ADD 20
    3, 5,   // SUBTRACT 5
    4,      // PRINT
    99      // HALT
};
```

### **Output**
```
Accumulator: 25
Program halted.
```

---
