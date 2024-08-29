//  Arquitetura do sistema de xadrez:
//  Camada de Aplicação         - Modo que consome os dados do utilizador 
//  <-> Camada Jogo de Xadrez   - Regras do jogo
//  <-> Camada Tabuleiro        - Representação dos objetos(peças, tabuleiro)
// Em camadas facilita o projeto em manutenções, boas práticas de código pois temos tudo bem organizado e distinguido.
using tabuleiro;
using System;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {


            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Termidada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);
                        bool[,] posicoesPossiveis = partida.Tab.SupPeca(origem).VerificarMovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

/*
           Posicao p; objeto instanciado para posicao linha x coluna
           p = new Posicao(3, 4);
           Console.Write(p); 


//PosicaoXadrez pos = new PosicaoXadrez('c', 7);
//Console.WriteLine(pos);

//Console.WriteLine(pos.ToPosicao());
           
 */


