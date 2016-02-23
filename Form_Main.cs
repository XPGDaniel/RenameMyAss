using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using RenameMyAss.Class;
using System.Collections.Specialized;
using System.Diagnostics;

namespace RenameMyAss
{
    public partial class Form_Main : Form
    {
        private XMLHelper xmlH = new XMLHelper();
        public bool MakeRAR = false;
        private enum MoveDirection { Up = -1, Down = 1 };
        public Form_Main()
        {
            InitializeComponent();
            comboBox_Target.SelectedIndex = 0;
            comboBox_Mode.SelectedIndex = 3;
            button_Undo.Enabled = false;
            button_UpdateRule.Enabled = false;
            button_MoveUp.Enabled = false;
            button_MoveDown.Enabled = false;
            button_RemoveRule.Enabled = false;
            button_RemovefromList.Enabled = false;
            button_Preview.Enabled = false;
            button_Rename.Enabled = false;
            AuxParameters_Control(false);
            if (File.Exists(GlobalConst.PATH_WINRAR)) comboBox_Target.Items.Add(GlobalConst.TARGETTYPE_FOLDERNAME);
        }
        #region ComboBoxes
        private void comboBox_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Mode.Items.Clear();
            AuxParameters_Control(false);
            label_Parameter2.Text = GlobalConst.LABEL_PARAMETER2_DEFAULT;
            switch (comboBox_Target.Text)
            {
                case GlobalConst.TARGETTYPE_FILENAME:
                case GlobalConst.TARGETTYPE_FILEEXTENSION:
                case GlobalConst.TARGETTYPE_FOLDERNAME:
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_PREFIX);
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_SUFFIX);
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_REMOVE);
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_REPLACE);
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_UPPERCASE);
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_LOWERCASE);
                    comboBox_Mode.Items.Add(GlobalConst.MODETYPE_INSERT);
                    if (comboBox_Target.Text == GlobalConst.TARGETTYPE_FOLDERNAME) comboBox_Mode.Items.Add(GlobalConst.MODETYPE_MAKERAR);
                    comboBox_Mode.Visible = true;
                    label_Mode.Visible = true;
                    comboBox_Parameter1.Visible = true;
                    label_Parameter1.Visible = true;
                    comboBox_Parameter2.Visible = true;
                    label_Parameter2.Visible = true;
                    break;
            }
        }

        private void comboBox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Parameter1.Items.Clear();
            AuxParameters_Control(false);
            label_Parameter2.Text = GlobalConst.LABEL_PARAMETER2_DEFAULT;
            switch (comboBox_Mode.Text)
            {
                case GlobalConst.MODETYPE_PREFIX:
                case GlobalConst.MODETYPE_SUFFIX:
                    label_Parameter1.Text = GlobalConst.LABEL_PARAMETER1_TARGETPHASE;
                    comboBox_Parameter1.Text = GlobalConst.EMPTY_STRING;
                    comboBox_Parameter2_Controls(false, null);
                    break;
                case GlobalConst.MODETYPE_REPLACE:
                    label_Parameter1.Text = GlobalConst.LABEL_PARAMETER1_TARGETPHASE;
                    comboBox_Parameter1.Text = GlobalConst.EMPTY_STRING;
                    comboBox_Parameter2_Controls(true, GlobalConst.LABEL_PARAMETER2_REPLACEWITH);
                    break;
                case GlobalConst.MODETYPE_REMOVE:
                case GlobalConst.MODETYPE_UPPERCASE:
                case GlobalConst.MODETYPE_LOWERCASE:
                    label_Parameter1.Text = GlobalConst.LABEL_PARAMETER1_PATTERN;
                    comboBox_Parameter1.Text = GlobalConst.EMPTY_STRING;
                    comboBox_Parameter2_Controls(false, null);
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_ALL);
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_LEFT);
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_RIGHT);
                    if (comboBox_Mode.Text == GlobalConst.MODETYPE_REMOVE)
                    {
                        comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_CENTER);
                    }
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_FIRSTLETTER);
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_MATCHPHASE);
                    if (comboBox_Mode.Text == GlobalConst.MODETYPE_REMOVE)
                    {
                        comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_BETWEENBRACKETS);
                        comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_FROMLEFTBRACKET);
                        comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_BEFORERIGHTBRACKET);
                    }
                    break;
                case GlobalConst.MODETYPE_INSERT:
                    label_Parameter1.Text = GlobalConst.LABEL_PARAMETER1_PATTERN;
                    comboBox_Parameter1.Text = GlobalConst.EMPTY_STRING;
                    comboBox_Parameter2_Controls(false, null);
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_PHASE);
                    comboBox_Parameter1.Items.Add(GlobalConst.FUNCTYPE_DIGIT);
                    break;
                case GlobalConst.MODETYPE_MAKERAR:
                    label_Parameter1.Text = GlobalConst.LABEL_PARAMETER1_DEFAULT;
                    comboBox_Parameter1.Text = GlobalConst.EMPTY_STRING;
                    comboBox_Parameter2_Controls(false, null);
                    break;
            }
        }
        private void comboBox_Parameter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AuxParameters_Control(false);
            switch (comboBox_Mode.Text)
            {
                case GlobalConst.MODETYPE_REMOVE:
                case GlobalConst.MODETYPE_UPPERCASE:
                case GlobalConst.MODETYPE_LOWERCASE:
                    switch (comboBox_Parameter1.Text)
                    {
                        case GlobalConst.FUNCTYPE_BETWEENBRACKETS:
                        case GlobalConst.FUNCTYPE_FROMLEFTBRACKET:
                        case GlobalConst.FUNCTYPE_BEFORERIGHTBRACKET:
                        case GlobalConst.FUNCTYPE_FIRSTLETTER:
                        case GlobalConst.FUNCTYPE_ALL:
                            comboBox_Parameter2_Controls(false, null);
                            break;
                        case GlobalConst.FUNCTYPE_LEFT:
                        case GlobalConst.FUNCTYPE_RIGHT:
                            comboBox_Parameter2_Controls(true, GlobalConst.LABEL_PARAMETER2_LENGTH);
                            break;
                        case GlobalConst.FUNCTYPE_CENTER:
                            comboBox_Parameter2_Controls(true, GlobalConst.LABEL_PARAMETER2_RANGE);
                            break;
                        case GlobalConst.FUNCTYPE_MATCHPHASE:
                            comboBox_Parameter2_Controls(true, GlobalConst.LABEL_PARAMETER1_TARGETPHASE);
                            break;
                        default:
                            comboBox_Parameter2_Controls(false, null);
                            break;
                    }
                    break;
                case GlobalConst.MODETYPE_INSERT:
                    comboBox_Parameter2_Controls(true, GlobalConst.LABEL_PARAMETER2_POSITION);
                    switch (comboBox_Parameter1.Text)
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            AuxParameters_Control(true, GlobalConst.AUX_PARAMETER_PHASE);
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            AuxParameters_Control(true, GlobalConst.AUX_PARAMETER_LENGTH, GlobalConst.AUX_PARAMETER_STARTFROM, GlobalConst.AUX_PARAMETER_STEP);
                            break;
                    }
                    break;
            }
        }
        private void comboBox_Parameter2_Controls(bool Visibility, string label)
        {
            comboBox_Parameter2.Enabled = Visibility;
            label_Parameter2.Visible = Visibility;
            label_Parameter2.Text = !string.IsNullOrEmpty(label) ? label : GlobalConst.EMPTY_STRING;
            comboBox_Parameter2.Text = (label_Parameter2.Text == GlobalConst.LABEL_PARAMETER2_POSITION || label_Parameter2.Text == GlobalConst.LABEL_PARAMETER2_LENGTH) ? GlobalConst.ZERO : GlobalConst.EMPTY_STRING;
            comboBox_Parameter2.Visible = Visibility;
        }
        private void AuxParameters_Control(bool Visibility, string Ax1 = null, string Ax2 = null, string Ax3 = null)
        {
            label_AuxParameter1.Visible = Visibility;
            label_AuxParameter1.Text = !string.IsNullOrEmpty(Ax1) ? Ax1 : GlobalConst.EMPTY_STRING;
            comboBox_AuxParameter1.Text = GlobalConst.EMPTY_STRING;
            comboBox_AuxParameter1.Visible = Visibility;
            comboBox_AuxParameter1.Enabled = Visibility;

            if (string.IsNullOrEmpty(Ax2))
            {
                Visibility = false;
            }
            label_AuxParameter2.Visible = Visibility;
            label_AuxParameter2.Text = !string.IsNullOrEmpty(Ax2) ? Ax2 : GlobalConst.EMPTY_STRING;
            comboBox_AuxParameter2.Text = GlobalConst.EMPTY_STRING;
            comboBox_AuxParameter2.Visible = Visibility;
            comboBox_AuxParameter2.Enabled = Visibility;

            if (string.IsNullOrEmpty(Ax3))
            {
                Visibility = false;
            }
            label_AuxParameter3.Visible = Visibility;
            label_AuxParameter3.Text = !string.IsNullOrEmpty(Ax3) ? Ax3 : GlobalConst.EMPTY_STRING;
            comboBox_AuxParameter3.Text = GlobalConst.EMPTY_STRING;
            comboBox_AuxParameter3.Visible = Visibility;
            comboBox_AuxParameter3.Enabled = Visibility;
        }
        #endregion

        private void button_Preview_Click(object sender, EventArgs e)
        {
            if (listView_Rules.Items.Count > 0 && listView_FileList.Items.Count > 0)
            {
                List<FileToRename> newFilenameList = new List<FileToRename>();
                foreach (ListViewItem vi in listView_FileList.Items)
                {
                    FileToRename ftr = new FileToRename();
                    ftr.Path = vi.Text;
                    ftr.FileName = vi.SubItems[1].Text;
                    newFilenameList.Add(ftr);
                }
                if (newFilenameList.Any())
                {
                    foreach (ListViewItem rule in listView_Rules.Items)
                    {
                        switch (rule.Text)
                        {
                            case GlobalConst.TARGETTYPE_FILENAME:
                                switch (rule.SubItems[1].Text)
                                {
                                    case GlobalConst.MODETYPE_REMOVE:
                                        newFilenameList = RuleHandler.RemoveFileName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_REPLACE:
                                        newFilenameList = RuleHandler.ReplaceFileName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_PREFIX:
                                        newFilenameList = RuleHandler.PrefixFileName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case GlobalConst.MODETYPE_SUFFIX:
                                        newFilenameList = RuleHandler.SuffixFileName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case GlobalConst.MODETYPE_UPPERCASE:
                                        newFilenameList = RuleHandler.ChangeFileNameCase(newFilenameList, GlobalConst.MOVE_UP, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_LOWERCASE:
                                        newFilenameList = RuleHandler.ChangeFileNameCase(newFilenameList, GlobalConst.MOVE_DOWN, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_INSERT:
                                        newFilenameList = RuleHandler.InsertIntoFileName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text, rule.SubItems[4].Text);
                                        break;
                                }
                                break;
                            case GlobalConst.TARGETTYPE_FILEEXTENSION:
                                switch (rule.SubItems[1].Text)
                                {
                                    case GlobalConst.MODETYPE_REMOVE:
                                        newFilenameList = RuleHandler.RemoveFileExtension(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_REPLACE:
                                        newFilenameList = RuleHandler.ReplaceFileExtension(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_PREFIX:
                                        newFilenameList = RuleHandler.PrefixFileExtension(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case GlobalConst.MODETYPE_SUFFIX:
                                        newFilenameList = RuleHandler.SuffixFileExtension(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case GlobalConst.MODETYPE_UPPERCASE:
                                        newFilenameList = RuleHandler.ChangeFileExtensionCase(newFilenameList, GlobalConst.MOVE_UP, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_LOWERCASE:
                                        newFilenameList = RuleHandler.ChangeFileExtensionCase(newFilenameList, GlobalConst.MOVE_DOWN, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_INSERT:
                                        newFilenameList = RuleHandler.InsertIntoFileExtension(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text, rule.SubItems[4].Text);
                                        break;
                                }
                                break;
                            case GlobalConst.TARGETTYPE_FOLDERNAME:
                                switch (rule.SubItems[1].Text)
                                {
                                    case GlobalConst.MODETYPE_REMOVE:
                                        newFilenameList = RuleHandler.RemoveFolderName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_REPLACE:
                                        newFilenameList = RuleHandler.ReplaceFolderName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_PREFIX:
                                        newFilenameList = RuleHandler.PrefixFolderName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case GlobalConst.MODETYPE_SUFFIX:
                                        newFilenameList = RuleHandler.SuffixFolderName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case GlobalConst.MODETYPE_UPPERCASE:
                                        newFilenameList = RuleHandler.ChangeFolderNameCase(newFilenameList, GlobalConst.MOVE_UP, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_LOWERCASE:
                                        newFilenameList = RuleHandler.ChangeFolderNameCase(newFilenameList, GlobalConst.MOVE_DOWN, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case GlobalConst.MODETYPE_INSERT:
                                        newFilenameList = RuleHandler.InsertIntoFolderName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text, rule.SubItems[4].Text);
                                        break;
                                    case GlobalConst.MODETYPE_MAKERAR:
                                        MakeRAR = true;
                                        break;
                                }
                                break;
                        }
                    }
                    for (int i = 0; i < newFilenameList.Count; i++)
                    {
                        ListViewItem fooItem = listView_FileList.Items.Find(newFilenameList[i].Path, false)[0];
                        fooItem.SubItems[2].Text = newFilenameList[i].FileName;
                    }
                }
                newFilenameList.Clear();
            }
        }
        private void button_Rename_Click(object sender, EventArgs e)
        {
            button_Preview_Click(null, null);
            if (listView_Rules.Items.Count > 0 && listView_FileList.Items.Count > 0)
            {
                List<FileToRename> RenameList = new List<FileToRename>();
                foreach (ListViewItem vi in listView_FileList.Items)
                {
                    FileToRename ftr = new FileToRename();
                    ftr.Path = vi.Text;
                    ftr.FileName = vi.SubItems[1].Text;
                    ftr.NewFileName = vi.SubItems[2].Text;
                    RenameList.Add(ftr);
                }
                if (RenameList.Any())
                {

                    switch (comboBox_Target.Text)
                    {
                        case GlobalConst.TARGETTYPE_FOLDERNAME:
                            foreach (FileToRename fileCandidate in RenameList)
                            {
                                if (Directory.Exists(fileCandidate.Path))
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName))
                                        {
                                            if (fileCandidate.Path != Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName)
                                            {
                                                Directory.Move(fileCandidate.Path, Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName);
                                                fileCandidate.Result = GlobalConst.RESULT_RENAME_OK;
                                            }
                                            if (MakeRAR)
                                            {
                                                using (Process p = new Process())
                                                {
                                                    p.StartInfo.FileName = GlobalConst.PATH_WINRAR;
                                                    //p.StartInfo.RedirectStandardOutput = true;
                                                    //p.StartInfo.RedirectStandardError = true;
                                                    p.StartInfo.UseShellExecute = false;
                                                    p.StartInfo.CreateNoWindow = false; //Default:true
                                                    p.StartInfo.Arguments = " a -ep1 -r \"" + Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName + ".rar\" \"" + Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName + "\\*\" -rr3p";
                                                    p.Start();
                                                    //string stdoutx = p.StandardOutput.ReadToEnd();
                                                    //string stderrx = p.StandardError.ReadToEnd();
                                                    p.WaitForExit();
                                                    //Console.WriteLine("Exit code : {0}", p.ExitCode);
                                                    //Console.WriteLine("Stdout : {0}", stdoutx);
                                                    //Console.WriteLine("Stderr : {0}", stderrx);
                                                    fileCandidate.Result += GlobalConst.RESULT_RAR_OK;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            fileCandidate.Result = GlobalConst.RESULT_INVALID_NEW_FOLDERNAME;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = GlobalConst.RESULT_RENAME_FAIL;
                                    }
                                }
                            }
                            break;
                        default:
                            foreach (FileToRename fileCandidate in RenameList)
                            {
                                if (File.Exists(fileCandidate.Path))
                                {
                                    try
                                    {
                                        FileInfo fi = new FileInfo(fileCandidate.Path);
                                        if (!string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(fileCandidate.NewFileName)) && !string.IsNullOrEmpty(Path.GetExtension(fileCandidate.NewFileName)))
                                        {
                                            File.Move(fi.FullName, fi.FullName.Replace(fileCandidate.FileName, fileCandidate.NewFileName));
                                            fileCandidate.Result = GlobalConst.RESULT_RENAME_OK;
                                        }
                                        else
                                        {
                                            fileCandidate.Result = GlobalConst.RESULT_INVALID_NEW_FILENAME_FILEEXTENSION;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = GlobalConst.RESULT_RENAME_FAIL;
                                    }
                                }
                            }
                            break;
                    }
                }
                for (int i = 0; i < RenameList.Count; i++)
                {
                    ListViewItem fooItem = listView_FileList.Items.Find(RenameList[i].Path, false)[0];
                    fooItem.SubItems[3].Text = RenameList[i].Result;
                }
                RenameList.Clear();
                button_Undo.Enabled = true;
            }
            MessageBox.Show("Done!");
            MakeRAR = false;
        }
        private void button_Undo_Click(object sender, EventArgs e)
        {
            if (listView_FileList.Items.Count > 0)
            {
                List<FileToRename> UndoList = new List<FileToRename>();
                foreach (ListViewItem vi in listView_FileList.Items)
                {
                    FileToRename ftr = new FileToRename();
                    ftr.Path = vi.Text;
                    ftr.FileName = vi.SubItems[1].Text;
                    ftr.NewFileName = vi.SubItems[2].Text;
                    ftr.Result = vi.SubItems[3].Text;
                    UndoList.Add(ftr);
                }
                if (UndoList.Any())
                {
                    switch (comboBox_Target.Text)
                    {
                        case GlobalConst.TARGETTYPE_FOLDERNAME:
                            foreach (FileToRename fileCandidate in UndoList)
                            {
                                string CurrentNewFolderName = Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName;
                                if (Directory.Exists(CurrentNewFolderName))
                                {
                                    try
                                    {
                                        Directory.Move(CurrentNewFolderName, fileCandidate.Path);
                                        fileCandidate.Result = GlobalConst.RESULT_UNDO_OK;
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = GlobalConst.RESULT_UNDO_FAIL;
                                    }
                                }
                            }
                            break;
                        default:
                            foreach (FileToRename fileCandidate in UndoList)
                            {
                                string CurrentNewFileName = fileCandidate.Path.Replace(fileCandidate.FileName, fileCandidate.NewFileName);
                                if (File.Exists(CurrentNewFileName))
                                {
                                    try
                                    {
                                        File.Move(CurrentNewFileName, CurrentNewFileName.Replace(fileCandidate.NewFileName, fileCandidate.FileName));
                                        fileCandidate.Result = GlobalConst.RESULT_UNDO_OK;
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = GlobalConst.RESULT_UNDO_FAIL;
                                    }
                                }
                            }
                            break;
                    }
                    for (int i = 0; i < UndoList.Count; i++)
                    {
                        ListViewItem fooItem = listView_FileList.Items.Find(UndoList[i].Path, false)[0];
                        fooItem.SubItems[3].Text = UndoList[i].Result;
                    }
                }
                UndoList.Clear();
                button_Undo.Enabled = false;
            }
        }


        #region listView_FileList
        private void listView_FileList_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string s in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (Directory.Exists(s))
                {//Add files from folder
                    switch (comboBox_Target.Text)
                    {
                        case GlobalConst.TARGETTYPE_FOLDERNAME:
                            ListViewItem lvid = new ListViewItem();
                            DirectoryInfo di = new DirectoryInfo(s);
                            lvid.Name = di.FullName;
                            lvid.Text = s;
                            lvid.SubItems.Add(di.Name);
                            lvid.SubItems.Add(GlobalConst.EMPTY_STRING);
                            lvid.SubItems.Add(GlobalConst.EMPTY_STRING);
                            listView_FileList.Items.Add(lvid);
                            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvid, lvid.Index));
                            break;
                        default:
                            List<string> filepaths = new List<string>();
                            filepaths.AddRange(Directory.GetFiles(s, "*.*", SearchOption.AllDirectories));
                            foreach (string item in filepaths)
                            {
                                ListViewItem lvi = new ListViewItem();
                                FileInfo fi = new FileInfo(item);
                                lvi.Name = fi.FullName;
                                lvi.Text = item;
                                lvi.SubItems.Add(fi.Name);
                                lvi.SubItems.Add(GlobalConst.EMPTY_STRING);
                                lvi.SubItems.Add(GlobalConst.EMPTY_STRING);
                                listView_FileList.Items.Add(lvi);
                                listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
                            }
                            break;
                    }
                }
                else
                {
                    try
                    {
                        if (File.Exists(s))
                        {
                            ListViewItem lvi = new ListViewItem();
                            FileInfo fi = new FileInfo(s);
                            lvi.Name = fi.FullName;
                            lvi.Text = s;
                            lvi.SubItems.Add(fi.Name);
                            lvi.SubItems.Add(GlobalConst.EMPTY_STRING);
                            lvi.SubItems.Add(GlobalConst.EMPTY_STRING);
                            listView_FileList.Items.Add(lvi);
                            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, "Failed to perform the" +
                          " specified operation:\n\n" + ex.Message,
                          "File operation failed", MessageBoxButtons.OK,
                          MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void listView_FileList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void listView_FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_FileList.SelectedItems.Count > 0)
            {
                button_RemovefromList.Enabled = true;
            }
            else
            {
                button_RemovefromList.Enabled = false;
            }
        }

        private void listView_FileList_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Set the effect based upon the KeyState.
            if ((e.KeyState & GlobalConst.KEY_SHIFT) == GlobalConst.KEY_SHIFT &&
                (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;

            }
            else if ((e.KeyState & GlobalConst.KEY_CTRL) == GlobalConst.KEY_CTRL &&
                (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // By default, the drop action should be move, if allowed.
                e.Effect = DragDropEffects.Move;

                switch (comboBox_Target.Text)
                {
                    case GlobalConst.TARGETTYPE_FOLDERNAME:
                        string[] folders = (string[])e.Data.GetData(DataFormats.FileDrop);
                        if (folders.Length > 0 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                            e.Effect = DragDropEffects.Copy;
                        break;
                    default:
                        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                        if (files.Length > 0 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                            e.Effect = DragDropEffects.Copy;
                        break;
                }
            }
            else
                e.Effect = DragDropEffects.None;

            // This is an example of how to get the item under the mouse
            //Point pt = listView_FileList.PointToClient(new Point(e.X, e.Y));
            //ListViewItem itemUnder = listView_FileList.GetItemAt(pt.X, pt.Y);
        }

        private void button_ResetList_Click(object sender, EventArgs e)
        {
            listView_FileList.Items.Clear();
            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            button_RemovefromList.Enabled = false;
            button_Undo.Enabled = false;
        }

        private void button_RemovefromList_Click(object sender, EventArgs e)
        {
            if (listView_FileList.SelectedItems.Count != 0)
            {
                string[] files = new string[listView_FileList.SelectedItems.Count];
                foreach (ListViewItem item in listView_FileList.SelectedItems)
                {
                    listView_FileList.Items.Remove(item);
                }
            }
        }
        #endregion


        #region listView_Rules
        private void listView_Rules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 0)
            {
                button_UpdateRule.Enabled = false;
                button_MoveUp.Enabled = false;
                button_MoveDown.Enabled = false;
                button_RemoveRule.Enabled = false;
                return;
            }

            if (listView_Rules.SelectedItems.Count == 1)
            {
                comboBox_Target.Text = listView_Rules.SelectedItems[0].Text;
                comboBox_Mode.Text = listView_Rules.SelectedItems[0].SubItems[1].Text;
                comboBox_Parameter1.Text = string.IsNullOrEmpty(listView_Rules.SelectedItems[0].SubItems[2].Text) ? GlobalConst.EMPTY_STRING : listView_Rules.SelectedItems[0].SubItems[2].Text;
                comboBox_Parameter2.Text = string.IsNullOrEmpty(listView_Rules.SelectedItems[0].SubItems[3].Text) ? GlobalConst.EMPTY_STRING : listView_Rules.SelectedItems[0].SubItems[3].Text;
                button_UpdateRule.Enabled = true;
                button_MoveUp.Enabled = true;
                button_MoveDown.Enabled = true;
                button_RemoveRule.Enabled = true;
                if (comboBox_Mode.Text == GlobalConst.MODETYPE_INSERT)
                {
                    switch (comboBox_Parameter1.Text)
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            comboBox_AuxParameter1.Text = string.IsNullOrEmpty(listView_Rules.SelectedItems[0].SubItems[4].Text) ? GlobalConst.EMPTY_STRING : listView_Rules.SelectedItems[0].SubItems[4].Text; ;
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            string[] auxsplit = listView_Rules.SelectedItems[0].SubItems[4].Text.Split(';');
                            comboBox_AuxParameter1.Text = auxsplit[0];
                            comboBox_AuxParameter2.Text = auxsplit[1];
                            comboBox_AuxParameter3.Text = auxsplit[2];
                            break;
                    }
                }
            }

        }
        private void button_AddRule_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(comboBox_Target.Text) && !string.IsNullOrEmpty(comboBox_Mode.Text) && !string.IsNullOrEmpty(comboBox_Parameter1.Text)) ||
            (comboBox_Target.Text == GlobalConst.TARGETTYPE_FOLDERNAME && comboBox_Mode.Text == GlobalConst.MODETYPE_MAKERAR))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = comboBox_Target.Text;
                lvi.SubItems.Add(comboBox_Mode.Text);
                if (!string.IsNullOrEmpty(comboBox_Parameter1.Text))
                {
                    lvi.SubItems.Add(comboBox_Parameter1.Text);
                }
                else
                {
                    lvi.SubItems.Add(GlobalConst.EMPTY_STRING);
                }
                if (!string.IsNullOrEmpty(comboBox_Parameter2.Text))
                {
                    switch (comboBox_Parameter1.Text)
                    {
                        case GlobalConst.FUNCTYPE_LEFT:
                            try
                            {
                                Convert.ToInt32(comboBox_Parameter2.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Numbers only !");
                                comboBox_Parameter2.Text = GlobalConst.ZERO;
                            }
                            break;
                        case GlobalConst.FUNCTYPE_RIGHT:
                            try
                            {
                                Convert.ToInt32(comboBox_Parameter2.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Numbers only !");
                                comboBox_Parameter2.Text = GlobalConst.ZERO;
                            }
                            break;
                        default:
                            break;
                    }
                    lvi.SubItems.Add(comboBox_Parameter2.Text);
                }
                else
                {
                    lvi.SubItems.Add(GlobalConst.EMPTY_STRING);
                }
                if (comboBox_Mode.Text == GlobalConst.MODETYPE_INSERT)
                {
                    string aux = GlobalConst.EMPTY_STRING;
                    switch (comboBox_Parameter1.Text)
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            aux = comboBox_AuxParameter1.Text;
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            if (!string.IsNullOrEmpty(comboBox_AuxParameter1.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter2.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter3.Text))
                                aux = string.Join(GlobalConst.AUX_CONNECTOR, new string[] { comboBox_AuxParameter1.Text, comboBox_AuxParameter2.Text, comboBox_AuxParameter3.Text });
                            break;
                    }
                    if (!string.IsNullOrEmpty(aux)) lvi.SubItems.Add(aux);
                    comboBox_AuxParameter1.Text = GlobalConst.EMPTY_STRING;
                    comboBox_AuxParameter2.Text = GlobalConst.EMPTY_STRING;
                    comboBox_AuxParameter3.Text = GlobalConst.EMPTY_STRING;
                }
                listView_Rules.Items.Add(lvi);
                comboBox_Parameter1.Text = (comboBox_Mode.Text == GlobalConst.MODETYPE_INSERT) ? comboBox_Parameter1.Text : GlobalConst.EMPTY_STRING;
                comboBox_Parameter2.Text = GlobalConst.EMPTY_STRING;
                listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
            }
        }
        private void button_UpdateRule_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 1 &&
                !string.IsNullOrEmpty(comboBox_Target.Text) && !string.IsNullOrEmpty(comboBox_Mode.Text) &&
                !string.IsNullOrEmpty(comboBox_Parameter1.Text))
            {
                listView_Rules.SelectedItems[0].Text = comboBox_Target.Text;
                listView_Rules.SelectedItems[0].SubItems[1].Text = comboBox_Mode.Text;
                listView_Rules.SelectedItems[0].SubItems[2].Text = comboBox_Parameter1.Text;
                listView_Rules.SelectedItems[0].SubItems[3].Text = comboBox_Parameter2.Text;
                button_UpdateRule.Enabled = false;
                if (comboBox_Mode.Text == GlobalConst.MODETYPE_INSERT)
                {
                    switch (comboBox_Parameter1.Text)
                    {
                        case GlobalConst.FUNCTYPE_PHASE:
                            listView_Rules.SelectedItems[0].SubItems[4].Text = comboBox_AuxParameter1.Text;
                            break;
                        case GlobalConst.FUNCTYPE_DIGIT:
                            if (!string.IsNullOrEmpty(comboBox_AuxParameter1.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter2.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter3.Text))
                                listView_Rules.SelectedItems[0].SubItems[4].Text = string.Join(GlobalConst.AUX_CONNECTOR, new string[] { comboBox_AuxParameter1.Text, comboBox_AuxParameter2.Text, comboBox_AuxParameter3.Text });
                            break;
                    }
                }
            }
        }

        private void button_MoveUp_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 1)
            {
                int seletedIndex = listView_Rules.SelectedIndices[0];
                MoveItems(listView_Rules, MoveDirection.Up);
                if (seletedIndex > 0)
                {
                    listView_Rules.Items[seletedIndex - 1].Selected = true;
                }
                else
                {
                    listView_Rules.Items[seletedIndex].Selected = true;
                }
                listView_Rules.Select();
            }
        }

        private void button_MoveDown_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 1)
            {
                int seletedIndex = listView_Rules.SelectedIndices[0];
                MoveItems(listView_Rules, MoveDirection.Down);
                if (seletedIndex > 0 && seletedIndex < listView_Rules.Items.Count - 1)
                {
                    listView_Rules.Items[seletedIndex + 1].Selected = true;
                }
                else
                {
                    listView_Rules.Items[seletedIndex].Selected = true;
                }
                listView_Rules.Select();
            }
        }
        private void button_ResetRules_Click(object sender, EventArgs e)
        {
            listView_Rules.Items.Clear();
            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            MakeRAR = false;
        }

        private void button_RemoveRule_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count != 0)
            {

                string[] files = new string[listView_Rules.SelectedItems.Count];
                foreach (ListViewItem item in listView_Rules.SelectedItems)
                {
                    if (item.SubItems[1].Text.ToLowerInvariant() == GlobalConst.MODETYPE_MAKERAR) MakeRAR = false;
                    listView_Rules.Items.Remove(item);
                }
                if (listView_Rules.Items.Count == 0)
                {
                    listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }
        private void button_SaveProfile_Click(object sender, EventArgs e)
        {
            if (listView_Rules.Items.Count > 0)
            {
                List<ListViewItem> Itemlist = new List<ListViewItem>();
                foreach (ListViewItem vi in listView_Rules.Items)
                {
                    Itemlist.Add(vi);
                }
                if (Itemlist.Any())
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = GlobalConst.FILETYPE_XML_FILTER;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        xmlH.SaveProfileXML(Itemlist, saveFileDialog.FileName);
                    }
                }
            }
        }
        private void button_LoadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = GlobalConst.FILETYPE_XML_FILTER;
            ofd.FilterIndex = 0;
            ofd.DefaultExt = GlobalConst.FILETYPE_XML;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!String.Equals(Path.GetExtension(ofd.FileName),
                                   "." + GlobalConst.FILETYPE_XML,
                                   StringComparison.OrdinalIgnoreCase))
                {
                    // Invalid file type selected; display an error.
                    MessageBox.Show("The type of the selected file is not supported by this application. You must select an " + GlobalConst.FILETYPE_XML + " file.",
                                    "Invalid File Type",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    // Optionally, force the user to select another file.
                    // ...
                }
                else
                {
                    List<Rules> rulist = xmlH.LoadProfileXML(ofd.FileName);
                    if (rulist.Any())
                    {
                        listView_Rules.Items.Clear();
                        foreach (Rules ru in rulist)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = ru.Target;
                            lvi.SubItems.Add(ru.Mode);
                            lvi.SubItems.Add(ru.Parameter1);
                            lvi.SubItems.Add(ru.Parameter2);
                            lvi.SubItems.Add(ru.AuxParameter);
                            listView_Rules.Items.Add(lvi);
                            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
                        }
                    }
                }
            }
        }
        private void listView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //The projects ListView has been changed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    //MessageBox.Show("An Item Has Been Added To The ListView!");
                    if (listView_FileList.Items.Count > 0)
                    {
                        button_Preview.Enabled = true;
                        button_Rename.Enabled = true;
                    }
                    if (listView_Rules.Items.Count > 0)
                    {
                        button_SaveProfile.Enabled = true;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    //MessageBox.Show("The ListView Has Been Cleared!");
                    if (listView_FileList.Items.Count == 0)
                    {
                        button_Preview.Enabled = false;
                        button_Rename.Enabled = false;
                    }
                    if (listView_Rules.Items.Count == 0)
                    {
                        button_SaveProfile.Enabled = false;
                    }
                    break;
            }
        }
        private void MoveItems(ListView sender, MoveDirection direction)
        {
            bool valid = sender.SelectedItems.Count > 0 &&
                        ((direction == MoveDirection.Down && (sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1))
                        || (direction == MoveDirection.Up && (sender.SelectedItems[0].Index > 0)));

            if (valid)
            {
                bool start = true;
                int first_idx = 0;
                List<ListViewItem> items = new List<ListViewItem>();

                // ambil data
                foreach (ListViewItem i in sender.SelectedItems)
                {
                    if (start)
                    {
                        first_idx = i.Index;
                        start = false;
                    }
                    items.Add(i);
                }

                sender.BeginUpdate();

                // hapus
                foreach (ListViewItem i in sender.SelectedItems) i.Remove();

                // insert
                if (direction == MoveDirection.Up)
                {
                    int insert_to = first_idx - 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                else
                {
                    int insert_to = first_idx + 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                sender.EndUpdate();
            }
        }
        #endregion

    }
}
