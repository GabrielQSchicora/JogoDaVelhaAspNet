using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JogoDaVelha;

namespace JogoDaVelhaWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/JogoDaVelha")]
    public class JogoDaVelhaController : Controller
    {

        static TabuleiroJogo Jogo { get; set; }

        [HttpGet("{jogador}/{posX}/{posY}")]
        public int Jogar(int jogador, int posX, int posY)
        {
            Jogo.Jogar(jogador, posX, posY);
            int checkWinner = Jogo.VerificarGanhador(null);

            if(checkWinner == 0 && jogador == 1)
            {

                int playPosX = 999;
                int playPosY = 999;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int[][] tabuleiroTeste = new int[3][];
                        tabuleiroTeste[0] = new int[3];
                        tabuleiroTeste[1] = new int[3];
                        tabuleiroTeste[2] = new int[3];
                        Array.Copy(Jogo.Tabuleiro[0], tabuleiroTeste[0], 3);
                        Array.Copy(Jogo.Tabuleiro[1], tabuleiroTeste[1], 3);
                        Array.Copy(Jogo.Tabuleiro[2], tabuleiroTeste[2], 3);

                        if (tabuleiroTeste[i][j] == 0)
                        {
                            tabuleiroTeste[i][j] = jogador;

                            var result = Jogo.VerificarGanhador(tabuleiroTeste);

                            if(result == jogador)
                            {
                                playPosX = j;
                                playPosY = i;
                                break;
                            }
                            else
                            {
                                tabuleiroTeste[i][j] = Jogo.JogadorAtual;

                                var result2 = Jogo.VerificarGanhador(tabuleiroTeste);

                                if (result2 == Jogo.JogadorAtual)
                                {
                                    playPosX = j;
                                    playPosY = i;
                                    break;
                                }
                            }
                        }
                    }
                    if (playPosX != 999 || playPosY != 999)
                    {
                        break;
                    }
                }

                if(playPosX == 999 || playPosY == 999)
                {
                    Random rand = new Random();
                    do
                    {
                        playPosX = rand.Next(3);
                        playPosY = rand.Next(3);
                    } while (Jogo.Tabuleiro[playPosY][playPosX] != 0);
                }
                
                return this.Jogar(Jogo.JogadorAtual, playPosX, playPosY);
            }
            else
            {
                return checkWinner;
            }
        }

        [HttpGet("Reset")]
        public String Reiniciar()
        {
            Jogo = new TabuleiroJogo();
            return "Jogo Reiniciado";
        }

        [HttpGet()]
        public TabuleiroJogo Get()
        {
            return Jogo;
        }

    }
}