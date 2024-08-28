using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.SupPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor; //Captura a cor de fundo da tela (preto) 
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray; //Alterar a cor para cinza escura quando estiver marcado

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    { //se a posição estiver marcada como uma posição de movimento,
                      //então mudamos a cor das posições possíveis para locomoção das peças
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.SupPeca(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = fundoOriginal;
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }


        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor; // Captura a cor das letras no console
                    Console.ForegroundColor = ConsoleColor.Yellow; // Altera a cor para amarelo
                    Console.Write(peca); // Exibe o texto peca com a cor amarela
                    Console.ForegroundColor = aux; // Restaura a cor original do texto
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + ""); // + "" força que seja string
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
