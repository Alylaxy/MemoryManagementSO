using System;
using System.Diagnostics;

namespace MemoryManagement
{
    public static class MemoryManage
    {
        public static List<int> MemoryBlock = Enumerable.Repeat(0, 64).ToList();

        public static int[] ProcessSize { get; set; }

        public static int ProcessLength { get; }
        static MemoryManage()
        {
            ProcessSize = new int[] { 10, 6, 15, 80 };
            ProcessLength = ProcessSize.Length;

            MemoryBlock[9] = 1;
            MemoryBlock[21] = 1;
        }

        public static void BestFit()
        {
            foreach (int processSize in ProcessSize)
            {
                int bestIndex = -1;
                int bestSize = int.MaxValue;

                for (int i = 0; i < MemoryBlock.Count - 1; i++)
                {
                    if (MemoryBlock[i] == 0) // Start of a free block
                    {
                        int j;
                        for (j = i; j < MemoryBlock.Count - 1; j++)
                        {
                            if (MemoryBlock[j] == 1) // End of a free block
                            {
                                break;
                            }
                        }
                        int size = j - i;
                        if (size >= processSize && size < bestSize)
                        {
                            bestIndex = i;
                            bestSize = size;
                        }
                        i = j; // Skip to the end of the current free block
                    }
                }
                Console.WriteLine($"Reg. Base = {bestIndex}\tReg. Limite = {bestIndex + processSize}\nTamanho Processo: {processSize}");
                if (bestIndex != -1)
                {
                    // Allocate the processSize to the best free block
                    for (int i = bestIndex; i < bestIndex + processSize; i++)
                    {
                        MemoryBlock[i] = 1;
                    }
                }
                else
                {
                    Console.WriteLine("Memória insuficiente para conter o processo.");
                }
            }
            int k = 1;
            foreach (var block in MemoryBlock)
            {
                Console.WriteLine($"Bloco {k}:\t{block}");
                k++;
            }

            Console.ReadLine();
        }

        public static void FirstFit()
        {
            foreach (int processSize in ProcessSize)
            {
                int bestIndex = -1;

                for (int i = 0; i < MemoryBlock.Count - 1; i++)
                {
                    if (MemoryBlock[i] == 0) // Start of a free block
                    {
                        int j;
                        for (j = i; j < MemoryBlock.Count - 1; j++)
                        {
                            if (MemoryBlock[j] == 1) // End of a free block
                            {
                                break;
                            }
                        }
                        int size = j - i;
                        if (size >= processSize)
                        {
                            bestIndex = i;
                            break; // Found the first suitable free block
                        }
                        i = j; // Skip to the end of the current free block
                    }
                }
                Console.WriteLine($"Reg. Base = {bestIndex}\tReg. Limite = {bestIndex + processSize}\nTamanho Processo: {processSize}");
                if (bestIndex != -1)
                {
                    // Allocate the process to the first free block
                    for (int i = bestIndex; i < bestIndex + processSize; i++)
                    {
                        MemoryBlock[i] = 1;
                    }
                }
                else
                {
                    Console.WriteLine("Memória insuficiente para conter o processo.");
                }
            }
        }


    }
}