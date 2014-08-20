using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using makrovchain1;
using Precentian;
using System.Xml;

namespace dataanalessisGUI
{
    public partial class simultionDays : Form
    {
        DateValueList m_DVList;
        private const string c_comboBoxSrouceFIle = @"comboBoxfilds.txt";
        private const string c_dirctorysSrouceFIle = @"dirFilds.txt";
        List<double> m_posArray = new List<double>();
        List<double> m_zeroBuyList = new List<double>();
        private string m_BidFile;
        private string m_askFile;
        private string m_outFile;
        private string m_sammeryFile;
        private string m_baseOutFile;
        private string m_baseSammeryFile;
        private double  m_Num_Of_Std;
        private double m_MaxProfitPerMillon;
        private double m_MinProfitPerMillon;
        private double m_Max_Lost;
        private int m_Order_Value;
        private int    m_daysBackward;
        private string m_startDate;
        private string m_endDate;
        private bool   m_isNotRunEnd;
        private double m_avreagShift;
        private double m_divideTime;
        private int m_counter;
        private int m_daysToReggstion;
        private string m_typeOfPaper;
        private string[] m_dirctoryAsk; 
        private string[] m_dirctoryBid;
        private List<dealsTransLog> m_listOfTransAction = new List<dealsTransLog>();
        private Thread m_Thread;
        private bool m_isReggression;//m_isLearnFromFile
        private string m_inputStudiFile;
        private double m_MaxStd;
        private double m_MinStd;
        private double m_LenthFromStd;
        private List<string> m_listOfTradeDays = new List<string>();
        private csvReader m_PairCrosstable;
        private int m_id;
        private List<string> m_TypeSelectedcomboBox = new List<string>(new string[] {"MinStd", "MaxStd", "Days Back Word" ,"Max lost", "Number of std" ,"Len From zero Buy " ,"Days To Predict"});
        private string m_TypeOfArray;
        private string m_MarkovChain = "Markov Chain";
        private string m_Reggression = "Regression";
        private string m_ZeroBuyType;
        private int m_NumOfDaysForStdForMarkov;
        private int m_NumOfDaysForMarkovLearning;
        private bool m_MarkovLearnerWasInitialized = false;
        private MarkovChain m_Chain;
        private bool m_CalcDisByPrecentian;
        private bool m_LearnFromRegression;

        private double m_Precent;
        private csvReader m_DailyBid;
        private List<double> m_Differnces = new List<double>();
        private double m_Precentian;
        public simultionDays()
        {
            
            InitializeComponent();
            TB_daysForStdForTheMarkov.Text = "100";
            TB_DaysForMarkov.Text = "100";
            TB_PrecenteForPrecentian.Text = "90";
            zeroBuyTypeComboBox.Items.Add("Markov Chain");
            zeroBuyTypeComboBox.Items.Add("Regression");
            zeroBuyTypeComboBox.Text = (string)zeroBuyTypeComboBox.Items[0];
            m_ZeroBuyType = m_MarkovChain;
            this.Text = "simuletion Days";
            foreach(string x in m_TypeSelectedcomboBox)
            {
                cmb_TypeOfArray.Items.Add(x);
            }
   //t_textNumberOfSTD.Text = "4";
   //t_MaxProfitPerMill.Text = "5000";
   //t_MinProfitPerMill.Text = "500";
   //t_zerobuyForReg.Text = "1";
   // t_textMaxLos.Text = "50000";
   //t_textOrderValue.Text = "1000";
   //t_textAverageShift.Text = "0.0";
   //t_textDaysBack.Text= "120";  
   // t_textStartDate.Text = "01052010";
   //t_textEndDate.Text = "31122012";
   //t_textdivideTime.Text = "31";
   b_Stop.Enabled = false;
   //t_arrayOfAverageShift.Text = "0.04;0.025;0.05;";
   //t_arrayOFDayBackword.Text = "30;60;90;";
   //t_arrayOfSTD.Text = "4;6;8;";
   //t_textFIleAsk.Text = @"C:\dataAskAndBid\Ask";
   //t_textFileBid.Text = @"C:\dataAskAndBid\Bid";
   reloadDirFildsFromFile();
   //t_minStd.Text = "0.005";
   //t_maxStd.Text = "0.015";
            t_textOutFile.Text = @"C:\test\outfile.csv";
            p_progressBar.Minimum = 1;
            p_progressBar.Value = 1;
            p_progressBar.Step = 1;
            m_isNotRunEnd = true;
            labelstatus.Text = @"data isnt set";
          //  t_lenthZerobuyStd.Text = "2";
            
            bool isDataSet = false;
            m_BidFile = t_textFileBid.Text;
            m_askFile = t_textFIleAsk.Text;
            m_outFile = t_textOutFile.Text;
            m_baseOutFile = new string(m_outFile.ToArray());
            isDataSet = setInput();
            if (isDataSet == true)
            {
                labelstatus.Text = @"data is set up";
            }
            else
            {
                labelstatus.Text = @"data isnt set";
            }
           // reloadCombboxFromFile(c_comboBoxSrouceFIle);

            m_PairCrosstable = new csvReader("fullTableDays.csv");

            List<string> peparTypes = m_PairCrosstable.GetAttribuits();

            for (int i = 1; i < peparTypes.Count; i++)
                comboBoxPaperType.Items.Add(peparTypes[i]);

            Random random = new Random();
            m_id = random.Next(10000);
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            readDeffolteDir();
        }
        public string TypeOfPaper
        {
            get { return m_typeOfPaper; }
            set { m_typeOfPaper = value; }
        }
       private void readDeffolteDir()
       {
           saveInPutInFile();
           reloadDirFildsFromFile();

       }

        private void setUpB_Click(object sender, EventArgs e)
        {
            bool isDataSet = false;
            m_BidFile =  t_textFileBid.Text;
            m_askFile = t_textFIleAsk.Text;
            m_outFile = t_textOutFile.Text;


            isDataSet =  setInput();
            if (isDataSet == true)
            {
                labelstatus.Text = @"data is set up";
            }
            else 
            {
                labelstatus.Text = @"data isnt set";
            }
            if (m_typeOfPaper != null && m_typeOfPaper != string.Empty)
            {
                try
                {
                    int numberOfBDays = calcnumberOfBDays();
                    int numberOfDays = clacDays();
                    int numberOfMonth = numberOfDays / 30;
                    int numberOfWeeks = numberOfMonth * 4;
                    t_dateViwe.Text = @"number of days -> " + numberOfDays
+ @"
number of weeks ->" + numberOfWeeks +
@"
number of months ->" + numberOfMonth +
@"number of Business days ->" + numberOfBDays;
                }
                catch(Exception)
                { 
                }
            }


        }



       

        private bool setInput()
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(msg));
            //StringReader rdr = new StringReader(inputString);
            //msg resultingMessage = (msg)serializer.Deserialize(rdr);

            try
            {
                m_LenthFromStd = double.Parse(t_lenthZerobuyStd.Text);
                m_Max_Lost = double.Parse(t_textMaxLos.Text);
                m_Num_Of_Std = double.Parse(t_textNumberOfSTD.Text);
                m_Order_Value = int.Parse(t_textOrderValue.Text);
                m_daysBackward = int.Parse(t_textDaysBack.Text);
                m_NumOfDaysForStdForMarkov = int.Parse(TB_daysForStdForTheMarkov.Text);
                m_NumOfDaysForMarkovLearning = int.Parse(TB_DaysForMarkov.Text);
                m_Precent = double.Parse(TB_PrecenteForPrecentian.Text);
                m_daysToReggstion = int.Parse(t_zerobuyForReg.Text);
                m_startDate = t_textStartDate.Text;//#TODO make it safe to user
                m_endDate = t_textEndDate.Text;
                m_MaxProfitPerMillon = int.Parse(t_MaxProfitPerMill.Text);
                m_MinProfitPerMillon = int.Parse(t_MinProfitPerMill.Text); 
                m_avreagShift = double.Parse(t_textAverageShift.Text); 
                m_divideTime = double.Parse(t_textdivideTime.Text);
                m_MaxStd = double.Parse( t_maxStd.Text);
                m_MinStd = double.Parse(t_minStd.Text);
            }catch(FormatException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }

        private void runOnAllTime()
        {
            try
            {
                try
                {
                    this.Invoke(new MethodInvoker(start));
                    m_baseOutFile = t_textOutFile.Text;
                    bool isDataChecked = setAndCheckDataFIles();

                    if (isDataChecked == true)
                    {
                        MakeSuperDirctory();
                        runAlgo(m_startDate, m_endDate);
                    }

                    this.Invoke(new MethodInvoker(HardWorkComplete));
                }
                catch (ThreadAbortException)
                {
                    this.Invoke(new MethodInvoker(HardWorkCanceled));

                }
            }
            catch (ThreadInterruptedException)
            {
                //ignore interrupt

            }
 
        }


        private void runByDiveTime()
        {

            try
            {
                try
                {
                    this.Invoke(new MethodInvoker(start));
                    bool isDataChecked = setAndCheckDataFIles();
                    m_baseOutFile = t_textOutFile.Text;
                    if (isDataChecked == true)
                    {
                        MakeSuperDirctory();
                        int numberOfDays = clacDays();// clacknumberOfDays();
                       // m_divideTime = numberOfDays;
                        if (numberOfDays < m_divideTime)
                        {
                            MessageBox.Show("divide time must be less then month  number of month is--" + numberOfDays);
                            this.Invoke(new MethodInvoker(HardWorkComplete));
                        }
                        runOnDivTIme(numberOfDays);


                    }

                    this.Invoke(new MethodInvoker(HardWorkComplete));
                }
                catch (ThreadAbortException)
                {
                    this.Invoke(new MethodInvoker(HardWorkCanceled));
                }
            }
            catch (ThreadInterruptedException)
            {
                //ignore innterrupte
                //this.Invoke(new MethodInvoker(HardWorkCanceled));
            }
            //List<string> list = new List<string>();

            //for (int i = 0; i < m_posArray.Count; i++)
            //{

            //    list.Add(string.Format("{0},{1}", m_posArray[i], m_zeroBuyList[i]));

            //}
            //System.IO.File.WriteAllLines(@"C:\algoOutPut\outZero_And_pos.csv",list);
        }

        private void MakeInPut(string i_value,string i_type,ref InputClass io_input)
        {
            

            switch (i_type)
            {
                case "MinStd":
                    io_input.MinStd = double.Parse(i_value);
                    m_MinStd = io_input.MinStd;
                    break;
                case "MaxStd":
                    io_input.MaxStd = double.Parse(i_value);
                    m_MaxStd = io_input.MaxStd;
                    break;
                case "Days Back Word":
                    io_input.DaysBackward = int.Parse(i_value);
                    m_daysBackward = io_input.DaysBackward;
                    break;
                case "Max lost":
                    io_input.Max_Pos = int.Parse(i_value);
                    m_Max_Lost = io_input.Max_Pos;
                    break;
                case "Number of std":
                    io_input.Num_Of_Std = double.Parse(i_value);
                    m_Num_Of_Std = io_input.Num_Of_Std;
                    break;
                case "Len From zero Buy ":
                    io_input.LenFormZeroBuy = double.Parse(i_value);
                    m_LenthFromStd = io_input.LenFormZeroBuy;
                    break;
                case "Days To Predict":
                    io_input.DaysToPre = int.Parse(i_value);
                    m_daysToReggstion = io_input.DaysToPre;
                    break;
                case "":
                    break;
                default:
                    break;
            }


         
        }

      

        private void runOnVectors()
        {
            try
            {
            try
            {
                this.Invoke(new MethodInvoker(start));
                      List<string> peparTypes = m_PairCrosstable.GetAttribuits();
                   

                bool isDataChecked = setAndCheckDataFIles();
                string Dirname = Path.GetDirectoryName(t_textOutFile.Text);
                string FileName = Path.GetFileName(t_textOutFile.Text); 
               
                if (isDataChecked == true)
                {

                    // string TypeOfArray =(string)cmb_TypeOfArray.SelectedItem;
                    List<string> vector = new List<string>();
                    string ArrayAsString = t_inPutArray.Text;
                    string[] arrayOfSelectedType = ArrayAsString.Split(',');
                    InputClass toRun = new InputClass();
                    toRun.StartDate = m_startDate;
                    toRun.EndDate = m_endDate;
                    toRun.DaysBackward = m_daysBackward;
                    toRun.DaysToPre = m_daysToReggstion;
                    toRun.Max_Pos = m_Max_Lost;
                    toRun.MinPPm = (int)m_MinProfitPerMillon;
                    toRun.MaxPPm = (int)m_MaxProfitPerMillon;
                    toRun.MinStd = m_MinStd;
                    toRun.MaxStd = m_MaxStd;
                    toRun.LenFormZeroBuy = m_LenthFromStd;
                    toRun.Order_Value = m_Order_Value;
                    foreach (string x in arrayOfSelectedType)
                    {
                        vector.Add(x);
                    }
                    MakeInPut(arrayOfSelectedType[0], m_TypeOfArray, ref toRun);

                    for (int i = 0; i < arrayOfSelectedType.Length; i++)
                    {
                        if (i != 0)
                        {
                            MakeInPut(arrayOfSelectedType[i], m_TypeOfArray, ref toRun);
                        }
                        foreach (string y in peparTypes)
                        {
                            m_baseOutFile = Dirname + "_" + m_TypeOfArray + "_" + arrayOfSelectedType[i] + @"\" + FileName;

                            if (y == "Date" || y == " ")
                            {
                                continue;
                            }
                            this.Invoke(new MethodInvoker(prograss));

                            m_typeOfPaper = y;
                            MakeSuperDirctory();
                            int numberOfDays = clacDays();// clacknumberOfDays();
                            if (numberOfDays < m_divideTime)
                            {
                                MessageBox.Show("dive time must be less then month  number of month is--" + numberOfDays);
                                this.Invoke(new MethodInvoker(HardWorkComplete));
                            }
                            runOnDivTIme(numberOfDays, toRun);
                            m_baseOutFile = Dirname + "_" + m_TypeOfArray + "_" + arrayOfSelectedType[i] + @"\" + FileName;
                        }

                    }
                
              
                   
                }
                this.Invoke(new MethodInvoker(HardWorkComplete));
            }
            catch (ThreadAbortException)
            {
                this.Invoke(new MethodInvoker(HardWorkCanceled));
            }
             }
            catch (ThreadInterruptedException)
            {
                //ignore interrupt

            }
 
        }

        private bool setAndCheckDataFIles()
        {
            m_dirctoryAsk = Directory.GetFiles(m_askFile, "*ASK*.csv");
            m_dirctoryBid = Directory.GetFiles(m_BidFile, "*BID*.csv");

            if (m_typeOfPaper == "" || m_typeOfPaper == null)
            {
                MessageBox.Show("plase select paper type");
                return false;
            }

            SelectFilesBySubString(ref m_dirctoryAsk, ref m_dirctoryBid, m_typeOfPaper);
            if (checkIfAllDatesInData(m_dirctoryAsk, m_dirctoryBid) == false)
            {
                MessageBox.Show("data not set- missing dates");
                return false;
            }
            if (m_dirctoryAsk.Length == 0 || m_dirctoryBid.Length == 0)
            {
                MessageBox.Show("no files suitable to the - " + m_typeOfPaper + "- pair");
                return false;
            }
            return true;
        }

        private void b_runOnVectors_Click(object sender, EventArgs e)
        {
            doBeforRunning();
            m_TypeOfArray = (string)cmb_TypeOfArray.SelectedItem;
            m_Thread = new Thread(new ThreadStart(runOnVectors));
            m_Thread.Priority = ThreadPriority.AboveNormal;
            m_Thread.Start();
            buttonRunBydive.Enabled = false;
            
            b_runOnVectors.Enabled = false;
            b_Stop.Enabled = true;

        }




        private void buttonRunBydive_Click(object sender, EventArgs e)
        {
            doBeforRunning();
            m_Thread = new Thread(new ThreadStart(runByDiveTime));
            m_Thread.Priority = ThreadPriority.AboveNormal;
            m_Thread.Start();
            buttonRunBydive.Enabled = false;
         
            b_runOnVectors.Enabled = false;
            b_Stop.Enabled = true;
        }
        
        private void b_runOnAllTypes_Click(object sender, EventArgs e)
        {
            doBeforRunning();
            m_Thread = new Thread(new ThreadStart(runOnAllPeparType));
            m_Thread.Priority = ThreadPriority.AboveNormal;
            m_Thread.Start();
            buttonRunBydive.Enabled = false;
            b_runOnAllTypes.Enabled = false;
            b_runOnVectors.Enabled = false;
            b_Stop.Enabled = true;
           
        }

        private void runOnAllPeparType()
        {
            try
            {
                try
                {
                    this.Invoke(new MethodInvoker(start));
                    m_baseOutFile = t_textOutFile.Text;
                    
                        List<string> peparTypes = m_PairCrosstable.GetAttribuits();
                        foreach (string x in peparTypes)
                        {
                            m_MarkovLearnerWasInitialized = false;
                            if (m_Chain != null)
                            {
                                m_Chain.Clear();
                            }
                            if (x == "Date")
                            {
                                continue;
                            }
                            this.Invoke(new MethodInvoker(prograss));
                            
                            m_typeOfPaper = x;
                            bool isDataChecked = setAndCheckDataFIles();
                            if (isDataChecked == true)
                            {
                                MakeSuperDirctory();
                                int numberOfDays = clacDays();
                                if (numberOfDays < m_divideTime)
                                {
                                    MessageBox.Show("dive time must be less then month  number of month is--" + numberOfDays);
                                    this.Invoke(new MethodInvoker(HardWorkComplete));
                                }
                                runOnDivTIme(numberOfDays);
                                m_baseOutFile = t_textOutFile.Text;

                            }
                            else
                            {
                                continue;
                            }
                        }
                    

                    this.Invoke(new MethodInvoker(HardWorkComplete));
                }
                catch (ThreadAbortException)
                {
                    this.Invoke(new MethodInvoker(HardWorkCanceled));

                }
            }
            catch (ThreadInterruptedException)
            {
                //ignore interrupt

            }
        }

        private void doBeforRunning()
        {
           m_isReggression = ch_isLearnFromFile.Checked;
           Random random = new Random();
           m_id = random.Next(10000);
           m_ZeroBuyType = zeroBuyTypeComboBox.Text;
           m_CalcDisByPrecentian = CB_CalcByPrecentian.Checked;
        }

        private void b_Stop_Click(object sender, EventArgs e)
        {
            m_Thread.Abort();
        }

        private void start()
        {
            p_progressBar.Maximum = 15;
            p_progressBar.Minimum = 1;
            p_progressBar.Value = 2;
        }

        private void EndOfRun()
        {
            p_progressBar.Value = 1;
            m_Thread.Interrupt();

            // MessageBox.Show("algo was done");

        }

        private void prograss()
        {

            if (p_progressBar.Value > 14)
            {
                p_progressBar.Value = 1;
            }
            else 
            {
                p_progressBar.Value++;
            }

        }


        private void HardWorkComplete()
        {
            buttonRunBydive.Enabled = true;
            b_runOnAllTypes.Enabled = true;
            b_runOnVectors.Enabled = true;
            b_Stop.Enabled = false;
            EndOfRun();
        }

        private void HardWorkCanceled()
        {
            buttonRunBydive.Enabled = true;
            b_runOnAllTypes.Enabled = true;
            b_runOnVectors.Enabled = true;
            b_Stop.Enabled = false;
            MessageBox.Show("runing was canceled");
            EndOfRun();
          
        }

        private void MakeSuperDirctory()
        {
            string outFileName = Path.GetFileName(m_baseOutFile);
            string outFileDirctory = Path.GetDirectoryName(m_baseOutFile);
            string time = DateTime.Now.ToString("HH_mm_ss");
            outFileDirctory = outFileDirctory + @"\Run" + "_" + (float)m_avreagShift + "_"+ m_Num_Of_Std + "_" + m_daysBackward +"_" +m_startDate + "_" + m_endDate +"_" + m_typeOfPaper + "_" + time + "_" + m_id;
            m_baseOutFile = outFileDirctory + @"\" + outFileName;
          
            if ((Directory.Exists(outFileDirctory) == true))
            {
                MessageBox.Show("Data will be replaced, please select new summary or out file Path");
            }
            else
            {
                Directory.CreateDirectory(outFileDirctory);
            }
        }

        private void changeDirctory()
        { 
            string outFileName = Path.GetFileNameWithoutExtension(m_baseOutFile);
            string outFileDirctory = Path.GetDirectoryName(m_baseOutFile);

            string summryFileName = Path.GetFileNameWithoutExtension(m_baseOutFile);
            string summaryFileDirctory = Path.GetDirectoryName(m_baseOutFile);

            string time = DateTime.Now.ToString("HH_mm_ss");
          //  summaryFileDirctory = summaryFileDirctory + "_" + m_typeOfPaper + "_" + time;
            outFileDirctory = outFileDirctory + @"\Period"+ m_startDate + "_" + m_typeOfPaper + "_" + time;
           // m_sammeryFile = summaryFileDirctory + @"\" + summryFileName;
            m_outFile = outFileDirctory + @"\" + outFileName;

            if ((Directory.Exists(outFileDirctory) == true))
            {

                //MessageBox.Show(" data will be replace please select new summary or out file Path");
                System.Threading.Thread.Sleep(1050);//if the dirctory exists wait and try again
                changeDirctory();
            }
            else
            {
                //Directory.CreateDirectory(summaryFileDirctory);
                Directory.CreateDirectory(outFileDirctory);
            }

 
        }


        private void runAlgo(InputClass i_in)
        {
            this.Invoke(new MethodInvoker(prograss));         


            changeDirctory();
            makeInPutFile();

            AskBidByDate run;
            run = new AskBidByDate(i_in,  m_dirctoryBid,
  m_dirctoryAsk, m_outFile, m_sammeryFile, m_isReggression);

            run.ListOfTradeDays = reMoveDotsFromDates(m_PairCrosstable.GetColume("Date"));
            if (m_isReggression == true)
            {
                run.openingPrices = m_PairCrosstable.GetColume(m_typeOfPaper);
            }

            try
            {
                try
                {
                    run.RunByAskAndBid(i_in.Max_Pos, i_in.Num_Of_Std);
                }
                catch (OutOfMemoryException outM)
                {
                    MessageBox.Show(outM.Message);
                }

            }
            catch (masterExcption e)
            {
                if (e as dayNotExistException != null)
                {
                    return;
                }
                if (e as emptyQueueExcption != null)
                {
                    //MessageBox.Show(e.Message);
                }

                if (e as NoDealsExcption != null)
                {
                    //  MessageBox.Show("No deals in  - " + i_startDate + "- until ->" + i_endDate);
                }
            }

            try
            {
                run.printOutPutFiles();
            }
            catch (NoDealsExcption)
            {
               // MessageBox.Show("No deals in  - " + i_startDate + "- until ->" + i_endDate);
            }
        }

        DateTime stringToDate(string i_Str)
        {
            DateTime dt = new DateTime(int.Parse(i_Str.Substring(4,4)),int.Parse(i_Str.Substring(2,2)),int.Parse(i_Str.Substring(0,2)));
            return dt;
        }

        private void loadDataFromFiles()
        {
            if (m_DVList!= null && m_DVList.m_DateValueList != null)
            {
                m_DVList.m_DateValueList.Clear();
            }
            m_DVList = new DateValueList();
            System.Xml.Serialization.XmlSerializer reader = new
         System.Xml.Serialization.XmlSerializer(m_DVList.GetType());

            // Read the XML file.
            System.IO.StreamReader file =
               new System.IO.StreamReader(@"C:\openHighLow\" + m_typeOfPaper + ".XML");
            // Deserialize the content of the file into a Book object.
            m_DVList = (DateValueList)reader.Deserialize(file);
            file.Close();
        }

        private void runAlgo(string i_startDate, string i_endDate)
        {
            int j = 0;
            // List<string> ListOfTradeDays = reMoveDotsFromDates(m_PairCrosstable.GetColume("Date"));
            //List<string> openingPrices = m_PairCrosstable.GetColume(m_typeOfPaper);
            Dictionary<DateTime, double> dataInput = new Dictionary<DateTime, double>();
            dataInput.Clear();

            if (m_CalcDisByPrecentian == true)
            {
                InitDifferncedValues(i_startDate);
                InitPrecetian();
            }
            this.Invoke(new MethodInvoker(prograss));
            if (m_MarkovLearnerWasInitialized == false)
            {
                if (m_Chain != null)
                {
                    m_Chain.Clear();
                }
                for(int i=0; i<m_DVList.m_DateValueList.Count();i++)
                {
                    if (m_DVList.m_DateValueList[i].m_date >= stringToDate(i_startDate))
                    {
                        break;
                    }
                    dataInput.Add(m_DVList.m_DateValueList[i].m_date, m_DVList.m_DateValueList[i].m_Open);
                }
                m_Chain = new MarkovChain();
                m_Chain.SetParametersAndRun(m_typeOfPaper, dataInput, m_NumOfDaysForMarkovLearning, m_NumOfDaysForStdForMarkov);
                m_MarkovLearnerWasInitialized = true;
            }
            else
            {
                foreach(DateValueList.DateValue dv in m_DVList.m_DateValueList)
                {
                    if (dv.m_date == (stringToDate(i_startDate).AddDays(-1)))
                    {
                        m_Chain.makeMarksOnDataForNextDay(dv.m_Open, dv.m_date);
                        break;
                    }          
                    j++;
                }                
            }
            changeDirctory();
            makeInPutFile();

            AskBidByDate run;
            if (m_ZeroBuyType == m_MarkovChain)
            {
                m_isReggression = false;
            }
            else if (zeroBuyTypeComboBox.Text == m_Reggression)
            {
                m_isReggression = true;
            }
                run = new AskBidByDate(i_bidFileSrouces: m_dirctoryBid,
    i_AskFileSrouces: m_dirctoryBid, i_outPutFile: m_outFile, i_summaryFile: m_sammeryFile,
    i_startDate: i_startDate, i_endDate: i_endDate, i_daysTolearn: m_daysBackward,
    i_averageShift: m_avreagShift, i_weight: 0, i_isReggression: m_isReggression, i_TypeOfPaper: m_typeOfPaper, 
    i_CalcDistanceByPrecentianOfDiff: m_CalcDisByPrecentian, i_precentian: m_Precentian);//weight is no in us any more we,we keep it for forther us
                run.SetLearner(new MarkovLearner(m_Chain, m_typeOfPaper));
                
            run.MaxProfitPerunit = m_MaxProfitPerMillon;
            run.MinProfitPerunit = m_MinProfitPerMillon;
            run.Order = m_Order_Value;
            //run.DaysToPredictZeroBuy = m_daysToReggstion;
            run.MaxStd = m_MaxStd;
            run.MinStd = m_MinStd;
            run.LenthZeroBuyStd = m_LenthFromStd;
                
            run.ListOfTradeDays = reMoveDotsFromDates(m_PairCrosstable.GetColume("Date"));
            run.openingPrices = m_PairCrosstable.GetColume(m_typeOfPaper);
                
            
            try
            {
                try
                {
                    run.RunByAskAndBid(m_Max_Lost, m_Num_Of_Std);
                }
                catch (OutOfMemoryException outM)
                {
                    MessageBox.Show(outM.Message);
                }
                    
            }
            catch(masterExcption e)
            {
                if(e as dayNotExistException != null)
                {
                    return;
                }
                if(e as emptyQueueExcption != null)
                {
                    //MessageBox.Show(e.Message);
                }

                if (e as NoDealsExcption != null)
                {
                    //  MessageBox.Show("No deals in  - " + i_startDate + "- until ->" + i_endDate);
                }
            }

            try
            {
                run.printOutPutFiles();
            }
            catch (NoDealsExcption)
            {
                MessageBox.Show("No deals in  - " + i_startDate + "- until ->" + i_endDate);
            }
        }

        private void InitPrecetian()
        {
            Precentian.Precentian prec = new Precentian.Precentian();
            m_Precentian = prec.CalculatePrecentian(m_Differnces, m_Precent);
        }

        private void InitDifferncedValues(string i_StartDate)
        {
            m_Differnces.Clear();
            //int i = 0;
            //m_DailyBid = new csvReader(getNameOfDailyBid(m_typeOfPaper));
            //List<string> dateValues = reMoveDotsFromDates(m_DailyBid.GetColume("Time"));
            //List<string> highValues = m_DailyBid.GetColume("High");
            //List<string> lowValues = m_DailyBid.GetColume("Low");
            //foreach (string time in dateValues)
            //{
            //    if (time == i_StartDate)
            //    {
            //        break;
            //    }
            //    if ((double.Parse(highValues[i]) - double.Parse(lowValues[i])) > 0)
            //    {
            //        m_Differnces.Add(double.Parse(highValues[i]) - double.Parse(lowValues[i]));
            //    }
            //    i++;
            //}
            foreach(DateValueList.DateValue dv in m_DVList.m_DateValueList)
            {
                if (dv.m_date == stringToDate(i_StartDate))
                {
                    break;
                }
                m_Differnces.Add(dv.m_Diff);
            }
        }

        private string getNameOfDailyBid(string m_typeOfPaper)
        {
            string res = m_typeOfPaper + "_EET_Daily_Bid_2007.12.31_2014.07.19.csv";
            return res;
        }

        private List<string> reMoveDotsFromDates(List<string> i_DatesWithDots)
        {
            List<string> DateOut = new List<string>();

            foreach (string x in i_DatesWithDots)
            {
                DateOut.Add(toDateWithOutDots(x));
            }

            return DateOut;
        }

        private string toDateWithOutDots(string i_dateWithDote)
        {
            string date = "";

            string[] splitComps = i_dateWithDote.Split('.');
            splitComps[splitComps.Count() - 1] = splitComps[splitComps.Count() - 1].Substring(0, 2);//this line was added by Lee so this method would fit to other formats of files
            for (int i = splitComps.Length - 1; i >= 0; i-- )
            {
                date += splitComps[i];
            }

            return date;
        }


        private void runOnDivTIme(int i_numberOfDays)
        {
            
            string startDate = m_startDate;
            string endDate = addNumberOfMonthToDate(startDate, (i_numberOfDays) / (int)m_divideTime);
            m_MarkovLearnerWasInitialized = false;
            loadDataFromFiles();
            for(int i = 0 ; i < m_divideTime ; i++)
            {
                runAlgo(startDate, endDate);
                startDate = new string(endDate.ToCharArray());//string is a class and we dont want a flat copy
                endDate = addNumberOfMonthToDate(endDate, i_numberOfDays / (int)m_divideTime);
               
            //    changeOutPutAndSummaryFile(i_index:i);
            }
        }

        

        private void runOnDivTIme(int i_numberOfDays,InputClass i_in)
        {

            string startDate = m_startDate;
            string endDate = addNumberOfMonthToDate(startDate, (i_numberOfDays) / (int)m_divideTime);
            for (int i = 0; i < m_divideTime; i++)
            {
                i_in.StartDate = startDate;
                i_in.EndDate = endDate;
                runAlgo(i_in);
                startDate = new string(endDate.ToCharArray());//string is a class and we dont want a flat copy
                endDate = addNumberOfMonthToDate(endDate, i_numberOfDays / (int)m_divideTime);

                //    changeOutPutAndSummaryFile(i_index:i);
            }
        }



        private void changeOutPutAndSummaryFile(int i_index)//not in use 
        {
            m_sammeryFile = new string(m_baseSammeryFile.ToArray());
            m_outFile = new string(m_baseOutFile.ToArray());
            string outFileName = Path.GetFileNameWithoutExtension(m_outFile);
            string outFiledirctory = Path.GetDirectoryName(m_outFile);
            
            string summryFileName = Path.GetFileNameWithoutExtension(m_sammeryFile);
            string summaryFiledirctory = Path.GetDirectoryName(m_sammeryFile);
            string root = Path.GetPathRoot(m_sammeryFile);

            m_outFile =  outFiledirctory + @"\"+ outFileName + @"_" +i_index + @".csv";
            m_sammeryFile = summaryFiledirctory + @"\" + summryFileName + @"_" + i_index + @".csv";
        }

        private string addNumberOfMonthToDate(string i_date, int i_number)
        {
            string outcome = null;
            string days = i_date.Substring(0,2);
            string months = i_date.Substring(2, 2);
            string years = i_date.Substring(4, 4);


            //calc a priod forword
            int day, month, year;
            day = int.Parse(days);
            month = int.Parse(months);
            year = int.Parse(years);
            DateTime d1 = new DateTime(year, month, day);
            DateTime answer  =   d1.AddDays(i_number);
            //month = (month + i_number);
            
            //if (month > 12)
            //{
            //    month = 1;
            //    year = year + 1;
            //}

            //if (month < 10)
            //{
            //    months = "0" + month;
            //}
            //else
            //{
            //    months = month.ToString();
            //}
            
            //years = year.ToString();
            //outcome =days + months + years;
            string date = answer.ToShortDateString();
            outcome = date.Substring(0,2) + date.Substring(3,2) + date.Substring(6,4);
            return outcome;
        }


        private bool checkIfAllDatesInData(string[] i_askFiles, string[] i_BidFiles)
        {
            int minLen = i_askFiles.Length > i_BidFiles.Length ? i_BidFiles.Length : i_askFiles.Length;
            bool outcome = true;

            for(int i = 0; i < minLen ; i++ )
            {
                string askFileName = Path.GetFileNameWithoutExtension(i_askFiles[i]);
                string BidFileName = Path.GetFileNameWithoutExtension(i_BidFiles[i]);
                string askNum = askFileName.Substring(askFileName.Length - 6 , 6);
                string BidNum = BidFileName.Substring(askFileName.Length - 6 , 6);

                if (int.Parse(askNum) != int.Parse(BidNum))
                {
                    outcome = false;
                }
               
            }
            return outcome;
        }

        private void saveInPutInFile()
        {
            //--------------------------------------------------------
            //- Store the combobox values in a file. 1 value = 1 line
            //--------------------------------------------------------
            try
            {
                using (StreamWriter comboboxsw = new StreamWriter(c_dirctorysSrouceFIle))
                {
                         comboboxsw.WriteLine(t_textFIleAsk.Text);
                         comboboxsw.WriteLine(t_textFileBid.Text);
                         comboboxsw.WriteLine(t_textOutFile.Text);
                         comboboxsw.WriteLine(t_textAverageShift.Text);
                         comboboxsw.WriteLine(t_textDaysBack.Text);
                         comboboxsw.WriteLine(t_textdivideTime.Text);
                         comboboxsw.WriteLine(t_textMaxLos.Text);
                         comboboxsw.WriteLine(t_textNumberOfSTD.Text);
                         comboboxsw.WriteLine(t_textOrderValue.Text);
                         comboboxsw.WriteLine(t_textStartDate.Text);
                         comboboxsw.WriteLine(t_textEndDate.Text);
                         comboboxsw.WriteLine(t_minStd.Text);
                         comboboxsw.WriteLine(t_MinProfitPerMill.Text);
                         comboboxsw.WriteLine(t_maxStd.Text);
                         comboboxsw.WriteLine(t_MaxProfitPerMill.Text);
                         comboboxsw.WriteLine(t_zerobuyForReg.Text);
                         comboboxsw.WriteLine(t_lenthZerobuyStd.Text);
                      
                    
                } // End Using`
            }
            catch (Exception e)
            {
                //process exception
            }
        }
        
        private void reloadDirFildsFromFile()
        {
            //-------------------------------------------------
            //- Read the values back into the combobox
            //------------------------------------------------- 
            try
            {
                using (StreamReader comboboxsr = new StreamReader(c_dirctorysSrouceFIle))
                {
                    
                    while (!comboboxsr.EndOfStream)
                    {
                        t_textFIleAsk.Text = comboboxsr.ReadLine();
                        t_textFileBid.Text = comboboxsr.ReadLine();
                        t_textOutFile.Text  = comboboxsr.ReadLine();
                        t_textAverageShift.Text = comboboxsr.ReadLine();
                        t_textDaysBack.Text = comboboxsr.ReadLine();
                        t_textdivideTime.Text = comboboxsr.ReadLine();
                        t_textMaxLos.Text = comboboxsr.ReadLine();
                        t_textNumberOfSTD.Text = comboboxsr.ReadLine();
                        t_textOrderValue.Text = comboboxsr.ReadLine();
                        t_textStartDate.Text = comboboxsr.ReadLine();
                        t_textEndDate.Text = comboboxsr.ReadLine();
                        t_minStd.Text = comboboxsr.ReadLine();
                        t_MinProfitPerMill.Text = comboboxsr.ReadLine();
                        t_maxStd.Text = comboboxsr.ReadLine();
                        t_MaxProfitPerMill.Text = comboboxsr.ReadLine();
                        t_zerobuyForReg.Text = comboboxsr.ReadLine();
                        t_lenthZerobuyStd.Text = comboboxsr.ReadLine();
                      
                     
                    }
                } // End Using
            }
            catch (DirectoryNotFoundException dnf)
            {
                // Exception Processing
            }
            catch (FileNotFoundException fnf)
            {
                // Exception Processing
            }
            catch (Exception e)
            {
                // Exception Processing
            }
        }


        private void reloadCombboxFromFile(string comboboxFileName)
        {
            //-------------------------------------------------
            //- Read the values back into the combobox
            //------------------------------------------------- 
            try
            {
                using (StreamReader comboboxsr = new StreamReader(comboboxFileName))
                {
                    while (!comboboxsr.EndOfStream)
                    {
                        string itemread = comboboxsr.ReadLine();
                        comboBoxPaperType.Items.Add(itemread);
                    }
                } // End Using
            }
            catch (DirectoryNotFoundException dnf)
            {
                // Exception Processing
            }
            catch (FileNotFoundException fnf)
            {
                // Exception Processing
            }
            catch (Exception e)
            {
                // Exception Processing
            }
        }


private void saveComboboxInFile (String comboboxFileName )
{
   //--------------------------------------------------------
   //- Store the combobox values in a file. 1 value = 1 line
   //--------------------------------------------------------
   try
    {
        using (StreamWriter comboboxsw = new StreamWriter(comboboxFileName))
        {
            foreach (var cfgitem in comboBoxPaperType.Items)
            {
                comboboxsw.WriteLine(cfgitem);
            }
        } // End Using`
    }
    catch (Exception e)
    {
       //process exception
    }
}

        private void SelectFilesBySubString(ref string[]  i_askFIles,ref string[]  i_BidFIles, string i_subString)
        {

            List<string>  dirctoryAsk = new List<string>();
            List<string> dirctoryBid = new List<string>();

            int minLen = i_askFIles.Length > i_BidFIles.Length ? i_BidFIles.Length : i_askFIles.Length; 

            for (int i = 0; i < minLen; i++ )
            {
                if(i_askFIles[i].Contains(i_subString))
                {
                    dirctoryAsk.Add(i_askFIles[i]);
                }

                if (i_BidFIles[i].Contains(i_subString))
                {
                    dirctoryBid.Add(i_BidFIles[i]);
                }
            }


            i_askFIles = dirctoryAsk.ToArray();
            i_BidFIles = dirctoryBid.ToArray();


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

    

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddNewPaper_Click(object sender, EventArgs e)
        {

        }

        


        private bool isPaperTypeISInFormat(string i_paper)//#TODO check in Data base if the paper exist
        {
            bool outcome = true;
            if (i_paper.Length < 6 || i_paper.Length > 6)
            {
                outcome = false;
            }

            return outcome;
        }

        private void comboBoxPaperType_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxPaperType_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_typeOfPaper = comboBoxPaperType.SelectedItem as string; 
        }


        private string setDateFormat(string text)//change name and make i  for format 00/00/0000 eu 
        {
            if (text == null) return null;
            string outcome;
            //take days
            string days = text.Substring(0, 2);
            string months = text.Substring(2, 2);
            string years = text.Substring(4, 4);


            outcome = years + months + days;

            return outcome;
        }
     

        private int calcnumberOfBDays()
        {
            m_listOfTradeDays.Clear();
            int counter = 0;
            List<string> date = m_PairCrosstable.GetColume("Date");
            List<string> valueOfDate = m_PairCrosstable.GetColume(m_typeOfPaper);

            if(valueOfDate.Count > date.Count)
            {
                throw new Exception("data corapt");
            }

            for (int i = 0; i < date.Count ; i++ )
            {

                if(valueOfDate[i] != "NaN")
                {
                    m_listOfTradeDays.Add(ToCompFormat(date[i]));
                }
            }

            string start = setDateFormat(m_startDate);
            string end = setDateFormat(m_endDate);
            int targetDate = int.Parse(start);
            int nextDate =int.Parse(m_listOfTradeDays[0]);



            
            while(targetDate > nextDate)
            {
                counter++;
                nextDate = int.Parse(m_listOfTradeDays[counter]);
            }

            targetDate = int.Parse(end);//find the startDate index
            nextDate = int.Parse(m_listOfTradeDays[0]); 
            int counter2 = 0;
            while (targetDate > nextDate)
            {
                counter2++;
                nextDate = int.Parse(m_listOfTradeDays[counter2]);
            }

          

            return counter2 - counter;
        }

        private string ToCompFormat(string i_strDateWithDot)
        {
            string outCome = "";
            string[] toArray =  i_strDateWithDot.Split('.');
            for (int i = 0 ; i < toArray.Length ; i++ )
            {
                outCome += toArray[i];
            }

            return outCome;
        }

        private int clacDays()
        {
            string DayInStart = m_startDate.Substring(0, 2);
            string DayInEnd = m_endDate.Substring(0, 2);

            string MonthInStart = m_startDate.Substring(2, 2);
            string MonthInEnd = m_endDate.Substring(2, 2);

            string YearInStart = m_startDate.Substring(4, 4);
            string YearInEnd = m_endDate.Substring(4, 4);

            // int numberOfMonths = Math.Abs(int.Parse(numberOfMonthInStart) - int.Parse(numberOfMonthInEnd));
            // int numberOfYears = Math.Abs(int.Parse(numberOfYearInStart) - int.Parse(numberOfYearInEnd));
            int dayStart, monthStart, yearStart;
            int dayEnd, monthEnd, yearEnd;

            dayStart = int.Parse(DayInStart);
            monthStart = int.Parse(MonthInStart);
            yearStart = int.Parse(YearInStart);

            dayEnd = int.Parse(DayInEnd);
            monthEnd = int.Parse(MonthInEnd);
            yearEnd = int.Parse(YearInEnd);



            DateTime d1 = new DateTime(yearStart, monthStart, dayStart);
            DateTime d2 = new DateTime(yearEnd, monthEnd, dayEnd);



            //return  numberOfMonths = 12 * numberOfYears + numberOfMonths;
            return (int)(d2 - d1).TotalDays;
        }


        private void makeInPutFile()
        {
        //public string Papertype;
        //public int MaxLos;
        //public double AvregeShift;
        //public double NumberOfStd;
        //public double MaxPrecntStd;
        //public double MinPrecntStd;
        //public int DaysToLearn;
        //public int MinProfitPerMillion;
        //public int MaxProfitPerMillion;
        //public int OrderSize;
        //public int LenthFromStd;
        //public int DaysToPredict;
        //public int ProidLenth;



            List<string> list = new List<string>();
            list.Add(string.Format(
@"<ReadInput>
<Papertype>{0} </Papertype>
<MaxLos>{1}</MaxLos>
<AvregeShift>{2}</AvregeShift>
<NumberOfStd>{3}</NumberOfStd>
<MaxPrecntStd>{4}</MaxPrecntStd>
<MinPrecntStd>{5}</MinPrecntStd>
<DaysToLearn>{6}</DaysToLearn>
<MinProfitPerMillion>{7}</MinProfitPerMillion>
<MaxProfitPerMillion>{8}</MaxProfitPerMillion>
<OrderSize>{9}</OrderSize>
<LenthFromStd>{10}</LenthFromStd>
<DaysToPredict>{11}</DaysToPredict>
<ProidLenth>{12}</ProidLenth>
</ReadInput>", 
m_typeOfPaper, m_Max_Lost, m_avreagShift, 
m_Num_Of_Std, m_MaxStd, m_MinStd,
m_daysBackward, m_MaxProfitPerMillon, m_MinProfitPerMillon,
m_Order_Value, m_LenthFromStd, m_daysToReggstion
,clacDays() / m_divideTime));

            string summryFileName = Path.GetFileNameWithoutExtension(m_outFile);
            string summaryFiledirctory = Path.GetDirectoryName(m_outFile);
            string newName = summaryFiledirctory + @"\" + summryFileName + @"_Input.txt";
            System.IO.File.WriteAllLines(newName,list);
        }

        private void makePremotionThreeVector(int i_vectorLen,ref List<int> i_numOfSTD,ref List<int> i_DayTolearn,ref List<float> i_averageSHift)
        {
            List<int> xes = new List<int>();
            List<int> yies = new List<int>();
            List<float> zeds = new List<float>();

            for(int i = 0 ; i < i_numOfSTD.Count ; i++)
            {
               
              for(int j = 0 ; j < i_DayTolearn.Count ; j++)
              {
                  
                  for (int z = 0; z < i_averageSHift.Count ; z++ )
                  {
                      xes.Add(i_numOfSTD[i]);
                      yies.Add(i_DayTolearn[j]);
                      zeds.Add(i_averageSHift[z]);
                  }
              }
            }
         

            i_numOfSTD = xes;
            i_DayTolearn = yies;
            i_averageSHift = zeds;
        }

        private void runOnAllParams(List<int> i_numOfSTD,List<int> i_DayTolearn,List<float> i_averageSHift)
        {
            int numberOfMonths = calcnumberOfBDays();
           // m_divideTime = numberOfMonths;
            if (numberOfMonths < m_divideTime)
            {
                MessageBox.Show("dive time must be less then month  number of month is--" + numberOfMonths);
                return;
            }
            int minlen = Math.Min(i_numOfSTD.Count, i_DayTolearn.Count);
            minlen = Math.Min(minlen, i_averageSHift.Count);
            for (int i = 0; i < minlen ; i++ )
            {
                m_Num_Of_Std = i_numOfSTD[i];
                m_daysBackward = i_DayTolearn[i];
                m_avreagShift = i_averageSHift[i];
                MakeSuperDirctory();
                
                runOnDivTIme(numberOfMonths);
                m_baseOutFile = t_textOutFile.Text;
            }
            
        }

      


        private List<string> makeArrayFromBuffer(string i_Array)
        {
            string mark = ";";
            List<string> obj = new List<string>();
            int placeOfMark = 0;
            int nextMark = 0;
           
            while(nextMark >= 0)
            {
                
                nextMark = i_Array.IndexOf(mark,placeOfMark);//find the first
                if(nextMark >= 0)
                {
                    int len = nextMark - placeOfMark; //find the next one
                    obj.Add(i_Array.Substring(placeOfMark, len));
                    placeOfMark = i_Array.IndexOf(mark, nextMark) + 1;
                }
            }
            return obj;
        }

        private void t_checkTestMode_CheckedChanged(object sender, EventArgs e)
        {
            if (t_checkTestMode.Checked == true)
            {
              
            }
        }

        private void ch_isLearnFromFile_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void b_selectFileToLearnFrom_Click(object sender, EventArgs e)
        {
           
        }

        private void b_selectAsk_Click(object sender, EventArgs e)
        {
            t_textFIleAsk.Text = selectFolder("please select Ask srouce");
        }
        private string selectFolder(string i_message)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description =i_message;
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               return fbd.SelectedPath;
            }
            return fbd.SelectedPath;
        }

        private void b_selectBid_Click(object sender, EventArgs e)
        {
           t_textFileBid.Text = selectFolder("please select Bid srouce");
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void b_SelectOutPath_Click(object sender, EventArgs e)
        {
           t_textOutFile.Text = selectFolder("please select Bid srouce") + @"\" + "outfile.csv";
        }


        //private void readFromFile()
        //{
        //    string filePath = t_learnFromFilePath.Text;

        //    using (StreamReader csvLearning = new StreamReader(filePath))
        //    {
        //        while (!csvLearning.EndOfStream)
        //        {
        //            string itemread = csvLearning.ReadLine();
        //            string date;
        //            setData(itemread, out date);
        //            m_listOfTradeDays.Add(date);




        //        }
        //    } // End Using


        //}

        private void setData(string i_inputItem, out string i_date)
        {
            int indexOfMark = i_inputItem.IndexOf(",", 0);
            string date = i_inputItem.Substring(0, indexOfMark);
            i_date = date.Substring(0, 4) + date.Substring(5, 2) + date.Substring(8, 2);
        }

        private void simultionDays_Load(object sender, EventArgs e)
        {

        }

        private void t_textDaysBack_TextChanged(object sender, EventArgs e)
        {

        }

        private void inputPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}
