using tabuleiro;

namespace xadrez_console
{
    class Rei : Peca //Subclasse de peça
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
