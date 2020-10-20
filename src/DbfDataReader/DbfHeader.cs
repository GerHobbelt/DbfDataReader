using System;
using System.IO;

namespace DbfDataReader
{
    public class DbfCodePage
    {
        public int id { get; private set; }
        public string codePage { get; private set; }
        public string description { get; private set; }

        public DbfCodePage(int ldid, string cp, string descr)
        {
            id = ldid;
            codePage = cp;
            description = descr;
        }
    }

    public class DbfHeader
    {
        private const int DbfHeaderSize = 32;

        public DbfHeader(BinaryReader binaryReader)
        {
            Read(binaryReader);
        }

        public int Version { get; private set; }

        public DateTime UpdatedAt { get; private set; }
        public int HeaderLength { get; private set; }
        public int RecordLength { get; private set; }
        public long RecordCount { get; private set; }
        public int LanguageDriverID { get; private set; }

        public string VersionDescription
        {
            get
            {
                string description;

                // https://social.msdn.microsoft.com/Forums/en-US/315c582a-651f-4a2e-b51c-92aadef8bddf/opening-vfp-tables-with-fox26-dos?forum=visualfoxprogeneral
                // 
                // File type:
                // 0x02   FoxBASE
                // 0x03   FoxBASE+/Dbase III plus, no memo
                // 0x30   Visual FoxPro
                // 0x31   Visual FoxPro, autoincrement enabled
                // 0x32   Visual FoxPro, Varchar, Varbinary, or Blob-enabled
                // 0x43   dBASE IV SQL table files, no memo
                // 0x63   dBASE IV SQL system files, no memo
                // 0x83   FoxBASE+/dBASE III PLUS, with memo
                // 0x8B   dBASE IV with memo
                // 0xCB   dBASE IV SQL table files, with memo
                // 0xF5   FoxPro 2.x (or earlier) with memo
                // 0xFB   FoxBASE
                switch (Version)
                {
                    case 0x02:
                        description = "FoxPro";
                        break;
                    case 0x03:
                        description = "dBase III without memo file";
                        break;
                    case 0x04:
                        description = "dBase IV without memo file";
                        break;
                    case 0x05:
                        description = "dBase V without memo file";
                        break;
                    case 0x07:
                        description = "Visual Objects 1.x";
                        break;
                    case 0x30:
                        description = "Visual FoxPro";
                        break;
                    case 0x31:
                        description = "Visual FoxPro with AutoIncrement field";
                        break;
                    case 0x32:
                        description = "Visual FoxPro, Varchar, Varbinary, or Blob-enabled";
                        break;
                    case 0x43:
                        description = "dBASE IV SQL table files, no memo";
                        break;
                    case 0x63:
                        description = "dBASE IV SQL system files, no memo";
                        break;
                    case 0x7b:
                        description = "dBase IV with memo file";
                        break;
                    case 0x83:
                        description = "dBase III with memo file";
                        break;
                    case 0x87:
                        description = "Visual Objects 1.x with memo file";
                        break;
                    case 0x8b:
                        description = "dBase IV with memo file";
                        break;
                    case 0x8e:
                        description = "dBase IV with SQL table";
                        break;
                    case 0xcb:
                        description = "dBASE IV SQL table files, with memo";
                        break;
                    case 0xf5:
                        description = "FoxPro 2.x (or earlier) with memo";
                        break;
                    case 0xfb:
                        description = "FoxPro without memo file";
                        break;
                    default:
                        description = "Unknown";
                        break;
                }

                return description;
            }
        }

        public DbfCodePage CodePage
        {
            get
            {
                // Table from dbf.py by Ethan Furman at https://github.com/ethanfurman/dbf/blob/master/dbf/__init__.py,
                // mixed with table at https://github.com/olemb/dbfread/blob/master/dbfread/codepages.py
                switch (LanguageDriverID)
                {
                    case 0x00:
                        return new DbfCodePage(LanguageDriverID, "ascii", "plain ol' ascii");
                    case 0x01:
                        return new DbfCodePage(LanguageDriverID, "cp437", "U.S. MS-DOS");
                    case 0x02:
                        return new DbfCodePage(LanguageDriverID, "cp850", "International MS-DOS");
                    case 0x03:
                        return new DbfCodePage(LanguageDriverID, "cp1252", "Windows ANSI");
                    case 0x04:
                        return new DbfCodePage(LanguageDriverID, "mac_roman", "Standard Macintosh");
                    case 0x08:
                        return new DbfCodePage(LanguageDriverID, "cp865", "Danish OEM");
                    case 0x09:
                        return new DbfCodePage(LanguageDriverID, "cp437", "Dutch OEM");
                    case 0x0A:
                        return new DbfCodePage(LanguageDriverID, "cp850", "Dutch OEM (secondary)");
                    case 0x0B:
                        return new DbfCodePage(LanguageDriverID, "cp437", "Finnish OEM");
                    case 0x0D:
                        return new DbfCodePage(LanguageDriverID, "cp437", "French OEM");
                    case 0x0E:
                        return new DbfCodePage(LanguageDriverID, "cp850", "French OEM (secondary)");
                    case 0x0F:
                        return new DbfCodePage(LanguageDriverID, "cp437", "German OEM");
                    case 0x10:
                        return new DbfCodePage(LanguageDriverID, "cp850", "German OEM (secondary)");
                    case 0x11:
                        return new DbfCodePage(LanguageDriverID, "cp437", "Italian OEM");
                    case 0x12:
                        return new DbfCodePage(LanguageDriverID, "cp850", "Italian OEM (secondary)");
                    case 0x13:
                        return new DbfCodePage(LanguageDriverID, "cp932", "Japanese Shift-JIS");
                    case 0x14:
                        return new DbfCodePage(LanguageDriverID, "cp850", "Spanish OEM (secondary)");
                    case 0x15:
                        return new DbfCodePage(LanguageDriverID, "cp437", "Swedish OEM");
                    case 0x16:
                        return new DbfCodePage(LanguageDriverID, "cp850", "Swedish OEM (secondary)");
                    case 0x17:
                        return new DbfCodePage(LanguageDriverID, "cp865", "Norwegian OEM");
                    case 0x18:
                        return new DbfCodePage(LanguageDriverID, "cp437", "Spanish OEM");
                    case 0x19:
                        return new DbfCodePage(LanguageDriverID, "cp437", "English OEM (Britain)");
                    case 0x1A:
                        return new DbfCodePage(LanguageDriverID, "cp850", "English OEM (Britain) (secondary)");
                    case 0x1B:
                        return new DbfCodePage(LanguageDriverID, "cp437", "English OEM (U.S.)");
                    case 0x1C:
                        return new DbfCodePage(LanguageDriverID, "cp863", "French OEM (Canada)");
                    case 0x1D:
                        return new DbfCodePage(LanguageDriverID, "cp850", "French OEM (secondary)");
                    case 0x1F:
                        return new DbfCodePage(LanguageDriverID, "cp852", "Czech OEM");
                    case 0x22:
                        return new DbfCodePage(LanguageDriverID, "cp852", "Hungarian OEM");
                    case 0x23:
                        return new DbfCodePage(LanguageDriverID, "cp852", "Polish OEM");
                    case 0x24:
                        return new DbfCodePage(LanguageDriverID, "cp860", "Portuguese OEM");
                    case 0x25:
                        return new DbfCodePage(LanguageDriverID, "cp850", "Portuguese OEM (secondary)");
                    case 0x26:
                        return new DbfCodePage(LanguageDriverID, "cp866", "Russian OEM");
                    case 0x37:
                        return new DbfCodePage(LanguageDriverID, "cp850", "English OEM (U.S.) (secondary)");
                    case 0x40:
                        return new DbfCodePage(LanguageDriverID, "cp852", "Romanian OEM");
                    case 0x4D:
                        return new DbfCodePage(LanguageDriverID, "cp936", "Chinese GBK (PRC)");
                    case 0x4E:
                        return new DbfCodePage(LanguageDriverID, "cp949", "Korean (ANSI/OEM)");
                    case 0x4F:
                        return new DbfCodePage(LanguageDriverID, "cp950", "Chinese Big 5 (Taiwan)");
                    case 0x50:
                        return new DbfCodePage(LanguageDriverID, "cp874", "Thai (ANSI/OEM)");
                    case 0x57:
                        return new DbfCodePage(LanguageDriverID, "cp1252", "ANSI");
                    case 0x58:
                        return new DbfCodePage(LanguageDriverID, "cp1252", "Western European ANSI");
                    case 0x59:
                        return new DbfCodePage(LanguageDriverID, "cp1252", "Spanish ANSI");
                    case 0x64:
                        return new DbfCodePage(LanguageDriverID, "cp852", "Eastern European MS-DOS");
                    case 0x65:
                        return new DbfCodePage(LanguageDriverID, "cp866", "Russian MS-DOS");
                    case 0x66:
                        return new DbfCodePage(LanguageDriverID, "cp865", "Nordic MS-DOS");
                    case 0x67:
                        return new DbfCodePage(LanguageDriverID, "cp861", "Icelandic MS-DOS");
                    case 0x68:
                        return new DbfCodePage(LanguageDriverID, null, "Kamenicky (Czech) MS-DOS");
                    case 0x69:
                        return new DbfCodePage(LanguageDriverID, null, "Mazovia (Polish) MS-DOS");
                    case 0x6a:
                        return new DbfCodePage(LanguageDriverID, "cp737", "Greek MS-DOS (437G)");
                    case 0x6b:
                        return new DbfCodePage(LanguageDriverID, "cp857", "Turkish MS-DOS");
                    case 0x78:
                        return new DbfCodePage(LanguageDriverID, "cp950", "Traditional Chinese (Hong Kong SAR, Taiwan) Windows");
                    case 0x79:
                        return new DbfCodePage(LanguageDriverID, "cp949", "Korean Windows");
                    case 0x7a:
                        return new DbfCodePage(LanguageDriverID, "cp936", "Chinese Simplified (PRC, Singapore) Windows");
                    case 0x7b:
                        return new DbfCodePage(LanguageDriverID, "cp932", "Japanese Windows");
                    case 0x7c:
                        return new DbfCodePage(LanguageDriverID, "cp874", "Thai Windows");
                    case 0x7d:
                        return new DbfCodePage(LanguageDriverID, "cp1255", "Hebrew Windows");
                    case 0x7e:
                        return new DbfCodePage(LanguageDriverID, "cp1256", "Arabic Windows");
                    case 0x96:
                        return new DbfCodePage(LanguageDriverID, "mac_cyrillic", "Russian Macintosh");
                    case 0x97:
                        return new DbfCodePage(LanguageDriverID, "mac_latin2", "Macintosh EE");
                    case 0x98:
                        return new DbfCodePage(LanguageDriverID, "mac_greek", "Greek Macintosh");
                    case 0xc8:
                        return new DbfCodePage(LanguageDriverID, "cp1250", "Eastern European Windows");
                    case 0xc9:
                        return new DbfCodePage(LanguageDriverID, "cp1251", "Russian Windows");
                    case 0xca:
                        return new DbfCodePage(LanguageDriverID, "cp1254", "Turkish Windows");
                    case 0xcb:
                        return new DbfCodePage(LanguageDriverID, "cp1253", "Greek Windows");
                    case 0xf0:
                        return new DbfCodePage(LanguageDriverID, "utf8", "8-bit unicode");

                    default:
                        return new DbfCodePage(LanguageDriverID, null, $"Unable to guess encoding for language driver byte 0x{LanguageDriverID:X}");
                }
            }
        }

        public bool IsFoxPro => Version == 0x30 || Version == 0x31 || Version == 0xf5 || Version == 0xfb;

        public void Read(BinaryReader binaryReader)
        {
            Version = binaryReader.ReadByte();

            var year = binaryReader.ReadByte();
            var month = binaryReader.ReadByte();
            var day = binaryReader.ReadByte();

            UpdatedAt = new DateTime(year + 1900, month, day);

            RecordCount = binaryReader.ReadUInt32();
            HeaderLength = binaryReader.ReadUInt16();
            RecordLength = binaryReader.ReadUInt16();

            var reserved1 = binaryReader.ReadUInt16();

            var IncompleteTransaction = binaryReader.ReadByte();
            var EncryptionFlag = binaryReader.ReadByte();

            var FreeRecordThread = binaryReader.ReadUInt32();
            var reserved2 = binaryReader.ReadUInt32();
            var reserved3 = binaryReader.ReadUInt32();

            var MdxFlag = binaryReader.ReadByte();
            LanguageDriverID = binaryReader.ReadByte();

            var reserved4 = binaryReader.ReadUInt16();
        }
    }
}
