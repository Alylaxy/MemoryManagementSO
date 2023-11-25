using MemoryManagement;
using System;

public class MemoryManagementConsole
{
    public static void Main()
    {
        while (true)
        {
            //Console.WriteLine("\n\n");
            Console.WriteLine("Escolha o método que deseja usar:");
            Console.WriteLine("0 = Manejar memória | 1 = First Fit | 2 = Best Fit");
            int metodo = Convert.ToInt32(Console.ReadLine());

            switch (metodo)
            {
                case 0:
                    VariableManage.OptionsMenu();
                    break;
                case 1:
                    MemoryManage.FirstFit();
                    break;
                case 2:
                    MemoryManage.BestFit();
                    break;
                case -1:
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Escolhe um válido ai, man :(\nA não ser que queira sair. Ai basta escrever -1");
                    break;
            }
        }
    }
}