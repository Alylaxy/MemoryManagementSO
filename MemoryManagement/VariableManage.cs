using System;
using System.Reflection.PortableExecutable;


namespace MemoryManagement
{
    public static class VariableManage
    {
        public static void OptionsMenu(){
            while (true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("0 = Sair | 1 = Alterar bloco | 2 = Alterar processo | 3 = Mostrar blocos | 4 = Mostrar processos");
                int opcao = Convert.ToInt32(Console.ReadLine());
                switch (opcao){
                    case 1:
                        AlteraBloco();
                        break;
                    case 2:
                        AlteraProcesso();
                        break;
                    case 3:
                        MostraBlocos();
                        break;
                    case 4:
                        MostraProcessos();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Sério? Te dei as opções bonitinhas.\nVou considerar que você clicou errado sem querer e quer sair. >:(");
                        return;
                }
            }
        }

        private static void AlteraBloco()
        {
            int estado;
            MostraBlocos();
            Console.WriteLine("Qual bloco deseja alterar? Para voltar, digite 0");
            int bloco = Convert.ToInt32(Console.ReadLine());
            if (bloco < 1) return;
            while (true)
            {
                Console.WriteLine("Qual o novo estado? (0 - Livre | 1 - Ocupado)");
                estado = Convert.ToInt32(Console.ReadLine());
                if (estado == 0 || estado == 1)
                {
                    break;
                }
                Console.WriteLine("Valor inválido. Insira outro valor.\n(0 - Livre | 1 - Ocupado)");
            }
            MemoryManage.MemoryBlock[bloco - 1] = estado;
        }

        private static void AlteraProcesso()
        {
            Console.WriteLine("Qual processo deseja alterar?");
            MostraProcessos();
            int processo = Convert.ToInt32(Console.ReadLine());
            if (processo < 1) return;
            Console.WriteLine("Qual o novo tamanho do processo?");
            int tamanho = Convert.ToInt32(Console.ReadLine());
            MemoryManage.ProcessSize[processo - 1] = tamanho;
        }

        private static void MostraBlocos()
        {
            Console.WriteLine("\n\nBlocos de memória estão organizados da seguinte forma:");
            int k = 1;
            foreach (var block in MemoryManage.MemoryBlock)
            {
                Console.WriteLine($"Bloco {k}:\t{block}");
                k++;
            }
            Console.WriteLine("\n\n");
            return;
        }
        private static void MostraProcessos()
        {
            Console.WriteLine("Processos a serem incluídos estão organizados da seguinte forma:");
            for (int i = 0; i < MemoryManage.ProcessLength; i++)
            {
                Console.WriteLine($"Processo {i+1}:\t{MemoryManage.ProcessSize[i]}");
            }
            Console.ReadLine();
            return;
        }

    }
}
