using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ЛАБА_ТВИМС__1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartForHitogramms = new System.Windows.Forms.DataVisualization.Charting.Chart();
            button1 = new Button();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label3 = new System.Windows.Forms.Label();
            button5 = new Button();
            button6 = new Button();
            label4 = new System.Windows.Forms.Label();
            numericUpDown3 = new NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            numericUpDown4 = new NumericUpDown();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            numericUpDown5 = new NumericUpDown();
            numericUpDown6 = new NumericUpDown();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            numericUpDown7 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartForHitogramms)).BeginInit();
            SuspendLayout();
            // 
            // chartForHitogramms
            // 
            chartArea2.Name = "ChartArea1";
            this.chartForHitogramms.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartForHitogramms.Legends.Add(legend2);
            this.chartForHitogramms.Location = new System.Drawing.Point(12, 12);
            this.chartForHitogramms.Name = "chartForHitogramms";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartForHitogramms.Series.Add(series2);
            this.chartForHitogramms.Size = new System.Drawing.Size(977, 630);
            this.chartForHitogramms.TabIndex = 5;
            this.chartForHitogramms.Text = "chart1";
            // 
            // button1
            // 
            button1.ForeColor = Color.Black;
            button1.Location = new Point(1008, 8);
            button1.Name = "button1";
            button1.Size = new Size(350, 34);
            button1.TabIndex = 0;
            button1.Text = "Создать гистограмму с равномерным распределением";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(998, 523);
            numericUpDown1.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(186, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(1101, 596);
            numericUpDown2.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(137, 23);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(998, 505);
            label1.Name = "label1";
            label1.Size = new Size(189, 15);
            label1.TabIndex = 6;
            label1.Text = "Количество элементов выборки:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1098, 563);
            label2.Name = "label2";
            label2.Size = new Size(238, 30);
            label2.TabIndex = 7;
            label2.Text = "Количество интревалов для \r\nвычисления (интервалы равновероятны):";
            // 
            // button2
            // 
            button2.Location = new Point(1008, 48);
            button2.Name = "button2";
            button2.Size = new Size(350, 32);
            button2.TabIndex = 8;
            button2.Text = "Создать гистограмму с нормальным распределением";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(10, 664);
            button3.Name = "button3";
            button3.Size = new Size(128, 95);
            button3.TabIndex = 9;
            button3.Text = "Пересоздать выборку равномерного распределения";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(144, 664);
            button4.Name = "button4";
            button4.Size = new Size(126, 95);
            button4.TabIndex = 10;
            button4.Text = "Пересоздать выборку нормального распределения";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label3
            // 
            label3.Location = new Point(412, 672);
            label3.Name = "label3";
            label3.Size = new Size(139, 73);
            label3.TabIndex = 11;
            label3.Text = "При изменении количества элементов выборки сама выборка будет пересоздана!";
            // 
            // button5
            // 
            button5.Location = new Point(1008, 86);
            button5.Name = "button5";
            button5.Size = new Size(350, 59);
            button5.TabIndex = 12;
            button5.Text = "Создать гистограмму с помощью метода ступенчатой аппроксимации с равновероятными интревалами.\r\nf(x) = 0.5x на интервале [0 ; 2]";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(275, 664);
            button6.Name = "button6";
            button6.Size = new Size(132, 95);
            button6.TabIndex = 13;
            button6.Text = "Пересоздать выборку для апроксимации по заданной функции";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(1008, 148);
            label4.MaximumSize = new Size(350, 0);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.No;
            label4.Size = new Size(293, 15);
            label4.TabIndex = 14;
            label4.Text = "\"Здесь будут данные по методу распределения №3\"\r\n";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(1208, 523);
            numericUpDown3.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(140, 23);
            numericUpDown3.TabIndex = 15;
            numericUpDown3.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(1205, 476);
            label5.Name = "label5";
            label5.Size = new Size(164, 45);
            label5.TabIndex = 16;
            label5.Text = "Количество интервалов для \r\nпостроения гистограммы\r\n(все интервалы равны):";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1101, 712);
            label6.Name = "label6";
            label6.Size = new Size(247, 15);
            label6.TabIndex = 17;
            label6.Text = "Уровень значимости ошибки первого рода\r\n";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // numericUpDown4
            // 
            numericUpDown4.DecimalPlaces = 2;
            numericUpDown4.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown4.Location = new Point(1101, 730);
            numericUpDown4.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(137, 23);
            numericUpDown4.TabIndex = 19;
            numericUpDown4.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1008, 427);
            label8.Name = "label8";
            label8.Size = new Size(347, 30);
            label8.TabIndex = 21;
            label8.Text = "Здесь будет информация о гипотезе высоком качестве чисел\r\n(\"выборочное распределение совпадает с теоретическим\").\r\n";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1098, 639);
            label7.Name = "label7";
            label7.Size = new Size(206, 30);
            label7.TabIndex = 22;
            label7.Text = "Количество интервалов для рачёта\r\nнаблюдаемого значения хи-квадрат";
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(1101, 672);
            numericUpDown5.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown5.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(137, 23);
            numericUpDown5.TabIndex = 23;
            numericUpDown5.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(750, 672);
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(120, 23);
            numericUpDown6.TabIndex = 24;
            numericUpDown6.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(750, 654);
            label9.Name = "label9";
            label9.Size = new Size(345, 15);
            label9.TabIndex = 25;
            label9.Text = "Математическое ожидание для нормального распределения";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(750, 708);
            label10.Name = "label10";
            label10.Size = new Size(254, 15);
            label10.TabIndex = 26;
            label10.Text = "Дисперсия для нормального распределения\r\n";
            // 
            // numericUpDown7
            // 
            numericUpDown7.Location = new Point(750, 726);
            numericUpDown7.Name = "numericUpDown7";
            numericUpDown7.Size = new Size(120, 23);
            numericUpDown7.TabIndex = 27;
            numericUpDown7.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 778);
            Controls.Add(numericUpDown7);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(numericUpDown6);
            Controls.Add(numericUpDown5);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(numericUpDown4);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(numericUpDown3);
            Controls.Add(label4);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Гистограммы распределений";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartForHitogramms)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartForHitogramms;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private NumericUpDown numericUpDown7;
    }
}
