using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using tabuleiro;
using static System.Net.Mime.MediaTypeNames;

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
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Termidada = false;
            ConjuntoPeca = new HashSet<Peca>();
            ConjuntoCapturado = new HashSet<Peca>();
            Xeque = false;
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao ori, Posicao dest)
        {
            Peca p = Tab.RemoverPeca(ori);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(dest);
            Tab.AdicionarPeca(p, dest);
            if (pecaCapturada != null)
            {
                ConjuntoCapturado.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RemoverPeca(destino);
            p.DecrementarMovimentos();
            if (pecaCapturada != null)
            {
                Tab.AdicionarPeca(pecaCapturada, destino);
                ConjuntoCapturado.Remove(pecaCapturada);
            }
            Tab.AdicionarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversario(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversario(JogadorAtual)))
            {
                Termidada = true;
            }
            else
            {
                Turno++;
                AlteraJogador();
            }
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
            if (!Tab.SupPeca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existe movimentos possíveis.");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.SupPeca(origem).MovimentoPossivel(destino))
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

        //Método para definir o adversário
        private Cor Adversario(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca ReiFunc(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca r = ReiFunc(cor);

            if (r == null)
            {
                throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversario(cor)))
            {
                bool[,] mat = x.VerificarMovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.VerificarMovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.AdicionarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            ConjuntoPeca.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branco));
            ColocarNovaPeca('h', 7, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branco));

            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Preto));
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preto));

        }
    }
}
