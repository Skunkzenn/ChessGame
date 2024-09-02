using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using tabuleiro;
using xadrez;
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
        public Peca VulneravelEnPassant { get; private set; }; // Peça para verificar o movimento passant 
        private PartidaDeXadrez Partida; //Campo privativo, para que o rei tenha acesso a partida

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Termidada = false;
            ConjuntoPeca = new HashSet<Peca>();
            ConjuntoCapturado = new HashSet<Peca>();
            Xeque = false;
            VulneravelEnPassant = null;
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(destino);
            Tab.AdicionarPeca(p, destino);
            if (pecaCapturada != null)
            {
                ConjuntoCapturado.Add(pecaCapturada);
            }

            //# jogada especial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                //O rei esta na linha da torre, logo a origem do da torre para o rei é de 3 casas a frente, ou seja, 3 colunas
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);

                //A torre no roque, tem seu destino sendo uma casa a frente do rei, ou seja, +1
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tab.RemoverPeca(origemT);
                T.IncrementarMovimentos();
                Tab.AdicionarPeca(T, destinoT);
            }

            //# jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                //A posição de origem da torre é de -4 referente a posição do Rei
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna -4);

                //A posição de destino da torre é -1 posição da posição do Rei
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tab.RemoverPeca(origemT);
                T.IncrementarMovimentos();
                Tab.AdicionarPeca(T, destinoT);
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

            //# desfaz jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RemoverPeca(destinoT);
                T.DecrementarMovimentos();
                Tab.AdicionarPeca(T, origemT);
            }

            //# desfaz jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RemoverPeca(destinoT);
                T.DecrementarMovimentos();
                Tab.AdicionarPeca(T, origemT);
            }

        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            Peca p = Tab.SupPeca(destino);

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

            // #jogadaespecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
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
            // Peças brancas
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branco));
            for (char coluna = 'a'; coluna <= 'h'; coluna++)
            {
                ColocarNovaPeca(coluna, 2, new Peao(Tab, Cor.Branco, this));
            }

            // Peças pretas
            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preto));
            for (char coluna = 'a'; coluna <= 'h'; coluna++)
            {
                ColocarNovaPeca(coluna, 7, new Peao(Tab, Cor.Preto, this));
            }
        }
    }
}
