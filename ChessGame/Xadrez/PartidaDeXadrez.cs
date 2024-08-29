using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using tabuleiro;

namespace xadrez_console
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Termidada { get; private set; }
        private HashSet<Peca> ConjuntoPeca;
        private HashSet<Peca> ConjuntoCapturado;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Termidada = false;
            ConjuntoPeca = new HashSet<Peca>();
            ConjuntoCapturado = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao ori, Posicao dest)
        {
            Peca p = Tab.RemoverPeca(ori);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(dest);
            Tab.AdicionarPeca(p, dest);
            if (pecaCapturada != null)
            {
                ConjuntoCapturado.Add(pecaCapturada);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            AlteraJogador();
        }

        //Altera jogador atual
        private void AlteraJogador()
        {
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tab.SupPeca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça nessa posição escolhida.");
            }
            if (JogadorAtual != Tab.SupPeca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem, requer que seja uma sua.");
            }
            if (!Tab.SupPeca(pos).ExisteMovimentosPossiveis()){
                throw new TabuleiroException("Não existe movimentos possíveis.");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.SupPeca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino inválida!");
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in ConjuntoCapturado)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in ConjuntoPeca)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }


        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.AdicionarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            ConjuntoPeca.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branco));

            ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preto));

        }
    }
}
