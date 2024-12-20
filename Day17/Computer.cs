namespace Day17
{
    public class Computer
    {
        public long registerA { get; private set; }
        public long registerB { get; private set; }
        public long registerC { get; private set; }
        private int[] program;

        public Computer(long registerA, long registerB, long registerC, int[] program)
        {
            this.registerA = registerA;
            this.registerB = registerB;
            this.registerC = registerC;
            this.program = program;
        }

        internal bool Run(Func<int, bool> Output)
        {
            int instructionPointer = 0;
            while (instructionPointer >= 0 && instructionPointer < program.Length - 1)
            {
                switch (program[instructionPointer])
                {
                    case 0: // adv
                        registerA = registerA >> (int)combo(instructionPointer + 1);
                        break;
                    case 1: // bxl
                        registerB = registerB ^ literal(instructionPointer + 1);
                        break;
                    case 2: // bst
                        registerB = combo(instructionPointer + 1) % 8;
                        break;
                    case 3: // jnz
                        if (registerA != 0)
                        {
                            instructionPointer = literal(instructionPointer + 1);
                            continue;
                        }
                        break;
                    case 4: // bxc
                        registerB = registerB ^ registerC;
                        break;
                    case 5: // out
                        if (!Output((int)(combo(instructionPointer + 1) % 8))) return false;
                        break;
                    case 6: // bdv
                        registerB = registerA >> (int)combo(instructionPointer + 1);
                        break;
                    case 7: // cdv
                        registerC = registerA >> (int)combo(instructionPointer + 1);
                        break;
                    default:
                        throw new Exception("Unknown opcode");
                }
                instructionPointer += 2;
            }
            return true;
        }

        private int literal(int instructionPointer)
        {
            return program[instructionPointer];
        }

        private long combo(int pointer)
        {
            long operand = 0;
            switch (program[pointer])
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    operand = literal(pointer);
                    break;
                case 4:
                    operand = registerA;
                    break;
                case 5:
                    operand = registerB;
                    break;
                case 6:
                    operand = registerC;
                    break;
                default:
                    throw new Exception("Invalid Operand");
            }

            return operand;
        }
    }
}