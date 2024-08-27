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


    }
}
