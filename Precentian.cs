using ReadWriteCsv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Precentian
{
    class Precentian
    {
        double m_Precent;
        Dictionary<string, int> m_Attributes = new Dictionary<string, int>();
        CsvFileReader m_DataCsv;
        string m_InputPath;
        string m_OutputPath;
        int m_AttIndex;
        List<double> m_Data = new List<double>();
        List<List<string>> m_AllData = new List<List<string>>();
        double m_Precentian;
        double[] m_SortedArray;
        string m_InputFileName;

        public Precentian()
        {

        }
        /// <summary>
        /// please send the precent 95.5% as 95.5 (double)
        /// </summary>
        /// <param name="i_Data"></param>
        /// <param name="i_Precent"></param>
        public double CalculatePrecentian(List <double> i_Data, double i_Precent)

        {
            m_Precent= i_Precent;
            m_Data = i_Data;
            readDataAndCalculatePrecentian();
            return m_Precentian;
        }

        private void readDataAndCalculatePrecentian()
        {
            sortData();
            calcAndPrintPrecentian();
        }

        private void sortData()
        {
            if (m_Data.Count() == 0)
            {
                throw new IndexOutOfRangeException();
            }
            m_SortedArray = m_Data.ToArray();
            Quicksort(m_SortedArray, 0, m_Data.Count() - 1);
        }

        public static void Quicksort(double[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    double tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }

        private void calcAndPrintPrecentian()
        {
            double precent = m_Precent / 100;
            int index = (int)(precent * m_SortedArray.Count());
            m_Precentian = (m_SortedArray[index] + m_SortedArray[index + 1]) / 2;
        }
    }
}
