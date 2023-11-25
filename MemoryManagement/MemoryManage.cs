using System;
using System.Diagnostics;

namespace MemoryManagement
{
    public static class MemoryManage
    {
        public static List<int> MemoryBlock = Enumerable.Repeat(0, 64).ToList();

        public static List<int> ProcessSize { get; set; }

        public static List<Tuple<int, int>> Processos = new List<Tuple<int, int>>();

        static MemoryManage()
        {
            ProcessSize = new List<int> { 10, 6, 15, 80 };

            MemoryBlock[9] = 1;
            MemoryBlock[21] = 1;
        }

        public static void FirstFit()
        {
            foreach (int processSize in ProcessSize)
            {
                if (processSize > 64)
                {
                    Console.WriteLine("Processo maior do que capacidade total da memória.\nConsidere alterar seu tamanho nas configurações.");
                    continue;
                }
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
                Console.WriteLine($"Tamanho Processo: {processSize}\nReg. Base = {bestIndex}\tReg. Limite = {bestIndex + processSize}\n\n");
                if (bestIndex != -1)
                {
                    // Add process to the tuple
                    Processos.Add(new Tuple<int, int>(bestIndex, bestIndex + processSize));

                    // Allocate the process to the first free block
                    for (int i = bestIndex; i < bestIndex + processSize; i++)
                    {
                        MemoryBlock[i] = 1;
                    }
                }
                else
                {
                    // Find best fit after considering removing processes
                    int bestProcess = int.MaxValue;
                    for (int i = 0; i < Processos.Count - 1; i++)
                    {
                        Tuple<int, int>? processo = Processos[i];
                        var tamanho = processo.Item2 - processo.Item1;
                        if (tamanho >= processSize)
                        {
                            bestProcess = i;
                            break;
                        }
                    }

                    var inicio = Processos[bestProcess].Item1;
                    var fim = inicio + processSize;

                    //Remove previous process, and adds this new process on it's place
                    for (int i = Processos[bestProcess].Item1; i <= Processos[bestProcess].Item2; i++)
                    {

                        // Remove exceeding space from previous process
                        MemoryBlock[i] = i > fim ? 0 : 1;

                        // Remove previous process from tuple
                        Processos.RemoveAt(bestProcess);

                        // Add new process on tuple
                        Processos.Add(new Tuple<int, int>(inicio, fim));
                    }
                }
            }
            Console.WriteLine();
            int k = 1;
            foreach (var block in MemoryBlock)
            {
                Console.WriteLine($"Bloco {k}:\t{block}");
                k++;
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void BestFit()
        {
            foreach (int processSize in ProcessSize)
            {
                if (processSize > 64)
                {
                    Console.WriteLine("Processo maior do que capacidade total da memória.\nConsidere alterar seu tamanho nas configurações");
                    continue;
                }
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
                Console.WriteLine($"Tamanho Processo: {processSize}\nReg. Base = {bestIndex}\tReg. Limite = {bestIndex + processSize}\n\n");
                if (bestIndex != -1)
                {
                    // Add process to the tuple
                    Processos.Add(new Tuple<int, int>(bestIndex, bestIndex + processSize));

                    // Save the position of this process for future removal
                    var inicio = bestIndex;
                    var fim = bestIndex + processSize;

                    if (Processos != null)
                    {
                        Processos.Add(new Tuple<int, int>(inicio, fim));
                    }

                    // Allocate the processSize to the best free block
                    for (int i = bestIndex; i < bestIndex + processSize; i++)
                    {
                        MemoryBlock[i] = 1;
                    }
                }
                else
                {

                    // Find best fit after considering removing processes
                    int bestProcess = int.MaxValue;
                    for (int i = 0; i < Processos.Count; i++)
                    {
                        Tuple<int, int>? processo = Processos[i];
                        var tamanho = processo.Item2 - processo.Item1;
                        if(tamanho >= processSize && tamanho < bestProcess)
                        {
                            bestProcess = i;
                        }
                    }

                    var inicio = Processos[bestProcess].Item1;
                    var fim = inicio + processSize;

                    //Remove previous process, and adds this new process on it's place
                    for (int i = Processos[bestProcess].Item1; i <= Processos[bestProcess].Item2; i++)
                    {
                        
                        // Remove exceeding space from previous process
                        MemoryBlock[i] = i > fim ? 0 : 1;

                        // Remove previous process from tuple
                        Processos.RemoveAt(bestProcess);

                        // Add new process on tuple
                        Processos.Add(new Tuple<int, int>(inicio, fim));
                    }
                }
            }
            Console.WriteLine();
            int k = 1;
            foreach (var block in MemoryBlock)
            {
                Console.WriteLine($"Bloco {k}:\t{block}");
                k++;
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}