using tabuleiro;
using xadrez_console;

namespace xadrez
{

    class Peao : Peca
    {

        private PartidaDeXadrez Partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.SupPeca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.SupPeca(pos) == null;
        }

        public override bool[,] VerificarMovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branco)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && Livre(p2) && Tab.PosicaoValida(pos) && Livre(pos) && QntMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //// #JogadaEspecial En Passant
                //if (Posicao.Linha == 3)
                //{
                //    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                //    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.SupPeca(esquerda) == Partida.VulneravelEnPassant)
                //    {
                //        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                //    }
                //    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                //    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.SupPeca(direita) == Partida.VulneravelEnPassant)
                //    {
                //        mat[direita.Linha - 1, direita.Coluna] = true;
                //    }
                //}
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && Livre(p2) && Tab.PosicaoValida(pos) && Livre(pos) && QntMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //// #JogadaEspecial En Passant
                //if (Posicao.Linha == 4)
                //{
                //    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                //    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.SupPeca(esquerda) == Partida.VulneravelEnPassant)
                //    {
                //        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                //    }
                //    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                //    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.SupPeca(direita) == Partida.VulneravelEnPassant)
                //    {
                //        mat[direita.Linha + 1, direita.Coluna] = true;
                //    }
                //}
            }

            return mat;
        }
    }
}
