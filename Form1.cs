using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace CatchButton3_3번째_시도_
{
    public partial class Form1 : Form
    {
        private readonly Random _rand = new Random();
        private bool _isWaiting = false;

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

        private async void Button_MouseEnter(object sender, EventArgs e)
        {
            if (await MaybeWaitAsync())
                return;

            MoveButtonAway();
        }

        private async void Button_MouseMove(object sender, MouseEventArgs e)
        {
            // 이미 대기중이면 동작하지 않음
            if (_isWaiting)
                return;

            if (await MaybeWaitAsync())
                return;

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

        // 때때로 버튼이 잡히도록 최대 2초(랜덤) 동안 기다리게 함.
        // 반환값: 기다렸다면 true (이벤트 핸들러는 이동을 중단), 아니면 false
        private async Task<bool> MaybeWaitAsync()
        {
            // 이미 대기중이면 추가로 대기하지 않음
            if (_isWaiting)
                return true;

            // 대기 확률: 30%
            if (_rand.NextDouble() > 0.3)
                return false;

            // 랜덤 대기 시간: 200 ~ 2000 ms
            int waitMs = _rand.Next(200, 2001);
            _isWaiting = true;
            try
            {
                await Task.Delay(waitMs);
            }
            finally
            {
                _isWaiting = false;
            }

            return true;
        }

        private void 나잡아봐_Click(object sender, EventArgs e)
        {
            // 버튼 클릭 시 효과음 재생
            try
            {
                SystemSounds.Asterisk.Play();
            }
            catch
            {
                // 소리 재생 중 오류가 나더라도 앱이 중단되지 않도록 무시
            }
            // 클릭 시 축하 메시지 표시
            try
            {
                MessageBox.Show("축하합니다!!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                // 메시지 표시 실패 무시
            }
        }
    }
}
