using System.Data;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ЛАБА_ТВИМС__1
{
    public static class QuantileCalculator
    {
        public enum DistributionType { Normal, T, ChiSquare }

        /// <summary>
        /// Единая функция для вычисления квантилей.
        /// p – вероятность (например, 1-α/2 для верхнего квантиля доверительного интервала),
        /// df – число степеней свободы (используется для T и ChiSquare).
        /// </summary>
        public static double GetQuantile(DistributionType distribution, double p, int df = 0)
        {
            switch (distribution)
            {
                case DistributionType.Normal:
                    return InverseNormalCDF(p);
                case DistributionType.T:
                    return GetTQuantile(p, df);
                case DistributionType.ChiSquare:
                    return GetChiSquareQuantile(p, df);
                default:
                    throw new ArgumentException("Не поддерживаемый тип распределения.");
            }
        }

        // Обратная функция стандартного нормального распределения (алгоритм Питера Дж. Энкама)
        private static double InverseNormalCDF(double p)
        {
            if (p <= 0.0 || p >= 1.0)
                throw new ArgumentOutOfRangeException(nameof(p), "p должна принадлежать интервалу (0,1)");

            double[] a = { -39.69683028665376, 220.9460984245205, -275.9285104469687,
                           138.3577518672690, -30.66479806614716, 2.506628277459239 };
            double[] b = { -54.47609879822406, 161.5858368580409,
                           -155.6989798598866, 66.80131188771972, -13.28068155288572 };
            double[] c = { -0.007784894002430293, -0.3223964580411368,
                           -2.400758277161838, -2.549732539343734,
                           4.374664141464968, 2.938163982698783 };
            double[] d = { 0.007784695709041462, 0.32246712907004,
                           2.445134137142996, 3.754408661907416 };

            double plow = 0.02425;
            double phigh = 1 - plow;
            double q, r;
            if (p < plow)
            {
                q = Math.Sqrt(-2 * Math.Log(p));
                double num = ((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5];
                double den = (((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1;
                return -num / den;
            }
            else if (p > phigh)
            {
                q = Math.Sqrt(-2 * Math.Log(1 - p));
                double num = ((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5];
                double den = (((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1;
                return num / den;
            }
            else
            {
                q = p - 0.5;
                r = q * q;
                double numerator = ((((a[0] * r + a[1]) * r + a[2]) * r + a[3]) * r + a[4]) * r + a[5];
                double denominator = ((((b[0] * r + b[1]) * r + b[2]) * r + b[3]) * r + b[4]) * r + 1;
                return q * numerator / denominator;
            }
        }

        // Функция для приближённого вычисления квантиля t‑распределения.
        private static double GetTQuantile(double p, int df)
        {
            // Если df маленькое – можно использовать табличные значения.
            if (df == 1) return 12.706;
            if (df == 2) return 4.303;
            // Приближённое масштабирование для df>=3
            return InverseNormalCDF(p) * Math.Sqrt(df / (double)(df - 2));
        }

        // Функция для приближённого вычисления квантиля χ²‑распределения по приближению Уилсона–Хилфрити.
        private static double GetChiSquareQuantile(double p, int df)
        {
            double z = InverseNormalCDF(p);
            double term = 1 - 2.0 / (9.0 * df) + Math.Sqrt(2.0 / (9.0 * df)) * z;
            return df * Math.Pow(term, 3);
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chartForHitogramms.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
        }

        Random rand = new Random();

        private double Max;
        private double Min;
        private int initNumOfValuesReg = 0;
        private int initNumOfValuesNorm = 0;
        private int initNumOfValuesLinVal = 0;
        private double[] regularDistrib;
        private double[] normalDistrib;
        double[] Yvalue;
        double[] Xvalue;
        double[] sample;
        double[] xi;
        double sampleVar, groupedMedian, orderedMedian, modeSimple, modeAdjusted, avg;
        private void button1_Click(object sender, EventArgs e)
        {
            int numOfvalues = (int)this.numericUpDown1.Value;
            int numOfBars = (int)this.numericUpDown2.Value;

            if (CheckForNewParametersReg(numOfvalues) || regularDistrib == null)
                regularDistrib = CreateArrayForRegularDistib(numOfvalues);

            ShowChart(regularDistrib, numOfvalues, numOfBars);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int numOfvalues = (int)this.numericUpDown1.Value;
            int numOfBars = (int)this.numericUpDown2.Value;
            int mean = (int)this.numericUpDown6.Value;
            int varicance = (int)this.numericUpDown7.Value;

            if (CheckForNewParametersNorm(numOfvalues) || normalDistrib == null)
                normalDistrib = CreateArrayForNormalDistrib(numOfvalues, mean, varicance);

            // Выполнение оценки параметров и построения доверительных интервалов
            label4.Text = EstimateParametersAndCIs(normalDistrib, mean, varicance);

            CreateSterdjisChart(normalDistrib);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            regularDistrib = null;
            regularDistrib = CreateArrayForRegularDistib((int)this.numericUpDown1.Value);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            normalDistrib = null;
            int mean = (int)this.numericUpDown6.Value;
            int varicance = (int)this.numericUpDown7.Value;

            normalDistrib = CreateArrayForNormalDistrib((int)this.numericUpDown1.Value, mean, varicance);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int numOfvalues = (int)this.numericUpDown1.Value;
            int numOfBars = (int)this.numericUpDown2.Value;

            sample = null;
            xi = null;

            xi = CreateBordersForLin(numOfBars);
            sample = CreateSampleForLin(numOfvalues, numOfBars);
            sampleVar = ComputeSampleVariance();
            groupedMedian = ComputeGroupedMedian(numOfvalues, numOfBars);
            orderedMedian = ComputeOrderedMedian();
            var modes = ComputeModes();
            modeSimple = modes.modeSimple;
            modeAdjusted = modes.modeAdjusted;
            avg = sample.Average();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            chartForHitogramms.Series[0].Points.Clear();

            int numOfvalues = (int)numericUpDown1.Value;
            int numOfBars = (int)numericUpDown2.Value;
            int numOfBarsForHist = (int)numericUpDown3.Value;

            xi = CreateBordersForLin(numOfBars);

            if (CheckForNewParametersLin(numOfvalues))
                sample = CreateSampleForLin(numOfvalues, numOfBars);

            ShowChart(sample, numOfvalues, numOfBarsForHist);

            ComputeCharsOfLin();

            PirsDistributionHiSq();
        }
        private double[] CreateBordersForLin(int numOfBars)
        {
            double[] xi = null;
            xi = new double[numOfBars + 1];
            for (int i = 0; i <= numOfBars; i++)
                xi[i] = 2 * Math.Sqrt((double)i / numOfBars); // xi = 2√(i/n)
            return xi;
        }
        private double[] CreateSampleForLin(int numOfvalues, int numOfBars)
        {
            double[] sample_h = new double[numOfvalues];
            double Pi = 1 / (double)numOfBars;

            for (int i = 0; i < numOfvalues; i++)
            {
                double x = GetX(Pi, xi);
                sample_h[i] = x;
            }
            Array.Sort(sample_h);

            return sample_h;
        }
        private double GetX(double Pi, double[] xi)
        {
            int idx = 1;
            double r = rand.NextDouble();

            while (true)
            {
                if (r < Pi) break;
                r = r - Pi;
                idx++;
            }

            double r2 = rand.NextDouble();
            double x = xi[idx - 1] + r2 * (xi[idx] - xi[idx - 1]);
            return x;
        }
        private double ComputeSampleVariance()
        {
            double mean = sample.Average();
            double sum = 0;
            for (int i = 0; i < sample.Length; i++)
                sum += Math.Pow(sample[i] - mean, 2);
            double variance = (1 / (double)(sample.Length - 1) * sum);
            return variance;
        }
        private double ComputeGroupedMedian(int numValues, int numOfBars)
        {
            int bars = Yvalue.Length;
            double cumulative = 0.0;
            for (int i = 0; i < bars; i++)
            {
                double prevCumulative = cumulative;
                double width = i != bars - 1 ? Xvalue[i + 1] - Xvalue[i] : 2 - Xvalue[i];

                cumulative += Yvalue[i] * width;
                if (cumulative >= 0.5)
                {
                    double L = Xvalue[i];
                    double h = width;
                    double median = L + ((0.5 - prevCumulative) / Yvalue[i]) * h;
                    return median;
                }
            }
            return -1; // На практике эта точка найдется, поэтому возвращение -1 означает ошибку.
        }
        private double ComputeOrderedMedian()
        {
            int n = sample.Length;
            if (n % 2 == 1)
                return sample[n / 2];
            else
                return (sample[n / 2 - 1] + sample[n / 2]) / 2.0;
        }
        private (double modeSimple, double modeAdjusted) ComputeModes()
        {
            int bars = Yvalue.Length;
            int modalIndex = 0;
            double maxCount = Yvalue[0];
            for (int i = 1; i < bars; i++)
            {
                if (Yvalue[i] > maxCount)
                {
                    maxCount = Yvalue[i];
                    modalIndex = i;
                }
            }
            double modeSimple = (Xvalue[modalIndex] + (modalIndex != bars - 1 ? Xvalue[modalIndex + 1] : 2)) / 2.0;

            double L = Xvalue[modalIndex];
            double h = (modalIndex != bars - 1 ? Xvalue[modalIndex + 1] : 2) - Xvalue[modalIndex];

            double f_m = Yvalue[modalIndex];
            double f_prev = modalIndex == 0 ? 0 : Yvalue[modalIndex - 1];
            double f_next = modalIndex == bars - 1 ? 0 : Yvalue[modalIndex + 1];
            double denominator = (f_m - f_prev) + (f_m - f_next);
            double modeAdjusted = denominator == 0 ? modeSimple : L + ((f_m - f_prev) / denominator) * h;
            return (modeSimple, modeAdjusted);
        }
        private void ComputeCharsOfLin()
        {
            int numOfvalues = (int)numericUpDown1.Value;
            int numOfBars = (int)numericUpDown2.Value;

            sampleVar = ComputeSampleVariance();
            groupedMedian = ComputeGroupedMedian(numOfvalues, numOfBars);
            orderedMedian = ComputeOrderedMedian();
            var modes = ComputeModes();
            modeSimple = modes.modeSimple;
            modeAdjusted = modes.modeAdjusted;
            avg = sample.Average();

            string result =
                "Результаты оценки распределения:\n\n" +
                $"1. Дисперсия:\n" +
                $"   - По исходной (несгруппированной) выборке: {sampleVar:F4}\n\n" +
                $"2. Медиана:\n" +
                $"   - Через интервальный ряд (кумулятивная кривая): {groupedMedian:F4}\n" +
                $"   - По упорядоченной выборке: {orderedMedian:F4}\n\n" +
                $"3. Мода:\n" +
                $"   - Середина интервала с наибольшей частотой: {modeSimple:F4}\n" +
                $"   - С поправкой на соседние интервалы: {modeAdjusted:F4}\n\n" +
                $"4. Среднее значение выборки: {avg:F4}";

            this.label4.Text = result;
        }
        private void PirsDistributionHiSq()
        {
            var _ex = new Microsoft.Office.Interop.Excel.Application();

            int numIntervals = (int)numericUpDown5.Value;
            double overallMin = 0.0;
            double overallMax = 2.0;
            double step = (overallMax - overallMin) / numIntervals;
            double lvlOfSignificance = (double)numericUpDown4.Value;

            double[] filteredSample = sample.Where(x => x >= overallMin && x <= overallMax).ToArray();
            double N = filteredSample.Length;

            if (N == 0 || numIntervals < 2)
            {
                label8.Text = "Недостаточно данных или слишком мало интервалов.";
                return;
            }

            int degreesOfFreedom = numIntervals - 1;
            double critXiSq = Math.Round(_ex.WorksheetFunction.ChiInv(lvlOfSignificance, degreesOfFreedom), 3);

            double[] counts = new double[numIntervals];

            foreach (double value in filteredSample)
            {
                int index = (int)((value - overallMin) / step);
                if (index >= numIntervals) index = numIntervals - 1;
                counts[index]++;
            }

            double XiSq = 0;
            for (int j = 0; j < numIntervals; j++)
            {
                double lowBound = overallMin + j * step;
                double highBound = overallMin + (j + 1) * step;
                double p_i = ((highBound * highBound) - (lowBound * lowBound)) / 4.0;
                double expectedFrequency = N * p_i;

                if (expectedFrequency < 5)
                {
                    highBound += step;
                    p_i = ((highBound * highBound) - (lowBound * lowBound)) / 4.0;
                    expectedFrequency = N * p_i;
                }

                double diff = counts[j] - expectedFrequency;
                XiSq += (diff * diff) / expectedFrequency;
            }

            if (XiSq <= critXiSq)
            {
                label8.Text = $"Полученное значение Хи-Квадрат: {XiSq:F3}\n" +
                              $"Критическое значение: {critXiSq}\n" +
                              $"Гипотеза H0 подтверждается: выборочное распределение соответствует f(x)=0.5x.";
            }
            else
            {
                label8.Text = $"Полученное значение Хи-Квадрат: {XiSq:F3}\n" +
                              $"Критическое значение: {critXiSq}\n" +
                              $"Гипотеза H0 отвергается: выборочное распределение не соответствует f(x)=0.5x.";
            }
        }
        private double[] CreateArrayForRegularDistib(int numberOfvalues)
        {
            double[] massive = new double[numberOfvalues];
            Random rand = new Random();

            for (int i = 0; i < numberOfvalues; i++)
                massive[i] = Math.Round(rand.NextDouble(), 3);     // Получение случайного числа в радиусе от 0 до 1

            Array.Sort(massive);
            return massive;
        }
        private double[] CreateArrayForNormalDistrib(int numberOfValues, double targetMean, double targetVariance)
        {
            double[] massive = new double[numberOfValues];
            Random rand = new Random();

            // Параметры базовой выборки: сумма 20 uniform(0,1)
            double baseMean = 20 * 0.5;         // 20 * 0.5 = 10
            double baseVariance = 20.0 / 12.0;    // дисперсия uniform(0,1) равна 1/12, суммирование 20 чисел
            double baseStd = Math.Sqrt(baseVariance);

            for (int i = 0; i < numberOfValues; i++)
            {
                double sum = 0;
                for (int j = 0; j < 20; j++)
                    sum += rand.NextDouble();

                // Стандартизация: Z = (X - baseMean)/baseStd, где Z ~ N(0,1)
                double z = (sum - baseMean) / baseStd;
                // Масштабирование: Xfinal = targetMean + sqrt(targetVariance)*Z
                double finalValue = targetMean + Math.Sqrt(targetVariance) * z;
                massive[i] = Math.Round(finalValue, 3);
            }

            Array.Sort(massive);
            return massive;
        }
        private string EstimateParametersAndCIs(double[] sample, double targetMean, double targetVariance)
        {
            int n = sample.Length;
            double alpha = (double)numericUpDown4.Value;

            // Вычисляем точечную оценку математического ожидания – выборочное среднее
            double sampleMean = sample.Average();

            // Несмещённая оценка дисперсии при неизвестном μ
            double sumSq = sample.Sum(x => Math.Pow(x - sampleMean, 2));
            double sampleVarianceUnknownMean = sumSq / (n - 1);
            double sampleStdUnknownMean = Math.Sqrt(sampleVarianceUnknownMean);

            // Оценка дисперсии при известном μ (используя targetMean)
            double sumSqKnown = sample.Sum(x => Math.Pow(x - targetMean, 2));
            double varianceKnownMean = sumSqKnown / n;

            // 1. Доверительный интервал для МО при известной дисперсии:
            // Формула: X̄ ± z_(α/2)*(σ/sqrt(n)), где σ из targetVariance
            double zQuantile = QuantileCalculator.GetQuantile(QuantileCalculator.DistributionType.Normal, 1 - alpha / 2.0);
            double seKnown = Math.Sqrt(targetVariance) / Math.Sqrt(n);
            double ciMeanKnownLower = sampleMean - zQuantile * seKnown;
            double ciMeanKnownUpper = sampleMean + zQuantile * seKnown;

            // 2. Доверительный интервал для МО при неизвестной дисперсии:
            // Формула: X̄ ± t_(α/2, n-1)*(S/sqrt(n))
            double tQuantile = QuantileCalculator.GetQuantile(QuantileCalculator.DistributionType.T, 1 - alpha / 2.0, n - 1);
            double seUnknown = sampleStdUnknownMean / Math.Sqrt(n);
            double ciMeanUnknownLower = sampleMean - tQuantile * seUnknown;
            double ciMeanUnknownUpper = sampleMean + tQuantile * seUnknown;

            // 3. Доверительный интервал для дисперсии при известном μ:
            // Формула: [n*σ̂²/χ²_(1-α/2, n), n*σ̂²/χ²_(α/2, n)]
            double chiSqLower_n = QuantileCalculator.GetQuantile(QuantileCalculator.DistributionType.ChiSquare, 1 - alpha / 2.0, n);
            double chiSqUpper_n = QuantileCalculator.GetQuantile(QuantileCalculator.DistributionType.ChiSquare, alpha / 2.0, n);
            double ciVarKnownLower = n * varianceKnownMean / chiSqLower_n;
            double ciVarKnownUpper = n * varianceKnownMean / chiSqUpper_n;

            // 4. Доверительный интервал для дисперсии при неизвестном μ:
            // Формула: [(n-1)*S²/χ²_(1-α/2, n-1), (n-1)*S²/χ²_(α/2, n-1)]
            double chiSqLower_n1 = QuantileCalculator.GetQuantile(QuantileCalculator.DistributionType.ChiSquare, 1 - alpha / 2.0, n - 1);
            double chiSqUpper_n1 = QuantileCalculator.GetQuantile(QuantileCalculator.DistributionType.ChiSquare, alpha / 2.0, n - 1);
            double ciVarUnknownLower = (n - 1) * sampleVarianceUnknownMean / chiSqLower_n1;
            double ciVarUnknownUpper = (n - 1) * sampleVarianceUnknownMean / chiSqUpper_n1;

            StringBuilder result = new StringBuilder();
            result.AppendLine("Оценка математического ожидания при известной дисперсии:");
            result.AppendLine($"  Точечная оценка: {sampleMean:F3}");
            result.AppendLine($"  95% ДИ: [{ciMeanKnownLower:F3}, {ciMeanKnownUpper:F3}]");

            result.AppendLine("Оценка математического ожидания при неизвестной дисперсии:");
            result.AppendLine($"  Точечная оценка: {sampleMean:F3}");
            result.AppendLine($"  95% ДИ: [{ciMeanUnknownLower:F3}, {ciMeanUnknownUpper:F3}]");

            result.AppendLine("Оценка дисперсии при известном математическом ожидании:");
            result.AppendLine($"  Точечная оценка: {varianceKnownMean:F3}");
            result.AppendLine($"  95% ДИ: [{ciVarKnownLower:F3}, {ciVarKnownUpper:F3}]");

            result.AppendLine("Оценка дисперсии при неизвестном математическом ожидании:");
            result.AppendLine($"  Точечная оценка: {sampleVarianceUnknownMean:F3}");
            result.AppendLine($"  95% ДИ: [{ciVarUnknownLower:F3}, {ciVarUnknownUpper:F3}]");

            return result.ToString();
        }
        private void CreateSterdjisChart(double[] sample)
        {
            int n = sample.Length;
            // Определяем число интервалов по правилу Стёрджеса
            int k = (int)Math.Ceiling(1 + Math.Log(n, 2));
            double min = sample.Min();
            double max = sample.Max();
            double h = (max - min) / k;

            // Очищаем предыдущие серии и области, если есть
            chartForHitogramms.Series.Clear();
            chartForHitogramms.ChartAreas.Clear();

            // Создаём область графика
            ChartArea ca = new ChartArea("HistogramArea");
            ca.AxisX.Title = "Интервалы (бины)";
            ca.AxisY.Title = "Относительная частота";
            chartForHitogramms.ChartAreas.Add(ca);

            // Создаём серию столбчатой диаграммы
            Series series = new Series("Гистограмма");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            chartForHitogramms.Series.Add(series);

            // Расчёт частот для каждого интервала
            for (int i = 0; i < k; i++)
            {
                double binLow = min + i * h;
                double binHigh = binLow + h;
                // Для последнего бина включаем правую границу
                int count = sample.Count(x => x >= binLow && (i < k - 1 ? x < binHigh : x <= binHigh));
                double relFreq = (double)count / n;

                // Метка для X-оси: интервал вида "[a, b)"
                string label = $"[{binLow:F2}, {binHigh:F2})";
                // Если это последний бин – добавить закрывающую границу
                if (i == k - 1)
                    label = $"[{binLow:F2}, {binHigh:F2}]";

                // Добавляем точку (столбец) с заданной относительной частотой
                int pointIndex = series.Points.AddY(relFreq);
                series.Points[pointIndex].AxisLabel = label;
            }
            // Опционально можно задать формат осей и другие свойства графика
            ca.RecalculateAxesScale();
        }

        private bool CheckForNewParametersLin(int numOfvalues)
        {
            if (initNumOfValuesLinVal != numOfvalues)
            {
                initNumOfValuesLinVal = numOfvalues;
                return true;
            }
            return false;
        }
        private bool CheckForNewParametersReg(int numOfvalues)
        {
            if (initNumOfValuesReg != numOfvalues)
            {
                initNumOfValuesReg = numOfvalues;
                return true;
            }
            return false;
        }
        private bool CheckForNewParametersNorm(int numOfvalues)
        {
            if (initNumOfValuesNorm != numOfvalues)
            {
                initNumOfValuesNorm = numOfvalues;
                return true;
            }
            return false;
        }
        private void ShowChart(double[] massive, int numOfvalues, int numOfBars)
        {
            chartForHitogramms.Series[0].Points.Clear();

            Min = massive[0];
            Max = massive[numOfvalues - 1];

            double step = (Max - Min) / numOfBars;
            double cur_Step = 0;

            Yvalue = new double[numOfBars];
            Xvalue = new double[numOfBars];

            int idx = 0;

            for (int i = 0; i < numOfBars; i++)
            {
                double lowborder = Min + i * step;
                double highborder = Min + (i + 1) * step;

                while (idx < numOfvalues)
                {
                    if (massive[idx] >= lowborder && massive[idx] < highborder)
                    {
                        Yvalue[i]++;
                        idx++;
                    }
                    else break;
                }
                Yvalue[i] = Math.Round(Yvalue[i] / (numOfvalues * step), 2);
            }

            for (int i = 0; i < numOfBars; i++)
            {
                cur_Step += i == 0 ? 0 : step;
                Xvalue[i] = cur_Step;
                double lowVal = Math.Round(cur_Step, 2);
                double highVal = Math.Round(cur_Step + step, 2);
                chartForHitogramms.Series[0].Points.AddXY($"{lowVal} - {highVal}", Yvalue[i]);
            }
        }
    }
}
