namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas {  get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas; //No tabuleiro temos uma matriz de peças, logo deixamos em privado para apenas o tabuleiro realizar alterações

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas]; // Instancia a matriz de peças com a quantidade de linhas e colunas
        }

        public Peca SupPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public void AdicionarPeca(Peca p, Posicao pos)
        {
            Pecas[pos.Linha, pos.Coluna] = p; //Acessa a matriz de pecas em pos.linha e pos.coluna
            p.Posicao = pos; //Adiciona a peça na posicao
        }

    }
}
