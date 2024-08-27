using tabuleiro;

namespace xadrez_console
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; } 

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        //Método para converter a posição do xadrez para uma posição interna da matriz
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, // Se Linha for 1, 8 - 1 = 7 (a linha mais baixa da matriz).
                Coluna - 'a'); //Caracter 'a' internamente é um número inteiro, então se for 'a' - 'a' = 0, se for 'b' vai ser 'b' - 'a' = 1<<
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }

    }
}
