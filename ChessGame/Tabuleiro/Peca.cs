namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao {  get; set; }
        public Cor Cor {  get; protected set; } //Acessa dados apenas pela subclasse
        public int QntMovimentos { get; protected set; }
        public Tabuleiro Tab {  get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null; //quem coloca peças no tabuleiro é o tabuleiro e nao a peça, por isso a posição inicia-se em 0, para assim realizar os movimentos
            Cor = cor;
            Tab = tab;
            QntMovimentos = 0; //No inicio do jogo tem 0 movimentos
        }

        public void IncrementarMovimentos()
        {
            QntMovimentos++;
        }

        public void DecrementarMovimentos()
        {
            QntMovimentos--;
        }

        public bool ExisteMovimentosPossiveis() {
            bool[,] mat = VerificarMovimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++) {
                for (int j = 0; j < Tab.Colunas; j++) {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao pos)
        {
            return VerificarMovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] VerificarMovimentosPossiveis(); //Verdadeiro aonde for um movimento possível e falso onde não for.
       
    }
}
