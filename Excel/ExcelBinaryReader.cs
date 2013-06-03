using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel.Core.BinaryFormat;

namespace Excel
{
	/// <summary>
	/// ExcelDataReader Class
	/// </summary>
	public class ExcelBinaryReader : IExcelDataReader
	{
		#region Members

		private Stream m_file;
		private XlsHeader m_hdr;
		private List<XlsWorksheet> m_sheets;
		private XlsBiffStream m_stream;
		private DataSet m_workbookData;
		private XlsWorkbookGlobals m_globals;
		private ushort m_version;
		private bool m_ConvertOADate;
		private Encoding m_encoding;
		private bool m_isValid;
		private bool m_isClosed;
		private readonly Encoding m_Default_Encoding = Encoding.UTF8;
		private string m_exceptionMessage;
		private object[] m_cellsValues;
		private uint[] m_dbCellAddrs;
		private int m_dbCellAddrsIndex;
		private bool m_canRead;
		private int m_SheetIndex;
		private int m_depht;
		private int m_cellOffset;
		private int m_maxCol;
		private int m_maxRow;

		private bool m_IsFirstRead;
		private bool _isFirstRowAsColumnNames;

		private const string WORKBOOK = "Workbook";
		private const string BOOK = "Book";
		private const string COLUMN = "Column";

		private bool disposed;

		#endregion

		internal ExcelBinaryReader()
		{
			m_encoding = m_Default_Encoding;
			m_version = 0x0600;
			m_isValid = true;
			m_SheetIndex = -1;
			m_IsFirstRead = true;
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				if (disposing)
				{
					if (m_workbookData != null) m_workbookData.Dispose();

					if (m_sheets != null) m_sheets.Clear();
				}

				m_workbookData = null;
				m_sheets = null;
				m_stream = null;
				m_globals = null;
				m_encoding = null;
				m_hdr = null;

				disposed = true;
			}
		}

		~ExcelBinaryReader()
		{
			Dispose(false);
		}

		#endregion

		#region Private methods

		private int findFirstDataCellOffset(int startOffset)
		{
			XlsBiffDbCell startCell = (XlsBiffDbCell)m_stream.ReadAt(startOffset);
			XlsBiffRow row = null;

			int offs = startCell.RowAddress;

			do
			{
				row = m_stream.ReadAt(offs) as XlsBiffRow;
				if (row == null) break;

				offs += row.Size;

			} while (null != row);

			return offs;
		}

		private void readWorkBookGlobals()
		{
			//Read Header
			try
			{
				m_hdr = XlsHeader.ReadHeader(m_file);
			}
			catch (Exceptions.HeaderException ex)
			{
				fail(ex.Message);
				return;
			}
			catch (FormatException ex)
			{
				fail(ex.Message);
				return;
			}

			XlsRootDirectory dir = new XlsRootDirectory(m_hdr);
			XlsDirectoryEntry workbookEntry = dir.FindEntry(WORKBOOK) ?? dir.FindEntry(BOOK);

			if (workbookEntry == null)
			{ fail(Errors.ErrorStreamWorkbookNotFound); return; }

			if (workbookEntry.EntryType != STGTY.STGTY_STREAM)
			{ fail(Errors.ErrorWorkbookIsNotStream); return; }

			m_stream = new XlsBiffStream(m_hdr, workbookEntry.StreamFirstSector);

			m_globals = new XlsWorkbookGlobals();

			m_stream.Seek(0, SeekOrigin.Begin);

			XlsBiffRecord rec = m_stream.Read();
			XlsBiffBOF bof = rec as XlsBiffBOF;

			if (bof == null || bof.Type != BIFFTYPE.WorkbookGlobals)
			{ fail(Errors.ErrorWorkbookGlobalsInvalidData); return; }

			bool sst = false;

			m_version = bof.Version;
			m_sheets = new List<XlsWorksheet>();

			while (null != (rec = m_stream.Read()))
			{
				switch (rec.ID)
				{
					case BIFFRECORDTYPE.INTERFACEHDR:
						m_globals.InterfaceHdr = (XlsBiffInterfaceHdr)rec;
						break;
					case BIFFRECORDTYPE.BOUNDSHEET:
						XlsBiffBoundSheet sheet = (XlsBiffBoundSheet)rec;

						if (sheet.Type != XlsBiffBoundSheet.SheetType.Worksheet) break;

						sheet.IsV8 = isV8();
						sheet.UseEncoding = m_encoding;

						m_sheets.Add(new XlsWorksheet(m_globals.Sheets.Count, sheet));
						m_globals.Sheets.Add(sheet);

						break;
					case BIFFRECORDTYPE.MMS:
						m_globals.MMS = rec;
						break;
					case BIFFRECORDTYPE.COUNTRY:
						m_globals.Country = rec;
						break;
					case BIFFRECORDTYPE.CODEPAGE:

						m_globals.CodePage = (XlsBiffSimpleValueRecord)rec;

						try
						{
							m_encoding = Encoding.GetEncoding(m_globals.CodePage.Value);
						}
						catch (ArgumentException)
						{
							// Warning - Password protection
							// TODO: Attach to ILog
						}

						break;
					case BIFFRECORDTYPE.FONT:
					case BIFFRECORDTYPE.FONT_V34:
						m_globals.Fonts.Add(rec);
						break;
					case BIFFRECORDTYPE.FORMAT:
					case BIFFRECORDTYPE.FORMAT_V23:
						m_globals.Formats.Add(rec);
						break;
					case BIFFRECORDTYPE.XF:
					case BIFFRECORDTYPE.XF_V4:
					case BIFFRECORDTYPE.XF_V3:
					case BIFFRECORDTYPE.XF_V2:
						m_globals.ExtendedFormats.Add(rec);
						break;
					case BIFFRECORDTYPE.SST:
						m_globals.SST = (XlsBiffSST)rec;
						sst = true;
						break;
					case BIFFRECORDTYPE.CONTINUE:
						if (!sst) break;
						XlsBiffContinue contSST = (XlsBiffContinue)rec;
						m_globals.SST.Append(contSST);
						break;
					case BIFFRECORDTYPE.EXTSST:
						m_globals.ExtSST = rec;
						sst = false;
						break;
					case BIFFRECORDTYPE.PROTECT:
					case BIFFRECORDTYPE.PASSWORD:
					case BIFFRECORDTYPE.PROT4REVPASSWORD:
						//IsProtected
						break;
					case BIFFRECORDTYPE.EOF:
						if (m_globals.SST != null)
							m_globals.SST.ReadStrings();
						return;

					default:
						continue;
				}
			}
		}

		private bool readWorkSheetGlobals(XlsWorksheet sheet, out XlsBiffIndex idx)
		{
			idx = null;
			
			m_stream.Seek((int)sheet.DataOffset, SeekOrigin.Begin);

			XlsBiffBOF bof = m_stream.Read() as XlsBiffBOF;
			if (bof == null || bof.Type != BIFFTYPE.Worksheet) return false;

			XlsBiffRecord rec = m_stream.Read();
			if (rec == null) return false;
			if (rec is XlsBiffIndex)
			{
				idx = rec as XlsBiffIndex;
			}
			else if (rec is XlsBiffUncalced)
			{
				// Sometimes this come before the index...
				idx = m_stream.Read() as XlsBiffIndex;
			}

			if (null == idx)
			{
				// There is a record before the index! Chech his type and see the MS Biff Documentation
				return false;
			}

			idx.IsV8 = isV8();

			XlsBiffRecord trec;
			XlsBiffDimensions dims = null;

			do
			{
				trec = m_stream.Read();
				if (trec.ID == BIFFRECORDTYPE.DIMENSIONS)
				{
					dims = (XlsBiffDimensions)trec;
					break;
				}

			} while (trec != null && trec.ID != BIFFRECORDTYPE.ROW);

			m_maxCol = 256;

			if (dims != null)
			{
				dims.IsV8 = isV8();
				m_maxCol = dims.LastColumn - 1;
				sheet.Dimensions = dims;
			}

			m_maxRow = (int)idx.LastExistingRow;

			if (idx.LastExistingRow <= idx.FirstExistingRow)
			{
				return false;
			}

			m_depht = 0;

			return true;
		}

		private bool readWorkSheetRow()
		{
			m_cellsValues = new object[m_maxCol];

			while (m_cellOffset < m_stream.Size)
			{
				XlsBiffRecord rec = m_stream.ReadAt(m_cellOffset);
				m_cellOffset += rec.Size;

				if ((rec is XlsBiffDbCell)) { break; };//break;
				if (rec is XlsBiffEOF) { return false; };

				XlsBiffBlankCell cell = rec as XlsBiffBlankCell;

				if ((null == cell) || (cell.ColumnIndex >= m_maxCol)) continue;
				if (cell.RowIndex != m_depht) { m_cellOffset -= rec.Size; break; };

				pushCellValue(cell);
			}

			m_depht++;

			return m_depht < m_maxRow;
		}

		private DataTable readWholeWorkSheet(XlsWorksheet sheet)
		{
			XlsBiffIndex idx;

			if (!readWorkSheetGlobals(sheet, out idx)) return null;

			DataTable table = new DataTable(sheet.Name);

			bool triggerCreateColumns = true;

			m_dbCellAddrs = idx.DbCellAddresses;

			for (int index = 0; index < m_dbCellAddrs.Length; index++)
			{
				if (m_depht == m_maxRow) break;

				// init reading data
				m_cellOffset = findFirstDataCellOffset((int)m_dbCellAddrs[index]);

				//DataTable columns
				if (triggerCreateColumns)
				{
					if (_isFirstRowAsColumnNames && readWorkSheetRow() || (_isFirstRowAsColumnNames && m_maxRow == 1))
					{
						for (int i = 0; i < m_maxCol; i++)
						{
							if (m_cellsValues[i] != null && m_cellsValues[i].ToString().Length > 0)
								table.Columns.Add(m_cellsValues[i].ToString());
							else
								table.Columns.Add(string.Concat(COLUMN, i));
						}
					}
					else
					{
						for (int i = 0; i < m_maxCol; i++)
						{
							table.Columns.Add();
						}
					}

					triggerCreateColumns = false;

					table.BeginLoadData();
				}

				while (readWorkSheetRow())
				{
					table.Rows.Add(m_cellsValues);
				}

				if (m_depht > 0 && !(_isFirstRowAsColumnNames && m_maxRow == 1))
					table.Rows.Add(m_cellsValues);
			}

			table.EndLoadData();
			return table;
		}

		private void pushCellValue(XlsBiffBlankCell cell)
		{
			double _dValue;

			switch (cell.ID)
			{
				case BIFFRECORDTYPE.INTEGER:
				case BIFFRECORDTYPE.INTEGER_OLD:
					m_cellsValues[cell.ColumnIndex] = ((XlsBiffIntegerCell)cell).Value;
					break;
				case BIFFRECORDTYPE.NUMBER:
				case BIFFRECORDTYPE.NUMBER_OLD:

					_dValue = ((XlsBiffNumberCell)cell).Value;

					m_cellsValues[cell.ColumnIndex] = !m_ConvertOADate ?
						_dValue : tryConvertOADateTime(_dValue, cell.XFormat);

					break;
				case BIFFRECORDTYPE.LABEL:
				case BIFFRECORDTYPE.LABEL_OLD:
				case BIFFRECORDTYPE.RSTRING:
					m_cellsValues[cell.ColumnIndex] = ((XlsBiffLabelCell)cell).Value;
					break;
				case BIFFRECORDTYPE.LABELSST:
					string tmp = m_globals.SST.GetString(((XlsBiffLabelSSTCell)cell).SSTIndex);
					m_cellsValues[cell.ColumnIndex] = tmp;
					break;
				case BIFFRECORDTYPE.RK:

					_dValue = ((XlsBiffRKCell)cell).Value;

					m_cellsValues[cell.ColumnIndex] = !m_ConvertOADate ?
						_dValue : tryConvertOADateTime(_dValue, cell.XFormat);

					break;
				case BIFFRECORDTYPE.MULRK:

					XlsBiffMulRKCell _rkCell = (XlsBiffMulRKCell)cell;
					for (ushort j = cell.ColumnIndex; j <= _rkCell.LastColumnIndex; j++)
					{
						m_cellsValues[j] = _rkCell.GetValue(j);
					}

					break;
				case BIFFRECORDTYPE.BLANK:
				case BIFFRECORDTYPE.BLANK_OLD:
				case BIFFRECORDTYPE.MULBLANK:
					// Skip blank cells

					break;
				case BIFFRECORDTYPE.FORMULA:
				case BIFFRECORDTYPE.FORMULA_OLD:

					object _oValue = ((XlsBiffFormulaCell)cell).Value;

					if (null != _oValue && _oValue is FORMULAERROR)
					{
						_oValue = null;
					}
					else
					{
						m_cellsValues[cell.ColumnIndex] = !m_ConvertOADate ?
							_oValue : tryConvertOADateTime(_oValue, (ushort)(cell.XFormat + 75));//date time offset
					}

					break;
				default:
					break;
			}
		}

		private bool moveToNextRecord()
		{
			if (null == m_dbCellAddrs ||
				m_dbCellAddrsIndex == m_dbCellAddrs.Length ||
				m_depht == m_maxRow) return false;

			m_canRead = readWorkSheetRow();

			//read last row
			if (!m_canRead && m_depht > 0) m_canRead = true;

			if (!m_canRead && m_dbCellAddrsIndex < (m_dbCellAddrs.Length - 1))
			{
				m_dbCellAddrsIndex++;
				m_cellOffset = findFirstDataCellOffset((int)m_dbCellAddrs[m_dbCellAddrsIndex]);

				m_canRead = readWorkSheetRow();
			}

			return m_canRead;
		}

		private void initializeSheetRead()
		{
			if (m_SheetIndex == ResultsCount) return;

			m_dbCellAddrs = null;

			m_IsFirstRead = false;

			if (m_SheetIndex == -1) m_SheetIndex = 0;

			XlsBiffIndex idx;

			if (!readWorkSheetGlobals(m_sheets[m_SheetIndex], out idx))
			{
				//read next sheet
				m_SheetIndex++;
				initializeSheetRead();
				return;
			};

			m_dbCellAddrs = idx.DbCellAddresses;
			m_dbCellAddrsIndex = 0;
			m_cellOffset = findFirstDataCellOffset((int)m_dbCellAddrs[m_dbCellAddrsIndex]);
		}


		private void fail(string message)
		{
			m_exceptionMessage = message;
			m_isValid = false;

			m_file.Close();
			m_isClosed = true;

			m_workbookData = null;
			m_sheets = null;
			m_stream = null;
			m_globals = null;
			m_encoding = null;
			m_hdr = null;
		}

		private static object tryConvertOADateTime(double value, ushort XFormat)
		{
			switch (XFormat)
			{
				//Time format
				case 63:
				case 68:
					DateTime time = DateTime.FromOADate(value);

					return (time.Second == 0)
						? time.ToShortTimeString()
						: time.ToLongTimeString();

				//Date Format
				case 26:
				case 62:
				case 64:
				case 67:
				case 69:
				case 70:
				case 100: return DateTime.FromOADate(value).ToString(System.Globalization.CultureInfo.CurrentCulture);
				//case 100: return DateTime.FromOADate(value).ToString(System.Globalization.CultureInfo.InvariantCulture);

				default:
					return value;
			}
		}

		private static object tryConvertOADateTime(object value, ushort XFormat)
		{
			double _dValue;
			object r;

			try
			{
				_dValue = double.Parse(value.ToString());

				r = tryConvertOADateTime(_dValue, XFormat);
			}
			catch (FormatException)
			{
				r = value;
			}

			return r;
		}

		private bool isV8()
		{
			return m_version >= 0x600;
		}

		#endregion

		#region IExcelDataReader Members

		public void Initialize(Stream fileStream)
		{
			m_file = fileStream;

			readWorkBookGlobals();
		}

		public DataSet AsDataSet()
		{
			return AsDataSet(false);
		}

		public DataSet AsDataSet(bool convertOADateTime)
		{
			if (!m_isValid) return null;

			if (m_isClosed) return m_workbookData;

			m_ConvertOADate = convertOADateTime;
			m_workbookData = new DataSet();

			for (int index = 0; index < ResultsCount; index++)
			{
				DataTable table = readWholeWorkSheet(m_sheets[index]);

				if (null != table)
					m_workbookData.Tables.Add(table);
			}

			m_file.Close();
			m_isClosed = true;

			return m_workbookData;
		}

		public string ExceptionMessage
		{
			get { return m_exceptionMessage; }
		}

		public string Name
		{
			get
			{
				if (null != m_sheets && m_sheets.Count > 0)
					return m_sheets[m_SheetIndex].Name;
				else
					return null;
			}
		}

		public bool IsValid
		{
			get { return m_isValid; }
		}

		public void Close()
		{
			m_file.Close();
			m_isClosed = true;
		}

		public int Depth
		{
			get { return m_depht; }
		}

		public int ResultsCount
		{
			get { return m_globals.Sheets.Count; }
		}

		public bool IsClosed
		{
			get { return m_isClosed; }
		}

		public bool NextResult()
		{
			if (m_SheetIndex >= (this.ResultsCount - 1)) return false;

			m_SheetIndex++;

			m_IsFirstRead = true;

			return true;
		}

		public bool Read()
		{
			if (!m_isValid) return false;

			if (m_IsFirstRead) initializeSheetRead();

			return moveToNextRecord();
		}

		public int FieldCount
		{
			get { return m_maxCol; }
		}

		public bool GetBoolean(int i)
		{
			if (IsDBNull(i)) return false;

			return Boolean.Parse(m_cellsValues[i].ToString());
		}

		public DateTime GetDateTime(int i)
		{
			if (IsDBNull(i)) return DateTime.MinValue;

			string val = m_cellsValues[i].ToString();
			double dVal;

			try
			{
				dVal = double.Parse(val);
			}
			catch (FormatException)
			{
				return DateTime.Parse(val);
			}

			return DateTime.FromOADate(dVal);
		}

		public decimal GetDecimal(int i)
		{
			if (IsDBNull(i)) return decimal.MinValue;

			return decimal.Parse(m_cellsValues[i].ToString());
		}

		public double GetDouble(int i)
		{
			if (IsDBNull(i)) return double.MinValue;

			return double.Parse(m_cellsValues[i].ToString());
		}

		public float GetFloat(int i)
		{
			if (IsDBNull(i)) return float.MinValue;

			return float.Parse(m_cellsValues[i].ToString());
		}

		public short GetInt16(int i)
		{
			if (IsDBNull(i)) return short.MinValue;

			return short.Parse(m_cellsValues[i].ToString());
		}

		public int GetInt32(int i)
		{
			if (IsDBNull(i)) return int.MinValue;

			return int.Parse(m_cellsValues[i].ToString());
		}

		public long GetInt64(int i)
		{
			if (IsDBNull(i)) return long.MinValue;

			return long.Parse(m_cellsValues[i].ToString());
		}

		public string GetString(int i)
		{
			if (IsDBNull(i)) return null;

			return m_cellsValues[i].ToString();
		}

		public object GetValue(int i)
		{
			return m_cellsValues[i];
		}

		public bool IsDBNull(int i)
		{
			return (null == m_cellsValues[i]) || (DBNull.Value == m_cellsValues[i]);
		}

		public object this[int i]
		{
			get { return m_cellsValues[i]; }
		}

		#endregion

		#region  Not Supported IDataReader Members


		public DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		public int RecordsAffected
		{
			get { throw new NotSupportedException(); }
		}

		#endregion

		#region Not Supported IDataRecord Members


		public byte GetByte(int i)
		{
			throw new NotSupportedException();
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		public char GetChar(int i)
		{
			throw new NotSupportedException();
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		public IDataReader GetData(int i)
		{
			throw new NotSupportedException();
		}

		public string GetDataTypeName(int i)
		{
			throw new NotSupportedException();
		}

		public Type GetFieldType(int i)
		{
			throw new NotSupportedException();
		}

		public Guid GetGuid(int i)
		{
			throw new NotSupportedException();
		}

		public string GetName(int i)
		{
			throw new NotSupportedException();
		}

		public int GetOrdinal(string name)
		{
			throw new NotSupportedException();
		}

		public int GetValues(object[] values)
		{
			throw new NotSupportedException();
		}

		public object this[string name]
		{
			get { throw new NotSupportedException(); }
		}

		#endregion

		#region IExcelDataReader Members


		public bool IsFirstRowAsColumnNames
		{
			get
			{
				return _isFirstRowAsColumnNames;
			}
			set
			{
				_isFirstRowAsColumnNames = value;
			}
		}

		#endregion
	}
}