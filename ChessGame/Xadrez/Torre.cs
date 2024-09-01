using tabuleiro;

namespace xadrez_console
{
    class Torre : Peca //Subclasse de peça
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        //Método para verificar se a peça pode ser movida par X posição
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.SupPeca(pos);
            return p == null || p.Cor != Cor; // Retorna se a localidade da peça está livre ou quando a Cor da peça for diferente
        }

        public override bool[,] VerificarMovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima ⬆
            // Na torre, as posições precisam de ser marcadas até o ultimo ponto possível na matriz ou até encontrar uma peça adversária
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.SupPeca(pos) != null && Tab.SupPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1; //Continua verificando as posições acima
            }

            // abaixo ⬇
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.SupPeca(pos) != null && Tab.SupPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1; //Continua verificando as posições acima
            }

            // direita ⮕
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.SupPeca(pos) != null && Tab.SupPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1; //Continua verificando as posições acima
            }

            // esquerda ⬅
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.SupPeca(pos) != null && Tab.SupPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1; //Continua verificando as posições acima
            }

            return mat;
        }
    }
}
