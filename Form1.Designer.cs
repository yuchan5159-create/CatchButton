namespace CatchButton3_3번째_시도_
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.나잡아봐 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // 나잡아봐
            // 
            this.나잡아봐.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.나잡아봐.Location = new System.Drawing.Point(298, 143);
            this.나잡아봐.Name = "나잡아봐";
            this.나잡아봐.Size = new System.Drawing.Size(181, 73);
            this.나잡아봐.TabIndex = 0;
            this.나잡아봐.Text = "나 잡아봐";
            this.나잡아봐.UseVisualStyleBackColor = false;
            this.나잡아봐.Click += new System.EventHandler(this.나잡아봐_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.나잡아봐);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button 나잡아봐;
    }
}

