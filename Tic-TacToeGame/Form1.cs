using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_TacToeGame.Properties;

namespace Tic_TacToeGame
{
    public partial class frmTic_Tac_ToeGame : Form
    {
        enPlayer PlayerTurn = enPlayer.Player1;
        stGameStatus GameStatus;
 
        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public  enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }
        public frmTic_Tac_ToeGame()
        {
            InitializeComponent();
        }

        private void frmTic_Tac_ToeGame_Paint(object sender, PaintEventArgs e)
        {
            Color color = Color.White;
            Pen pen = new Pen(color);
            pen.Width = 10;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            
            e.Graphics.DrawLine(pen, 450, 100, 450, 350);
            e.Graphics.DrawLine(pen, 550, 100, 550, 350);

            e.Graphics.DrawLine(pen, 350, 175, 650, 175);
            e.Graphics.DrawLine(pen, 350, 275, 650, 275);

        }

        void EndGame()
        {
            lblTurn.Text = "Game Over";
           switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("Game over", "Game over", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
        }

        bool CheckValue(PictureBox pctb1, PictureBox pctb2, PictureBox pctb3)
        {
            if(pctb1.Tag.ToString() != "?" && 
                pctb1.Tag.ToString() == pctb2.Tag.ToString() && pctb1.Tag.ToString() == pctb3.Tag.ToString())
            {
                pctb1.BackColor = Color.Green;
                pctb2.BackColor = Color.Green;
                pctb3.BackColor = Color.Green;

                if(pctb1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                if (pctb1.Tag.ToString() == "O")
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
               
            }
            GameStatus.GameOver = false;
            return false;
        }

        void CheckWinner()
        {
            if(CheckValue(pictureBox1,pictureBox2,pictureBox3))
                return;
            if (CheckValue(pictureBox4, pictureBox5, pictureBox6))
                return;
            if (CheckValue(pictureBox7, pictureBox8, pictureBox9))
                return;
            if (CheckValue(pictureBox1, pictureBox4, pictureBox7))
                return;
            if (CheckValue(pictureBox2, pictureBox5, pictureBox8))
                return;
            if (CheckValue(pictureBox3, pictureBox6, pictureBox9))
                return;
            if (CheckValue(pictureBox1, pictureBox5, pictureBox9))
                return;
            if (CheckValue(pictureBox3, pictureBox5, pictureBox7))
                return;

        }

        void UpdatePicture(PictureBox pctb)
        {
            
           if (pctb.Tag.ToString() == "?" )
            {
                switch(PlayerTurn)
                {
                    case enPlayer.Player1:
                        pctb.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        pctb.Tag = "X";
                        lblTurn.Text = "Player2";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                     case enPlayer.Player2:
                        pctb.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        pctb.Tag = "O";
                        lblTurn.Text = "Player1";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;

                }
            }
           else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }
           if(GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
          
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            UpdatePicture((PictureBox)sender);

        }

       

        void ResetPicturBox(PictureBox  pictureBox)
        {
            pictureBox.Image=Resources.question_mark_96;
            pictureBox.Tag ="?";
            pictureBox.BackColor=Color.Black;
        }
       void ResstartGame()
        {
            ResetPicturBox(pictureBox1);
            ResetPicturBox(pictureBox2);
            ResetPicturBox(pictureBox3);
            ResetPicturBox(pictureBox4);
            ResetPicturBox(pictureBox5);
            ResetPicturBox(pictureBox6);
            ResetPicturBox(pictureBox7);
            ResetPicturBox(pictureBox8);
            ResetPicturBox(pictureBox9);

            lblTurn.Text = "Player1";
            lblWinner.Text = "In Progess";
            PlayerTurn = enPlayer.Player1;
            GameStatus.PlayCount = 0;
            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.GameOver = false;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResstartGame();
        }
    }
}
