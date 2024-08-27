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
            /*
            Posicao p; objeto instanciado para posicao linha x coluna
            p = new Posicao(3, 4);
            Console.Write(p); 
            */

            /* try { 

            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.AdicionarPeca(new Torre(tab, Cor.Azul), new Posicao(0,0));
            tab.AdicionarPeca(new Torre(tab, Cor.Branco), new Posicao(1,3));
            tab.AdicionarPeca(new Rei(tab, Cor.Azul), new Posicao(4,3));

            Tela.ImprimirTabuleiro(tab);

            Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }*/

            PosicaoXadrez pos = new PosicaoXadrez('c', 7);
            Console.WriteLine(pos);

            Console.WriteLine(pos.ToPosicao());

        }
    }
}
