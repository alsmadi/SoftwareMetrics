using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

namespace Metrics
{
	/// <summary>
	/// Main Form
	/// </summary>
	public class SWMetricsForm : System.Windows.Forms.Form
	{
	
		private static int i;
		private static string fileName;
		private static string directoryName;
		private static string logFile;
		
		
		private static int lineNumber=0;
		private System.Windows.Forms.TextBox txtDirectory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdBrowser;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox txttype;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListView listFiles;
		private System.Windows.Forms.TreeView tree;
		private System.Windows.Forms.Button cmdCount;
		private System.Windows.Forms.StatusBar statusBar;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ColumnHeader columnHeaderFunction;
	private System.Windows.Forms.ColumnHeader columnHeaderFile;
		
		private System.Windows.Forms.ColumnHeader columnHeaderDirectory;
	private System.Windows.Forms.ColumnHeader columnHeaderlines;
		private System.Windows.Forms.ColumnHeader columnHeaderlines1;

	private System.Windows.Forms.ColumnHeader columnHeaderSLOC;
	private System.Windows.Forms.ColumnHeader columnHeaderSLOCMath;
		private System.Windows.Forms.ColumnHeader columnHeaderMCDC;
		private System.Windows.Forms.ColumnHeader columnHeaderMaxNest;
	private System.Windows.Forms.ColumnHeader columnHeaderCComplexity;

		bool running;				// if we are currently running, if set to false, the running thread will stop executing
		ArrayList files;			// list of CodeEntry
		string startPath;
		private System.Windows.Forms.LinkLabel linkAbout;			// root path where we are looking now
		ColumnSorter columnSorter;
		private System.Windows.Forms.Label label3;
	//	private System.Windows.Forms.ProgressBar progressBar1;
		//private System.Windows.Forms.LinkLabel cmdOptions;
		commentsOptions options;

		public SWMetricsForm(string startPath)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			files = new ArrayList();
			columnSorter = new ColumnSorter();
			listFiles.ListViewItemSorter = columnSorter;
			this.startPath = startPath;
			this.txtDirectory.Text = startPath;
			this.linkAbout.Links.Add(0, linkAbout.Text.Length, "www.cs.ndsu.edu/~alsmadi");
			this.options = new commentsOptions();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBrowser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txttype = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listFiles = new System.Windows.Forms.ListView();
            this.columnHeaderFunction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDirectory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderlines = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderlines1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSLOC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSLOCMath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMCDC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMaxNest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCComplexity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tree = new System.Windows.Forms.TreeView();
            this.cmdCount = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.linkAbout = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.Location = new System.Drawing.Point(60, 4);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(724, 20);
            this.txtDirectory.TabIndex = 0;
            this.txtDirectory.Text = "Source Code";
            this.txtDirectory.TextChanged += new System.EventHandler(this.txtDirectory_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory:";
            // 
            // cmdBrowser
            // 
            this.cmdBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowser.Location = new System.Drawing.Point(788, 4);
            this.cmdBrowser.Name = "cmdBrowser";
            this.cmdBrowser.Size = new System.Drawing.Size(72, 23);
            this.cmdBrowser.TabIndex = 2;
            this.cmdBrowser.Text = "Browse...";
            this.cmdBrowser.Click += new System.EventHandler(this.cmdBrowser_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "type:";
            // 
            // txttype
            // 
            this.txttype.Items.AddRange(new object[] {
            "*.aspx",
            "*.c;*.cpp;*.h",
            "*.cs",
            "*.cfm",
            "*.html",
            "*.java",
            "*.jsp",
            "*.txt",
            "*.vb"});
            this.txttype.Location = new System.Drawing.Point(60, 32);
            this.txttype.Name = "txttype";
            this.txttype.Size = new System.Drawing.Size(128, 21);
            this.txttype.TabIndex = 5;
            this.txttype.Text = "*.cs";
            this.txttype.SelectedIndexChanged += new System.EventHandler(this.txttype_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.listFiles);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.tree);
            this.panel1.Location = new System.Drawing.Point(4, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 351);
            this.panel1.TabIndex = 6;
            // 
            // listFiles
            // 
            this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFunction,
            this.columnHeaderFile,
            this.columnHeaderDirectory,
            this.columnHeaderlines,
            this.columnHeaderlines1,
            this.columnHeaderSLOC,
            this.columnHeaderSLOCMath,
            this.columnHeaderMCDC,
            this.columnHeaderMaxNest,
            this.columnHeaderCComplexity});
            this.listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFiles.Location = new System.Drawing.Point(215, 0);
            this.listFiles.MultiSelect = false;
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(641, 351);
            this.listFiles.TabIndex = 2;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Details;
            this.listFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listFiles_ColumnClick);
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            // 
            // columnHeaderFunction
            // 
            this.columnHeaderFunction.Text = "Function";
            this.columnHeaderFunction.Width = 120;
            // 
            // columnHeaderFile
            // 
            this.columnHeaderFile.Text = "Module";
            this.columnHeaderFile.Width = 120;
            // 
            // columnHeaderDirectory
            // 
            this.columnHeaderDirectory.Text = "Directory";
            this.columnHeaderDirectory.Width = 175;
            // 
            // columnHeaderlines
            // 
            this.columnHeaderlines.Text = "lines";
            this.columnHeaderlines.Width = 75;
            // 
            // columnHeaderlines1
            // 
            this.columnHeaderlines1.Text = "LOC";
            this.columnHeaderlines1.Width = 75;
            // 
            // columnHeaderSLOC
            // 
            this.columnHeaderSLOC.Text = "SLOC";
            this.columnHeaderSLOC.Width = 75;
            // 
            // columnHeaderSLOCMath
            // 
            this.columnHeaderSLOCMath.Text = "#op";
            this.columnHeaderSLOCMath.Width = 75;
            // 
            // columnHeaderMCDC
            // 
            this.columnHeaderMCDC.Text = "#MC/DC";
            this.columnHeaderMCDC.Width = 75;
            // 
            // columnHeaderMaxNest
            // 
            this.columnHeaderMaxNest.Text = "Max Nest";
            this.columnHeaderMaxNest.Width = 75;
            // 
            // columnHeaderCComplexity
            // 
            this.columnHeaderCComplexity.Text = "CComplexity";
            this.columnHeaderCComplexity.Width = 75;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(212, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 351);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Left;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(212, 351);
            this.tree.TabIndex = 0;
            this.tree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCollapse);
            this.tree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterExpand);
            // 
            // cmdCount
            // 
            this.cmdCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCount.Location = new System.Drawing.Point(784, 416);
            this.cmdCount.Name = "cmdCount";
            this.cmdCount.Size = new System.Drawing.Size(75, 23);
            this.cmdCount.TabIndex = 7;
            this.cmdCount.Text = "Count";
            this.cmdCount.Click += new System.EventHandler(this.cmdCount_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 443);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(864, 23);
            this.statusBar.TabIndex = 8;
            // 
            // linkAbout
            // 
            this.linkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkAbout.Location = new System.Drawing.Point(784, 36);
            this.linkAbout.Name = "linkAbout";
            this.linkAbout.Size = new System.Drawing.Size(72, 20);
            this.linkAbout.TabIndex = 11;
            this.linkAbout.TabStop = true;
            this.linkAbout.Text = "IAlsmadi";
            this.linkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAbout_LinkClicked);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(624, 416);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Counting";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // SWMetricsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(864, 466);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkAbout);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.cmdCount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txttype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdBrowser);
            this.Controls.Add(this.label1);
            this.Name = "SWMetricsForm";
            this.Text = "SWMetrics";
            this.Load += new System.EventHandler(this.SWMetricsForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			
			if (startPath.Length > 0)
				this.cmdCount_Click(null, EventArgs.Empty);
		}


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			string startPath = args.Length == 0 ? string.Empty : args[0];
			SWMetricsForm form = new SWMetricsForm(startPath);
			Application.Run(form);
		}

		private void cmdBrowser_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = this.txtDirectory.Text;
			if (dlg.ShowDialog() == DialogResult.OK)
				this.txtDirectory.Text = dlg.SelectedPath;
		}
		private int Count(string dir, string[] type, TreeNode parent)
		{
			this.statusBar.Text = dir;
			this.statusBar.Update();
			
		//	this.progressBar1.PerformStep();
		

			// go thru directories first
			int totalCount = 0;
			//int totallineCount = 0;
			string[] dirs = Directory.GetDirectories(dir);
			
			
			foreach (string innerdir in dirs)
			{
				if (!running)
					return 0;

		TreeNode codeNode = new TreeNode(Path.GetFileName(innerdir));
				codeNode.ImageIndex = 0;
				codeNode.SelectedImageIndex = 0;
				int count = Count(innerdir, type, codeNode);
				//progressBar1.Minimum=1;
				//progressBar1.Maximum=count;
				//progressBar1.Step=1;
				if (count > 0)
				{
					//		node.Text += " (" + count + ")";
					totalCount += count;
					
					
					parent.Nodes.Add(codeNode);
				}
			}

			string dirName = dir.Substring(this.startPath.Length);
			foreach (string pattern in type)
			{
			//		progressBar1.Step=1;
		//		progressBar1.Value += 1;
			//	this.progressBar1.Update();
			//	this.progressBar1.Value +=1;
				
				string function="";
				string[] filenames = Directory.GetFiles(dir, pattern);
		//		progressBar1.Minimum=1;
				
			//	this.progressBar1.Maximum=dirs.Length+filenames.Length;
			//MessageBox.Show(this.progressBar1.Maximum.ToString());
				foreach (string file in filenames)
				{
						if (!running)
						return 0;
				
					

					int LocCount = CountLoc(file);
					int lineCount = Countlines(file);
					int SLocCount = CountSLoc(file);
					int MCDCCount =CountMCDC(file);
					int MaxNesting =CountMaxNesting(file);
					int cyclicComplexity=CyComplexity(file);
					
					 int SLocMathCount=CountSLocMath(file);
					if (LocCount > 0)
					{
				string name = Path.GetFileName(file);
				string name1 = Path.GetFileNameWithoutExtension(file);
				fileName=name1;
				directoryName=dirName;
				functionmetrics(file);
		
						

						
				TreeNode node = new TreeNode(name);
						node.ImageIndex = 1;
						node.SelectedImageIndex = 1;
						parent.Nodes.Add(node);
						totalCount += SLocCount;
						//totallineCount += lineCount;
						
						CodeEntry entry = new CodeEntry(function,name, dirName, lineCount,LocCount,SLocCount,SLocMathCount,MCDCCount,MaxNesting,cyclicComplexity);	
				//		swFromFile.Write("function" + "     '");
				//		swFromFile.Write("name" + "     '");
				//		swFromFile.Write("dirName" + "     '");
				//		swFromFile.Write("lineCount" + "     '");
				//		swFromFile.Write("LocCount" + "     '");
				//		swFromFile.Write("SLocCount" + "     '");
				//		swFromFile.Write("SLocMathCount" + "     '");
				//		swFromFile.Write("MCDCCount" + "     '");
				//	swFromFile.Write("MaxNesting" + "     '");
				//		swFromFile.Write("cyclicComplexity" + "     '");
						this.files.Add(entry);
				//		swFromFile.Flush();
				//		swFromFile.Close();
					}
					
					
				}
				
				
			}
		
			return totalCount;
		}
		
		private bool methodstart(string line)
		{string inter="";
			bool methodqout=false;
			bool methodclose=false;
			bool loops=false;
			
			string line1 = line.Trim();
			if(line1.StartsWith("if")||line1.StartsWith("#")||line1.StartsWith("//")||(line1.StartsWith("/*")&&line1.EndsWith("*/"))||line1.StartsWith("switch")||line1.StartsWith("for")||line1.StartsWith("*")||line1.StartsWith("while")||line1.StartsWith("else")||line1.StartsWith("return"))
				loops=true;
			for(int iIndex=0;iIndex < line1.Length;iIndex++)
			{							
				inter=line1.Substring(iIndex,1);
				if(inter=="(")
				methodqout=true;
				if(inter==")")
				methodclose=true;
	if(methodqout==true && methodclose==true &&line1.IndexOf("(")<line1.IndexOf(")")&& loops==false)
					return true;
				if(line1.StartsWith("void"))
					return true;					
			}
			return false;
		}
		private int mcdc( string linetrimmed)
		{
			int count=1;
			if(linetrimmed.StartsWith("if") || linetrimmed.StartsWith("else")|| linetrimmed.StartsWith("switch")|| linetrimmed.StartsWith("case"))
			{
			
	for(int Index2=0;Index2 < linetrimmed.Length-1;Index2=Index2+2)
				{
								
					
					string inter="";	
					string inter1="";	
					inter=linetrimmed.Substring(Index2,2);
					if(Index2<linetrimmed.Length-2)
						inter1=linetrimmed.Substring(Index2+1,2);
		
	if(inter=="&&" || inter=="||" || inter1=="&&" || inter1=="||")															  
					{
						count=count+2;
					}
				}
				
	
			}
		//	if(count>0)
		//	MessageBox.Show(count.ToString());
			return count;
		}
		private int mathOperation( string line)
		{int count=0;
			bool comment=false;
		for(int Index2=0;Index2 < line.Length-1;Index2=Index2+2)
			{comment=false;
				string intermediatemethod1="";
				string intermediatemethod="";
				string inter="";	
				string inter1="";	
				inter=line.Substring(Index2,2);
				if(Index2<line.Length-2)
				inter1=line.Substring(Index2+1,2);
		
	if(inter=="/*" || inter=="*/" || inter1=="/*" || inter1=="*/")															  {
					comment=true;
		
				}
				intermediatemethod1=inter.Substring(0,1);
				intermediatemethod=inter.Substring(1,1);
			

				if(comment==false)
				{
					if(inter=="++"||inter=="--"||inter=="*="||inter=="%="||inter=="+="||inter=="-="||inter=="/="||inter=="**"||inter1=="++"||inter1=="--"||inter1=="*="||inter1=="%="||inter1=="+="||inter1=="-="||inter1=="/="||inter1=="**")
					{
						
						count++;
					}
				
				
					else if(intermediatemethod1=="+"||intermediatemethod1=="-"||intermediatemethod1=="/"||intermediatemethod1=="*"||intermediatemethod1=="%"||intermediatemethod1=="=")
					{
						
						count++;

					}
					else if(intermediatemethod=="+"||intermediatemethod=="-"||intermediatemethod=="/"||intermediatemethod=="*"||intermediatemethod=="%"||intermediatemethod=="=")
					{
						
						count++;

					}
				}
	
				}
			
			return count;
			}
		private int CyComplexity( string path)
		{
			int lineNumber=0;
			string inter1="";
			int CComplexity=1;
			int CComplexityTotal=0;
		
			bool loc=false;
			try
			{
				StreamReader reader = new StreamReader(path);

				bool inComment = false; // set to true, if we're inside multi-line comment
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					lineNumber++;
					string linetrimmed = line.Trim();
					if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
					}
			
					
					if (linetrimmed.Length > 0 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&&inComment==false)
					{
						loc=true;
					}
					if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
					if(loc==true)
					{
						//	MessageBox.Show("loc true in line number " + lineNumber);
						for(int Index1=0;Index1 < linetrimmed.Length;Index1++)
						{							
							inter1=linetrimmed.Substring(Index1,1);
						
							if(inter1=="if" || inter1=="while"|| inter1=="repeat"|| inter1=="for"|| inter1=="and"|| inter1=="or"|| inter1=="&&"|| inter1=="||"|| inter1=="case")
							{
							
								CComplexity++;
							}
				
						
						}
					}
				}
				CComplexityTotal=CComplexityTotal+CComplexity;

				reader.Close();
			}
			catch (IOException)
			{
			}
			//MessageBox.Show("count here is"+ count);
			return CComplexityTotal;
		}

		private void functionmetrics ( string path)
		{
		bool loc=false;
			bool sloc=false;
	//		string methodname="";
			int bracket=0;
			string intermediateString="";
			string intermediateString1="";
			string totalstring="";
			string intermediatemethod1="";
	//		string intermediate1="";
		//	int methodcount=0;
		//	int startcomment=0;
		//	int endcomment=0;
			string comment1="";
			string functionname="";
			bool bracketenabled=false;
			bool methodcheckstart=false;
	
			bool methodflag=false;
			bool linebracketenabled=true;
			int nestingLevel=0;
			int maxNestingTotal;
			int CComplexity=1;
			int CComplexityTotal=0;
			int lineCount=0;
			int locLines=0;
			int slocLines=0;
			int mathcount1=0;
			int mathCount=0;
			int Countmcdc=0;
			bool mcdcBracketStart=false;
			bool enableMCDC=false;
	
			try
			{
				StreamReader reader = new StreamReader(path);
			//	string logFile = DateTime.Now.ToShortDateString().Replace(@"/",@"-").Replace(@"\",@"-") + ".log";
    
	//					FileStream fs = new FileStream(fileName,
	//		FileMode.Append, FileAccess.Write, FileShare.None);

			StreamWriter swFromFile = new StreamWriter(logFile,true);
				bool inComment = false; // set to true, if we're inside multi-line comment
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					
					string linetrimmed = line.Trim();
				//	MessageBox.Show(linetrimmed.Length.ToString());
				

			if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
				}

					for(int Indexcomment=0;Indexcomment < linetrimmed.Length-1;Indexcomment++)
					{
				comment1=linetrimmed.Substring(Indexcomment,2);
						if(comment1=="/*")
						{
							inComment=true;}
						if(comment1=="*/")
						{
							inComment=false;
						}
					} 

 
			if (!linetrimmed.StartsWith(options.SingleLineCommentStart)&&inComment==false&&!linetrimmed.StartsWith("#")&&!(linetrimmed.StartsWith("/*")&&linetrimmed.EndsWith("*/"))&&!linetrimmed.StartsWith("if")&&!linetrimmed.StartsWith("switch")&&!linetrimmed.StartsWith("for")&&!linetrimmed.StartsWith("while"))
			{
				loc=true;
			}
			else
			{
			loc=false;
			}
		if (linetrimmed.Length > 2 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&& inComment==false&&!linetrimmed.StartsWith("#")&&!linetrimmed.StartsWith("using")&&!linetrimmed.StartsWith("int")&&!linetrimmed.StartsWith("double")&&!linetrimmed.StartsWith("struct")&&!linetrimmed.StartsWith("signed")&&!linetrimmed.StartsWith("unsigned")&&!linetrimmed.StartsWith("char")&&!linetrimmed.StartsWith("const")&&!linetrimmed.StartsWith("class")&&!linetrimmed.StartsWith("public")&&!linetrimmed.StartsWith("private")&&!linetrimmed.StartsWith("static")&&!linetrimmed.StartsWith("BEGIN")&&!linetrimmed.StartsWith("END")&&!linetrimmed.StartsWith("BOOL")&&!linetrimmed.StartsWith("protected")&&!linetrimmed.StartsWith("void")&&!linetrimmed.StartsWith("break")&&!linetrimmed.StartsWith("return")&&!linetrimmed.StartsWith("try")&&!linetrimmed.StartsWith("bool")&&!linetrimmed.StartsWith("else")&&!linetrimmed.StartsWith("typedef")&&!linetrimmed.StartsWith("inline")&&!linetrimmed.StartsWith("virtual"))
		//&&!(line.StartsWith("{") )&&!line.StartsWith("}"))
					{
						sloc=true;
					}
					else
					{
						sloc=false;
					}

		if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
					methodcheckstart=methodstart(linetrimmed);
				
			for(int Index1=0;Index1 < linetrimmed.Length;Index1++)
					{							
			intermediatemethod1=linetrimmed.Substring(Index1,1);
						if(intermediatemethod1=="}")
						{
							bracket--;
						//	closebracketflag=true;
						}
						if(intermediatemethod1=="{"){
							bracket++;
							nestingLevel++;
							}
						if(intermediatemethod1=="(")
						nestingLevel++;
					}
		// added just for a short test
            methodcheckstart = true;
		if(methodcheckstart==true)
		methodflag=true;
					if(methodflag ==true && methodcheckstart==true)
					{
	
		if(linetrimmed.StartsWith("void"))
		linetrimmed=linetrimmed.Remove(0,5);
		for(int Index1=0;Index1 < linetrimmed.Length;Index1++)
							{			
			if(linebracketenabled==true)
				functionname=totalstring;			
			intermediatemethod1=linetrimmed.Substring(Index1,1);
			totalstring = totalstring+intermediatemethod1;
								if(intermediatemethod1=="("){
							linebracketenabled=false;
									break;}
					}				} 	
					
					if(methodflag==true)
							{
				
						
			for(int Index1=0;Index1 < linetrimmed.Length;Index1++)
						{							
			intermediatemethod1=linetrimmed.Substring(Index1,1);
							if(intermediatemethod1=="{")
							{
								bracketenabled=true;}
						}
		
		 if(mcdcenabled(linetrimmed)==true)
						enableMCDC=true;
					if(enableMCDC==true)
					{
						Countmcdc++;
if(linetrimmed.StartsWith("if")||linetrimmed.StartsWith("switch"))
						Countmcdc++;

					}
if(linetrimmed.StartsWith("if")||linetrimmed.StartsWith("while")||linetrimmed.StartsWith("repeat")||linetrimmed.StartsWith("for")||linetrimmed.StartsWith("and")||linetrimmed.StartsWith("or")||linetrimmed.StartsWith("&&")||linetrimmed.StartsWith("||")||linetrimmed.StartsWith("case"))
	CComplexity++;
		
		
			/*			if(mcdcenabled(linetrimmed)==true && linetrimmed.StartsWith("if")||linetrimmed.StartsWith("switch"))
						{
							Countmcdc=Countmcdc+2;
							enableMCDC=true;
						}
						else if(mcdcenabled(linetrimmed)==true)
						{
							enableMCDC=true;
							Countmcdc=Countmcdc++;} */
					if(enableMCDC==true || mcdcBracketStart==true)
						{
						
							
							for(int Index2=0;Index2 < linetrimmed.Length-1;Index2=Index2+2)
							{
								
								string interm1="";
								string interm="";
								string inter="";	
								string inter1="";	
								inter=linetrimmed.Substring(Index2,2);
								interm1=inter.Substring(0,1);
								interm=inter.Substring(1,1);
								
								if(Index2<linetrimmed.Length-2)
									inter1=linetrimmed.Substring(Index2+1,2);
								if(interm1=="(" || interm=="(")
									
									mcdcBracketStart=true;
								if(interm1==")" || interm==")")
									
									mcdcBracketStart=false;
								if(linetrimmed.IndexOf(")") > linetrimmed.IndexOf("("))
									mcdcBracketStart=false;

								
								if(inter=="&&" || inter=="||" || inter1=="&&" || inter1=="||")															  
								{
									Countmcdc++;
		//	MessageBox.Show("in line  " + lineNumber + Countmcdc);
									
								}
							}
							
	
						}
						enableMCDC=false;
						
							

						
						lineCount++;
						if(loc==true){
						locLines++;
						if(sloc==true)
						slocLines++;
					mathcount1=mathOperation(linetrimmed);
					mathCount=mathCount+mathcount1;
					
						}
					}		
		CComplexityTotal=CComplexityTotal+CComplexity;			
			
		if(bracket==0 && methodflag==true && bracketenabled==true){			
				
		CodeEntry entry = new CodeEntry(functionname,fileName, directoryName, lineCount,locLines,slocLines,mathCount,Countmcdc,nestingLevel,CComplexityTotal);

						this.files.Add(entry);
		//	MessageBox.Show(functionname);
			swFromFile.Write(functionname+"    ,");
			swFromFile.Write(fileName+"    ,");
			swFromFile.Write(directoryName+"    ,");
			swFromFile.Write(lineCount+"    ,");
			swFromFile.Write(locLines+"    ,");
			swFromFile.Write(slocLines+"    ,");
			swFromFile.Write(mathCount+"    ,");
			swFromFile.Write(Countmcdc+"    ,");
			swFromFile.Write(nestingLevel+"    ,");
			swFromFile.Write(CComplexityTotal+"    ,");
			swFromFile.WriteLine();
						
					
			
			
			//maxNestingTotal+=nestingLevel;
			//MessageBox.Show(maxNestingTotal.ToString());
					
				
					lineCount=0;
					locLines=0;
					slocLines=0;
					methodflag=false;
					bracketenabled=false;
					linebracketenabled=true;
					mathcount1=0;
					mathCount=0;
			CComplexity=1;
			CComplexityTotal=0;
					Countmcdc=0;
			nestingLevel=0;
						totalstring="";
						intermediatemethod1="";
						intermediateString="";
						intermediateString1="";
						}
		
						 i=i+1;
				
				} 	// close loc and bracket ==0.	

				swFromFile.Flush();
				swFromFile.Close();
				reader.Close();
			} // close the while loop.
			catch (IOException)
			{
			}
		 
		}

		private int CountMaxNesting(string path)
		{
			int lineNumber=0;
			string inter1="";
			int nestingLevel=0;
			int maxNestingLevel=0;
			bool loc=false;
			try
			{
				StreamReader reader = new StreamReader(path);

				bool inComment = false; // set to true, if we're inside multi-line comment
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					lineNumber++;
					string linetrimmed = line.Trim();
					if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
					}
			
					
					if (linetrimmed.Length > 0 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&&inComment==false)
					{
						loc=true;
					}
					if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
					if(loc==true)
					{
		//	MessageBox.Show("loc true in line number " + lineNumber);
				for(int Index1=0;Index1 < linetrimmed.Length;Index1++)
						{							
							inter1=linetrimmed.Substring(Index1,1);
						
							if(inter1=="{" || inter1=="(")
							{
							
								nestingLevel++;
							}
				
						
						}
					}
				}
maxNestingLevel=maxNestingLevel+nestingLevel;

				reader.Close();
			}
			catch (IOException)
			{
			}
			//MessageBox.Show("count here is"+ count);
			return maxNestingLevel;
		//	return maxNestingTotal;
		}
		private bool mcdcenabled( string line)
		{
		//	int count = 0;
			
	//		try
	//		{
	//			StreamReader reader = new StreamReader(path);
				bool loc=false;
				bool inComment = false; // set to true, if we're inside multi-line comment
				
		//		while ((line = reader.ReadLine()) != null)
		//		{
					string linetrimmed = line.Trim();
					if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
					}
						
					if (linetrimmed.Length > 0 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&&inComment==false)
					{
						loc=true;
					}
					if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
						
					//else{
					//	MessageBox.Show("this is line" + count++);
					//	count++;}
				
					if(loc==true && linetrimmed.StartsWith("if") || linetrimmed.StartsWith("else")|| linetrimmed.StartsWith("switch")|| linetrimmed.StartsWith("case"))
					{
						return true;
					
					}
			else 
				return false;
				
				
		}

		private int CountMCDC(string path)
		{				
			int CountNodes=0;
			int countNodesTotal = 0;
		//	int countBrackets=0;
			bool bracketEnabled=false;
			bool enableMCDC=false;
	//		bool bracketsOpened=false;

			
			try
			{
				StreamReader reader = new StreamReader(path);
							string line;
				while ((line = reader.ReadLine()) != null)
				{
					lineNumber++;
					
					string linetrimmed = line.Trim();
					if(mcdcenabled(linetrimmed)==true)
						enableMCDC=true;
					if(enableMCDC==true)
					{
						countNodesTotal++;
if(linetrimmed.StartsWith("if")||linetrimmed.StartsWith("switch"))
						countNodesTotal++;

					}
			
					
					if(enableMCDC==true || bracketEnabled==true)
					{
				
	for(int Index2=0;Index2 < linetrimmed.Length-1;Index2=Index2+2)
						{
								
							string intermediatemethod1="";
							string intermediatemethod="";
							string inter="";	
							string inter1="";	
							inter=linetrimmed.Substring(Index2,2);
				intermediatemethod1=inter.Substring(0,1);
				intermediatemethod=inter.Substring(1,1);
	//	MessageBox.Show("in line " + lineNumber + intermediatemethod1+intermediatemethod);
							if(Index2<linetrimmed.Length-2)
						inter1=linetrimmed.Substring(Index2+1,2);
		if(intermediatemethod1=="(" || intermediatemethod=="(")
			bracketEnabled=true;
		if(intermediatemethod1==")" || intermediatemethod==")")
		bracketEnabled=false;
		if(linetrimmed.IndexOf(")") > linetrimmed.IndexOf("("))
		bracketEnabled=false;

		if(inter=="&&" || inter=="||" || inter1=="&&" || inter1=="||")															  
							{
							CountNodes++;
		
							}
							}
						
	
					}
					enableMCDC=false;
						}
				reader.Close();
				countNodesTotal=countNodesTotal+CountNodes;
					}
			catch (IOException)
			{
			}
			
			return countNodesTotal;
			//MessageBox.Show("count here is"+ count);
			
		}
		private int CountLoc(string path)
		{
			
			int count = 0;
			try
			{
				StreamReader reader = new StreamReader(path);

				bool inComment = false; // set to true, if we're inside multi-line comment
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					string linetrimmed = line.Trim();
					if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
					}
			
					
					if (linetrimmed.Length > 0 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&&inComment==false)
					{
						count++;
					}
					if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
						
					//else{
					//	MessageBox.Show("this is line" + count++);
					//	count++;}
				}
				


				reader.Close();
			}
			catch (IOException)
			{
			}
			//MessageBox.Show("count here is"+ count);
			return count;
		}
		private int CountSLoc(string path)
		{
			

			int count = 0;
			try
			{
				StreamReader reader = new StreamReader(path);

				bool inComment = false; // set to true, if we're inside multi-line comment
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					//	string [] tempArray = line;
					/*	for (int i = 0; i < line.Length; i++)
						{
						if(line.StartsWith(null)){
							line=line. .Trim();}
						} */
					/*while (line.Substring(0,1) == null) 
					{ 
					line = line.TrimStart(null); 
					} */
					string linetrimmed = line.Trim();



					if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
					}
			
					
					
					if (linetrimmed.Length > 2 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&& inComment==false&&!linetrimmed.StartsWith("#")&&!linetrimmed.StartsWith("using")&&!linetrimmed.StartsWith("int")&&!linetrimmed.StartsWith("double")&&!linetrimmed.StartsWith("struct")&&!linetrimmed.StartsWith("signed")&&!linetrimmed.StartsWith("unsigned")&&!linetrimmed.StartsWith("char")&&!linetrimmed.StartsWith("const")&&!linetrimmed.StartsWith("class")&&!linetrimmed.StartsWith("public")&&!linetrimmed.StartsWith("public")&&!linetrimmed.StartsWith("static")&&!linetrimmed.StartsWith("BEGIN")&&!linetrimmed.StartsWith("END")&&!linetrimmed.StartsWith("BOOL")&&!linetrimmed.StartsWith("protected")&&!linetrimmed.StartsWith("void")&&!linetrimmed.StartsWith("break")&&!linetrimmed.StartsWith("return")&&!linetrimmed.StartsWith("try")&&!linetrimmed.StartsWith("bool")&&!linetrimmed.StartsWith("else")&&!linetrimmed.StartsWith("typedef")&&!linetrimmed.StartsWith("inline")&&!linetrimmed.StartsWith("virtual"))
						//&&!(line.StartsWith("{") )&&!line.StartsWith("}"))
					{
						count++;
					}
					if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
						
					//else{
					//	MessageBox.Show("this is line" + count++);
					//	count++;}
				}
				


				reader.Close();
			}
			catch (IOException)
			{
			}
			//MessageBox.Show("count here is"+ count);
			return count;
		}
		private int CountSLocMath(string path)
		{
			int totfind=0;
			bool sloc=false;
	//		string intermediatemethod1="";
		//	int count = 0;
			try
			{
				StreamReader reader = new StreamReader(path);

				bool inComment = false; // set to true, if we're inside multi-line comment
				string line;
				
				int test=0;
				while ((line = reader.ReadLine()) != null)
				{
					test++;
					
					string linetrimmed = line.Trim();



					if (linetrimmed.StartsWith("/*"))
					{
						inComment = true;
					}
			
					
					if (linetrimmed.Length > 2 &&!linetrimmed.StartsWith(options.SingleLineCommentStart)&& inComment==false&&!linetrimmed.StartsWith("#")&&!linetrimmed.StartsWith("using")&&!linetrimmed.StartsWith("int")&&!linetrimmed.StartsWith("double")&&!linetrimmed.StartsWith("struct")&&!linetrimmed.StartsWith("signed")&&!linetrimmed.StartsWith("unsigned")&&!linetrimmed.StartsWith("char")&&!linetrimmed.StartsWith("const")&&!linetrimmed.StartsWith("class")&&!linetrimmed.StartsWith("public")&&!linetrimmed.StartsWith("public")&&!linetrimmed.StartsWith("static")&&!linetrimmed.StartsWith("BEGIN")&&!linetrimmed.StartsWith("END")&&!linetrimmed.StartsWith("BOOL")&&!linetrimmed.StartsWith("protected")&&!linetrimmed.StartsWith("void")&&!linetrimmed.StartsWith("break")&&!linetrimmed.StartsWith("return")&&!linetrimmed.StartsWith("try")&&!linetrimmed.StartsWith("bool")&&!linetrimmed.StartsWith("else")&&!linetrimmed.StartsWith("typedef")&&!linetrimmed.StartsWith("inline")&&!linetrimmed.StartsWith("virtual"))
					
					{
						
						sloc=true;
					}
		if (linetrimmed.IndexOf(options.MultiLineCommentEnd) >= 0)
					{	inComment = false;}
					
					if (sloc==true) 
					{


				bool comment=false;
						
	for(int Index1=0;Index1 < linetrimmed.Length-1;Index1=Index1+2)
	{
		comment=false;
		string inter1="";
		string intermediatemethod="";
		string intermediatemethod1="";
		string inter="";	
			
		inter=linetrimmed.Substring(Index1,2);
		if(Index1<linetrimmed.Length-2)
			inter1=linetrimmed.Substring(Index1+1,2);
	if(inter=="/*" || inter=="*/" || inter1=="/*" || inter1=="*/")															  
		{
			comment=true;
		
		}
		intermediatemethod1=inter.Substring(0,1);
		intermediatemethod=inter.Substring(1,1);
	//	MessageBox.Show("inter 1 is   "+intermediatemethod1 +"  inter  is    "+intermediatemethod);
		
		if(comment==false)
		{
			if(inter=="++"||inter=="--"||inter=="*="||inter=="%="||inter=="+="||inter=="-="||inter=="/="||inter=="**"||inter1=="++"||inter1=="--"||inter1=="*="||inter1=="%="||inter1=="+="||inter1=="-="||inter1=="/="||inter1=="**")
			{
	//		MessageBox.Show("I am in 1 column number " + test);
				totfind++;
			}
				
				
			else if(intermediatemethod1=="+"||intermediatemethod1=="-"||intermediatemethod1=="/"||intermediatemethod1=="*"||intermediatemethod1=="%"||intermediatemethod1=="=")
			{
		//		MessageBox.Show("I am in 2 column number " + test);		
				totfind++;

			}
			else if(intermediatemethod=="+"||intermediatemethod=="-"||intermediatemethod=="/"||intermediatemethod=="*"||intermediatemethod=="%"||intermediatemethod=="=")
			{
		//	MessageBox.Show("I am in 3 column number " + test);	
				totfind++;

			}
		}
					}

					}

					}
				


						reader.Close();
				}
			catch (IOException)
			{
			}
			//MessageBox.Show("count here is"+ count);
			return totfind;
		} 
		
		private int Countlines(string path)
		{
			int count = 0;
			try
			{
				StreamReader reader = new StreamReader(path);

			
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					// if we count blank lines and comment lines,
					// we can just increment line count, and no need
					// to trim line
				
					count++;
					
					
				}
				reader.Close();
			}
			catch (IOException)
			{
			}

			return count;
		}

		private void CountStart()
		{
			this.cmdCount.Text = "Cancel";
			running = true;
			TreeNode root = new TreeNode("Root");
			root.ImageIndex = 0;
			this.startPath = txtDirectory.Text;
			string[] type = this.txttype.Text.Split(';');
			files.Clear();
			int count = Count(txtDirectory.Text, type, root);
			//this.progressBar1.Minimum=1;
			//this.progressBar1.Value=10;
			//this.progressBar1.Step=10;
		//	this.progressBar1.Maximum=count;
			

			root.Text = "All Files & Directories ";

			tree.Invoke(new CountFinishedDelegate(CountFinished), new object[]{root, count});
		}

		public delegate void CountFinishedDelegate(TreeNode root, int count);

		private void CountFinished(TreeNode root, int count)
		{
			tree.Nodes.Clear();
			listFiles.Items.Clear();

			if (running)
			{
				tree.Nodes.Add(root);
				tree.Nodes[0].Expand();
			
				// disable column sorting
				this.listFiles.ListViewItemSorter = null;

				ListViewItem[] items = new ListViewItem[files.Count];
				for (int i=0; i<files.Count; i++)
				{
					CodeEntry entry = (CodeEntry)files[i];
					string[] names = {entry.Function,entry.Name, entry.Dir, entry.Lines.ToString(),entry.Loc.ToString(),entry.SLoc.ToString(),entry.SLocMath.ToString(), entry.MCDC.ToString(),entry.MaxNest.ToString(),entry.CComplexity.ToString()};
					ListViewItem item = new ListViewItem(names);
					item.Tag = entry;
					items[i] = item;
				}
				this.listFiles.Items.AddRange(items);
				columnSorter.SortOrder = SortOrder.Descending;
				columnSorter.Column = 2;
				listFiles.ListViewItemSorter = columnSorter;
				//this.txtTotal.Text = count.ToString("#,#");
			}
			else
				//this.txtTotal.Text = string.Empty;

			this.statusBar.Text = string.Empty;
			this.running = false;
			this.cmdCount.Text = "Count";
			this.label3.Text="Done";
			
		}

		private void cmdCount_Click(object sender, System.EventArgs e)
		{

			if (running)
			{
				running = false;
			}
			else
			{
             //   this.txtDirectory.Text = "C:\\SWMetrics";
               // this.txtDirectory.Text = this.txtDirectory.Text;
                if (Directory.Exists(this.txtDirectory.Text))
				{
				
			Thread thread = new Thread(new ThreadStart(CountStart));
				//	progressBar1.Value += 1;
	SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		//	string logFile;
					saveFileDialog1.DefaultExt = "csv";
			saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
				saveFileDialog1.Title = "Saving the metrics to a file";
					saveFileDialog1.ShowDialog();
					if(saveFileDialog1.FileName != "")
					
					logFile=saveFileDialog1.FileName;
					else
						logFile= DateTime.Now.ToShortDateString().Replace(@"/",@"-").Replace(@"\",@"-") + ".log";

    
					//			FileStream fs = new FileStream(fileName,
					//	FileMode.Append, FileAccess.Write, FileShare.None);

					StreamWriter swFromFile = new StreamWriter(logFile,true);
					swFromFile.Write("functionName" + "     '");
					swFromFile.Write("Filename" + "     '");
					swFromFile.Write("dirName" + "     '");
					swFromFile.Write("lineCount" + "     '");
					swFromFile.Write("LocCount" + "     '");
					swFromFile.Write("SLocCount" + "     '");
					swFromFile.Write("SLocMathCount" + "     '");
					swFromFile.Write("MCDCCount" + "     '");
					swFromFile.Write("MaxNesting" + "     '");
					swFromFile.Write("cyclicComplexity" + "     '");
					swFromFile.WriteLine();
					swFromFile.Flush();
					swFromFile.Close();
					thread.Start();
					label3.Visible=true;
					label3.Text="Counting";
				//	cmdCount.Enabled=false;
				}
				else
					MessageBox.Show("Start directory not found");
			}
		}

		private void tree_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = 2;
			e.Node.SelectedImageIndex = 2;
		}

		private void tree_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = 0;
			e.Node.SelectedImageIndex = 0;
		}

		private void listFiles_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			if (columnSorter.Column == e.Column)
				columnSorter.FlipSortOrder();
			else
			{
				columnSorter.SortOrder = SortOrder.Ascending;
				columnSorter.Column = e.Column;
			}
			listFiles.Sort();
		}

		private void linkAbout_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.cs.ndsu.edu/~alsmadi");
		}

		/*	private void cmdOptions_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
			{
				OptionsForm frm = new OptionsForm();
				frm.Options = this.options;
				frm.ShowDialog();		
			} */

		private void listFiles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void SWMetricsForm_Load(object sender, System.EventArgs e)
		{
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
		}

		private void txtDirectory_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txttype_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}

	class ColumnSorter : IComparer
	{
		int column;
		SortOrder sortOrder;

		public ColumnSorter()
		{
			this.column = 0;
			this.sortOrder = SortOrder.Ascending;
		}

		public void FlipSortOrder()
		{
			if (sortOrder == SortOrder.Ascending)
				sortOrder = SortOrder.Descending;
			else
				sortOrder = SortOrder.Ascending;
		}

		public int Compare(object r1, object r2)
		{
			ListViewItem row1 = (ListViewItem)r1;
			ListViewItem row2 = (ListViewItem)r2;

			CodeEntry e1 = (CodeEntry)row1.Tag;
			CodeEntry e2 = (CodeEntry)row2.Tag;

			if (sortOrder == SortOrder.Ascending)
			{

				if (column == 0)
					return e1.Name.CompareTo(e2.Name);
				else if (column == 1)
					return e1.Dir.CompareTo(e2.Dir);
				else if (column == 2)
					return e1.Lines < e2.Lines ? -1 : e1.Lines > e2.Lines ? 1 : 0;
				else
					return 0;
			}
			else
			{
				if (column == 0)
					return e2.Name.CompareTo(e1.Name);
				else if (column == 1)
					return e2.Dir.CompareTo(e1.Dir);
				else if (column == 2)
					return e2.Lines < e1.Lines ? -1 : e2.Lines > e1.Lines ? 1 : 0;
				else
					return 0;

			}
		}

		public int Column
		{
			get { return this.column; }
			set { this.column = value; }
		}

		public System.Windows.Forms.SortOrder SortOrder
		{
			get { return this.sortOrder; }
			set { this.sortOrder = value; }
		}
	}
}
