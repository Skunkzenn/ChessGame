# Chess Console Game in C#

## Sobre o Projeto
Este é um jogo de Xadrez desenvolvido em C#, jogado via consola. O objetivo é criar uma simulação simples e funcional de uma partida de Xadrez, com todas as peças e regras do jogo implementadas.

## Estrutura do Projeto
O projeto está organizado em duas pastas principais: Tabuleiro e Xadrez.

## Pasta Tabuleiro
Esta pasta contém as classes e componentes fundamentais que formam a base do tabuleiro de Xadrez:
- **Cor.cs**: Define as cores das peças (Branco e Preto).
- **Peca.cs**: Classe base para todas as peças do jogo.
- **Posicao.cs**: Representa uma posição no tabuleiro (linha e coluna).
- **Tabuleiro.cs**: Representa o tabuleiro de Xadrez em si, gerenciando as posições e peças.
- **TabuleiroException.cs**: Trata exceções relacionadas ao tabuleiro (como posições inválidas).
- **Tela.cs**: Responsável por mostrar o estado do tabuleiro e as peças no console.

## Pasta Xadrez
Esta pasta contém as classes específicas do jogo de Xadrez:

- **PartidaDeXadrez.cs**: Gerencia o andamento da partida, controlando os turnos, movimentos e regras.
- **PosicaoXadrez.cs**: Converte posições tradicionais de Xadrez (como "e2") para o formato de Posicao usado no tabuleiro.

Peças do Xadrez:
- Rei.cs
- Rainha.cs
- Torre.cs
- Bispo.cs
- Cavalo.cs
- Peao.cs

## Funcionalidades
- **Implementação completa das regras do Xadrez.**
- **Suporte para todas as peças: Rei, Rainha, Torre, Bispo, Cavalo e Peão.**
- **Movimentos especiais como roque, promoção de peão e en passant.**
- **Validação de movimentos e captura de peças.**
- **Tratamento de exceções para garantir movimentos válidos.**

### Como Jogar
Clone este repositório:
```bash
git clone https://github.com/seu-usuario/chess-console-game.git

2. Navegue até o diretório do projeto:
```bash
cd chess-console-game

Compile e execute o projeto:
dotnet run
Siga as instruções no console para jogar uma partida de Xadrez.

Exemplo de Uso
No início do jogo, o tabuleiro será exibido no console. Os jogadores serão solicitados a inserir seus movimentos no formato de notação de Xadrez (por exemplo, "e2 e4" para mover um peão).
