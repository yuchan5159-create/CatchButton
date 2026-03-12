using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchButton3_3번째_시도_
{
    public partial class Form1 : Form
    {
        private readonly Random _rand = new Random();

        public Form1()
        {
            InitializeComponent();
            // 마우스가 버튼 위에 올라갈 때 도망가도록 이벤트 등록
            this.나잡아봐.MouseEnter += Button_MouseEnter;
            this.나잡아봐.MouseMove += Button_MouseMove;
            // 버튼 위치 변경 시 제목에 좌표 표시
            this.나잡아봐.LocationChanged += Button_LocationChanged;
            // 폼 크기 변경 시 버튼이 폼 밖으로 나가지 않도록 처리
            this.Resize += Form1_Resize;
            this.ClientSizeChanged += Form1_ClientSizeChanged;
            UpdateTitle();
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            MoveButtonAway();
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            // 마우스가 버튼 내부에서 움직일 때도 도망가게 함
            MoveButtonAway();
        }

        private void MoveButtonAway()
        {
            var btn = this.나잡아봐;
            var client = this.ClientSize;
            int maxX = Math.Max(0, client.Width - btn.Width);
            int maxY = Math.Max(0, client.Height - btn.Height);

            // 현재 마우스 위치 (폼 기준)
            Point mouse = this.PointToClient(Cursor.Position);

            // 새 위치를 반복해서 뽑아 마우스와 충분히 멀게 한다
            for (int i = 0; i < 100; i++)
            {
                int x = _rand.Next(0, maxX + 1);
                int y = _rand.Next(0, maxY + 1);
                var candidate = new Point(x, y);
                double dx = candidate.X + btn.Width / 2 - mouse.X;
                double dy = candidate.Y + btn.Height / 2 - mouse.Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist > Math.Min(client.Width, client.Height) * 0.25) // 충분히 멀리
                {
                    btn.Location = candidate;
                    UpdateTitle();
                    return;
                }
            }

            // 실패하면 그냥 랜덤으로 배치
            btn.Location = new Point(_rand.Next(0, maxX + 1), _rand.Next(0, maxY + 1));
            UpdateTitle();
        }

        private void Button_LocationChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ClampButtonInsideForm();
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            ClampButtonInsideForm();
        }

        private void ClampButtonInsideForm()
        {
            var btn = this.나잡아봐;
            var client = this.ClientSize;
            int maxX = Math.Max(0, client.Width - btn.Width);
            int maxY = Math.Max(0, client.Height - btn.Height);

            int x = Math.Min(Math.Max(0, btn.Location.X), maxX);
            int y = Math.Min(Math.Max(0, btn.Location.Y), maxY);

            if (x != btn.Location.X || y != btn.Location.Y)
            {
                btn.Location = new Point(x, y);
            }

            UpdateTitle();
        }

        private void UpdateTitle()
        {
            var p = this.나잡아봐.Location;
            this.Text = $"Button: ({p.X}, {p.Y})";
        }
    }
}
