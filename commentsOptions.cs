using System;

namespace Metrics
{
	/// <summary>
	/// Summary description for Options.
	/// </summary>
	public class commentsOptions
	{
		bool countBlankLines;
		bool countCommentLines;
		string singleLineCommentStart;
		string multiLineCommentStart;
		string multiLineCommentEnd;

		public commentsOptions()
		{
			countBlankLines = true;
			countCommentLines = true;
			singleLineCommentStart = "//";
			multiLineCommentStart = "/*";
			multiLineCommentEnd = "*/";
		}

		/// <summary>
		/// if true, blank lines will be included in count
		/// Default is true
		/// </summary>
		public bool CountBlankLines
		{
			get { return this.countBlankLines; }
			set { this.countBlankLines = value; }
		}

		/// <summary>
		/// if true, comment lines will be included in count
		/// Default is true
		/// </summary>
		public bool CountCommentLines
		{
			get { return this.countCommentLines; }
			set { this.countCommentLines = value; }
		}

		/// <summary>
		/// this is the starting character sequence for multline comments
		/// Default is /* (C#, C, C++, Java) comment
		/// </summary>
		public string MultiLineCommentEnd
		{
			get { return this.multiLineCommentEnd; }
			set { this.multiLineCommentEnd = value; }
		}

		/// <summary>
		/// this is the ending character sequence for multiline comments
		/// Default is */ (C#, C, C++, Java) comment
		/// </summary>
		public string MultiLineCommentStart
		{
			get { return this.multiLineCommentStart; }
			set { this.multiLineCommentStart = value; }
		}

		/// <summary>
		/// this is the character sequence that starts single line comments
		/// </summary>
		public string SingleLineCommentStart
		{
			get { return this.singleLineCommentStart; }
			set { this.singleLineCommentStart = value; }
		}
	}
}
