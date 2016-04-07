using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RenameToolbox.Class
{
    public class RuleHandler
    {
        #region Filename Handling
        public static List<FileToRename> RemoveFileName(List<FileToRename> list, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                string filename = GlobalConst.EMPTY_STRING, extension = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName);
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_ALL:
                            list[i].FileName = extension;
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                list[i].FileName = filename.Substring(0 + Convert.ToInt32(subparameter)) + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                list[i].FileName = filename.Substring(0, filename.Length - Convert.ToInt32(subparameter)) + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_CENTER:
                            if (!string.IsNullOrEmpty(subparameter) && filename.Length >= 3)
                            {
                                int center = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(filename.Length) / 2))), leftlength = 0, rightlength = 0;
                                if (filename.Length < (Convert.ToInt32(subparameter) * 2))
                                {
                                    subparameter = Convert.ToString(center);
                                }
                                if (center + Convert.ToInt32(subparameter) > filename.Length)
                                {
                                    rightlength = filename.Length - center;
                                }
                                else
                                {
                                    rightlength = Convert.ToInt32(subparameter);
                                }
                                if (center - Convert.ToInt32(subparameter) < 0)
                                {
                                    leftlength = 0;
                                }
                                else
                                {
                                    leftlength = Convert.ToInt32(subparameter);
                                }
                                list[i].FileName = filename.Substring(0, center - leftlength - 1) + filename.Substring(center + rightlength) + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                            if (filename.Length > 1)
                            {
                                list[i].FileName = filename.Substring(1) + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                list = ReplaceFileName(list, subparameter, GlobalConst.EMPTY_STRING);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_BETWEENBRACKETS:
                            string regex = "(\\(.*\\))"; //"(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";
                            list[i].FileName = Regex.Replace(filename, regex, GlobalConst.EMPTY_STRING) + extension;
                            break;
                        case GlobalConst.FUNCTYPE_FROMLEFTBRACKET:
                            int LeftBucketIndex = filename.IndexOf('(');
                            if (LeftBucketIndex > 0)
                            {
                                list[i].FileName = filename.Substring(0, LeftBucketIndex) + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_BEFORERIGHTBRACKET:
                            int RightBucketIndex = filename.LastIndexOf(')') + 1;
                            if (RightBucketIndex > 0)
                            {
                                list[i].FileName = filename.Substring(RightBucketIndex) + extension;
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> ReplaceFileName(List<FileToRename> list, string target, string newstring)
        {
            if (!string.IsNullOrEmpty(target) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(target, newstring);
                }
            }
            return list;
        }
        public static List<FileToRename> PrefixFileName(List<FileToRename> list, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = prefix + list[i].FileName;
                }
            }
            return list;
        }
        public static List<FileToRename> SuffixFileName(List<FileToRename> list, string suffix)
        {
            if (!string.IsNullOrEmpty(suffix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = Path.GetFileNameWithoutExtension(list[i].FileName) + suffix + Path.GetExtension(list[i].FileName);
                }
            }
            return list;
        }
        public static List<FileToRename> ChangeFileNameCase(List<FileToRename> list, string UpDown, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(UpDown) && list != null)
            {
                UpDown = UpDown.ToLowerInvariant();
                string filename = GlobalConst.EMPTY_STRING, extension = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName);
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_ALL:
                            if (UpDown == GlobalConst.MOVE_UP)
                            {
                                list[i].FileName = filename.ToUpperInvariant() + extension;
                            }
                            else
                            {
                                list[i].FileName = filename.ToLowerInvariant() + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                string ChangedLeftSlice = GlobalConst.EMPTY_STRING;
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    ChangedLeftSlice = filename.Substring(0, Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedLeftSlice = filename.Substring(0, Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = ChangedLeftSlice + filename.Substring(Convert.ToInt32(subparameter)) + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (filename.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(filename.Length);
                                }
                                string ChangedRightSlice = GlobalConst.EMPTY_STRING;
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    ChangedRightSlice = filename.Substring(filename.Length - Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedRightSlice = filename.Substring(filename.Length - Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = filename.Substring(0, filename.Length - Convert.ToInt32(subparameter)) + ChangedRightSlice + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                            if (filename.Length > 1)
                            {
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    list[i].FileName = char.ToUpper(filename[0]) + filename.Substring(1) + extension;
                                }
                                else
                                {
                                    list[i].FileName = char.ToLower(filename[0]) + filename.Substring(1) + extension;
                                }
                            }
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    list = ReplaceFileName(list, subparameter, subparameter.ToUpperInvariant());
                                }
                                else
                                {
                                    list = ReplaceFileName(list, subparameter, subparameter.ToLowerInvariant());
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> InsertIntoFileName(List<FileToRename> list, string mode, string position, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(subparameter) && list != null)
            {
                string[] auxp = null;
                int Digit = 0, CurrentNo = 0, Steps = 0;
                if (mode.ToLowerInvariant() == GlobalConst.FUNCTYPE_DIGIT && subparameter.Contains(GlobalConst.AUX_CONNECTOR))
                {
                    auxp = subparameter.Split(';'); //length, start from , step
                    Digit = Convert.ToInt32(auxp[0]); //length
                    CurrentNo = Convert.ToInt32(auxp[1]); //start from
                    Steps = Convert.ToInt32(auxp[2]); //step
                }
                string filename = GlobalConst.EMPTY_STRING, extension = GlobalConst.EMPTY_STRING, SubLeft = GlobalConst.EMPTY_STRING, SubRight = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName);
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            if (position == GlobalConst.ZERO)
                            {
                                list = PrefixFileName(list, subparameter);
                                return list;
                            }
                            else if (Convert.ToInt32(position) >= filename.Length)
                            {
                                list = SuffixFileName(list, subparameter);
                                return list;
                            }
                            else
                            {
                                SubLeft = filename.Substring(0, Convert.ToInt32(position));
                                SubRight = filename.Substring(Convert.ToInt32(position));
                                list[i].FileName = SubLeft + subparameter + SubRight + extension;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            string strgen = CurrentNo.ToString();
                            if (CurrentNo.ToString().Length != Digit)
                            {
                                while (strgen.Length != Digit)
                                {
                                    strgen = GlobalConst.ZERO + strgen;
                                }
                            }
                            if (Convert.ToInt32(position) > filename.Length)
                            {
                                position = filename.Length.ToString();
                            }
                            SubLeft = filename.Substring(0, Convert.ToInt32(position));
                            SubRight = filename.Substring(Convert.ToInt32(position));
                            list[i].FileName = SubLeft + strgen + SubRight + extension;
                            if ((CurrentNo + Steps).ToString().Length > Digit || (CurrentNo + Steps) < 0)
                            {
                                Steps = 0;
                            }
                            CurrentNo += Steps;
                            break;
                    }
                }
            }
            return list;
        }
        #endregion

        #region Extension Handling
        public static List<FileToRename> RemoveFileExtension(List<FileToRename> list, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                string filename = GlobalConst.EMPTY_STRING, extension = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName).Substring(1);
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_ALL:
                            list[i].FileName = filename;
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                list[i].FileName = filename + "." + extension.Substring(0 + Convert.ToInt32(subparameter));
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                list[i].FileName = filename + "." + extension.Substring(0, extension.Length - Convert.ToInt32(subparameter));
                            }
                            break;
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                            if (extension.Length > 1)
                            {
                                list[i].FileName = filename + "." + extension.Substring(1);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                list[i].FileName = filename + "." + extension.Replace(subparameter, GlobalConst.EMPTY_STRING);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_BETWEENBRACKETS:
                            string regex = "(\\(.*\\))";
                            list[i].FileName = filename + "." + Regex.Replace(extension, regex, GlobalConst.EMPTY_STRING);
                            break;
                        case GlobalConst.FUNCTYPE_FROMLEFTBRACKET:
                            int LeftBucketIndex = extension.IndexOf('(');
                            if (LeftBucketIndex > 0)
                            {
                                list[i].FileName = filename + "." + extension.Substring(0, LeftBucketIndex);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_BEFORERIGHTBRACKET:
                            int RightBucketIndex = extension.LastIndexOf(')') + 1;
                            if (RightBucketIndex > 0)
                            {
                                list[i].FileName = filename + "." + extension.Substring(RightBucketIndex);
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> ReplaceFileExtension(List<FileToRename> list, string oldExtension, string newExtension)
        {
            if (!string.IsNullOrEmpty(newExtension) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(oldExtension, newExtension);
                }
            }
            return list;
        }
        public static List<FileToRename> PrefixFileExtension(List<FileToRename> list, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(Path.GetExtension(list[i].FileName), "." + prefix + Path.GetExtension(list[i].FileName).Substring(1));
                }
            }
            return list;
        }
        public static List<FileToRename> SuffixFileExtension(List<FileToRename> list, string suffix)
        {
            if (!string.IsNullOrEmpty(suffix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName + suffix;
                }
            }
            return list;
        }
        public static List<FileToRename> ChangeFileExtensionCase(List<FileToRename> list, string UpDown, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                UpDown = UpDown.ToLowerInvariant();
                string filename = GlobalConst.EMPTY_STRING, extension = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName).Substring(1);
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_ALL:
                            if (UpDown == GlobalConst.MOVE_UP)
                            {
                                list[i].FileName = filename + "." + extension.ToUpperInvariant();
                            }
                            else
                            {
                                list[i].FileName = filename + "." + extension.ToLowerInvariant();
                            }
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                string ChangedLeftSlice = GlobalConst.EMPTY_STRING;
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    ChangedLeftSlice = extension.Substring(0, Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedLeftSlice = extension.Substring(0, Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = filename + "." + ChangedLeftSlice + extension.Substring(Convert.ToInt32(subparameter));
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (extension.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(extension.Length);
                                }
                                string ChangedRightSlice = GlobalConst.EMPTY_STRING;
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    ChangedRightSlice = extension.Substring(extension.Length - Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedRightSlice = extension.Substring(extension.Length - Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = filename + "." + extension.Substring(0, extension.Length - Convert.ToInt32(subparameter)) + ChangedRightSlice;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                            if (extension.Length > 1)
                            {
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    list[i].FileName = filename + "." + char.ToUpper(extension[0]) + extension.Substring(1);
                                }
                                else
                                {
                                    list[i].FileName = filename + "." + char.ToLower(extension[0]) + extension.Substring(1);
                                }
                            }
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    list[i].FileName = filename + "." + extension.Replace(subparameter, subparameter.ToUpperInvariant());
                                }
                                else
                                {
                                    list[i].FileName = filename + "." + extension.Replace(subparameter, subparameter.ToLowerInvariant());
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> InsertIntoFileExtension(List<FileToRename> list, string mode, string position, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(subparameter) && list != null)
            {
                string[] auxp = null;
                int Digit = 0, CurrentNo = 0, Steps = 0;
                if (mode.ToLowerInvariant() == GlobalConst.FUNCTYPE_DIGIT && subparameter.Contains(GlobalConst.AUX_CONNECTOR))
                {
                    auxp = subparameter.Split(';'); //length, start from , step
                    Digit = Convert.ToInt32(auxp[0]); //length
                    CurrentNo = Convert.ToInt32(auxp[1]); //start from
                    Steps = Convert.ToInt32(auxp[2]); //step
                }
                string filename = GlobalConst.EMPTY_STRING, extension = GlobalConst.EMPTY_STRING, SubLeft = GlobalConst.EMPTY_STRING, SubRight = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    filename = Path.GetFileNameWithoutExtension(list[i].FileName);
                    extension = Path.GetExtension(list[i].FileName).Substring(1); ;
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            if (position == GlobalConst.ZERO)
                            {
                                list = PrefixFileExtension(list, subparameter);
                                return list;
                            }
                            else if (Convert.ToInt32(position) >= extension.Length)
                            {
                                list = SuffixFileExtension(list, subparameter);
                                return list;
                            }
                            else
                            {
                                SubLeft = extension.Substring(0, Convert.ToInt32(position));
                                SubRight = extension.Substring(Convert.ToInt32(position));
                                list[i].FileName = filename + "." + SubLeft + subparameter + SubRight;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            string strgen = CurrentNo.ToString(); ;
                            if (CurrentNo.ToString().Length != Digit)
                            {
                                while (strgen.Length != Digit)
                                {
                                    strgen = GlobalConst.ZERO + strgen;
                                }
                            }
                            if (Convert.ToInt32(position) > extension.Length)
                            {
                                position = extension.Length.ToString();
                            }
                            SubLeft = extension.Substring(0, Convert.ToInt32(position));
                            SubRight = extension.Substring(Convert.ToInt32(position));
                            list[i].FileName = filename + "." + SubLeft + strgen + SubRight;
                            if ((CurrentNo + Steps).ToString().Length > Digit || (CurrentNo + Steps) < 0)
                            {
                                Steps = 0;
                            }
                            CurrentNo += Steps;
                            break;
                    }
                }
            }
            return list;
        }
        #endregion

        #region FolderName Handling
        public static List<FileToRename> RemoveFolderName(List<FileToRename> list, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && list != null)
            {
                string foldername = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    foldername = list[i].FileName;
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_ALL:
                            list[i].FileName = GlobalConst.EMPTY_STRING;
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (foldername.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(foldername.Length);
                                }
                                list[i].FileName = foldername.Substring(0 + Convert.ToInt32(subparameter));
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (foldername.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(foldername.Length);
                                }
                                list[i].FileName = foldername.Substring(0, foldername.Length - Convert.ToInt32(subparameter));
                            }
                            break;
                        case GlobalConst.FUNCTYPE_CENTER:
                            if (!string.IsNullOrEmpty(subparameter) && foldername.Length >= 3)
                            {
                                int center = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(foldername.Length) / 2))), leftlength = 0, rightlength = 0;
                                if (foldername.Length < (Convert.ToInt32(subparameter) * 2))
                                {
                                    subparameter = Convert.ToString(center);
                                }
                                if (center + Convert.ToInt32(subparameter) > foldername.Length)
                                {
                                    rightlength = foldername.Length - center;
                                }
                                else
                                {
                                    rightlength = Convert.ToInt32(subparameter);
                                }
                                if (center - Convert.ToInt32(subparameter) < 0)
                                {
                                    leftlength = 0;
                                }
                                else
                                {
                                    leftlength = Convert.ToInt32(subparameter);
                                }
                                list[i].FileName = foldername.Substring(0, center - leftlength - 1) + foldername.Substring(center + rightlength);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                            if (foldername.Length > 1)
                            {
                                list[i].FileName = foldername.Substring(1);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                list = ReplaceFileName(list, subparameter, GlobalConst.EMPTY_STRING);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_BETWEENBRACKETS:
                            string regex = "(\\(.*\\))"; //"(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";
                            list[i].FileName = Regex.Replace(foldername, regex, GlobalConst.EMPTY_STRING);
                            break;
                        case GlobalConst.FUNCTYPE_FROMLEFTBRACKET:
                            int LeftBucketIndex = foldername.IndexOf('(');
                            if (LeftBucketIndex > 0)
                            {
                                list[i].FileName = foldername.Substring(0, LeftBucketIndex);
                            }
                            break;
                        case GlobalConst.FUNCTYPE_BEFORERIGHTBRACKET:
                            int RightBucketIndex = foldername.LastIndexOf(')') + 1;
                            if (RightBucketIndex > 0)
                            {
                                list[i].FileName = foldername.Substring(RightBucketIndex);
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> ReplaceFolderName(List<FileToRename> list, string target, string newstring)
        {
            if (!string.IsNullOrEmpty(target) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName.Replace(target, newstring);
                }
            }
            return list;
        }
        public static List<FileToRename> PrefixFolderName(List<FileToRename> list, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = prefix + list[i].FileName;
                }
            }
            return list;
        }
        public static List<FileToRename> SuffixFolderName(List<FileToRename> list, string suffix)
        {
            if (!string.IsNullOrEmpty(suffix) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FileName = list[i].FileName + suffix;
                }
            }
            return list;
        }
        public static List<FileToRename> ChangeFolderNameCase(List<FileToRename> list, string UpDown, string mode, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(UpDown) && list != null)
            {
                UpDown = UpDown.ToLowerInvariant();
                string foldername = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    foldername = list[i].FileName;
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_ALL:
                            if (UpDown == GlobalConst.MOVE_UP)
                            {
                                list[i].FileName = foldername.ToUpperInvariant();
                            }
                            else
                            {
                                list[i].FileName = foldername.ToLowerInvariant();
                            }
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (foldername.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(foldername.Length);
                                }
                                string ChangedLeftSlice = GlobalConst.EMPTY_STRING;
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    ChangedLeftSlice = foldername.Substring(0, Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedLeftSlice = foldername.Substring(0, Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = ChangedLeftSlice + foldername.Substring(Convert.ToInt32(subparameter));
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (foldername.Length < Convert.ToInt32(subparameter))
                                {
                                    subparameter = Convert.ToString(foldername.Length);
                                }
                                string ChangedRightSlice = GlobalConst.EMPTY_STRING;
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    ChangedRightSlice = foldername.Substring(foldername.Length - Convert.ToInt32(subparameter)).ToUpperInvariant();
                                }
                                else
                                {
                                    ChangedRightSlice = foldername.Substring(foldername.Length - Convert.ToInt32(subparameter)).ToLowerInvariant();
                                }
                                list[i].FileName = foldername.Substring(0, foldername.Length - Convert.ToInt32(subparameter)) + ChangedRightSlice;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                            if (foldername.Length > 1)
                            {
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    list[i].FileName = char.ToUpper(foldername[0]) + foldername.Substring(1);
                                }
                                else
                                {
                                    list[i].FileName = char.ToLower(foldername[0]) + foldername.Substring(1);
                                }
                            }
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            if (!string.IsNullOrEmpty(subparameter))
                            {
                                if (UpDown == GlobalConst.MOVE_UP)
                                {
                                    list = ReplaceFolderName(list, subparameter, subparameter.ToUpperInvariant());
                                }
                                else
                                {
                                    list = ReplaceFolderName(list, subparameter, subparameter.ToLowerInvariant());
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }
        public static List<FileToRename> InsertIntoFolderName(List<FileToRename> list, string mode, string position, string subparameter)
        {
            if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(subparameter) && list != null)
            {
                string[] auxp = null;
                int Digit = 0, CurrentNo = 0, Steps = 0;
                if (mode.ToLowerInvariant() == GlobalConst.FUNCTYPE_DIGIT && subparameter.Contains(GlobalConst.AUX_CONNECTOR))
                {
                    auxp = subparameter.Split(';'); //length, start from , step
                    Digit = Convert.ToInt32(auxp[0]); //length
                    CurrentNo = Convert.ToInt32(auxp[1]); //start from
                    Steps = Convert.ToInt32(auxp[2]); //step
                }
                string foldername = GlobalConst.EMPTY_STRING, SubLeft = GlobalConst.EMPTY_STRING, SubRight = GlobalConst.EMPTY_STRING;
                for (int i = 0; i < list.Count; i++)
                {
                    foldername = list[i].FileName;
                    switch (mode.ToLowerInvariant())
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            if (position == GlobalConst.ZERO)
                            {
                                list = PrefixFileName(list, subparameter);
                                return list;
                            }
                            else if (Convert.ToInt32(position) >= foldername.Length)
                            {
                                list = SuffixFileName(list, subparameter);
                                return list;
                            }
                            else
                            {
                                SubLeft = foldername.Substring(0, Convert.ToInt32(position));
                                SubRight = foldername.Substring(Convert.ToInt32(position));
                                list[i].FileName = SubLeft + subparameter + SubRight;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            string strgen = CurrentNo.ToString();
                            if (CurrentNo.ToString().Length != Digit)
                            {
                                while (strgen.Length != Digit)
                                {
                                    strgen = GlobalConst.ZERO + strgen;
                                }
                            }
                            if (Convert.ToInt32(position) > foldername.Length)
                            {
                                position = foldername.Length.ToString();
                            }
                            SubLeft = foldername.Substring(0, Convert.ToInt32(position));
                            SubRight = foldername.Substring(Convert.ToInt32(position));
                            list[i].FileName = SubLeft + strgen + SubRight;
                            if ((CurrentNo + Steps).ToString().Length > Digit || (CurrentNo + Steps) < 0)
                            {
                                Steps = 0;
                            }
                            CurrentNo += Steps;
                            break;
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
