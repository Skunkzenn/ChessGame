namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas {  get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas; //No tabuleiro temos uma matriz de peças, logo deixamos em privado para apenas o tabuleiro realizar alterações

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas]; // Instancia a matriz de peças com a quantidade de linhas e colunas
        }

        //Método que usa coordenadas (linha e coluna) como parâmetros para acessar uma peça específica na matriz Pecas
        public Peca SupPeca(int linha, int coluna) 
        {
            return Pecas[linha, coluna];
        }

        //Método com sobrecarga que usa um objeto Posicao que encapsula as coordenadas
        public Peca SupPeca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        //Método para verificar se existe uma peça em dada posição
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos); //Valida a posição sob o tabuleiro, para não haver peças foram da matriz
            return SupPeca(pos) != null; //Obtem a posição da peça na matriz
        }


        //Método para adicionar peças ao tabuleiro
        public void AdicionarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos)) //Só podemos adiconar uma peça em lugares onde não temos peças
            {
                throw new TabuleiroException("Já existe uma peça nessa posição.");
            }
            Pecas[pos.Linha, pos.Coluna] = p; //Acessa a matriz de pecas em pos.linha e pos.coluna
            p.Posicao = pos; //Adiciona a peça na posicao
        }

        public Peca RemoverPeca(Posicao pos)
        {
            // Verifica se há uma peça na posição; se não houver, retorna null
            if (SupPeca(pos) == null)
            {
                return null;
            }
            Peca aux = SupPeca(pos); // Armazena a peça que está na posição
            aux.Posicao = null; // Remove a referência à posição da peça (a peça não está mais no tabuleiro)
            Pecas[pos.Linha, pos.Coluna] = null; // Remove a peça da matriz, definindo a posição como null (vazia)
            return aux;
        }


        //Função para verificar se a posição(pos) da peça é valida dentro do tabuleiro, de forma que não ultrapasse a matriz
        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Coluna < 0 || pos.Coluna >= Colunas || pos.Linha < 0 || pos.Linha >= Linhas)
            {
                return false;
            }
            return true;
        }

        //Função para validar a posição e lançar exceção 
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida.");
            }
        }

    }
}
