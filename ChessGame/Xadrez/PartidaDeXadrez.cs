using System.Runtime.CompilerServices;
using tabuleiro;

namespace xadrez_console
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Termidada {  get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Termidada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao ori, Posicao dest)
        {
            Peca p = Tab.RemoverPeca(ori);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(dest);
            Tab.AdicionarPeca(p, dest);
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

        private void ColocarPecas()
        {
            Tab.AdicionarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.AdicionarPeca(new Rei(Tab, Cor.Branco), new PosicaoXadrez('d', 1).ToPosicao());

            Tab.AdicionarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.AdicionarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.AdicionarPeca(new Rei(Tab, Cor.Preto), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }
}
