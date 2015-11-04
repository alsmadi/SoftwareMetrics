
using System;

namespace Metrics
{
	/// <summary>
	/// Single file entry
	/// </summary>
	public class CodeEntry
	{
		readonly int lines;
		readonly int loc;
		readonly int sloc;
		readonly string name;
		readonly string dir;
		readonly int slocmath;
		readonly string function;
		readonly int mcdc;
		readonly int maxnesting;
		readonly int ccomplexity;

		public CodeEntry(string function,string name, string dir, int lines, int loc, int sloc, int slocmath, int mcdc, int maxnesting, int ccomplexity)
	//	public CodeEntry(string name, string dir, int lines, int loc, int sloc, int slocmath)
		{
			this.function=function;
			this.lines = lines;
			this.name = name;
			this.dir = dir;
			this.loc=loc;
			this.sloc=sloc;
			this.slocmath=slocmath;
			this.mcdc=mcdc;
			this.maxnesting=maxnesting;
			this.ccomplexity=ccomplexity;
		}

		public string Name
		{
			get { return this.name; }
		}
		public string Function
		{
			get { return this.function; }
		}
		

		public string Dir
		{
			get { return this.dir; }
		}

		public int Lines
		{
			get { return this.lines; }
		}
		public int Loc
		{
			get { return this.loc; }
		}
		public int SLoc
		{
			get { return this.sloc; }
		}
		public int SLocMath
		{
			get { return this.slocmath;}
		}
		public int MCDC
		{
			get { return this.mcdc;}
		}
		public int MaxNest
		{
			get { return this.maxnesting;}
		}
		public int CComplexity
		{
			get { return this.ccomplexity;}
		}

	}
}
