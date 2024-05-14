using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainPagesProjetoJane
{
    public partial class homePage : Form
    {
        private Rectangle[] cardOriginalRectangle = new Rectangle[4];
        private Rectangle originalFormSize;

        public homePage()
        {
            InitializeComponent();
        }

        private void homePage_Load(object sender, EventArgs e)
        {
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            cardOriginalRectangle[0] = fazerRetangulo(card1);
            cardOriginalRectangle[1] = fazerRetangulo(card2);
            cardOriginalRectangle[2] = fazerRetangulo(card3);
            cardOriginalRectangle[3] = fazerRetangulo(card4);
        }

        private void resizeControlWidth(Rectangle r, Control c, bool posicaoFixa)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);

            int newX = (int)(r.Width * xRatio);
            int newY = (int)(r.Height * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            
            if(posicaoFixa == false)
                c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private Rectangle fazerRetangulo(Control obj)
        {
            Rectangle r = new Rectangle(obj.Location.X, obj.Location.Y, obj.Width, obj.Height);
            return r;
        }

        private void homePage_Resize(object sender, EventArgs e)
        {
            resizeControlWidth(cardOriginalRectangle[0], card1, true);
            resizeControlWidth(cardOriginalRectangle[1], card2, true);
            resizeControlWidth(cardOriginalRectangle[2], card3, true);
            resizeControlWidth(cardOriginalRectangle[3], card4, true);
        }
    }
}
