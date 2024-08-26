namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao {  get; set; }
        public Cor cor {  get; protected set; } //Acessa dados apenas pela subclasse
        public int QntMovimentos { get; protected set; }
        public Tabuleiro tab {  get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab)
        {
            this.posicao = posicao;
            this.cor = cor;
            this.tab = tab;
            QntMovimentos = 0; //No inicio do jogo tem 0 movimentos
        }
    }
}
