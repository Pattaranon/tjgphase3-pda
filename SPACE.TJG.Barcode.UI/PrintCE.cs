using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace PrintCENET
{
	public enum MeasureUnit
	{
		kPixels = 0,
		kInches,
		kMillimeters,
		kCentimeters,
		kPoints
	};

	public enum TextHorAlign
	{
		hLeft = 0,
		hRight,
		hCenter
	};

	public enum TextVertAlign
	{
		vTop = 0,
		vBottom,
		vCenter
	};

	public enum FillStyle
	{
		Transparent = 0,
		Opaque = 1
	};

	public enum ASCII_PORT
	{
        ASCII_NONE = -1,
		ASCII_IR = 0,
		ASCII_COM1 = 1,
		ASCII_COM2 = 2,
		ASCII_COM3 = 3,
		ASCII_COM4 = 4,
		ASCII_COM5 = 5,
		ASCII_COM6 = 6,
		ASCII_COM7 = 7,
		ASCII_COM8 = 8,
		ASCII_FILE = 9,
		ASCII_NET = 10,
		ASCII_COM9 = 11,
		ASCII_COM10 = 12,
		ASCII_COM11 = 13,
		ASCII_COM12 = 14,
        ASCII_BTBROAD = 15,
        ASCII_BTMSOFT = 16,
        ASCII_LPT1 = 17,
        ASCII_USB = 18
	};

	public struct PrintInfo
	{
		public int			printer;
		public int			port;
		public int			paper;
		public int			paper_width;
		public int			paper_height;
		public int			color_mode;
		public bool			portrait;
		public bool			draft_mode;
		public int			left_margin;
		public int			top_margin;
		public int			right_margin;
		public int			bottom_margin;
		public int			start_page;
		public int			end_page;
	}; 

	public class PrintCE_LoadLib_Exception : System.Exception
	{
		public override string Message
		{
			get
			{
				return "PrintCE.dll not found";
			}
		}											 
	}

	public class PrintCE_VersionLib_Exception : System.Exception
	{
		public override string Message
		{
			get
			{
				return "You need PrintCE.dll version 2.5 or higher";
			}
		}											 
	}
      

	/// <summary>
	/// Summary description for PrintCE.
	/// </summary>
	public class PrintCE
	{
		public PrintCE()
		{
			if(LoadLibrary("PrintCE.dll") == 0)
				throw(new PrintCE_LoadLib_Exception());

			if(prnGetVersion()/1000.0 < 2.5)
				throw(new PrintCE_VersionLib_Exception());
		}

		[StructLayoutAttribute(LayoutKind.Sequential)]
			private struct PRNINFO 
		{
			public int		printer;
			public int		port;
			public int		paper;
			public int		paper_size_x;
			public int		paper_size_y;
			public int		color_mode;
			public int		portrait;
			public int		draft_mode;
			public int		margin_left;
			public int		margin_top;
			public int		margin_right;
			public int		margin_bottom;
			public int		start_page;
			public int		end_page;
		}

		[ DllImport("CoreDll.dll", EntryPoint="LoadLibrary") ]
		private static extern int LoadLibrary(string lpFileName);

        [ DllImport("Coredll.dll", EntryPoint="DeleteObject")]
		private static extern bool DeleteObject(IntPtr hObject);

		[ DllImport("PrintCE.dll", EntryPoint="prnInit") ]
		private static extern void prnInit(string szLicenseKey);

		[ DllImport("PrintCE.dll", EntryPoint="prnUnInit") ]
		private static extern void prnUnInit();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetupDlg") ]
		private static extern bool prnSetupDlg(IntPtr parent);

		[ DllImport("PrintCE.dll", EntryPoint="prnSilentPrintSetup") ]
		private static extern bool prnSilentPrintSetup(IntPtr parent, ref PRNINFO info);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetCurrentSetup") ]
		private static extern void prnGetCurrentSetup(ref PRNINFO info);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetState") ]
		private static extern int prnGetState();

		[ DllImport("PrintCE.dll", EntryPoint="prnIsConnection") ]
		private static extern int prnIsConnection();

		[ DllImport("PrintCE.dll", EntryPoint="prnStartDoc") ]
		private static extern int prnStartDoc();

		[ DllImport("PrintCE.dll", EntryPoint="prnStartPage") ]
		private static extern int prnStartPage();

		[ DllImport("PrintCE.dll", EntryPoint="prnEndDoc") ]
		private static extern int prnEndDoc();

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawTextFlow") ]
		private static extern int NETprnDrawTextFlow(string lpString,ref double x, ref double y, ref double width, ref double height);

		[DllImport("PrintCE.dll", EntryPoint = "NETprnGetTextFlowHeight")]
		private static extern void NETprnGetTextFlowHeight(string lpString, ref double width, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawText") ]
		private static extern void NETprnDrawText(string lpString,ref double x, ref double y);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawText2") ]
		private static extern void	NETprnDrawText2(string lpString,ref double x, ref double y, int color);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawAlignedText") ]
		private static extern void	NETprnDrawAlignedText(string lpString,ref double x, ref double y, int hor_align, int vert_align);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawEllipse") ]
		private static extern void	NETprnDrawEllipse(ref double x1, ref double y1, ref double x2, ref double y2);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawCircle") ]
		private static extern void	NETprnDrawCircle(ref double x1, ref double y1, ref double radius);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawLine") ]
		private static extern void	NETprnDrawLine(ref double x1, ref double y1, ref double x2, ref double y2);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawRect") ]
		private static extern void	NETprnDrawRect(ref double x1, ref double y1, ref double x2, ref double y2);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawSolidRect") ]
		private static extern void	NETprnDrawSolidRect(ref double x1, ref double y1, ref double x2, ref double y2, int color);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawRoundRect") ]
		private static extern void	NETprnDrawRoundRect(ref double x1, ref double y1, ref double x2, ref double y2, ref double width, ref double height);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawPicture") ]
		private static extern bool NETprnDrawPicture(string path, ref double x, ref double y, ref double width, ref double height, bool keep_aspect);

        [DllImport("PrintCE.dll", EntryPoint = "NETprnDrawBitmap")]
        private static extern bool NETprnDrawBitmap(IntPtr hBitmap, ref double x, ref double y, ref double width, ref double height, bool keep_aspect);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetPictureSize") ]
		private static extern bool	prnGetPictureSize(string path, ref double width, ref double height);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnConvertValue") ]
		private static extern void NETprnConvertValue(ref double value, int from, int to, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetMeasureUnit") ]
		private static extern void	prnSetMeasureUnit(int val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetMeasureUnit") ]
		private static extern int prnGetMeasureUnit();

		[ DllImport("PrintCE.dll", EntryPoint="NETprnSetLineWidth") ]
		private static extern void NETprnSetLineWidth(ref double val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetLineWidth") ]
		private static extern void NETprnGetLineWidth(ref double width);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetLineColor") ]
		private static extern void prnSetLineColor(int val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetLineColor") ]
		private static extern int prnGetLineColor();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFillColor") ]
		private static extern void prnSetFillColor(int val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetFillColor") ]
		private static extern int prnGetFillColor();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFillStyle") ]
		private static extern void prnSetFillStyle(int val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetFillStyle") ]
		private static extern int prnGetFillStyle();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetTextColor") ]
		private static extern void prnSetTextColor(int val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetTextColor") ]
		private static extern int prnGetTextColor();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFontName") ]
		private static extern void prnSetFontName(string name);
		
		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetFontName") ]
		private static extern void NETprnGetFontName(ref char name);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnSetFontSize") ]
		private static extern void NETprnSetFontSize(ref double val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetFontSize") ]
		private static extern void NETprnGetFontSize(ref double size);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFontBold") ]
		private static extern void prnSetFontBold(bool val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetFontBold") ]
		private static extern bool prnGetFontBold();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFontItalic") ]
		private static extern void prnSetFontItalic(bool val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetFontItalic") ]
		private static extern bool prnGetFontItalic();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFontStrike") ]
		private static extern void prnSetFontStrike(bool val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetFontStrike") ]
		private static extern bool prnGetFontStrike();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetFontUnderline") ]
		private static extern void prnSetFontUnderline(bool val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetFontUnderline") ]
		private static extern bool prnGetFontUnderline();

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetTextHeight") ]
		private static extern void NETprnGetTextHeight(string str, ref double height);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetTextWidth") ]
		private static extern void NETprnGetTextWidth(string str,ref double width);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetPageHeight") ]
		private static extern void NETprnGetPageHeight(ref double height);
		
		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetPageWidth") ]
		private static extern void NETprnGetPageWidth(ref double width);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetLeftMargin") ]
		private static extern void NETprnGetLeftMargin(ref double margin);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnSetLeftMargin") ]
		private static extern void NETprnSetLeftMargin(ref double val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetRightMargin") ]
		private static extern void NETprnGetRightMargin(ref double margin);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnSetRightMargin") ]
		private static extern void NETprnSetRightMargin(ref double val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetTopMargin") ]
		private static extern void NETprnGetTopMargin(ref double margin);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnSetTopMargin") ]
		private static extern void NETprnSetTopMargin(ref double val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnGetBottomMargin") ]
		private static extern void NETprnGetBottomMargin(ref double margin);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnSetBottomMargin") ]
		private static extern void NETprnSetBottomMargin(ref double val);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetVersion") ]
		private static extern int prnGetVersion();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetSilentMode") ]
		private static extern void prnSetSilentMode(bool silent);

		[ DllImport("PrintCE.dll", EntryPoint="prnGetSentBytes") ]
		private static extern int prnGetSentBytes();

		[ DllImport("PrintCE.dll", EntryPoint="prnSetTransparentTextBgr") ]
		private static extern void prnSetTransparentTextBgr(bool val);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetTextHorAlign") ]
		private static extern void prnSetTextHorAlign(int hor_align);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetTextVertAlign") ]
		private static extern void prnSetTextVertAlign(int vert_align);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetPDFFile") ]
		private static extern void prnSetPDFFile(string pdf_file);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetLanguage") ]
		private static extern void prnSetLanguage(int lang_id);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetPrnParam") ]
		private static extern void prnSetPrnParam(int param, int val);

        [DllImport("PrintCE.dll", EntryPoint = "prnSetFontAngle")]
        private static extern void prnSetFontAngle(int val);

        [DllImport("PrintCE.dll", EntryPoint = "prnGetFontAngle")]
        private static extern int prnGetFontAngle();

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawCodaBar") ]
		private static extern void NETprnDrawCodaBar(string str,ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawPostnet") ]
		private static extern void NETprnDrawPostnet(string str,ref double pos_x, ref double pos_y, bool add_text, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawCode39") ]
		private static extern void NETprnDrawCode39(string str,ref double pos_x, ref double pos_y, bool checksum, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawCode93") ]
		private static extern void NETprnDrawCode93(string str,ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawEAN13") ]
		private static extern void NETprnDrawEAN13(string str, string add_str, ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);
		
		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawEAN8") ]
		private static extern void NETprnDrawEAN8(string str, string add_str, ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);
		
		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawUPCA") ]
		private static extern void NETprnDrawUPCA(string str, string add_str, ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawUPCE") ]
		private static extern void NETprnDrawUPCE(string str, string add_str, ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDraw2OF5") ]
		private static extern void NETprnDraw2OF5(string str,ref double pos_x, ref double pos_y, bool checksum, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawCode128") ]
		private static extern void NETprnDrawCode128(string str,ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawUCC128") ]
		private static extern void NETprnDrawUCC128(string str,ref double pos_x, ref double pos_y, bool add_text, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawMSI") ]
		private static extern void NETprnDrawMSI(string str,ref double pos_x, ref double pos_y, bool add_text, int checksum_type, int thickness, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETprnDrawPDF417") ]
		private static extern void NETprnDrawPDF417(string str,ref double pos_x, ref double pos_y, int columns, int rows, bool trun_symbol, bool add_text, ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETSetBarCodeHeight") ]
		private static extern void NETSetBarCodeHeight(ref double height);

        [DllImport("PrintCE.dll", EntryPoint = "NETGetBarCodeHeight")]
        private static extern void NETGetBarCodeHeight(ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="NETSetBarCodeScale") ]
		private static extern void NETSetBarCodeScale(ref double scale);

        [DllImport("PrintCE.dll", EntryPoint = "NETGetBarCodeScale")]
        private static extern void NETGetBarCodeScale(ref double ret_val);

		[ DllImport("PrintCE.dll", EntryPoint="prnSetBarCodeAngle") ]
		private static extern void prnSetBarCodeAngle(int angle);

		public MeasureUnit MeasureUnit
		{
			get 
			{ 
				return (MeasureUnit) prnGetMeasureUnit();
			}
			set
			{
				prnSetMeasureUnit((int) value);
			}
		}

		public double LineWidth
		{
			get 
			{ 
				double width = 0.0;
				NETprnGetLineWidth(ref width);
				return width;
			}
			set
			{
				NETprnSetLineWidth(ref value);
			}
		}

		public Color LineColor
		{
			get 
			{ 
				return Color.FromArgb(prnGetLineColor());
			}
			set
			{
				prnSetLineColor(ColorToBGR(value));
			}
		}

		public FillStyle FillStyle
		{
			get 
			{ 
				return (FillStyle) prnGetFillStyle();
			}
			set
			{
				prnSetFillStyle((int) value);
			}
		}

		public Color FillColor
		{
			get 
			{ 
				return Color.FromArgb(prnGetFillColor());
			}
			set
			{
				prnSetFillColor(ColorToBGR(value));
			}
		}

		public Color TextColor
		{
			get 
			{ 
				return Color.FromArgb(prnGetTextColor());
			}
			set
			{
				prnSetTextColor(ColorToBGR(value));
			}
		}

		public string FontName
		{
			get 
			{ 
				char [] str = new char [100];
				NETprnGetFontName(ref str[0]);
				return new string(str);
			}
			set
			{
				prnSetFontName(value);
			}
		}

		public double FontSize
		{
			get 
			{ 
				double size = 0.0;
				NETprnGetFontSize(ref size);
				return size;
			}
			set
			{
				NETprnSetFontSize(ref value);
			}
		}
		
		public bool FontBold
		{
			get 
			{ 
				return prnGetFontBold();
			}
			set
			{
				prnSetFontBold(value);
			}
		}

		public bool FontItalic
		{
			get 
			{ 
				return prnGetFontItalic();
			}
			set
			{
				prnSetFontItalic(value);
			}
		}

		public bool FontStrike
		{
			get 
			{ 
				return prnGetFontStrike();
			}
			set
			{
				prnSetFontStrike(value);
			}
		}

		public bool FontUnderline
		{
			get 
			{ 
				return prnGetFontUnderline();
			}
			set
			{
				prnSetFontUnderline(value);
			}
		}

        public int FontAngle
        {
            get
            {
                return prnGetFontAngle();
            }
            set
            {
                prnSetFontAngle(value);
            }
        }

		public double PageHeight
		{
			get 
			{ 
				double height = 0.0;

				NETprnGetPageHeight(ref height);

				return height;
			}
		}

		public double PageWidth
		{
			get 
			{ 
				double width = 0.0;

				NETprnGetPageWidth(ref width);

				return width;
			}
		}

		public double LeftMargin
		{
			get 
			{ 
				double margin = 0.0;

				NETprnGetLeftMargin(ref margin);

				return margin;
			}
			set
			{
				NETprnSetLeftMargin(ref value);
			}
		}

		public double RightMargin
		{
			get 
			{ 
				double margin = 0.0;

				NETprnGetRightMargin(ref margin);

				return margin;
			}
			set
			{
				NETprnSetRightMargin(ref value);
			}
		}

		public double TopMargin
		{
			get 
			{ 
				double margin = 0.0;

				NETprnGetTopMargin(ref margin);

				return margin;
			}
			set
			{
				NETprnSetTopMargin(ref value);
			}
		}

		public double BottomMargin
		{
			get 
			{ 
				double margin = 0.0;

				NETprnGetBottomMargin(ref margin);

				return margin;
			}
			set
			{
				NETprnSetBottomMargin(ref value);
			}
		}

		public int Connection
		{
			get 
			{ 
				return prnIsConnection();
			}
		}

		public int Version
		{
			get 
			{ 
				return prnGetVersion();
			}
		}

		public bool SilentMode
		{
			set 
			{ 
				prnSetSilentMode(value);
			}
		}

		public bool TransparentTextBgr
		{
			set 
			{ 
				prnSetTransparentTextBgr(value);
			}
		}

		public TextHorAlign TextHorAlign
		{
			set 
			{ 
				prnSetTextHorAlign((int) value);
			}
		}

		public TextVertAlign TextVertAlign
		{
			set 
			{ 
				prnSetTextVertAlign((int) value);
			}
		}

		public string PDFFile
		{
			set 
			{ 
				prnSetPDFFile(value);
			}
		}

		public int Language
		{
			set 
			{ 
				prnSetLanguage(value);
			}
		}

		public int SentBytes
		{
			get 
			{ 
				return prnGetSentBytes();
			}
		}

		public int State
		{
			get 
			{ 
				return prnGetState();
			}
		}

		public void Init(string szLicenseKey)
		{
			prnInit(szLicenseKey);
		}

		public void UnInit()
		{
			prnUnInit();
		}

		public bool	SetupDlg(IntPtr parent)
		{
			return prnSetupDlg(parent);
		}

		public int StartDoc()
		{
			return prnStartDoc();
		}

		public int StartPage()
		{
			return prnStartPage();
		}

		public int EndDoc()
		{
			return prnEndDoc();
		}

		public void DrawText(string lpString,double x, double y)
		{
			NETprnDrawText(lpString,ref x,ref y);
		}

		public void DrawText2(string lpString,double x, double y, Color color)
		{
			NETprnDrawText2(lpString,ref x,ref y,ColorToBGR(color));
		}
		
		public int DrawTextFlow(string lpString, double x, double y, double width, double height)
		{
			return NETprnDrawTextFlow(lpString,ref x,ref y,ref width,ref height);
		}

		public double GetTextFlowHeight(string lpString, double width)
		{
			double ret_val = 0.0;

			NETprnGetTextFlowHeight(lpString, ref width, ref ret_val);

			return ret_val;
		}

		public void DrawAlignedText(string lpString, double x, double y, TextHorAlign hor_align, TextVertAlign vert_align)
		{
			NETprnDrawAlignedText(lpString, ref x, ref y, (int) hor_align, (int) vert_align);
		}

		public void DrawEllipse(double x1, double y1, double x2, double y2)
		{
			NETprnDrawEllipse(ref x1, ref y1, ref x2, ref y2);
		}

		public void DrawCircle(double x1, double y1, double radius)
		{
			NETprnDrawCircle(ref x1, ref y1, ref radius);
		}

		public void DrawLine(double x1, double y1, double x2, double y2)
		{
			NETprnDrawLine(ref x1, ref y1, ref x2, ref y2);
		}

		public void DrawRect(double x1, double y1, double x2, double y2)
		{
			NETprnDrawRect(ref x1, ref y1, ref x2, ref y2);
		}

		public void DrawSolidRect(double x1, double y1, double x2, double y2, Color color)
		{
			NETprnDrawSolidRect(ref x1, ref y1, ref x2, ref y2, ColorToBGR(color));
		}

		public void DrawRoundRect(double x1, double y1, double x2, double y2, double width, double height)
		{
			NETprnDrawRoundRect(ref x1, ref y1, ref x2, ref y2, ref width, ref height);
		}

		public bool DrawPicture(string path, double x, double y, double width, double height, bool keep_aspect)
		{
			return NETprnDrawPicture(path, ref x, ref y, ref width, ref height, keep_aspect);
		}

        public bool DrawBitmap(Bitmap bitmap, double x, double y, double width, double height, bool keep_aspect)
        {
            IntPtr hBmp = bitmap.GetHbitmap();

            bool ret = NETprnDrawBitmap(hBmp, ref x, ref y, ref width, ref height, keep_aspect);

            DeleteObject(hBmp);

            return ret;
        }

		public bool	GetPictureSize(string path, out double width, out double height)
		{
			width = 0.0;
			height = 0.0;

			return prnGetPictureSize(path, ref width, ref height);
		}

		public double ConvertValue(double val, MeasureUnit from, MeasureUnit to)
		{
			double ret_val = 0.0;

			NETprnConvertValue(ref val, (int) from, (int) to, ref ret_val);

			return ret_val;
		}

		public double GetTextHeight(string str)
		{
			double height = 0.0;

			NETprnGetTextHeight(str,ref height);

			return height;
		}

		public double GetTextWidth(string str)
		{
			double width = 0.0;

			NETprnGetTextWidth(str, ref width);

			return width;
		}

		public bool SilentPrintSetup(IntPtr parent, PrintInfo info)
		{
			PRNINFO prn_info = new PRNINFO();

			prn_info.printer = info.printer;
			prn_info.port = info.port;
			prn_info.paper = info.paper;
			prn_info.paper_size_x = info.paper_width;
			prn_info.paper_size_y = info.paper_height;
			prn_info.color_mode = info.color_mode;
			prn_info.portrait = info.portrait ? 1 : 0;
			prn_info.draft_mode = info.draft_mode ? 1 : 0;
			prn_info.margin_left = info.left_margin;
			prn_info.margin_right = info.right_margin;
			prn_info.margin_top = info.top_margin;
			prn_info.margin_bottom = info.bottom_margin;
			prn_info.start_page = info.start_page;
			prn_info.end_page = info.end_page;

			return prnSilentPrintSetup(parent, ref prn_info);
		}

		public void GetCurrentSetup(out PrintInfo info)
		{
			info = new PrintInfo();

			PRNINFO prn_info = new PRNINFO();
			
			prnGetCurrentSetup(ref prn_info);

			info.printer = prn_info.printer;
			info.port = prn_info.port;
			info.paper = prn_info.paper;
			info.paper_width = prn_info.paper_size_x;
			info.paper_height = prn_info.paper_size_y;
			info.color_mode = prn_info.color_mode;
			info.portrait = prn_info.portrait != 0 ? true : false;
			info.draft_mode = prn_info.draft_mode != 0 ?  true : false;
			info.left_margin = prn_info.margin_left;
			info.right_margin = prn_info.margin_right;
			info.top_margin = prn_info.margin_top;
			info.bottom_margin = prn_info.margin_bottom;
			info.start_page = prn_info.start_page;
			info.end_page = prn_info.end_page;
		}

		public void SetPrnParam(int param, int val)
		{
			prnSetPrnParam(param, val);
		}

		public double DrawCodaBar(string str,double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawCodaBar(str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);

			return ret_val;
		}

		public double DrawPostnet(string str,double pos_x, double pos_y, bool add_text)
		{
			double ret_val = 0.0;

			NETprnDrawPostnet(str, ref pos_x, ref pos_y, add_text, ref ret_val);

			return ret_val;
		}

		public double DrawCode39(string str,double pos_x, double pos_y, bool checksum, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawCode39(str, ref pos_x, ref pos_y, checksum, add_text, thickness, ref ret_val);
			
			return ret_val;
		}

		public double DrawCode93(string str,double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawCode93(str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);
			
			return ret_val;
		}

		public double DrawEAN13(string str, string add_str, double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawEAN13(str, add_str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);
			
			return ret_val;
		}
		
		public double DrawEAN8(string str, string add_str, double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawEAN8(str, add_str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);
			
			return ret_val;
		}
		
		public double DrawUPCA(string str, string add_str, double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawUPCA(str, add_str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);

			return ret_val;
		}

		public double DrawUPCE(string str, string add_str, double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawUPCE(str, add_str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);
			
			return ret_val;
		}

		public double Draw2OF5(string str,double pos_x, double pos_y, bool checksum, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDraw2OF5(str, ref pos_x, ref pos_y, checksum, add_text, thickness, ref ret_val);
			
			return ret_val;
		}

		public double DrawCode128(string str,double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawCode128(str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);
			
			return ret_val;
		}

		public double DrawUCC128(string str,double pos_x, double pos_y, bool add_text, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawUCC128(str, ref pos_x, ref pos_y, add_text, thickness, ref ret_val);
			
			return ret_val;
		}

		public double DrawMSI(string str,double pos_x, double pos_y, bool add_text, int checksum_type, int thickness)
		{
			double ret_val = 0.0;

			NETprnDrawMSI(str, ref pos_x, ref pos_y, add_text, checksum_type, thickness, ref ret_val);
			
			return ret_val;
		}

		public double DrawPDF417(string str, double pos_x, double pos_y, int columns, int rows, bool trun_symbol, bool add_text)
		{
			double ret_val = 0.0;

			NETprnDrawPDF417(str, ref pos_x, ref pos_y, columns, rows, trun_symbol, add_text, ref ret_val);
			
			return ret_val;
		}

		public double BarcodeHeight
		{
			set 
			{ 
				NETSetBarCodeHeight(ref value);
			}
            get
            {
                double ret_val = 0.0;
                NETGetBarCodeHeight(ref ret_val);
                return ret_val;
            }
		}

		public double BarcodeScale
		{
			set 
			{ 
				NETSetBarCodeScale(ref value);
			}
            get
            {
                double ret_val = 0.0;
                NETGetBarCodeScale(ref ret_val);
                return ret_val;
            }
		}

		public int BarcodeAngle
		{
			set 
			{ 
				prnSetBarCodeAngle(value);
			}
		}

		private int ColorToBGR(Color color)
		{
			return (((int)color.B) << 16) | (((int)color.G) << 8) | color.R;
		}
	}

	/// <summary>
	/// Summary description for PrintCE.
	/// </summary>
	public class PrintASCII
	{
		[ DllImport("CoreDll.dll", EntryPoint="LoadLibrary") ]
		private static extern int LoadLibrary(string lpFileName);
		
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_Init") ]
		private static extern void ASCII_Init(string szLicenseKey);

		[ DllImport("PrintCE.dll", EntryPoint="ASCII_UnInit") ]
		private static extern void ASCII_UnInit();

		[ DllImport("PrintCE.dll", EntryPoint="ASCII_SendChar") ]
		private static extern int ASCII_SendChar(byte ch);
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_SendString") ]
		private static extern int ASCII_SendString(string str);
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_SendArray") ]
		private static extern int ASCII_SendArray(byte[] array, int size);
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_Connect") ]
		private static extern int ASCII_Connect(int port, string param);

        [DllImport("PrintCE.dll", EntryPoint = "ASCII_ConnectSilent")]
        private static extern int ASCII_ConnectSilent(int port, string port_param);
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_Disconnect") ]
		private static extern void ASCII_Disconnect();
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_GetState") ]
		private static extern int ASCII_GetState();
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_GetSentBytes") ]
		private static extern int ASCII_GetSentBytes();
	
		[ DllImport("PrintCE.dll", EntryPoint="ASCII_GetVersion") ]
		private static extern int ASCII_GetVersion();

        [DllImport("PrintCE.dll", EntryPoint = "ASCII_SetComPortParam")]
        private static extern void ASCII_SetComPortParam(int BaudRate, int fParity, int Parity, int StopBits, int ByteSize, int fOutX, int fInX, int fOutxCtsFlow, int fOutxDsrFlow, int fDtrControl, int fRtsControl, int fDsrSensitivity, int fTXContinueOnXoff);

        [ DllImport("PrintCE.dll", EntryPoint="ASCII_SetLanguage") ]
		private static extern void ASCII_SetLanguage(int lang_id);

        [DllImport("PrintCE.dll", EntryPoint = "ASCII_SetupPort")]
        private static extern int ASCII_SetupPort(IntPtr parent);

		public PrintASCII()
		{
			if(LoadLibrary("PrintCE.dll") == 0)
				throw(new PrintCE_LoadLib_Exception());

			if(ASCII_GetVersion()/1000.0 < 2.5)
				throw(new PrintCE_VersionLib_Exception());
		}

		public int State
		{
			get 
			{ 
				return ASCII_GetState();
			}
		}

		public int SentBytes
		{
			get 
			{ 
				return ASCII_GetSentBytes();
			}
		}

		public int Version
		{
			get 
			{ 
				return ASCII_GetVersion();
			}
		}

		public int Language
		{
			set 
			{ 
				ASCII_SetLanguage(value);
			}
		}

		public void Init(string szLicenseKey)
		{
			ASCII_Init(szLicenseKey);
		}

		public void UnInit()
		{
			ASCII_UnInit();
		}
	
		public bool SendByte(byte val)
		{
			return ASCII_SendChar(val) != 0;
		}
	
		public bool SendString(string str) 
		{
			return ASCII_SendString(str) != 0;
		}
	
		public bool SendBytes(byte [] bytes, int count) 
		{
			return ASCII_SendArray(bytes, count) != 0;
		}
	
	
		public bool Connect(ASCII_PORT port,string param) 
		{
			return ASCII_Connect((int)port, param) != 0;
		}

        public bool ConnectSilent(ASCII_PORT port,string param) 
		{
			return ASCII_ConnectSilent((int)port, param) != 0;
		}
	
		public void Disconnect()
		{
			ASCII_Disconnect();
		}

        public void SetComPortParam(int BaudRate, int fParity, int Parity, int StopBits, int ByteSize, int fOutX, int fInX, int fOutxCtsFlow, int fOutxDsrFlow, int fDtrControl, int fRtsControl, int fDsrSensitivity, int fTXContinueOnXoff)
        {
            ASCII_SetComPortParam(BaudRate, fParity, Parity, StopBits, ByteSize, fOutX, fInX, fOutxCtsFlow, fOutxDsrFlow, fDtrControl, fRtsControl, fDsrSensitivity, fTXContinueOnXoff);
        }

        public int SetupPort(IntPtr parent)
        {
            return ASCII_SetupPort(parent);
        }

	}
}
